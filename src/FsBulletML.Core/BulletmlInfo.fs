namespace FsBulletML

[<StructAttribute>]
type BulletmlInfo =
  val Name: string
  val Bulletml: Bulletml
  new(name: string, bulletml: Bulletml) = 
    { Name = name; Bulletml = bulletml }

  member this.BulletmlTask () =
    BulletRunner.convertBulletmlTask this.Bulletml

  member this.BulletmlTaskOption () =
    BulletRunner.convertBulletmlTask this.Bulletml |> Some

[<AutoOpen>]
module BulletmlInfoModule =
  let createBulletmlInfo name bulletml = new BulletmlInfo(name, bulletml)