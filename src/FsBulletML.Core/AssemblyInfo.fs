module AssemblyInfo

open System.Resources
open System.Reflection
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

//[<assembly: AssemblyVersion("0.8.*")>]
[<assembly: AssemblyVersion("0.8.7")>]
[<assembly: AssemblyFileVersion("0.8.7")>]
[<assembly: AssemblyInformationalVersion("0.8.7")>]

[<assembly: AssemblyTitle("FsBulletML")>]
[<assembly: AssemblyDescription("F# implementation of BulletML for internal DSL, and Xml.")>]
[<assembly: AssemblyCompany("")>]
[<assembly: AssemblyProduct("FsBulletML.Core")>]
[<assembly: AssemblyCopyright("Copyright (C) 2013-2014 zecl")>]
[<assembly: AssemblyTrademark("")>]
[<assembly: AssemblyCulture("")>]
[<assembly: NeutralResourcesLanguage("ja-JP")>]

[<assembly: ComVisible(false)>]
[<assembly: Guid("2EBAF051-E207-4186-A395-FA81F4A27600")>]

[<assembly: InternalsVisibleTo("FsBulletML.Parser")>]
#if DEBUG
[<assembly: InternalsVisibleTo("FsBulletML.Parser.Tests")>]
[<assembly: InternalsVisibleTo("CreateBullets")>]
#endif

#if Debug
[<assembly: AssemblyConfiguration("Debug")>]
#else
[<assembly: AssemblyConfiguration("Release")>]
#endif

do()