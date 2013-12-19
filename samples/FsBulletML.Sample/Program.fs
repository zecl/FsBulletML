namespace FsBulletML.Sample

open Microsoft.Xna.Framework
open FsBulletML

module Program =
  [<EntryPoint>]
  let main (args : string[]) = 
    BulletMLManager.Init(new BulletFunctions())
    use game = new FsBulletMLSampleGame()
    game.Run()
    0
