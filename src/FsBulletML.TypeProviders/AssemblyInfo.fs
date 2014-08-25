module AssemblyInfo
open System.Resources
open System.Reflection
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Microsoft.FSharp.Core.CompilerServices

//[<assembly: AssemblyVersion("0.9.*")>]
[<assembly: AssemblyVersion("0.9.1")>]
[<assembly: AssemblyFileVersion("0.9.1")>]
[<assembly: AssemblyInformationalVersion("0.9.1")>]

[<assembly: AssemblyTitle("FsBulletML")>]
[<assembly: AssemblyDescription("F# Implementation of BulletML. BulletML TypeProviders(Xml, Sxml, Fsb)")>]
[<assembly: AssemblyCompany("")>]
[<assembly: AssemblyProduct("FsBulletML.TypeProviders")>]
[<assembly: AssemblyCopyright("Copyright (C) 2013-2014 zecl All Rights Reserved.")>]
[<assembly: AssemblyTrademark("")>]
[<assembly: AssemblyCulture("")>]
[<assembly: NeutralResourcesLanguage("ja-JP")>]

[<assembly: ComVisible(false)>]
[<assembly: Guid("9A0CF746-36E8-4F2A-A3CD-E2B27C1E2E9D")>]


#if DEBUG
[<assembly: AssemblyConfiguration("Debug")>]
#else
[<assembly: AssemblyConfiguration("Release")>]
#endif

[<assembly:TypeProviderAssembly>] 
do()