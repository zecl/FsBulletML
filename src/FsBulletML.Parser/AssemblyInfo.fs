module AssemblyInfo
open System.Resources
open System.Reflection
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

//[<assembly: AssemblyVersion("0.8.*")>]
[<assembly: AssemblyVersion("0.8.4")>]
[<assembly: AssemblyFileVersion("0.8.4")>]
[<assembly: AssemblyInformationalVersion("0.8.4")>]

[<assembly: AssemblyTitle("FsBulletML")>]
[<assembly: AssemblyDescription("F# Implementation of BulletML for external DSL(SXML, FSB).")>]
[<assembly: AssemblyCompany("")>]
[<assembly: AssemblyProduct("FsBulletML.Parser")>]
[<assembly: AssemblyCopyright("Copyright (C) 2013-2014 zecl All Rights Reserved.")>]
[<assembly: AssemblyTrademark("")>]
[<assembly: AssemblyCulture("")>]
[<assembly: NeutralResourcesLanguage("ja-JP")>]

[<assembly: ComVisible(false)>]
[<assembly: Guid("88DCD0C9-CEEE-428B-9AB9-8FD72B774275")>]

#if DEBUG
[<assembly: InternalsVisibleTo("FsBulletML.Parser.Tests")>]
[<assembly: InternalsVisibleTo("CreateBullets")>]
#endif

#if DEBUG
[<assembly: AssemblyConfiguration("Debug")>]
#else
[<assembly: AssemblyConfiguration("Release")>]
#endif

do()