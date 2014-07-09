namespace FsBulletML.Unity2D

open System
open System.Collections.Generic
open System.Runtime.Serialization
open UnityEngine
open FsBulletML

module Manager =

  [<CompiledName("Enemies")>]
  let enemies : List<IDefaultBullet> = new List<IDefaultBullet>() 

  [<CompiledName("RootBullets")>]
  let rootBullets : List<IDefaultBullet> = new List<IDefaultBullet>() 

  [<CompiledName("EnemyBullets")>]
  let enemyBullets : List<IDefaultBullet> = new List<IDefaultBullet>() 

  [<CompiledName("PlayerBullets")>]
  let playerBullets : List<IDefaultBullet> = new List<IDefaultBullet>() 

  [<CompiledName("SpaceMax")>]
  let spaceMax = 
    let a = (Settings.Display.Width * Settings.Display.PixcelsToUnits) + (Settings.Space.Width * Settings.Display.PixcelsToUnits * 2.f) / (Settings.Space.Width * Settings.Display.PixcelsToUnits)
    let b = (Settings.Display.Height * Settings.Display.PixcelsToUnits) + (Settings.Display.Height * Settings.Display.PixcelsToUnits * 2.f) / (Settings.Space.Height * Settings.Display.PixcelsToUnits)
    a + b |> int

  [<CompiledName("EnemySpaces")>]
  let enemySpaces : List<IDefaultBullet> array = Array.create spaceMax (new List<IDefaultBullet>()) 

  [<CompiledName("EnemyBulletSpaces")>]
  let enemyBulletSpaces : List<IDefaultBullet> array = Array.create spaceMax (new List<IDefaultBullet>()) 

  [<CompiledName("PlayerBulletSpaces")>]
  let playerBulletSpaces : List<IDefaultBullet> array = Array.create spaceMax (new List<IDefaultBullet>()) 

  [<CompiledName("AddEnemy")>]
  let addEnemy (enemy:IDefaultBullet) = 
    enemies.Add(enemy) 

  [<CompiledName("AddRootBullet")>]
  let addRootBullet (bullet:IDefaultBullet) = 
    rootBullets.Add(bullet) 

  [<CompiledName("AddRootBulletPos")>]
  let addRootBulletPos (bullet:IDefaultBullet, original:Vector2) = 
    rootBullets.Add(bullet) 
    bullet.X <- original.x 
    bullet.Y <- original.y 

  [<CompiledName("AddEnemyPos")>]
  let addEnemyPos (enemy:IDefaultBullet, original:Vector2) = 
    enemies.Add(enemy) 
    enemy.X <- original.x 
    enemy.Y <- original.y 

  [<CompiledName("AddEnemyBullet")>]
  let addEnemyBullet (bullet:IDefaultBullet) = 
    enemyBullets.Add(bullet)

  [<CompiledName("AddEnemyBulletPos")>]
  let addEnemyBulletPos (bullet:IDefaultBullet,original:Vector2) = 
    enemyBullets.Add(bullet) 
    bullet.X <- original.x
    bullet.Y <- original.y

  [<CompiledName("AddPlayerBullet")>]
  let addPlayerBullet (bullet:IDefaultBullet) = 
    playerBullets.Add(bullet)

  [<CompiledName("AddPlayerBulletPos")>]
  let addPlayerBulletPos (bullet:IDefaultBullet,original:Vector2) = 
    playerBullets.Add(bullet) 
    bullet.X <- original.x
    bullet.Y <- original.y

  [<CompiledName("Free")>]
  let free () = 
    let free (source:List<IDefaultBullet>) = 
      let mutable i = 0
      for j = 0 to source.Count - 1 do
        if not source.[i].Used then
          let mb = source.[i] :?> UnityEngine.MonoBehaviour 
          if mb <> null then
            mb.enabled <- false
            source.Remove(source.[i]) |> ignore
            UnityEngine.MonoBehaviour.DestroyObject(mb.gameObject)
            i <- i - 1
        i <- i + 1
    [enemies;rootBullets;enemyBullets;playerBullets] |> List.iter free

  [<CompiledName("RemoveNonEnemy")>]
  let removeNonEnemy () = 
    [rootBullets;enemyBullets; playerBullets] |> Seq.iter (fun x -> x |> Seq.iter(fun b -> b.Used <- false))

  [<CompiledName("RemoveAll")>]
  let removeAll () = 
    [enemies;rootBullets;enemyBullets; playerBullets] |> Seq.iter (fun x -> x |> Seq.iter(fun b -> b.Used <- false))

  [<CompiledName("GetSpaceIndex")>]
  let getSpaceIndex (pos:Vector2) = 
    let a = (Math.Abs(pos.x) * Settings.Display.PixcelsToUnits) / (Settings.Space.Width * Settings.Display.PixcelsToUnits)
    let b = (Math.Abs(pos.y) * Settings.Display.PixcelsToUnits) / (Settings.Space.Height * Settings.Display.PixcelsToUnits)
    a + b |> int

  [<CompiledName("UpdateSpace")>]
  let updateSpace () =
    enemyBulletSpaces |> Array.iter (fun x -> x.Clear())
    playerBulletSpaces |> Array.iter (fun x -> x.Clear())
    let add (spaces:List<IDefaultBullet> array) (source:List<IDefaultBullet>) = 
      for target in source do
        let spaceIndex = getSpaceIndex <| Vector2(target.Pos.x,target.Pos.y)
        spaces.[spaceIndex].Add(target) 

    [enemyBullets;] |> List.iter (add enemyBulletSpaces)
    [playerBullets;] |> List.iter (add playerBulletSpaces)

  [<CompiledName("CheckCollision")>]
  let private checkCollision (pos:Vector2) (radius:float32) (targetSpaces:List<IDefaultBullet> array) free cont =
    let index = getSpaceIndex pos
    let checkCollision (targetSpace:List<IDefaultBullet> array) = 
      let targetSpace = targetSpaces.[index]
      for target in targetSpace do
        let distance = Vector2.Distance(Vector2(target.Pos.x,target.Pos.y), pos)
        if (distance < target.Radius + radius) then
          free(target)
          cont ()
    checkCollision targetSpaces  

  [<CompiledName("CheckPlayerCollision")>]
  let checkPlayerCollision playerPos radius cont =
    checkCollision playerPos radius enemyBulletSpaces (fun target -> target.Used <- false) cont

  [<CompiledName("CheckEnemyCollision")>]
  let checkEnemyCollision enemyPos radius cont =
    checkCollision enemyPos radius playerBulletSpaces (fun target -> target.Used <- false) cont