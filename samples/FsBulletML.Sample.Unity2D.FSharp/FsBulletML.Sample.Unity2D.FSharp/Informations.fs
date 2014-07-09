namespace FsBulletML.Sample.Unity2D.FSharp

open System
open System.Text 
open System.Collections.Generic
open System.Runtime.Serialization
open UnityEngine
open FsBulletML
 
[<ExecuteInEditMode()>]
type Informations () =
  inherit MonoBehaviour()

  [<DefaultValue>]val mutable public show : bool
  [<DefaultValue>]val mutable public showInEditor : bool
  [<DefaultValue>]val mutable public intervalTime : float32

  [<DefaultValue>]val mutable private enemy : Enemy
  [<DefaultValue>]val mutable private player : Player
  [<DefaultValue>]val mutable private oldTime : float32
  [<DefaultValue>]val mutable private frame : float32
  [<DefaultValue>]val mutable private frameRate : float32
  [<DefaultValue>]val mutable private info : string
  [<DefaultValue>]val mutable private enemyBullets : int
  [<DefaultValue>]val mutable private playerBullets : int

  member this.Awake () = 
    Application.targetFrameRate <- 40
    this.enemy <- GameObject.FindObjectOfType<Enemy>()
    this.player <- GameObject.FindObjectOfType<Player>()
    this.useGUILayout <- false

  member this.Start () =
    this.oldTime <- Time.realtimeSinceStartup

  member this.Update () = 
    if (not this.show) then
      ()
    else
      this.frame <- this.frame + 1.f
      let time = Time.realtimeSinceStartup - this.oldTime
      if time >= this.intervalTime then
        this.frameRate <- this.frame / time
        this.info <- "FPS:" + this.frameRate.ToString()
        this.oldTime <- Time.realtimeSinceStartup
        this.frame <- 0.f
        this.enemyBullets <- GameObject.FindGameObjectsWithTag("EnemyBullet").Length
        this.playerBullets <- GameObject.FindGameObjectsWithTag("PlayerBullet").Length

  member this.OnGUI () =
    if ((Application.isPlaying && this.show) || (not Application.isPlaying && this.showInEditor)) then
      GUI.Box(new Rect(5.f, 30.f, 475.f, 96.f), "")
      GUI.Label(new Rect(10.f, 30.f, 1000.f, 200.f), this.GetShowText())

    if (GUI.Button(new Rect(445.f, 35.f, 25.f, 22.f), if this.show then  "▲" else "▼")) then
        this.show <- not this.show

    // Prev
    if (GUI.Button(new Rect(5.f, 5.f, 25.f, 22.f), "<")) then
        this.enemy.Prev()

    // Next
    if (GUI.Button(new Rect(445.f, 5.f, 25.f, 22.f), ">")) then
        this.enemy.Next()

  member this.GetShowText () : string =
    let sb = new StringBuilder();
    // FPS
    sb.Append(String.Format("FPS:{0:F2}fps\n", this.frameRate)) |> ignore
    // 弾名
    sb.Append(String.Format("Name:{0}\n", this.enemy.BulletName)) |> ignore
    // ボスライフ
    sb.Append(String.Format("Boss Life:{0}\n", this.enemy.Life)) |> ignore
    // プレイヤーダメージ
    sb.Append(String.Format("Player Damages:{0}\n", this.player.Damage)) |> ignore
    // 敵弾数
    sb.Append(String.Format("EnemyBullets:{0}\n", this.enemyBullets)) |> ignore
    // 自機弾数
    sb.Append(String.Format("PlayerBullets:{0}\n", this.playerBullets)) |> ignore
    sb.ToString()