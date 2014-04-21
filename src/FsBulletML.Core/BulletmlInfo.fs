namespace FsBulletML

[<StructAttribute>]
type BulletmlInfo =
  val Name: string
  val Bulletml: Bulletml
  val BulletmlTask : BulletmlTask
  val BulletmlTaskOption : BulletmlTask option
  new(name: string, bulletml: Bulletml) = 
    let task = BulletRunner.convertBulletmlTask bulletml
    { Name = name; Bulletml = bulletml; BulletmlTask = task; BulletmlTaskOption = Some task }

[<AutoOpen>]
module BulletmlInfoModule =
  let createBulletmlInfo name bulletml = new BulletmlInfo(name, bulletml)