namespace FsBulletML.TypeProviders

type Style =
  | Xml  = 0
  | Sxml = 1
  | Fsb  = 2

open System
open System.IO
open System.Xml
open System.Xml.Resolvers
open System.Xml.Linq
open System.Reflection
open System.ComponentModel
open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes
open FsBulletML

[<CompilerMessage("hidden...", 13730, IsError = false, IsHidden = true)>]
module Impl =
  let asm = Assembly.GetExecutingAssembly()
  let ns = typeof<Style>.Namespace
  let createProvidedTypeDefinition ns = 
    ProvidedTypeDefinition(asm, ns, "BulletML", Some (typeof<obj>), HideObjectMethods = true, IsErased = true)

  let paramSprit strArg = 
    let separators = [|";";","|]
    (strArg:string).Split(separators, StringSplitOptions.None)
    |> Array.map (fun x -> x.Trim())
    |> Array.toList  

  let getExtention style = style |> function
    | Style.Xml  -> "xml"
    | Style.Sxml -> "sxml"
    | Style.Fsb  -> "fsb"
    | _ -> failwith "err"
  
  let getBulletmlInfo bullets style config watch ctx =
    bullets |> Seq.mapi (fun i str -> 
    let extention = getExtention style
    if not <| (str:string).EndsWith("." + extention) then
      str, String.Format("Bullet{0}",i)
    else
      let resolvedFileName = Helper.findConfigFile (config:TypeProviderConfig).ResolutionFolder str
      try
        let str = File.ReadAllText(resolvedFileName)
        if watch then
          Helper.watchFile resolvedFileName ctx
        str, Path.GetFileNameWithoutExtension(resolvedFileName)
      with | _ -> failwithf "Error %s path %A" extention resolvedFileName)

  let read style bulletml = 
    match style with
    | Style.Xml  -> bulletml |> Bulletml.readXmlString
    | Style.Sxml -> bulletml |> Bulletml.readSxmlString
    | Style.Fsb  -> bulletml |> Bulletml.readFsbString
    | _ -> failwith "err"

  let addProperties typ bullets style config watch ctx =
    let bulletmlInfos = getBulletmlInfo bullets style config watch ctx
    bulletmlInfos |> Seq.iter (fun (bulletml,_) -> read style bulletml |> ignore)
    bulletmlInfos |> Seq.iter (fun (bulletml,propName) -> 
      (typ:ProvidedTypeDefinition).AddMemberDelayed(fun () -> 
        let instanceProp = 
          ProvidedProperty(
            propertyName = propName, 
            propertyType = typeof<Bulletml>, 
            GetterCode= (fun _ -> <@@ read style bulletml @@>))

        instanceProp.AddXmlDocDelayed(fun () ->
          let result = read style bulletml
          let docText = 
            let defaultDoc = @"BulletML of internal DSL."
            match result.Name with
            | Some bulletName -> sprintf "<summary><para>%s</para><para>BulletML's name is \"%s\".</para></summary>" defaultDoc bulletName
            | None -> sprintf "<summary><para>%s</para></summary>" defaultDoc
          docText)
        instanceProp))

  let registerDependencies config registerProbingFolder =
    let thisAssembly = Assembly.GetAssembly(typeof<Style>)
    let path = Path.GetDirectoryName(thisAssembly.Location)
    registerProbingFolder path
      
    let packagePath p = Helper.getUpDirectory 3 path + p
    let currentPath p = path + p
#if NET40
    let tf = "net40"
#endif
#if NET45
    let tf = "net45"
#endif
    let packageConfig = 
      Helper.findConfigFile (config:TypeProviderConfig).ResolutionFolder "packages.config"
    let packageInfo = 
      if File.Exists(packageConfig) then
        use xmlReader = XmlReader.Create(packageConfig)
        let doc = XDocument.Load(xmlReader)
        let (!) x = XName.op_Implicit x
        query {
          for packages in doc.Elements(!"packages") do
          for package in packages.Elements(!"package") do
          select (package.Attribute(!"id").Value, package.Attribute(!"version").Value,package.Attribute(!"targetFramework").Value) } 
      else Seq.empty 

    let getInfo name defaultVersion =  
        match packageInfo |> Seq.tryFind(fun (x,_,_) -> x = name) with
        | Some (_,v,tf) -> v, tf
        | None -> defaultVersion, tf

    let dependencies =
      let core =
        let name = "FsBulletML.Core"
        let version, targetFramework = getInfo name "0.9.0"
        [sprintf @"\%s.%s\lib\%s" name version targetFramework]
      let fparsec = 
        let name = "FParsec"
        let version, _ = getInfo name "1.0.1"
        [sprintf @"\%s.%s\lib\net40-client" name version]
      let parser = 
        let name = "FsBulletML.Parser"
        let version, targetFramework = getInfo name "0.8.6"
        [sprintf @"\%s.%s\lib\%s" name version targetFramework]
      core @ fparsec @ parser

    let packages = 
      dependencies 
      |> Seq.map packagePath
      |> Seq.append (dependencies |> Seq.map currentPath)
      |> Seq.filter (fun x -> Directory.Exists x)
    packages |> Seq.iter registerProbingFolder