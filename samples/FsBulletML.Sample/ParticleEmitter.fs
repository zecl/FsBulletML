namespace FsBulletML.Sample

open System
open System.Collections.Generic 
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type ParticleEmitter () as this =
  [<DefaultValue>]val mutable private particles : List<Particle>
  [<DefaultValue>]val mutable private rand : Random

  do
    this.particles <- new List<Particle>()
    this.rand <- new Random()

  member this.Clear () =
    this.particles.Clear()

  member this.Emmit (texture, pos, scale, shrinkRate, duration, amount, maxSpeed, colour) =
    for i in [0..amount-1]  do
      let angle = this.rand.Next(0, 360) |> float32
      let speed = this.rand.Next(1, maxSpeed) |> float32
      let p = new Particle(texture, pos, speed, angle, scale, shrinkRate, duration, colour)
      if this.particles |> Seq.length < 1000 then
        this.particles.Add(p)

  member this.Update(delta:float) =
    let toRemove = new List<Particle>()
    this.particles |> Seq.iter (fun p -> p.Update(delta); if p.alive |> not then toRemove.Add(p))
    for i in [0..toRemove.Count-1] do
      this.particles.Remove(toRemove.[i]) |> ignore

  member this.Draw(sp:SpriteBatch) =
    this.particles |> Seq.iter (fun p -> p.Draw(sp))
