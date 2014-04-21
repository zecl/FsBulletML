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
[<assembly: AssemblyDescription("FsBulletML for MonoGame.")>]
[<assembly: AssemblyCompany("")>]
[<assembly: AssemblyProduct("FsBulletML.MonoGame")>]
[<assembly: AssemblyCopyright("Copyright (C) 2013-2014 zecl")>]
[<assembly: AssemblyTrademark("")>]
[<assembly: AssemblyCulture("")>]
[<assembly: NeutralResourcesLanguage("ja-JP")>]

[<assembly: ComVisible(false)>]
[<assembly: Guid("565CAB1D-7E73-4F0E-944A-09CD895FC1D5")>]

#if DEBUG
[<assembly: InternalsVisibleTo("FsBulletML.MonoGame.Tests")>]
[<assembly: InternalsVisibleTo("CreateBullets")>]
#endif

#if Debug
[<assembly: AssemblyConfiguration("Debug")>]
#else
[<assembly: AssemblyConfiguration("Release")>]
#endif

do()