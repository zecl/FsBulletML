namespace FsBulletML.Sample

open System
open System.Collections.Generic
open System.Runtime.Serialization
open Microsoft.FSharp.Core.Operators.Unchecked 
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework.Graphics

open FsBulletML
open FsBulletML.MonoGame

type FsBulletMLSampleGame () as this = 
  inherit Game()
  [<DefaultValue>]val mutable boss : Enemy
  [<DefaultValue>]val mutable currentKeyState : KeyboardState 
  [<DefaultValue>]val mutable prevKeyState : KeyboardState 
  [<DefaultValue>]val mutable enemyIndex : int 
  [<DefaultValue>]val mutable background : Background
  [<DefaultValue>]val mutable fps : Fps
  [<DefaultValue>]val mutable bulletName : string
  [<DefaultValue>]val mutable enemyTasks : (string * BulletmlTask) list
  [<DefaultValue>]val mutable emitter : ParticleEmitter

  static let mutable gmanager = null : GraphicsDeviceManager
  let gametitle, sprite = "FsBulletML.Sample", lazy new SpriteBatch(this.GraphicsDevice)
  let bulletTexture,enemyBullet1Texture, enemyBullet2Texture, playerTexture, playerBullet1Texture, playerBullet2Texture, enemyTexture, backgroundTexture, particleTexture, enemyBullet3Texture = 
    ["bullet"; "enemy_bullet1";"enemy_bullet2";"player";"p_bullet_s";"player_bullet2";"enemy1";"background";"particle";"g_bullet_s"] 
    |> List.map (fun name -> lazy this.Content.Load<Texture2D>(@"..\..\Content\Sprites\" + name)) |> function 
    |  a::b::c::d::e::f::g::h::i::j::[] -> a,b,c,d,e,f,g,h,i,j | _ -> invalidArg "tlist" "長さが違う"
  let sfont = lazy this.Content.Load<SpriteFont>(@"..\..\Content\font\SpriteFont2")
  let drawText (msg:string) (v:Vector2) c = sprite.Force() |> function
    | x -> sfont.Force() |> fun font -> [font, msg, Vector2(v.X+2.f,v.Y+2.f), Color.Gray; font, msg, v, c ] 
                                       |> List.iter (fun (font, msg, v, c) -> x.DrawString(font, msg, v, c))

  static let mutable ship = defaultof<Player>
  static member Player : Player = ship
  static member Graphics = gmanager

  let enemyDefaultPos = new Vector2(Settings.Enemy.X, Settings.Enemy.Y)
  let createEnemy pos bulletMove = 
      let enemy = new Enemy()
      Manager.addEnemy(enemy)
      enemy.pos <- pos 
      enemy.SetBulletTask(bulletMove)
      enemy

  let getEnemyInfo = fun index ->
    let enemys = EnemyControl.enemys enemyDefaultPos (this.enemyTasks)
    let len = enemys |> List.length 
    if len <= (index) then
      this.enemyIndex  <- 0
    enemys 
    |> Seq.nth(this.enemyIndex)
    |> fun (pos,(name,task)) -> task.Init(); pos, (name,task)

  do 
    gmanager <- new GraphicsDeviceManager(this)
    gmanager.PreferredBackBufferWidth <- Settings.Display.Width |> int
    gmanager.PreferredBackBufferHeight <- Settings.Display.Height |> int

  override this.Initialize() =
    base.Initialize()

  override this.LoadContent() =
    base.Window.Title <-gametitle
    this.enemyTasks <- EnemyControl.enemyTasks 

    let pos, (name,bm) = getEnemyInfo(this.enemyIndex)
    this.bulletName <- name
    this.boss <- createEnemy pos (name,bm) 

    ship <- new Player()
    ship.Init()
    ship.texture <- playerTexture.Force()

    this.emitter <- new ParticleEmitter()
    this.fps <- new Fps()
    let bgTexture = backgroundTexture.Force()
    this.background <- new Background(bgTexture, gmanager.PreferredBackBufferHeight |> float32, 64.f)
    base.LoadContent()

  override this.UnloadContent () = 
    base.UnloadContent()

  override this.Update(gameTime) = 
    this.background.Update(gameTime.ElapsedGameTime.TotalSeconds)
    this.fps.Update(gameTime.ElapsedGameTime.TotalSeconds)

    this.prevKeyState <- this.currentKeyState;
    this.currentKeyState <- Keyboard.GetState();

    if (this.boss:>IEnemy).Life = 0 || (this.IsPressed(Keys.Enter)) then
      Manager.removeAll()
      this.enemyIndex <- this.enemyIndex + 1
      let pos, (name,bm) = getEnemyInfo(this.enemyIndex)
      (this.boss :> IBullet).Used <- false
      this.bulletName <- name
      this.boss <- createEnemy pos (name,bm) 

    if Keyboard.GetState().IsKeyDown(Keys.Escape) then
      this.Exit()

    ship.Update()
    Manager.update()
    Manager.free()
    Manager.updateSpace()

    let createParticle pos =
      let rand = new Random ()
      let v = new Vector2 (rand.Next(0, 10) |> float32, rand.Next(0, 10) |> float32)
      let particleTexture = particleTexture.Force()
      this.emitter.Emmit(particleTexture, pos + v, 1.4f, 1.2f, 1.0f, 2, 45, Color.Gold);
      this.emitter.Emmit(particleTexture, pos + v, 1.2f, 1.5f, 1.2f, 1, 40, Color.OrangeRed );
      this.emitter.Emmit(particleTexture, pos + v, 1.2f, 1.2f, 0.9f, 1, 45, Color.LightGoldenrodYellow);
      this.emitter.Emmit(particleTexture, pos + v, 1.6f, 2.0f, 1.8f, 2, 40, Color.Orange);
      this.emitter.Emmit(particleTexture, pos + v, 1.0f, 1.5f, 1.5f, 2, 40, Color.DarkRed);
      this.emitter.Emmit(particleTexture, pos + v, 1.3f, 1.2f, 1.4f, 2, 40, Color.Tomato);

    Manager.checkPlayerCollision ship (fun x -> createParticle x.Pos ;x.damageCounter <- x.damageCounter + 1)
    Manager.checkEnemyCollision (this.boss:>IEnemy) (fun x -> createParticle x.Pos ;if x.Life > 0 then x.Life <- x.Life - 1)
    this.emitter.Update(gameTime.ElapsedGameTime.TotalSeconds);
    base.Update gameTime

  override this.Draw(gameTime) = base.Draw gameTime |> fun _ ->
    gmanager.GraphicsDevice.Clear(Color.Black)
    let spriteBatch = sprite.Force()
    spriteBatch.Begin()
    this.background.Draw(spriteBatch)
    drawText (String.Format("FPS: {0:F5}", this.fps.value)) (new Vector2(3.f, 3.f)) Color.White

    let enemyBullet1Texture = enemyBullet3Texture.Force()
    let enemyBullet2Texture = enemyBullet2Texture.Force()
    let playerBullet1Texture = playerBullet1Texture.Force()
    let playerBullet2Texture = playerBullet2Texture.Force()
    let enemyTexture = enemyTexture.Force()

    Manager.playerBullets
    |> Seq.iter (fun bullet -> let textureCenter = new Vector2(enemyBullet1Texture.Width / 2 |> float32, enemyBullet1Texture.Height / 2 |> float32);
                               let position = Manager.getDrawPos bullet playerBullet1Texture + textureCenter
                               spriteBatch.Draw(playerBullet1Texture, position, System.Nullable(), Color.White,bullet.Dir, textureCenter, 1.0f, SpriteEffects.None, 0.f))
    Manager.enemyBullets
    |> Seq.iter (fun bullet -> let textureCenter = new Vector2(enemyBullet1Texture.Width / 2 |> float32, enemyBullet1Texture.Height / 2 |> float32);
                               let position = Manager.getDrawPos bullet enemyBullet1Texture + textureCenter
                               spriteBatch.Draw(enemyBullet1Texture, position, System.Nullable(), Color.White,bullet.Dir, textureCenter, 1.0f, SpriteEffects.None, 0.f))
    Manager.enemys 
    |> Seq.iter (fun enemy -> spriteBatch.Draw(enemyTexture, Manager.getDrawPos enemy enemyTexture, Color.AntiqueWhite))

    spriteBatch.Draw(ship.texture, Manager.getDrawPos ship ship.texture, Color.AntiqueWhite)

    drawText (sprintf "Name :%s " this.bulletName ) (new Vector2(3.f, 18.f)) Color.White  
    drawText (sprintf "Boss Life : %s" <| (this.boss:>IEnemy).Life.ToString()) (new Vector2(3.f, 33.f)) Color.White  
    drawText (sprintf "Player Damage : %s" <| ship.damageCounter.ToString()) (new Vector2(3.f, 48.f)) Color.White  

    this.emitter.Draw(spriteBatch)

    spriteBatch.End()

  override this.EndRun () = base.EndRun()

  member this.IsPressed(key:Keys) =
    (this.currentKeyState.IsKeyDown(key) && this.prevKeyState.IsKeyUp(key));

