namespace FsBulletML
open System

[<AutoOpen>]
module Exception =

  type BulletmlDTDViolationException (message:string, ?innerException:exn) =
    inherit Exception (message, 
        match innerException with | Some(ex) -> ex | _ -> null)       
