module AssemblyInfo

open System.Resources
open System.Reflection
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

//[<assembly: AssemblyVersion("0.1.*")>]
[<assembly: AssemblyVersion("0.1.0")>]
[<assembly: AssemblyFileVersion("0.1.0")>]
[<assembly: AssemblyInformationalVersion("0.1.0")>]

[<assembly: AssemblyTitle("FsBulletML")>]
[<assembly: AssemblyDescription("Definition of FsBulletML internal DSL.")>]
[<assembly: AssemblyCompany("")>]
[<assembly: AssemblyProduct("FsBulletML.Bullets")>]
[<assembly: AssemblyCopyright("Copyright (C) 2013-2014 zecl")>]
[<assembly: AssemblyTrademark("")>]
[<assembly: AssemblyCulture("")>]
[<assembly: NeutralResourcesLanguage("ja-JP")>]

[<assembly: ComVisible(false)>]
[<assembly: Guid("ACDC8B67-F702-4DBC-BE32-1ADB56A594F0")>]

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