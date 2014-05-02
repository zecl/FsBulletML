namespace FsBulletML.MonoGame

open System
open System.Collections.Generic
open System.Runtime.Serialization
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics 
open FsBulletML

module Manager =

  [<CompiledName("Enemys")>]
  let enemys : List<IBullet> = new List<IBullet>() 

  [<CompiledName("EnemyBullets")>]
  let enemyBullets : List<IBullet> = new List<IBullet>() 

  [<CompiledName("PlayerBullets")>]
  let playerBullets : List<IBullet> = new List<IBullet>() 

  [<CompiledName("SpaceMax")>]
  let spaceMax = (Settings.Display.Width + (Settings.Space.Width * 2.f) / Settings.Space.Width + Settings.Display.Height + (Settings.Display.Height * 2.f) / Settings.Space.Height |> int)

  [<CompiledName("EnemySpaces")>]
  let enemySpaces : List<IBullet> array = Array.create spaceMax (new List<IBullet>()) 

  [<CompiledName("EnemyBulletSpaces")>]
  let enemyBulletSpaces : List<IBullet> array = Array.create spaceMax (new List<IBullet>()) 

  [<CompiledName("PlayerBulletSpaces")>]
  let playerBulletSpaces : List<IBullet> array = Array.create spaceMax (new List<IBullet>()) 

  [<CompiledName("AddEnemy")>]
  let addEnemy (enemy:IBullet) = 
    enemys.Add(enemy) 

  [<CompiledName("AddEnemyPos")>]
  let addEnemyPos (enemy:IBullet, original:Vector2) = 
    enemys.Add(enemy) 
    enemy.X <- original.X 
    enemy.Y <- original.Y 

  [<CompiledName("AddEnemyBullet")>]
  let addEnemyBullet (bullet:IBullet) = 
    enemyBullets.Add(bullet)

  [<CompiledName("AddEnemyBulletPos")>]
  let addEnemyBulletPos (bullet:IBullet,original:Vector2) = 
    enemyBullets.Add(bullet) 
    bullet.X <- original.X
    bullet.Y <- original.Y

  [<CompiledName("AddPlayerBullet")>]
  let addPlayerBullet (bullet:IBullet) = 
    playerBullets.Add(bullet)

  [<CompiledName("AddPlayerBulletPos")>]
  let addPlayerBulletPos (bullet:IBullet,original:Vector2) = 
    playerBullets.Add(bullet) 
    bullet.X <- original.X
    bullet.Y <- original.Y

  [<CompiledName("Update")>]
  let update () = 
    let update (source:List<IBullet>) = 
      for i = 0 to source.Count - 1 do
        source.[i].Update()

    [enemys; enemyBullets; playerBullets] |> List.iter update

  [<CompiledName("Free")>]
  let free () = 
    let free (source:List<IBullet>) = 
      let mutable i = 0
      for j = 0 to source.Count - 1 do
        if not source.[i].Used then
          source.Remove(source.[i]) |> ignore
          i <- i - 1
        i <- i + 1
    [enemys; enemyBullets;playerBullets] |> List.iter free

  [<CompiledName("RemoveAll")>]
  let removeAll () = 
    [enemys; enemyBullets; playerBullets] |> Seq.iter (fun x -> x.Clear())
    enemyBulletSpaces |> Seq.iter (fun x -> x.Clear())
    playerBulletSpaces |> Array.iter (fun x -> x.Clear())
  
  [<CompiledName("GetDrawPos")>]
  let getDrawPos (pos:Vector2) (texture:Texture2D)= 
    new Vector2(pos.X - (float32(texture.Width/2)), pos.Y - (float32 (texture.Height/2)))

  [<CompiledName("GetSpaceIndex")>]
  let getSpaceIndex (pos:Vector2) = 
    pos.X / Settings.Space.Width + pos.Y / Settings.Space.Height |> int

  [<CompiledName("UpdateSpace")>]
  let updateSpace () =
    enemyBulletSpaces |> Array.iter (fun x -> x.Clear())
    playerBulletSpaces |> Array.iter (fun x -> x.Clear())
    let add (spaces:List<IBullet> array) (source:List<IBullet>) = 
      for target in source do
        let spaceIndex = getSpaceIndex target.Pos 
        spaces.[spaceIndex].Add(target) 

    [enemyBullets;] |> List.iter (add enemyBulletSpaces)
    [playerBullets;] |> List.iter (add playerBulletSpaces)

  [<CompiledName("CheckCollision")>]
  let private checkCollision (pos:Vector2) (radius:float32) (selfSpaces:List<IBullet> array) (targetSpaces:List<IBullet> array) free cont =
    let index = getSpaceIndex pos
    let checkCollision (targetSpace:List<IBullet> array) = 
      let targetSpace = targetSpaces.[index]
      for target in targetSpace do
        let distance = Vector2.Distance(target.Pos, pos)
        if (distance < target.Radius + radius) then
          free(target)
          cont ()
    checkCollision targetSpaces  

  [<CompiledName("CheckPlayerCollision")>]
  let checkPlayerCollision playerPos radius cont =
    checkCollision playerPos radius playerBulletSpaces enemyBulletSpaces (fun target -> target.Used <- false) cont

  [<CompiledName("CheckEnemyCollision")>]
  let checkEnemyCollision enemyPos radius cont =
    checkCollision enemyPos radius enemySpaces playerBulletSpaces ignore cont
    checkCollision enemyPos radius enemyBulletSpaces playerBulletSpaces (fun target -> target.Used <- false) cont