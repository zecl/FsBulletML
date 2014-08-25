#nowarn "13730"
namespace FsBulletML.TypeProviders

open System
open System.ComponentModel
open System.IO
open System.Xml
open System.Xml.Resolvers
open System.Reflection
open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes
open FsBulletML
open Impl

[<TypeProvider>]
[<CompilerMessage("hidden...", 13730, IsError = false, IsHidden = true)>]
type BulletMLFromXmlTypeProvider (config: TypeProviderConfig) as this =
  inherit TypeProviderForNamespaces()
  let ctx = new Context(this.Invalidate)

  let ns = ns + ".Xml"
  let typ = createProvidedTypeDefinition ns
  do
    let docText = 
      """<summary>Typed representation of BulletML, Xml style.</summary>
         <param name='bulletmls'>Location of BulletML files or string BulletML documents. delimiter is `;` or `,`.</param>
         <param name='watch'>Specify whether or not to monitor the file</param>"""

    typ.AddXmlDoc docText
    typ.DefineStaticParameters(
        [ProvidedStaticParameter("bulletmls", typeof<string>)
         ProvidedStaticParameter("watch", typeof<bool>, false)],
        fun typeName parameters ->
          let bulletmls = string parameters.[0]
          let style = Style.Xml 
          let watch = parameters.[1] :?> bool
          let bullets = paramSprit bulletmls
          let typ = ProvidedTypeDefinition(asm, ns, typeName, Some typeof<obj>, HideObjectMethods = true)
          let ctor = ProvidedConstructor(parameters = [ ], InvokeCode= (fun _ -> <@@ bullets @@>))
          typ.AddMember ctor
          addProperties typ bullets style config watch ctx
          typ)
  do
    this.Disposing.Add(fun _ -> (ctx :> IDisposable).Dispose()) 
    registerDependencies config this.RegisterProbingFolder
    this.AddNamespace(ns, [typ])
    