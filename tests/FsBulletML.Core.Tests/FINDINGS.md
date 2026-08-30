# 走らせる側を測って出てきたもの

リファクタリング前に挙動を固める作業（枝 `feature/core-tests`）で出た調査結果。
**ここに書いたものは 1 つも直していない。** 控え（`tests/TestData/trace/`）は
「正しい姿」ではなく「いまの姿」の記録なので、リファクタリングで控えが動いたら、
直ったのか別の壊れ方をしたのかは人が決めること。

測ったのは `dba9d27`（`master`、2026-08-31 時点）。

---

## 1. `bullet` の中に書いた `direction` が効かない

固めた場所: `BulletDirection.fs` / 控え `bullet-direction-where.txt`

4 条件で切り分けた。30 度 = 0.524 rad、自機 (30,100) を狙う aim = 2.850 rad。

| 書いた場所 | 参照 | パラメータ | 結果 |
|---|---|---|---|
| `bullet` の中 | なし | なし | `d=2.850` 効かない |
| `bullet` の中 | `bulletRef` | なし | `d=2.850` 効かない |
| `bullet` の中 | `bulletRef` | `$1` | `d=2.850` 効かない |
| `fire` の中 | なし | なし | `d=0.524` 効く |

**参照かどうかもパラメータかどうかも関係ない。**「`bullet` に書いたか `fire` に書いたか」
だけで決まる。同じ `bullet` の中の `speed` は 4 条件とも効いているので非対称。

機構も見えている。

```fsharp
// BulletRunner.fs の fireCommand
| ProcessableBulletml.Bullet(attr,_,speed,_) ->
    match speed with ...          // speed しか読んでいない
...
newBullet.Dir <- bulletmlTask.FireData.[...].SrcDir |> calcDir
                                  // 向きは fire 側からしか入らない
```

```fsharp
// Processable.fs:192
| Bullet of BulletAttrs * Direction option * Speed option * ProcessableBulletml list
//                        ^^^^^^^^^^^^^^^^ これが _ で捨てられている
```

そして `Processable.fs:190`、型の 2 行上に DTD がそのまま書いてある。

```
<!ELEMENT bullet (direction?, speed?, (action | actionRef)* )>
```

**仕様は許していて、パーサも AST に入れていて、走らせる側だけが捨てている。**

直すなら `fire` 側の `direction` との優先順位を決める必要がある。BulletML では
`fire` に `direction` があればそちらが勝ち、無ければ `bullet` の `direction`、
どちらも無ければ `aim`、という形。いまは 2 段めが抜けている。

---

## 2. `wait` の最初の 1 周だけ 1 フレーム短い

固めた場所: `Timing.fs` / 控え `wait-intervals.txt`

`wait` を 1..4、`repeat` の有無で 8 条件。

| | 最初の間隔 | 以降 |
|---|---|---|
| `repeat` あり | `w` | `w+1` |
| `repeat` なし | `w+1` | `w+2` |

規則が 2 つ出ている。

**(a) 最初の 1 周だけ 1 フレーム短い。** `repeat` の有無に関係なく。
ぜくるの判断は「不具合の可能性が高い。現状として記録」（2026-08-31）。

**(b) `top` action のループは `repeat` より毎周 1 フレーム多い。**
`RunTask` が `Processed` のとき `task.Init()` する設計から来ているので、たぶん意図したもの。

`vanish` の控えでも同じ 1 フレームのずれが出ている（`wait 2` のあと f04 で消える）。
(a) と同じ現象と見ている。

---

## 3. `run` に、絶対値を返す枝が 2 つある。どちらも届かなかった

固めた場所: `Degenerate.fs` / 控え `run-returns-delta-or-absolute.txt` ほか 2 本

`run` は X / Y を差分で返す —— のだが、2 か所だけ絶対値を返す。

```fsharp
// BulletRunner.fs の run
match bullet.Task with
| None ->
    RunResult(true, bullet.X, bullet.Y)           // (a) 絶対値
| Some bulletmlTask ->
    let tasks = bulletmlTask.Tasks
    if tasks :> obj <> null then
      ...
      RunResult(_, x, y)                          // 差分（accel + sin(dir)*speed）
    else
      RunResult(true, bullet.X, bullet.Y)         // (b) 絶対値
```

呼ぶ側（`BaseBullet.RunTask` / `DefaultBullet`）はどちらの枝でも `X <- X + result.X` と
足すので、(a) か (b) を通ると**そのフレームで座標が 2 倍になる**。

**届くのかを測った。届かなかった。**

- (a) は `RunTask` が `match this.self.Task with | None -> ()` で先に弾いているので、
  本番から `run` に入らない
- (b) は座標を `X=7 / Y=11` に置いて `Tasks` が空の状態で `run` を直に呼んで確かめた。
  返ってきたのは `X=0 / Y=0` で**差分のほう**。F# の空リストは `null` ではないので
  `tasks :> obj <> null` が真になり、(b) には入らない

`convertBulletmlTask` は必ずリストを入れる（`toProcessable` の `List.map`、
`bulletml` が null のときも `Tasks = []`）ので、(b) に入る作り方が見当たらない。

**つまりいまは死んだ枝。** 直すべきものではないが、リファクタリングで
「絶対値を返す枝がある」ことを見落として整理すると、**届くようにした瞬間に壊れる**。
`top` ラベルの action が無い BulletML と、action が 1 つも無い BulletML を
控えに入れてある（どちらも落ちず、毎フレーム `P+` のまま動かない）。

---

## 4. 未解決の `$N` が `0` ではなく `N` になる

固めた場所: `Expressions.fs` / 控え `expr-unresolved-param.txt` `expr-short-params.txt`

`getValue` は、置き換え残りの `$` を `0` に潰そうとしている。

```fsharp
// Processable.fs:117
let s = System.Text.RegularExpressions.Regex.Replace(s,"\$d*","0")
```

正規表現が `\$d*`（`$` のあとに文字 `d` が 0 個以上）になっている。
`\$\d*`（`$` のあとに数字が 0 個以上）のつもりだったように見える。

いまの形だと `$` だけが `0` に置き換わり、**数字がそのまま残る**。

| 式 | いま | `\$\d*` なら |
|---|---|---|
| `$1` | `01` → **1** | `0` → 0 |
| `$2` | `02` → **2** | 0 |
| `$12` | `012` → **12** | 0 |
| `1+$1` | `1+01` → **2** | 1 |

**届く。本番の経路で踏める。**

```
fireRef に param を 1 つだけ渡し、参照先が $1+$2 を使う場合
  param 2 つ（2 と 5）  s=7.000   $1+$2 = 2+5   正しい
  param 1 つ（2 だけ）  s=4.000   $1+$2 = 2+2   $2 が 0 でなく 2 になっている
```

つまり **参照にパラメータを足りなく渡すと、欠けた `$N` が静かに `N` として計算される**。
エラーにも 0 にもならない。

式の評価そのものは正しい（`expr-arithmetic.txt`）。
優先順位、括弧、`10/4 = 2.5`、`7%3 = 1`、負の数、いずれも合っている。
`$rand` / `$rank` の置換と算術も合っている（`expr-rand-rank.txt`）。

---

## 5. `<bulletml type>` は走らせる側に効かない

固めた場所: `ShootingType.fs` / 控え `shooting-type-no-effect.txt` `shooting-type-unknown.txt`

見つけたのは planner。こちらで数え直して確認した。

`ShootingDirection` を右辺で読んで**分岐している箇所が無い**。運ばれる経路はこう。

```
XML の type="vertical"
  -> IntermediateParser.fs:191-196  文字列から DU へ
  -> :201-202                       BulletmlAttrs.bulletmlType へ
  -> BulletRunner.fs:399-413        取り出して Task に set
  -> ここで終わり
```

同じ数え方で `BulletType` を数えると分岐が 5 か所出るので、数え方のほうは効いている。

**1 か所だけ訂正**（planner は「読む 0 件」としていた）。`DTD.fs:204-206` が読んでいる。
ただし XML へ書き戻すときの文字列化で、挙動の分岐ではない。
**挙動には効かないが、XML の往復には効く。** 往復は `Parser.Tests` の領分なので
ここでは触っていない。

フロント側は別物が同じ名前で置かれている。

```
Core      BulletRunner.fs:404       既定 BulletVertical
MonoGame  BaseBullet.fs:28          member val ... = BulletHorizontal
Unity2D   DefaultBullet.fs:30       member val ... = BulletHorizontal
```

`member val` の独立プロパティなので、**Core が set した値を受け取る経路が無い**。
`Processable.fs:147` が `abstract` で置いているので Core 側が呼ぶ設計だったはずだが、
呼んでいない。既定値も Core と フロントで逆。

### 控えの取り方をふつうと変えてある

読む側が居ないので、**軌跡の控えを 1 本取っても「どの type でも緑」になり何も担保しない**。
なので 3 つの type で**同じ軌跡になること**のほうを見ている。
効くようになったらここが赤くなる。

知らない値（`type="diagonal"`）を書いた場合は例外が飛ぶ。これは効いている。

```
BulletmlDTDViolationException: not support ShootingDirection.：[diagonal]
```

---

## 測り方について

- 継ぎ目は既にインターフェースとして空いていたので、**本体は 1 行も触っていない**
  - `IBulletMLManager` … `$rand` / `$rank` / 自機位置
  - `IBulletmlObject` … 弾そのもの
- 1 フレームの中身は `BaseBullet.RunTask` と `DefaultBullet.Update` を写した。
  `run` が返すのは差分なので呼ぶ側が足す
- 「毎フレーム生きている弾を作られた順に回す」ところはゲームループ側の話で Core には無い。
  **このフレームで産まれた弾は次のフレームから回す**、と決め打ちしてある
- 控えの桁は 3。`float32` の有効桁が約 7 で、座標が 100 台のとき有効 6 桁になり
  1 桁ぶん余る。`speed 1.7` を 120 フレーム回して `x=121.747284` まで測った。
  **座標が 1000 を超えると限界に当たる**ので軌跡は短く保つこと
- 控えが無いときは書いて落ちる。黙って作って緑にすると、控えを作っただけで
  何も担保していない状態が緑になるため
- 門が働くことは、控えを 1 か所 `0.001` だけ壊して確かめてある

## 仕様と突き合わせて合っていたもの

控えは全部いちど目で読んでいる。次は BulletML の仕様どおりだった。

- `changeDirection` の absolute / relative / aim / sequence
- `changeSpeed` の absolute / relative / sequence
- `accel` の absolute / relative / sequence（`horizontal` と `vertical` が別々に効く）
- `repeat` の回数、入れ子の `repeat`（`sequence` は外側をまたいでも累積する）
- `actionRef` / `fireRef` のパラメータ展開（`$1` が `repeat` の `times` にも届く）
- `$rand` / `$rank` の式への置換と算術
- `vanish` で消えた弾が以降フレームに出てこない
- 弾の中の `action` からさらに撃つ
