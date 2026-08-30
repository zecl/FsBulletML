namespace FsBulletML.Core.Tests

open System
open System.Collections.Generic
open System.Globalization
open System.Text
open FsBulletML
open FsBulletML.Processable

/// XML を食わせて N フレーム回し、生きている弾ぜんぶの軌跡を作る。
///
/// 1 つの弾の 1 フレームは本番を写したもの。MonoGame の BaseBullet.RunTask と
/// Unity の DefaultBullet.Update が同じ形で、run が返すのは**差分**なので
/// 呼ぶ側が足す。ここを間違えると誰も通らない経路を測ることになる。
///
/// 「毎フレーム、生きている弾を作られた順に 1 回ずつ回す」ところは本番の
/// ゲームループ側の話で Core には無い。**このフレームで産まれた弾は次のフレームから回す**、
/// と決め打ちしてある。控えを読むときはそれを前提にすること。
module Trace =

  /// 浮動小数の桁。float32 なので固定しないと環境や版で揺れて偽の赤が出る。
  ///
  /// 3 にした根拠は測ったもの。float32 の有効桁は約 7。小数 3 桁だと、
  /// 座標が 100 台のとき有効 6 桁で、限界まで 1 桁ぶん余る。
  /// speed 1.7 を 120 フレーム回して x=121.747284（6 桁で読むと 4 桁め以降は既に雑音）。
  /// **座標が 1000 を超えると限界に当たる**ので、軌跡は短く保つこと。
  let mutable digits = 3

  let private fmt (v: float32) =
    let r = Math.Round(float v, digits)
    let r = if r = 0.0 then 0.0 else r   // -0.000 を 0.000 に寄せる
    r.ToString("F" + string digits, CultureInfo.InvariantCulture)

  /// XML 文字列を frames フレーム回した軌跡を返す
  let run (xml: string) (frames: int) : string =
    let born = List<FakeBullet>()
    let root = FakeBullet(0, born)
    let o = root :> IBulletmlObject
    o.Init()

    let bulletml = readXmlString xml
    o.Task <- BulletRunner.convertBulletmlTaskOption bulletml

    let sb = StringBuilder()
    let mutable seen = 0

    let step (b: FakeBullet) =
      let o = b :> IBulletmlObject
      if o.Used then
        match o.Task with
        | None -> sb.AppendLine(sprintf "  b%d --" b.Id) |> ignore
        | Some task ->
          let before = b.VanishCount
          let result = BulletRunner.run o
          o.X <- o.X + result.X
          o.Y <- o.Y + result.Y
          if result.Processed then task.Init()
          let mark = if result.Processed then "P+" else "P-"
          sb.Append(sprintf "  b%d %s x=%s y=%s d=%s s=%s"
                      b.Id mark (fmt o.X) (fmt o.Y) (fmt o.Dir) (fmt o.Speed)) |> ignore
          if b.VanishCount > before then sb.Append(" vanish") |> ignore
          sb.AppendLine() |> ignore

    for i in 0 .. frames - 1 do
      sb.AppendLine(sprintf "f%02d" i) |> ignore
      // このフレームで回す顔ぶれを先に固める。途中で産まれた弾は次のフレームから
      let live = Array.append [| root |] (born.ToArray())
      for b in live do step b
      // このフレームで産まれた弾を、産まれた順に出す
      while seen < born.Count do
        let b = born.[seen]
        let bo = b :> IBulletmlObject
        sb.AppendLine(sprintf "  +b%d d=%s s=%s" b.Id (fmt bo.Dir) (fmt bo.Speed)) |> ignore
        seen <- seen + 1

    sb.ToString().Replace("\r\n", "\n")
