namespace FsBulletML.Sample.MonoGame.FSharp

open System
open System.Xml 
open System.Collections.Generic
open System.Runtime.Serialization
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework.Graphics
open FsBulletML
open FsBulletML.MonoGame
 
type Player () as this =
  [<DefaultValue>]val mutable timer : int
  [<DefaultValue>]val mutable pos : Vector2
  [<DefaultValue>]val mutable speed : float32
  [<DefaultValue>]val mutable radius : float32
  [<DefaultValue>]val mutable damageCounter : int32
  [<DefaultValue>]val mutable texture : Texture2D

  let shoot2WayLeftBullet (player:Player) =
#if NET35 
    let ``2wayLeftBullet`` = Bulletml.readXml (@"..\..\..\Content\xml\PlayerBullet\2wayLeft.xml") |> BulletRunner.convertBulletmlTask |> Some
#endif
#if NET40 
    let ``2wayLeftBullet`` = Bulletml.readXml (@"..\..\..\Content\xml\PlayerBullet\2wayLeft.xml", None)  |> BulletRunner.convertBulletmlTask |> Some
#endif
    if player.timer > 0 then
      let bullet = new PlayerBullet()
      Manager.addPlayerBulletPos(bullet, new Vector2(this.pos.X - 10.f, this.pos.Y + 1.f))
      bullet.SetTask(``2wayLeftBullet``) 

  let shoot2WayRightBullet (player:Player) =
#if NET35 
    let ``2wayRightBullet`` = Bulletml.readXml (@"..\..\..\Content\xml\PlayerBullet\2wayRight.xml") |> BulletRunner.convertBulletmlTask |> Some
#endif
#if NET40 
    let ``2wayRightBullet`` = Bulletml.readXml (@"..\..\..\Content\xml\PlayerBullet\2wayRight.xml", None)  |> BulletRunner.convertBulletmlTask |> Some
#endif
    if player.timer > 0 then
      let bullet = new PlayerBullet()
      Manager.addPlayerBulletPos(bullet, new Vector2(this.pos.X + 10.f, this.pos.Y + 1.f ))
      bullet.SetTask(``2wayRightBullet``) 

  let shootHomingBullet (player:Player) = 
#if NET35 
    let homingBullet = Bulletml.readXml (@"..\..\..\Content\xml\PlayerBullet\homing.xml") |> BulletRunner.convertBulletmlTask |> Some
#endif
#if NET40
    let homingBullet = Bulletml.readXml (@"..\..\..\Content\xml\PlayerBullet\homing.xml", None) |> BulletRunner.convertBulletmlTask |> Some
#endif
    if player.timer > 60 then
      let bullet = new PlayerBullet()
      Manager.addPlayerBulletPos(bullet, this.pos)
      bullet.SetTask(homingBullet) 

  do
    this.pos <- new Vector2()
    this.speed <- 3.f
    this.radius <- 3.5f

  member this.Pos with get () = this.pos
                   and set (v) = this.pos <- v 

  member this.Speed with get () = this.speed
                     and set (v) = this.speed <- v 

  member this.Radius with get () = this.radius 
                      and set (v) = this.radius <- v
  member this.Init () =
    this.pos.X <- Settings.Player.X
    this.pos.Y <- Settings.Player.Y

  member this.Update () = 

    if (Keyboard.GetState().IsKeyDown(Keys.Left)) then
      if this.pos.X - this.speed  >= 0.f then
        this.pos.X <- this.pos.X - this.speed
    if (Keyboard.GetState().IsKeyDown(Keys.Right)) then
      if this.pos.X - this.speed <= Settings.Display.Width then
        this.pos.X <- this.pos.X +  this.speed
    if (Keyboard.GetState().IsKeyDown(Keys.Up)) then
      if this.pos.Y - this.speed >= 0.f then
        this.pos.Y <- this.pos.Y - this.speed
    if (Keyboard.GetState().IsKeyDown(Keys.Down)) then
      if this.pos.Y - this.speed <= Settings.Display.Height then
        this.pos.Y <- this.pos.Y + this.speed

    this.timer <- this.timer + 1
    if Keyboard.GetState().IsKeyDown(Keys.Z) then
      [shoot2WayLeftBullet; shoot2WayRightBullet; shootHomingBullet] |> List.iter (fun f -> f this)

    if (this.timer > 60) then
       this.timer <- 0
