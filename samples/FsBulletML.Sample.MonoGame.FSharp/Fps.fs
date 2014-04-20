namespace FsBulletML.Sample.MonoGame.FSharp

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type Fps () as this =
  [<DefaultValue>]val mutable value : float32
  [<DefaultValue>]val mutable private frameCount : float32
  [<DefaultValue>]val mutable private updateTimer : float32
  [<DefaultValue>]val mutable private interval : float32

  do
    this.interval <- 1.f

  member this.Update(delta:float) =
    this.frameCount <- this.frameCount + 1.f
    this.updateTimer <- this.updateTimer + float32 delta
    if (this.updateTimer > this.interval) then
      this.value <- this.frameCount / this.updateTimer
      this.frameCount <- 0.f
      this.updateTimer <- this.updateTimer - this.interval
