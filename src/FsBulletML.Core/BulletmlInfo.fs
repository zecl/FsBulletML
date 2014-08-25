namespace FsBulletML

[<StructAttribute>]
type BulletmlInfo =
  val Name: string
  val Bulletml: Bulletml
  new(bulletml: Bulletml) = 
    { Name = match bulletml.Name with | Some x -> x | None -> ""
      Bulletml = bulletml }

  member this.BulletmlTask () =
    BulletRunner.convertBulletmlTask this.Bulletml

  member this.BulletmlTaskOption () =
    BulletRunner.convertBulletmlTask this.Bulletml |> Some

[<AutoOpen>]
module BulletmlInfoModule =
  let createBulletmlInfo bulletml = new BulletmlInfo(bulletml)