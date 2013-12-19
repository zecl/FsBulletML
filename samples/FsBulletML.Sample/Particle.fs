namespace FsBulletML.Sample

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type Particle (texture, pos, speed, angle, scale, shrinkRate, duration, color) as this =
  [<DefaultValue>]val mutable private texture : Texture2D 
  [<DefaultValue>]val mutable private pos : Vector2 
  [<DefaultValue>]val mutable private direction : Vector2 
  [<DefaultValue>]val mutable private originPos : Vector2 
  [<DefaultValue>]val mutable private duration : float32
  [<DefaultValue>]val mutable private scale : float32
  [<DefaultValue>]val mutable private shrinkRate : float32
  [<DefaultValue>]val mutable private speed : float32
  [<DefaultValue>]val mutable alive : bool
  [<DefaultValue>]val mutable private color : Color

  do
    this.texture <- texture
    this.pos <- pos
    this.scale <- scale
    this.shrinkRate <- shrinkRate
    this.alive <- true
    this.duration <- duration
    this.color <- color
    this.speed <- speed
    this.originPos <- new Vector2(texture.Width / 2 |> float32, texture.Height / 2 |> float32)
    let angle = MathHelper.ToRadians(angle)
    let up = new Vector2(0.f, -1.f)
    let rot = Matrix.CreateRotationZ(angle)
    this.direction <- Vector2.Transform(up, rot)

  member this.Update(delta:float) =
    let delta = float32 delta
    this.pos <- this.pos + this.direction * this.speed * delta
    this.scale <- this.scale - this.shrinkRate * delta
    this.duration <- this.duration - delta
     
    if (this.scale <= 0.f || this.duration <= 0.f) then
      this.alive <- false
      this.pos <- new Vector2(-100.f, -100.f)

  member this.Draw(sp:SpriteBatch) =
    sp.Draw(this.texture, this.pos, new Nullable<Rectangle>(), this.color, 0.f, this.originPos, this.scale, SpriteEffects.None, 0.f)
