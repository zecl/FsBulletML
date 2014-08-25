namespace FsBulletML.TypeProvider

open System.IO
open System.Xml
open System.Xml.Resolvers
open System.Reflection
open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes
open FsBulletML

[<TypeProvider>]
type BulletmlTypeProvider(config: TypeProviderConfig) as this =
  inherit TypeProviderForNamespaces()

  let asm = Assembly.GetExecutingAssembly()
  let ns = "FsBulletML.TypeProviders"

  let typ = ProvidedTypeDefinition(asm, ns, "XML", Some (typeof<obj>), HideObjectMethods = true)
  do typ.DefineStaticParameters(
        [ProvidedStaticParameter("source", typeof<string>)],
        fun typeName parameters ->
          let xml = 
            let str = string parameters.[0]
            if not <| str.EndsWith(".xml") then
              str
            else
              let path = Path.Combine(config.ResolutionFolder, str)
              try
                File.ReadAllText(path)
              with
              | _ -> 
                  failwithf "Error xml path %A" path

          let typ = ProvidedTypeDefinition(asm, ns, typeName, Some typeof<obj>, HideObjectMethods = true)
          let ctor = ProvidedConstructor(parameters = [ ], 
                                          InvokeCode= (fun args -> <@@ xml :> obj @@>))
          typ.AddMember ctor

          let bulletml = (xml, None) |> FsBulletML.Xml.Bulletml.readXmlString
          let bulletml2 = Bulletml({ bulletmlXmlns = Some "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml"; bulletmlType = Some ShootingDirection.BulletVertical }, [])
          let instanceProp = 
            ProvidedProperty(propertyName = "Value", 
                             propertyType = typeof<FsBulletML.DTD.Bulletml>, 
                             GetterCode= (fun _ -> <@@ bulletml  @@>))
          instanceProp.AddXmlDoc(System.String.Format(@"BulletMLを取得します。"))

          typ.AddMember(instanceProp)

          typ.HideObjectMethods <- true
          typ
  )
  do 
    let myAssembly = Assembly.GetAssembly(this.GetType())
    let path = System.IO.Path.GetDirectoryName(myAssembly.Location)
    this.RegisterProbingFolder(path)
    this.AddNamespace(ns, [typ])

  override this.ResolveAssembly(args) = 
    let name = System.Reflection.AssemblyName(args.Name)
    let existingAssembly = 
      let hoge = System.AppDomain.CurrentDomain.GetAssemblies()
      hoge |> Seq.tryFind(fun a -> name = a.GetName())
    match existingAssembly with
    | Some a -> a
    | None -> base.ResolveAssembly(args)
  
[<assembly:TypeProviderAssembly>] 
do()