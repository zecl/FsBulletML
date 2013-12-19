namespace FsBulletML.Sample

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type Background (texture, screenHeight, scrollSpeed) as this =
  [<DefaultValue>]val mutable private texture : Texture2D 
  [<DefaultValue>]val mutable private pos : Vector2 
  [<DefaultValue>]val mutable private originPos : Vector2 
  [<DefaultValue>]val mutable private scrollSpeed : float32
  [<DefaultValue>]val mutable private screenHeight : float32

  do
    this.scrollSpeed <- scrollSpeed
    this.screenHeight <- screenHeight
    this.texture <- texture
    this.originPos <- new Vector2(0.f, 0.f)
    this.pos <- new Vector2(this.originPos.X, this.originPos.Y)

  member this.Update(delta:float) =
    this.pos.Y <- this.pos.Y + this.scrollSpeed * (delta |> float32)
    this.pos.Y <- this.pos.Y % (float32 this.texture.Height)

  member this.Draw(sp:SpriteBatch) =
    if (this.pos.Y < this.screenHeight) then
      sp.Draw(this.texture, this.pos, Color.White)
    let drawPos = this.pos - new Vector2(0.f, this.texture.Height |> float32)
    sp.Draw(this.texture, drawPos, Color.White)
