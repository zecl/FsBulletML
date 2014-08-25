#nowarn "13730"
namespace FsBulletML.TypeProviders
open System
open System.IO
open System.Xml
open System.Xml.Resolvers
open System.Reflection
open System.ComponentModel
open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes
open FsBulletML
open Impl
open System.Text.RegularExpressions

[<TypeProvider>]
[<CompilerMessage("hidden...", 13730, IsError = false, IsHidden = true)>]
type BulletMLTypeProvider (config: TypeProviderConfig) as this =
  inherit TypeProviderForNamespaces()
  let ctx = new Context(this.Invalidate)
  let typ = createProvidedTypeDefinition ns

  do
    let docText = 
      """<summary>Typed representation of BulletML.</summary>
         <param name='bulletmls'>Location of BulletML files or string BulletML documents. delimiter is `;` or `,`.</param>
         <param name='style'>BulletML Style (Xml or Sxml or Fsb).</param>
         <param name='watch'>Specify whether or not to monitor the file</param>"""

    typ.AddXmlDoc docText
    let parameters = 
      [ProvidedStaticParameter("bulletmls", typeof<string>);
       ProvidedStaticParameter("style", typeof<Style>, Style.Xml)
       ProvidedStaticParameter("watch", typeof<bool>, false) ]

    typ.DefineStaticParameters(
        parameters,
        fun typeName parameters ->
          let bulletmls = string parameters.[0]
          let style = parameters.[1] :?> Style
          let watch = parameters.[2] :?> bool
          let bullets = paramSprit bulletmls
          let typ = ProvidedTypeDefinition(asm, ns, typeName, Some typeof<obj>, HideObjectMethods = true, IsErased = true)
          let ctor = ProvidedConstructor(parameters = [ ], InvokeCode= (fun _ -> <@@ bullets @@>))
          typ.AddMember ctor
          addProperties typ bullets style config watch ctx
          typ)

    this.Disposing.Add(fun _ -> (ctx :> IDisposable).Dispose())
    registerDependencies config this.RegisterProbingFolder
    this.AddNamespace(ns, [typ])