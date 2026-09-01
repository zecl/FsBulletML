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

  // Was: probe for FsBulletML.Core/FParsec/FsBulletML.Parser under a NuGet packages.config-style
  // "packages\<id>.<version>\lib\<tf>" layout, keyed off "net40"/"net45" via #if NET40/NET45 (an
  // ifdef that nothing defines any more - a latent, always-broken build target under this
  // project's later configurations). There is no packages.config in this repo; dependencies are
  // resolved through ProjectReference/PackageReference instead, and the build already copies
  // FsBulletML.Core.dll, FsBulletML.Parser.dll and FParsec.dll next to FsBulletML.TypeProviders.dll,
  // which is where ordinary assembly resolution looks first. Registering this assembly's own
  // directory as a probing folder (as this function's first line did) is therefore also redundant.