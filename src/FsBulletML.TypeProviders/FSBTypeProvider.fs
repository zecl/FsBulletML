namespace FsBulletML.TypeProvider

open System.IO
open System.Reflection
open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes
open FsBulletML

[<TypeProvider>]
type FSBTypeProvider(config: TypeProviderConfig) as this =
  inherit TypeProviderForNamespaces()

  let asm = Assembly.GetExecutingAssembly()
  let ns = "FsBulletML.TypeProviders"

  let typ = ProvidedTypeDefinition(asm, ns, "SXML", Some (typeof<obj>), HideObjectMethods = true)
  do typ.DefineStaticParameters(
        [ProvidedStaticParameter("source", typeof<string>)],
        fun typeName parameters ->
          let source = 
            let str = string parameters.[0]
            if not <| str.EndsWith(".fsb") then
              str
            else
              let path = Path.Combine(config.ResolutionFolder, str)
              try
                File.ReadAllText(path)
              with
              | _ -> 
                  failwithf "Error fsb path %A" path

          let typ = ProvidedTypeDefinition(asm, ns, typeName, Some typeof<obj>, HideObjectMethods = true)
          let ctor = ProvidedConstructor(parameters = [ ], 
                                          InvokeCode= (fun args -> <@@ source :> obj @@>))
          typ.AddMember ctor
          typ.AddMemberDelayed(fun () -> 
            let instanceProp = 
              ProvidedProperty(propertyName = "Value", 
                                propertyType = typeof<FsBulletML.DTD.Bulletml>, 
                                GetterCode= (fun _ -> <@@ source |> FsBulletML.DTD.Bulletml.ReadFsbString @@>))
            instanceProp.AddXmlDocDelayed (fun () -> System.String.Format(@"BulletMLを取得します。"))
            instanceProp)

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