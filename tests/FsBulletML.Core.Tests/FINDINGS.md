# 走らせる側を測って出てきたもの

## 目次

| | 何が | 直すと挙動が変わるか |
|---|---|---|
| [1](#1-bullet-の中に書いた-direction-が効かない) | `bullet` の中の `direction` が効かず `aim` に落ちる | 変わる |
| [2](#2-wait-の最初の-1-周だけ-1-フレーム短い) | `wait` の最初の 1 周だけ 1 フレーム短い | 変わる |
| [3](#3-run-に絶対値を返す枝が-2-つあるどちらも届かなかった) | 絶対値を返す枝が 2 つ。どちらも届かない | 変わらない（死んだ枝） |
| [4](#4-未解決の-n-が-0-ではなく-n-になる) | 未解決の `$N` が `0` ではなく `N` になる | 変わる |
| [5](#5-bulletml-type-は走らせる側に効かない) | `<bulletml type>` が効かない。既定値が 3 層で違う | 繋ぐなら変わる |
| [6](#6-小数点がカンマのカルチャでは式の評価が落ちる) | `,` が小数点のカルチャで BulletML が 1 つも走らない | 直せば動くようになる |
| [7](#7-fire-の-speed-typerelative-が親の速さを見ていない) | `fire` の `speed type="relative"` が親の速さを見ない | 変わる |
| [8](#8-相互参照でスタックが溢れプロセスごと落ちる) | 参照が輪になると `StackOverflow` でプロセス死 | 直せば読めるようになる |
| [9](#9-top-が複数あっても先頭に-wait-があると後ろが-1-度も走らない) | 複数の `top*` のうち先頭しか走らない | 変わる |
| [10](#10-同梱のサンプルを全部走らせた結果) | 同梱サンプル 227 本を全部走らせた結果 | — |
| [11](#11-ref-の-param-に入れた値は展開のときに数へ潰される) | `ref` の `param` が展開のとき数へ潰され、`$rank` が凍る | 変わる |
| [12](#12-同じ-label-が-2-つあると文書順で先にあるほうが走る) | 同じ `label` が 2 つあると先勝ち（同梱に実例 0 本） | 実例が無いので変わらない |
| [13](#13-name--description-は往復で消えるただし経路で割れる) | `name` / `description` が DTD 経由の往復で消える | 直す向きを決める必要がある |

控えを置いていない読みは [こちら](#控えを置いていない読み当てる先が-0-件のもの)。

**8 は zecl から「当時の妥協。今回の改修で直せたら直したい」**（2026-08-31）。

正しく動いていたものは末尾の [仕様と突き合わせて合っていたもの](#仕様と突き合わせて合っていたもの) に。


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

### 既定値が 3 つある

見つけたのは planner。同じ問いに 3 つ別の答えがある。

```
DTD       none        DTD.fs:141  <!ATTLIST bulletml type (none|vertical|horizontal) "none">
Core      vertical    BulletRunner.fs:404  | None -> ShootingDirection.BulletVertical
フロント   horizontal  BaseBullet.fs:28 / DefaultBullet.fs:30  member val ... = BulletHorizontal
```

**Core の枝は届く。** `type` を省いた BulletML を食わせても落ちず、
`IntermediateParser.fs:200-204` が `bulletmlType = None` を返すので `:404` に入る。
（最初こちらは「省くと例外が飛ぶので届かない枝」と読んだが、**測ったら落ちなかった**。
`IntermediateParser.fs:221` の `None` は type 属性ではなく attrs レコード全体にかかっている。）

いまは `type` 自体が挙動に効かないので、この食い違いは見えない。
**効くようにした瞬間に、3 つのうちどれを採るかを決めることになる。**

### ついでに、例外のメッセージが条件と合っていない

```fsharp
// IntermediateParser.fs:221
| None -> new BulletmlDTDViolationException("this element should have ShootingDirection attribute.") |> raise
```

条件は `tryFindBulletmlAttrs` が `None` のとき、つまり
**`bulletml` 要素の属性が取れなかったとき**であって、`type` 属性の有無ではない。
`type` を省いても、この例外は飛ばない（上で測ったとおり）。

---

## 6. 小数点がカンマのカルチャでは、式の評価が落ちる

固めた場所: `Culture.fs` / 控え `culture-decimal.txt` `culture-literal.txt`

見つけたのは planner（本人は「未測定・推論」と明記）。こちらで走らせて測った。
**読みは当たっていて、実際の壊れ方は予想より硬かった。** 値が狂うのではなく落ちる。

```
en-US    10/4=2.500  1/3=0.333  0-2.5=-2.500  2*1.5=3.000
ja-JP    10/4=2.500  1/3=0.333  0-2.5=-2.500  2*1.5=3.000
de-DE    全部 XPathException
fr-FR    FormatException（2*1.5 だけ XPathException）
```

小数リテラル `2.5` を単体で書いただけでも落ちる。

```fsharp
// Util.fs:27-33
let eval (expression:string) =
  ...
  let ev = nav.Evaluate(String.Format("number({0})", xexpr)) |> string
  Single.Parse(ev)          // CultureInfo を渡していない
```

**測ったのは `Thread.CurrentThread.CurrentCulture` を差し替えた場合だけ。**
`CurrentUICulture` や OS の既定は触っていない。

### 「小数のときだけ」ではなかった

最初「落ちるのは小数が出るときだけ」と書きかけたが、**測っていない断定だった**。
整数だけの式も測ったら、de-DE / fr-FR では**全部落ちた**。

```
de-DE / fr-FR   1+2*3  (1+2)*3  7%3  0-3  8/4   すべて XPathException
```

### 落ちている式は、こちらが書いた式ではなかった

例外のメッセージまで取ったら、失敗している式が `<speed>` ではなく `<wait>` の値だった。

```
XPathException: Function 'number' in 'number(10,0000000000)' has an invalid number of arguments.
```

`<wait>` の値を振って確かめた。`wait=3` なら `3,0000000000`、`wait=7` なら `7,0000000000`。
**値が float を経由して小数 10 桁で文字列化され、カンマが XPath の引数区切りと読まれている。**

### 出どころは 4 か所

```
IntermediateParser.fs:632   (single:float32).ToString("F10")           カルチャ指定なし
IntermediateParser.fs:959   (Processable.getValue x).ToString("F10")   カルチャ指定なし
Processable.fs:116          rand.ToString() / rank.ToString()          カルチャ指定なし
Util.fs:33                  Single.Parse(ev)                           カルチャ指定なし
```

上 3 つが**カンマを作る側**、いちばん下が**カンマを読み違える側**。
planner が最初に見つけたのは `Util.fs:33` で、これは下流のほう。
上流を直さないと `ToString("F10")` がカンマを作り続ける。

### `$rand` の経路は、いまは観測できない

`Processable.fs:116` の `rand.ToString()` もカルチャ指定が無いので、
de-DE では `0,5` が式に入るはず —— という読み（planner）を測ろうとした。**測れなかった。**

```
<wait>10</wait> あり   number(10,0000000000)   ← wait が先に落ちる
<wait> を外す          number(0,0000000000)    ← 今度は direction の 0 が落ちる
<speed>2</speed> だけ  number(0,0000000000)    ← リテラルでも同じ
```

**どの数値も同じ `ToString("F10")` を通るので、最初に評価されたものが必ず落ちる。**
`$rand` に固有の壊れ方があるかは、**上流を直すまで見えない**。

裏を返すと、`,` が小数点のカルチャでは
**BulletML を 1 つも走らせられない**（値を 1 つでも書けば落ちる）。
壊れ方は「一部の式」ではなく「全部」。

fr-FR で `2.5` のときだけ `FormatException` が出るのは、
`ToString("F10")` を通らずに `Single.Parse` へ直行する経路があるためと思われる。
**推論。** 直すときに測り直すこと。

---

## 7. `fire` の `speed type="relative"` が、親の速さを見ていない

固めた場所: `FireShapes.fs` / 控え `fire-speed-relative-base.txt` `bullet-action-relative.txt`

弾の中の `action` からさらに撃つとき、`<speed type="relative">N</speed>` は
**親の速さを無視して N になる**。

```
親 speed=1  relative 1  ->  1.000     親を基準なら 2
親 speed=3  relative 1  ->  1.000     親を基準なら 4
親 speed=5  relative 2  ->  2.000     親を基準なら 7
```

**向きのほうは親を見ている。** 同じ `relative` で基準が違う。

```
親 dir=90 度(1.571) から relative 45 度  ->  2.356 = 1.571 + 0.785   親が基準 ✓
```

機構は `BulletRunner.fs` の `fireCommand` の中で、45 行離れて割れている。

```fsharp
// 向き（:181-186）— 3 つに分かれている
| DirectionType.Sequence -> SrcDir <- SrcDir + changeDir * revise
| DirectionType.Absolute -> SrcDir <- changeDir * revise
| DirectionType.Relative -> SrcDir <- changeDir * revise + bullet.Dir   // 親を見る

// 速さ（:228-232）— 2 つしかない
if (speed.speedType = SpeedType.Sequence || speed.speedType = SpeedType.Relative) then
  SrcSpeed <- SrcSpeed + changeSpeed        // Relative が Sequence と同じ扱い
else
  SrcSpeed <- changeSpeed
```

**`bullet.Speed` が 1 度も出てこない。** `SrcSpeed` は fire の並びの累積値で、
新しい弾の `FireData` は `[<DefaultValue>]` なので 0 から始まる。
だから `relative N` が `0 + N` になる。

BulletML の仕様では `sequence` は「直前の fire からの差」、`relative` は
「撃つ弾自身の速さからの差」で別物。ここでは同じものになっている。

直していない。直すと `speed type="relative"` を使っている弾幕の見た目が変わる。

---

## 8. 相互参照でスタックが溢れ、プロセスごと落ちる

固めた場所: `SelfReference.fs`（**`[<Explicit>]`。通常の走行では回らない**）

見つけたのは planner（本人は未測定と明記）。こちらで走らせて測った。

```
3 つまとめて走らせる       Stack overflow. テストのホスト プロセスがクラッシュしました
actionRef の自己参照 単独  通る
bulletRef の自己参照 単独  通る
```

**落ちるのは相互参照**（`top` が `b` を、`b` が `top` を参照する形）。
消去法で決めた —— 3 つ走らせると落ち、他の 2 つは単独で通るため。

スタックはこう積まれていた。

```
at FsBulletML.IntermediateParser.convertRefBulletml(RecBulletml, RecBulletml)
at FsBulletML.IntermediateParser+convert@961-3.Invoke(RecBulletml)
at Microsoft.FSharp.Collections.ListModule.Map(...)
   ... 以下同じ 3 つの繰り返し
```

参照を展開する `convertRefBulletml` に**打ち止めが無い**。深さの上限も、
辿った label の記録も持っていないので、輪になっていると戻ってこない。

**`StackOverflowException` は .NET では捕まえられない。** `try ... with` を
書いてもプロセスが死ぬ。ライブラリとしては、**壊れた BulletML を 1 つ読ませるだけで
ゲームごと落とせる**ということになる。

### これは既知の妥協

2026-08-31、zecl から。**当時この実装を妥協したのを覚えている。今回の改修で
最終的に直せたら直したい**、とのこと。

つまり「気づかずに壊れていた」ではなく「分かっていて置いた」もの。
直すときは、打ち止めの入れ方（深さの上限か、辿った label の記録か）を
決める話になる。**この控えは、直したことを確かめるための土台**になる。

### この件だけテストの置き方を変えてある

控えを取っていない。**落ちると控えが書けない**ため。
fixture は `[<Explicit>]` で、名指しでなければ回らない。

```
dotnet test tests/FsBulletML.Core.Tests --filter "FullyQualifiedName~SelfReference"
```

通常の走行（48 件）に混ぜると、他の 47 件を巻き添えにして全部落ちる。
実際に 1 回巻き添えにしてから隔離した。

---

## 9. `top` が複数あっても、先頭に `wait` があると後ろが 1 度も走らない

固めた場所: `PlayerAndTops.fs` / 控え `multiple-top.txt` `multiple-top-blocked.txt`

`toProcessable` は `label.StartsWith("top")` で拾うので、
`top` / `top1` / `top2` は 3 本とも task になる（`BulletRunner.fs:25-36`）。
ところが実際に撃つのは先頭の 1 本だけだった。

```
先頭に wait あり   +b1 d=0.000 s=1.000                    top だけ
先頭に wait なし   +b1 top / +b2 top1 / +b3 top / ...      両方が毎フレーム撃つ
```

`run` は `tasks` を順に回すが、`wait` が `RunState.Stop` を返した時点で
`stop <- true` になり、`while i < len && not stop` を抜ける。
**先頭の `wait` が、後ろの `top*` を永久に塞ぐ。**

BulletML の弾幕は `top` の中に `<wait>` を書くのがふつうなので、
**複数の `top*` は事実上使えない**。書いても静かに無視される。

`top` で始まる名前の拾い方そのものは効いている
（`topmost` は拾われ、`nottop` は拾われない）。

直していない。

---

## 通っていなかった分岐を通した（不具合ではない）

`BulletType.Player` の分岐 4 か所（`BulletRunner.fs:58 :189 :195 :298`）は、
ここまでの控えが 1 度も通っていなかった。こちらの弾がずっと `Enemy` だったため。

```
Enemy として撃つ    d=2.850   自機 (30,100) を狙う
Player として撃つ   d=5.695   敵 (-40,-60) を狙う
```

`atan2(-40, 60) = -0.588` を `calcDir` が `2π` 足して `5.695` にしている。
**正しく動いている。** 撃った弾が親の `BulletType` を継ぐのも確認した。

### 偽の弾の副作用を 1 つ踏んだ

最初、敵を原点に置いていた。根の弾も原点なので `atan2(0, -0)` が `π` になり、
**ライブラリの値ではなくこちらの置き方が控えに出ていた**（`d=3.142`）。
敵を `(-40,-60)` へずらして測り直した。

---

## 10. 同梱のサンプルを全部走らせた結果

固めた場所: `Corpus.fs` / 控え `corpus-smoke.txt`

`samples/` の実物の弾幕を全部、60 フレームずつ走らせた網。
**軌跡の控えは取らない。** 200 本ぶんの軌跡を控えにすると、リファクタリングのたびに
巨大な差分が出て誰も読まなくなる。残すのは名前と件数だけの**広く浅い網**。

```
一意な弾幕 227 本
  撃った       210 本
  撃たなかった   6 本
  落ちた         4 本
  避けた         7 本（StackOverflow で捕まえられないもの）
```

### 避けた 7 本 —— 8 が実物で当たった

**合成した入力ではなく、同梱のサンプルが 8 の無限再帰を踏む。**
`[ESP_RADE]_round_123_boss_izuna_fan.xml` ほか 6 本。

見つけ方は「処理する前にファイル名を控えへ書き、落ちたあと最後の行を読む」。
`StackOverflow` は捕まえられないので、これ以外に特定する道が無い。

そのあと静的に閉路を数える道具を書いて 7 本を先に出し、避けるリストに入れた。
**静的に出た 7 本が、実際に落ちるものを過不足なく覆っていた。**

直したらこのリストを空にして測り直すこと。

### 落ちた 4 本 —— どちらもライブラリが正しい。ただし理由が 2 つある

```
3 本  BulletmlDTDViolationException: repeat element cannot have multiple elements of (Action|ActionRef).
      [OtakuTwo]_self-0036.xml / _self-1010.xml / _self-1011.xml

1 本  XmlException: 'name' is an unexpected token. Expecting whitespace. Line 1, position 26.
      samples/FsBulletML.Sample.TypeProviders.Debug/2wayLeft.xml
```

**前者 3 本は DTD 違反。** `<!ELEMENT repeat (times, (action | actionRef))>` で 1 個だけと
定めているのに、`<repeat>` の中に `<actionRef>` を 2 つ書いている。弾くのが正しい。

**後者 1 本は XML として壊れている。**

```xml
<bulletml type="vertical"name="2way Left">
                        ^^ 空白が無い
```

`.NET の XmlReader` が拒否する。**同じ弾幕の他の 4 コピーは正常**（planner の調べ）。
この 1 本だけが壊れている。

同梱のサンプルのうち 4 本が読めない、という事実として記録する。
どちらもライブラリの側は正しく振る舞っている。

**この節はいちど間違えて書いた。** 控えには 2 種類のメッセージが出ていたのに、
上の 2 本だけ読んで「4 本とも DTD 違反」と書いた。planner の指摘で直した。
**一覧を作るときは、断定を確かめる癖が働かない。**

### 撃たなかった 6 本 —— 欠陥ではない

`move.xml` は `<fire>` を 1 つも持たない敵の移動パターン。
残り 5 本は `<fire>` を持つが 60 フレーム内に撃たない（長い `wait` か `$rank` 待ち）。

**10 フレームだと 23 本が「撃たなかった」に入った。** 60 に伸ばすと 6 本まで減ったので、
17 本は遅いだけだった。**窓の長さで数が変わる**ので、この数は単独では読めない。

### 分母を 1 回間違えた

最初この網はファイルシステムを歩いていて、`bin/Debug` へ複写されたものまで数えていた。

```
samples の下の xml   1399 個
git が追跡している    906 個
```

**差の 493 個はほとんどビルドの複写。** 焼いた直後と clean clone で分母が動く。
`bin` / `obj` / `Library` / `Temp` を外して、`git ls-files` の 906（一意 227）と一致させた。

なお planner は「未追跡の xml は 0 本」と報告している。**どちらも正しく、定義が違う。**
`git status` の `??` は 0（`.gitignore` で無視されているため）で、
`find` と `git ls-files` の差は 493。

---

## 11. `ref` の `param` に入れた値は、展開のときに数へ潰される

planner から「`existRandomParam` が `$rank` を見ていない（未測定）」と渡された件。
**測った。割れた。** そして原因は `$rank` を見ていないことではなかった。

### 測ったもの

走行の途中で manager の値を差し替えて、撃った弾の速さが追随するかを見る。
`Trace.runWith` にフレームごとのフックを足した（**動かさないと、毎回読み直しているのと
最初の 1 回を持ち回っているのが同じ控えになる**）。フレーム 4 で `0.2` から `0.7` へ。

★**この表はぜんぶ「`top` がひと回りをまたいだとき」の話**。
下の [ひと回りの中](#rand-も守られているのはひと回りに-1-回まで) は別の時計で、
**同じ `$rand` が追随したり凍ったりする。並べると片方が嘘に見えるので、時計を先に書く**
（planner の指摘）。

| 書き方 | ひと回りをまたぐと |
|---|---|
| `<speed>1+$rank*10</speed>` を直に書く | ○ 3.0 → 8.0 |
| `<speed>1+$rand*10</speed>` を直に書く | ○ 3.0 → 8.0 |
| `actionRef` を挟むが `param` 無し。先に `$rank` | ○ 3.0 → 8.0 |
| `<param>$rank</param>` 経由 | ✗ **0.2 のまま**（またいでも凍る） |
| `<param>1+$rank*10</param>` 経由 | ✗ **3.0 のまま**（またいでも凍る） |
| `<param>$rand</param>` 経由 | ○ 0.2 → 0.7（**またいだときだけ**） |

上の 3 行が校正点。**`actionRef` を挟むこと自体は凍らせない。**
凍らせているのは `param` の置き換えのほうで、`$rand` だけが例外的に助かっている。

### 原因は `IntermediateParser.fs:959`

```fsharp
let mapEval expr = List.map (fun x -> (Processable.getValue x).ToString("F10")) expr
```

`param` は子へ渡す前に **`getValue` で数へ潰される**。`$rank` は
そこで `"0.2000000000"` になり、以後ただの数字なので二度と読み直されない。

`$rand` が助かるのは、`BulletRunner.fs:409` の `existRandomParam` が
`$rand` を含む `ref` を見つけると `Original` に生の XML を持たせ、
`BulletmlTask.Init()` が**毎周まるごと展開し直す**ため（`Processable.fs:272`）。
機構そのものを直に測った控えが `freeze-original-flag`。

```
<param>3</param>         Original = None（持ち回る）
<param>$rand</param>     Original = Some（作り直す）
<param>$rank</param>     Original = None（持ち回る）
<param>1+$rand*2</param> Original = Some（作り直す）
<param>1+$rank*2</param> Original = None（持ち回る）
```

`judge` が見ているのは `x.Contains("$rand")` の 1 行だけ（`IntermediateParser.fs:842`）。

### `$rand` も、守られているのは「ひと回りに 1 回」まで

作り直しが起きるのは `task.Init()`、つまり `top` がひと回りしたとき。
**ひと回りの中で何発撃っても、展開は 1 回**。

```xml
<repeat><times>3</times>
  <action><actionRef label="shoot"><param>$rand</param></actionRef></action>
</repeat>
```

毎フレーム `$rand` を動かしながら走らせて、**3 発とも `s=0.200`**（`freeze-rand-within-loop`）。
`param` で `$rand` を撒く「ばらけた弾」は、ひと回りの中では**全部同じ値**になる。

### 直すときに

`mapEval` を外して `param` を**文字のまま**渡せば、`$rand` も `$rank` も
`getValue` が読む位置まで生き残る（`Param.replace` はもともと文字の置き換え）。
そうすると `existRandomParam` と `Original` の仕組みは要らなくなる。

ただし **`param` に入れた式を評価する時点が変わる**ので、既存の弾幕の見た目は動く。
`$1` を何度も使う `action` では、いまは 1 回ぶんの値が共有されているが、
文字のまま渡すと使うたびに転がることになる。**どちらが正しいかは仕様の話。**

`mapEval` の `ToString("F10")` は [6](#6-小数点がカンマのカルチャでは式の評価が落ちる) の
出どころの 1 つでもある。**同じ 1 行に 2 つの不具合が載っている。**

控え: `freeze-direct-rank` / `freeze-direct-rand` / `freeze-ref-noparam-rank` /
`freeze-param-rank` / `freeze-param-rank-expr` / `freeze-param-rand` /
`freeze-rand-within-loop` / `freeze-original-flag`（`RefParamFreeze.fs`）

---

## 12. 同じ `label` が 2 つあると、文書順で先にあるほうが走る

固めた場所: `Refs.fs` / 控え `now-duplicate-label-first-wins`

`tryFindAction` / `tryFindFire` / `tryFindBullet` は 3 つとも `List.tryFind`
（`IntermediateParser.fs:737` / `:781` / `:825`）なので、最初に見つかったものを返す。
DTD は `label` の一意性を要求していないので、**2 つ書いても不正ではない。**

planner が測る前に予測を凍結し、**当たった**。

```
<action label="dup"> を 2 つ置いて <actionRef label="dup"/> で呼ぶ
  予測  文書順で先にあるほうが走る
  実測  d=0.000 s=1.000   ← 先にあるほう
```

★**これは「いまはこうなる」の控えで、「こうあるべき」ではない。**
同梱の 227 本には実例が 0 本なので、**回帰を捕まえる網としては 0 点**（当てる先が無い）。
置いた理由は別で、**リファクタリングで解決の順序が変わっても誰も気づかないから**。
控えの名前に `now-` を付けてある。

---

## 13. `name` / `description` は往復で消える。ただし経路で割れる

固めた場所: `RoundTrip.fs` / 控え `now-roundtrip-bulletml-attrs`

planner が測る前に予測を凍結し、**当たった**。ただし**測ったら経路で割れて、
「XML の往復が非可逆」ではなく「DTD の型を通る往復だけが非可逆」だった。**

```
入力  <bulletml xmlns="..." type="vertical" name="No Name"><bullet /></bulletml>

  DTD 経由（bml.ToXmlStringForTest）   name が消える   ★ちがう
  XmlNode 経由（xml.ToXmlString）      name が残る     一致
```

`description` も同じ。両方いっぺんに書いても両方消える。

### 落としているのは writer ではない。**parser が緩い**

```
DTD.fs:140-141 のコメント     <!ATTLIST bulletml xmlns CDATA #IMPLIED>
                              <!ATTLIST bulletml type (none|vertical|horizontal) "none">
                              ★name も description も宣言が無い

IntermediateParser.fs:198-199 tryFindAttrValue attrs "name" / "description"   ★読む
DTD.fs:119                    BulletmlAttrs に bulletmlName / bulletmlDescription   ★持つ
DTD.fs:546 / :551             .Name / .Description で外へ出す                       ★出す
DTD.fs の writer              xmlns と type だけ書く                                ★書かない
```

**writer は DTD どおり。** DTD に無い属性を parser が拾って型に載せ、
API で外へ出しているので、**往復すると読めたものが消える。**
「writer が落としている」ではなく「parser が DTD より緩い」と読むほうが、
直す向きが決まる（planner の言い直し）。

### 既存の網は本物。**この壊れ方が網の通る経路に無かっただけ**

`Parser.Tests` の `文字列からのパース` は、入力の文字列と `ToXmlStringForTest()` の
**完全一致**を見る。かなり強い網。

```fsharp
source |> should equal (bml.ToXmlStringForTest())
source |> should equal (xml.ToXmlString())
```

**ただし TestCase 2 本が持つ属性は `xmlns` と `type` だけ**で、
`name` / `description` を持つ入力が 1 本も無い。

★これは、この文書の
[「壊しても赤くならない」の 3 通り](#この網が本当に守るのかを壊して確かめた)の **3 番**
（網は生きているが、その壊れ方が網の通る経路にない）。**1 番ではない。**

### 直すときに

`XmlParse.fs` の `TestCase` に `name` を持つ 1 本を足せば、**そのまま赤になる**。
控えではなく**普通の回帰の網**として置ける唯一の件なので、
直す側とセットで足すのが良い（いま足すと緑の枝に赤が残る）。

どちらへ寄せるかは仕様の話で、ぜくる待ちの札。
**2 つの直し方で、外に見える影響が違う**（planner）。★測ったら、**片方は同梱のサンプルを壊す。**

```
parser に合わせる   writer が書く。DTD に無い属性を書くことになるが、外への影響は無い
writer に合わせる   parser が読むのをやめる。★.Name / .Description が常に None になる
```

### `.Name` は使われている。`.Description` は使われていない

planner は `.Name` を「語がありふれていて網が張れないので未測定」としていたが、
**`bulletml.Name` / `bulletmlInfo.Name` / `bullet.Name` に絞れば測れる。**

```
DTD.fs:543-547    member this.Name          ← 定義
BulletmlInfo.fs:8 { Name = match bulletml.Name with | Some x -> x | None -> "" }
                                            ★Core 自身が通す

samples/…MonoGame.CSharp/FsBulletMLSampleGame.cs:70 :108   this.BulletName = bullet.Name
samples/…MonoGame.FSharp/FsBulletMLSampleGame.fs:60        (bullet.Name, bullet)
samples/…Unity2D.CSharp/Assets/Scripts/Enemy.cs:50         bullets[i].Name
samples/…Unity2D.FSharp/…/Enemy.fs:114                     this.BulletName <- bulletmlInfo.Name
```

★**同梱のサンプル 4 本とも、弾幕の名前を画面に出すのに使っている。**
`parser が読むのをやめる` を選ぶと、**4 本とも名前が空になる。**

```
DTD.fs:548-552   member this.Description   ← ★定義 1 件のみ。呼び出し 0 件
```

`.Description` のほうは planner の読みどおり 0 件。
**同じ 2 つの属性でも、外への影響は同じではない。**

`.Name` の他のヒットは .NET の `Type.Name` / `XmlReader.Name` で別物
（`TypeProviders` に 196 件、`Xml.fs` に 4 件）。**素の `.Name` で数えると 200 件超に見えるが、
`bulletml` / `bullet` / `bulletmlInfo` / `bulletMove` / `bullets[i]` を前に付けると 8 件**
（うち定義側の `BulletmlInfo.fs:8` と `Processable.fs:267` を除くと、**サンプル側が 6 件**）。

★**「語がありふれていて網が張れない」は、前に 1 語 付ければ張れる**ことがある。
測れないと決める前に、**その識別子を持っている型の名前で絞る**。

どちらも公開 API なので、**リポジトリの外の利用者は測れない。**

---

## 控えを置いていない読み（当てる先が 0 件のもの）

planner が読んで挙げたもの。**こちらで開いて確かめた結果だけを書く。**
どれも控えを置いていない。当てる先が無いか、`Parser.Tests` の領分。

### `<vanish>` は DTD が中身を許すが、実装は持てない

```
DTD.fs:163   <!ELEMENT vanish (#PCDATA)>   ← 中身を許す
DTD.fs:164   | Vanish                      ← 中身を持てない DU
DTD.fs:447   writer.WriteStartElement("vanish"); writer.WriteEndElement()
```

**`<vanish>x</vanish>` を読んで書き戻すと `<vanish />` になる。**
BulletML の `vanish` に意味のある中身は無いので実害は見当たらないが、
往復は非可逆。DU が payload を持たない以上、**コードから確実に言える**（測ってはいない）。

### `Core` 側の DTD は読み込みで効いていない

```
Core/Xml.fs:48-49   DtdProcessing.Ignore + XmlPreloadedResolver
Core/Xml.fs:47      ソースのコメントが「DOCTYPE is skipped」と自分で言っている
```

**ただし `Parser` 側は別。** `Parser/Xml.fs:19-20` は `ValidationType.DTD` を持ち、
`dtdProcessing` を引数で受ける（`:133` ほかで既定が `Ignore`）。
**呼ぶ側が `Parse` を渡せば検証は効く。** 「効いていない」は Core の既定経路の話。

### ★`ToXmlString` の呼び出しが 0 件、は外れ

planner の読みは「唯一の候補が `DTD.fs:134` のコメントアウトだけ」だったが、
**こちらで grep したら呼ばれている。**

```
Parser.fs:110 / :114 / :119 / :124   recBulletml.ToXmlString(...)
tests/FsBulletML.Parser.Tests/XmlParse.fs:15-16 :33-34 :46-47   既存 346 件が当てている
```

`DTD.fs:134` はデバッガ表示のコメントで、そこだけを見ると 0 件に見える。
**当てる範囲を `src` 全部と `tests` に広げると出る。**

★**その後、既存 346 件が何を当てているかを読んで、往復も測った。**
結果は [13](#13-name--description-は往復で消えるただし経路で割れる) に移した。
この節は「当てる範囲が足りずに 0 件と読んだ」という経緯の記録として残す。

---

## この網が本当に守るのかを、壊して確かめた

控えが 58 本そろったところで、**本体を実際に変えて何件赤くなるか**を測った。
1 本ずつの門は途中で確かめてきたが、網全体としては別の話なので。

変えたのは 1 文字。`BulletRunner.fs:177` の度→ラジアンの係数。

```fsharp
let revise = (float32 Math.PI) / 180.f
                                 ^^^   →  181.f
```

```
FsBulletML.Core.Tests     失敗 16 / 合格 42     ← この網が捕まえた
FsBulletML.Parser.Tests   失敗  0 / 合格 346    ← 既存のテストは 1 件も気づかない
```

**既存の 346 件は全部パース経路なので、走らせる側が変わっても緑のまま。**
埋めたかったのはこの穴そのものだった。

戻して両方緑に戻ることも確かめてある。

### [11](#11-ref-の-param-に入れた値は展開のときに数へ潰される) で網を広げたので、測り直した

```
控え 66 本  失敗 16 / 合格 50     捕まえた数は同じ 16
```

**足した 8 本はこの壊し方に反応しない。** どれも `direction type="absolute">0` を撃つので、
係数が何であれ 0 度は 0 ラジアンのまま。増えたのは合格のほうだけ。

### 測り直しのついでに、`revise` がもう 1 か所あるのに気づいた

最初にこの節を書いたとき「変えたのは 1 文字」としたが、**同じ式が
`BulletRunner.fs:177` と `:194` の 2 か所にある**。前回どちらを変えたかは記録が無い。
今回は 177 だけを変えて 16、**194 だけを変えると 0**（両方とも緑のまま）だった。

これは網の穴ではない。**194 の `revise` は誰も使っていない。**

```fsharp
| None ->
  let revise = (float32 Math.PI) / 180.f     // ← 束縛するだけ
  if bullet.BulletType = BulletType.Player then
    ... <- bullet.GetEnemyAimDir()           // ← revise を使わない
  else
    ... <- bullet.GetAimDir()                // ← こちらも使わない
```

`<direction>` を書かなかったときの枝で、向きは狙い撃ちの角度そのものになる。
度からラジアンへ直す相手が無いので、束縛が余っている。
**消しても挙動は変わらない**（F# は使われない `let` を警告しない）。

**ただし [3](#3-run-に絶対値を返す枝が-2-つあるどちらも届かなかった) とは直し方が違う**（planner の指摘）。
3 は届かない**枝**だが、これは**重複した束縛**で、`194` は `177` を shadow している。

```
177  fireCommand の頭。Some 枝の 5 か所（枝としては 4 つ）が使う
194  | None -> の中。177 を隠すが、この枝の代入 2 本はどちらも使わない
```

**値も同じなので、194 を消すと外側の 177 が見えるようになるだけで何も変わらない。**
枝ごと消す話ではなく、1 行消す話。

★**「壊しても赤くならない」には 3 通りある**（2 通りと書いていたのを planner が直した）。

```
1  網が届いていない                                 → 門を足す話
2  壊した先がもともと何もしていない                  → 消す話。★194 はこれ
3  壊した先は仕事をしているが、壊し方がその仕事を通らない
```

**3 は同じ日にこの節の上で自分が踏んでいる。** 足した 8 本が反応しなかったのは
「8 本が死んでいる」ではなく「`absolute 0` なので係数の経路を通らない」だった。
**2 と 3 は「その行が生きているか」で割れる**（194 は死んでいて、8 本は生きている）。

先に「網の穴だ」と読まずに、まずその行が誰に読まれているかを見ること。

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
- `term` を省いたときの DTD 違反（メッセージが条件と一致している）
- `repeat` の `times` に式（`7/2` は `3` に切り捨て。`times=0` でも止まらない）
- `action` の 5 段入れ子
- `fire` の `direction` / `speed` を省いたとき（`aim` と `speed 1` に落ちる。引き継がない）
- 同じ label が 2 つあるときは先に書いたほうが勝つ
- 存在しない label を参照すると `not found target Action element:<name>` で落ちる
- `BulletType.Player` の分岐（`aim` の相手が `GetEnemyAimDir` に変わる。弾が親の型を継ぐ）
- `top` で始まる名前の拾い方（`topmost` は拾い、`nottop` は拾わない）
- **兄弟の弾が可変状態を共有しない**（`StateIsolation.fs`）
  - 同じ `bulletRef` から撃った 3 発が、それぞれ自分の `term` を持って別々に加速する
  - 片方を `vanish` してももう片方は飛び続ける
  - `cloneProcessable` が効いている。リファクタリングで浅くすると静かに壊れる
- ひと回りしたあとも `sequence` の累積は続く（`task.Init()` で action は作り直されるが
  `FireData.SrcDir` は残る）。回り続ける扇はこれが要るので、たぶん意図どおり
