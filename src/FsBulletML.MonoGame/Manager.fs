namespace FsBulletML.MonoGame

open System
open System.Collections.Generic
open System.Runtime.Serialization
open Microsoft.Xna.Framework
open FsBulletML

module Manager =

  let enemys : List<IBullet> = new List<IBullet>() 
  let enemyBullets : List<IBullet> = new List<IBullet>() 
  let playerBullets : List<IBullet> = new List<IBullet>() 

  let spaceMax = (Settings.Display.Width + (Settings.Space.Width * 2.f) / Settings.Space.Width + Settings.Display.Height + (Settings.Display.Height * 2.f) / Settings.Space.Height |> int)
  let enemySpaces : List<IBullet> array = Array.create spaceMax (new List<IBullet>()) 
  let enemyBulletSpaces : List<IBullet> array = Array.create spaceMax (new List<IBullet>()) 
  let playerBulletSpaces : List<IBullet> array = Array.create spaceMax (new List<IBullet>()) 

  let addEnemy (enemy:IBullet) = 
    enemys.Add(enemy) 

  let addEnemyPos (enemy:IBullet, original:Vector2) = 
    enemys.Add(enemy) 
    enemy.X <- original.X 
    enemy.Y <- original.Y 

  let addEnemyBullet (bullet:IBullet) = 
    enemyBullets.Add(bullet)

  let addEnemyBulletPos (bullet:IBullet,original:Vector2) = 
    enemyBullets.Add(bullet) 
    bullet.X <- original.X
    bullet.Y <- original.Y

  let addPlayerBullet (bullet:IBullet) = 
    playerBullets.Add(bullet)

  let addPlayerBulletPos (bullet:IBullet,original:Vector2) = 
    playerBullets.Add(bullet) 
    bullet.X <- original.X
    bullet.Y <- original.Y

  let update () = 
    let update (source:List<IBullet>) = 
      for i = 0 to source.Count - 1 do
        source.[i].Update()

    [enemys; enemyBullets; playerBullets] |> List.iter update

  let free () = 
    let free (source:List<IBullet>) = 
      let mutable i = 0
      for j = 0 to source.Count - 1 do
        if not source.[i].Used then
          source.Remove(source.[i]) |> ignore
          i <- i - 1
        i <- i + 1
    [enemys; enemyBullets;playerBullets] |> List.iter free

  let removeAll () = 
    [enemys; enemyBullets; playerBullets] |> Seq.iter (fun x -> x.Clear())
    enemyBulletSpaces |> Seq.iter (fun x -> x.Clear())
    playerBulletSpaces |> Array.iter (fun x -> x.Clear())

  let inline getPos (target:^T) = (^T : (member Pos : Vector2) (target))
  let inline getRadius (target:^T) = (^T : (member Radius : float32) (target))
  let inline getSize (target:^T) = 
    let width =  (^T : (member Width : int) (target))
    let height =  (^T : (member Height : int) (target))
    width |> float32, height |> float32
  let inline getDrawPos (target:^a) (texture:^b)= 
    let pos = getPos target
    let width,height = getSize texture
    new Vector2(pos.X - (width/2.f), pos.Y - (height/2.f))

  let inline getSpaceIndex target = 
    let pos = getPos target 
    pos.X / Settings.Space.Width + pos.Y / Settings.Space.Height |> int

  let updateSpace () =
    enemyBulletSpaces |> Array.iter (fun x -> x.Clear())
    playerBulletSpaces |> Array.iter (fun x -> x.Clear())
    let add (spaces:List<IBullet> array) (source:List<IBullet>) = 
      for target in source do
        let spaceIndex = getSpaceIndex target
        spaces.[spaceIndex].Add(target) 

    [enemyBullets;] |> List.iter (add enemyBulletSpaces)
    [playerBullets;] |> List.iter (add playerBulletSpaces)

  let inline private checkCollision (self) (selfSpaces:List<IBullet> array) (targetSpaces:List<IBullet> array) free cont =
    let index = getSpaceIndex self
    let checkCollision (targetSpace:List<IBullet> array) = 
      let pos = getPos self
      let radius = getRadius self
      let targetSpace = targetSpaces.[index]
      for target in targetSpace do
        let distance = Vector2.Distance(target.Pos, pos)
        if (distance < target.Radius + radius) then
          free(target)
          cont self

    checkCollision targetSpaces  

  let inline checkPlayerCollision (self) cont =
    checkCollision self playerBulletSpaces enemyBulletSpaces (fun target -> target.Used <- false) cont

  let inline checkEnemyCollision (enemy) cont =
    checkCollision enemy enemySpaces playerBulletSpaces ignore cont
    checkCollision enemy enemyBulletSpaces playerBulletSpaces (fun target -> target.Used <- false) cont
