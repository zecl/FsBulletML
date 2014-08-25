namespace FsBulletML.TypeProviders
open System
open System.IO
open System.Runtime.CompilerServices 
open ProviderImplementation.ProvidedTypes
open System.ComponentModel

[<CompilerMessage("hidden...", 13730, IsError = false, IsHidden = true)>]
type Context (onChanged : unit -> unit) = 
  let disposingEvent = Event<_>()
  let lastChanged = ref (DateTime.Now.AddSeconds -1.0)
  let sync = obj()

  let trigger() =
    let shouldTrigger = lock sync (fun _ ->
      match !lastChanged with
      | time when DateTime.Now - time <= TimeSpan.FromSeconds 1. -> false
      | _ -> 
        lastChanged := DateTime.Now
        true
      )
    if shouldTrigger then onChanged()

  member __.Disposing: IEvent<unit> = disposingEvent.Publish
  member __.Trigger = trigger
  interface IDisposable with
    member __.Dispose() = disposingEvent.Trigger()

#nowarn "13730"
module internal Helper =
  let findConfigFile resolutionFolder configFileName =
    if Path.IsPathRooted configFileName then 
      configFileName 
    else 
      Path.Combine(resolutionFolder, configFileName)

  let watchFile (fileName:string) (ctx : Context) =    
    if fileName.StartsWith("http", System.StringComparison.InvariantCultureIgnoreCase) then ()  else

    let path = Path.GetDirectoryName(fileName)
    let name = Path.GetFileName(fileName)
    let watcher = new FileSystemWatcher(Filter = name, Path = path)
    let onChanged = (fun (fsargs : FileSystemEventArgs) -> 
      match fsargs.ChangeType with
      | WatcherChangeTypes.Changed | WatcherChangeTypes.Created | WatcherChangeTypes.Deleted -> ctx.Trigger()
      | _ -> ())
    try
      watcher.Deleted.Add onChanged
      watcher.Changed.Add onChanged
      watcher.Error.Add (fun _ -> watcher.Dispose())
      watcher.EnableRaisingEvents <- true
      ctx.Disposing.Add watcher.Dispose
    with | exn -> watcher.Dispose()

  let getDirectoryName path = 
    let directoryName = Path.GetDirectoryName path
    if directoryName = null then "" else directoryName

  let replaceDirSepFromAltSep path =
    if path = null then path else
    (path:string).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)

  let removeDirSep (path:string) =
    let r = path |> replaceDirSepFromAltSep
    if r.Substring(r.Length - 1,1) = string Path.DirectorySeparatorChar then 
      r.Substring(0, r.Length - 1) 
    else r

  let getUpDirectory count (filePath:string) = 
    let rec getUpDirectory' (filePath:string) n = 
      match n - 1 < 0 with
      | true -> filePath
      | _ -> 
        let filePath = filePath |> replaceDirSepFromAltSep
        let index = filePath.LastIndexOf(string Path.DirectorySeparatorChar)
        if index < 0 then filePath else
        let dinfo = (Directory.GetParent filePath) 
        let result = if dinfo = null then "" else dinfo.FullName
        if n - 1 = 0 then
          result
        else
          getUpDirectory' result (n-1)
    getUpDirectory' filePath count |> replaceDirSepFromAltSep |> removeDirSep