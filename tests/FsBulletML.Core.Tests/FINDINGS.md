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

### 落ちた 4 本 —— これはライブラリが正しい

```
BulletmlDTDViolationException: repeat element cannot have multiple elements of (Action|ActionRef).
```

DTD は `<!ELEMENT repeat (times, (action | actionRef))>` で 1 個だけと定めている。
落ちた 4 本は `<repeat>` の中に `<actionRef>` を 2 つ書いている。**サンプルのほうが DTD 違反。**
弾いているライブラリが正しく、メッセージも条件と合っている。

同梱のサンプルのうち 4 本が読めない BulletML、という事実として記録する。

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
