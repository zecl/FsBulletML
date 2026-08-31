# 走らせる側を測って出てきたもの

# 参考
BulletML公式ドキュメント
https://www.asahi-net.or.jp/~cs8k-cyu/bulletml/index.html

引いた先（2026-08-31）:
`bulletml_ref.html`（日本語のリファレンス）/ `bulletml_ref_e.html`（英語）/
`bulletml_applet.html`（デモの使い方。仕様は書いていない）

## 目次

| | 何が | 直すと挙動が変わるか |
|---|---|---|
| [1](#1-bullet-の中に書いた-direction-が効かない) | `bullet` の中の `direction` が効かず `aim` に落ちる。`speed` は値は勝つが `type` を見ていない | **直した**（`direction` は `bullet` が勝つ。`speed` の `type` は 3 か所で見るように） |
| [2](#2-wait-の最初の-1-周だけ-1-フレーム短い直した) | `wait` の最初の 1 周だけ 1 フレーム短い | **直した**（きっかり `w` フレーム。控え 17 本が動く） |
| [3](#3-run-に絶対値を返す枝が-2-つあるどちらも届かなかった) | 絶対値を返す枝が 2 つ。どちらも届かない | **直した**（届いても壊れない形に。挙動は変わらない） |
| [4](#4-未解決の-n-が-0-ではなく-n-になる) | 未解決の `$N` が `0` ではなく `N` になる | **直した**（正規表現のタイポ） |
| [5](#5-bulletml-type-は走らせる側に効かない) | `<bulletml type>` が効かない。既定値が 3 層で違う | 繋ぐなら変わる（例外メッセージだけ**直した**） |
| [6](#6-小数点がカンマのカルチャでは式の評価が落ちる直した) | `,` が小数点のカルチャで BulletML が 1 つも走らない | **直した。4 カルチャとも同じ値** |
| [7](#7-fire-の-speed-typerelative-が親の速さを見ていない) | `fire` の `speed type="relative"` が親の速さを見ない | **直した**（仕様どおり親を基準に） |
| [8](#8-相互参照でスタックが溢れプロセスごと落ちる段階-1-を直した) | 参照が輪になると `StackOverflow` でプロセス死 | 直した。**輪 8 個ぜんぶ解けた**（7 本とも動く） |
| [9](#9-top-が複数あっても先頭に-wait-があると後ろが-1-度も走らない) | 複数の `top*` のうち先頭しか走らない | **直した**（文書順に全部 走る） |
| [10](#10-同梱のサンプルを全部走らせた結果) | 同梱サンプル 227 本を全部走らせた結果 | — |
| [11](#11-ref-の-param-に入れた値は展開のときに数へ潰される) | `ref` の `param` が展開のとき数へ潰され、`$rank` が凍る | **直した**（文字のまま渡す。38 本が動く） |
| [12](#12-同じ-label-が-2-つあると文書順で先にあるほうが走る) | 同じ `label` が 2 つあると先勝ち（`samples/` に実例 0 本） | 実例が無いので変わらない |
| [13](#13-name--description-は往復で消えるただし経路で割れる) | `name` / `description` が DTD 経由の往復で消える | **直した**（writer が両方 書く。4 条件とも往復する） |

控えを置いていない読みは [こちら](#控えを置いていない読み当てる先が-0-件のもの)。

**8 は zecl から「当時の妥協。今回の改修で直せたら直したい」**（2026-08-31）。

正しく動いていたものは末尾の [仕様と突き合わせて合っていたもの](#仕様と突き合わせて合っていたもの) に。


リファクタリング前に挙動を固める作業（枝 `feature/core-tests`）で出た調査結果。
**1・2・3・4・6・7・8・9・11・13 と、番号の付いていない
[新しい札](#新しい札--bullet-直下の-speed-の-type-も見ていなかった)以外は 1 つも直していない**
（5 は例外メッセージだけ。届かない枝なので挙動は変わらない）。控え（`tests/TestData/trace/`）は
「正しい姿」ではなく「いまの姿」の記録なので、リファクタリングで控えが動いたら、
直ったのか別の壊れ方をしたのかは人が決めること。

測ったのは `dba9d27`（`master`、2026-08-31 時点）。

---

## いまの状態と、次の一手（2026-08-31 時点）

```
枝      feature/core-tests   ★手元のみ。origin には push していない
網      Core 89 件 ＋ 既存 Parser 346 件 すべて緑。控え 89 本
本体    1・2・3・4・6・7・8・9・11・13 ＋ 新しい札（bullet 直下の speed の type）を直した
        （IntermediateParser / Processable / BulletRunner / Util / DTD）
        それ以外は 1 行も触っていない
        5 は例外メッセージだけ直した（届かない枝。挙動は変わらない）
        13 は writer が name と description を書くようにした
判断待ち 5 の本体（フロントへ繋ぐ。範囲は配線 ＋ サンプルで 1 例）
        12（実例 0 本なので直さない）
```

[8](#8-相互参照でスタックが溢れプロセスごと落ちる段階-1-を直した) は段階 1・2・3 を直した（2026-08-31、zecl の判断）。
参照が輪でもプロセスが落ちなくなり、`Corpus.fs` の `known再帰` は空にした。

```
              8 の直前  8 の直後
撃った        210 本    217 本
撃たなかった    6 本      6 本
落ちた          4 本      4 本   ← 同じ 4 本。輪と無関係
避けた          7 本      0 本
```

★**この表は 8 の記録**なので、そのあと何を直しても書き換えない。
**いまの数は [10 の節](#10-同梱のサンプルを全部走らせた結果)**（9 を直したあと 218 / 5 になった）。

**輪 8 個ぜんぶ解けた**（`bullet self` 6 / `bullet mutual` 1 / `action self` 1）。
輪を持つ 7 本とも動く。

★**解いていない形が 2 つ残る。どちらもコーパスに実例 0 本。**

1 つめは **2 段以上の `action` の輪**（`top → b → top`）。
解いた結果に別の `action` が挟まり、差し替え先がフレームごとに内側へ移るので、
1 段ずつ解くと呼び出しが深くなる。**展開時に落として例外にしてある。**
2 つめは **種別をまたいで同じ `action` へ戻る輪**（`action:a → bullet:b → action:a`）。
`bullet` を経由すると `fire` が段を切るので**たぶん解ける**が、測っていないので落としている。

どちらも上の数には影響しない
（[段階 3 の節](#線引き--自己参照だけ解く) に測った結果がある。
`samples` 925 本の走査は planner が別の軸でやっていて、
`action` の多段 0 本 / 種別をまたぐ輪 0 本を確認している）。

★**取り消し**。この節には
「`action` の輪は実行時に新しいタスクができないので 1 段ずつ解いても進まない」
と書いてあった。**測ったら間違いだった。** 走らせる並びを差し替える形にすれば進む。
元は engineer の未測定の読みで、[8 の段階 3](#8-の段階-3--action-の輪も解いた輪-8-個ぜんぶ) に測った結果がある。
★**取り消したはずの読みが、この節にだけ出どころ無しで生き残っていた** ——
段階 2 の節では出どころを書いて留保したのに、こちらは断定のまま残った。

次の一手は、判断待ちの札から選ぶ。

**[13](#13-name--description-は往復で消えるただし経路で割れる) だけは、直す向きで外への影響が違う。**

```
parser に合わせる（writer が書く）      外への影響 ★無し
writer に合わせる（parser が読まない）  ★同梱サンプル 4 本の弾幕名が空になる
```

残り 9 件は「直すか / 別 PR か / 控えのまま残すか」の判断待ち。
目次の 3 列目（直すと挙動が変わるか）が、そのまま重さの目安になる。

道具は `atelier-shared/` に置いた（この repo には入れていない）。
★**置き場は一時ディレクトリなので、いつか消える。**
**この 2 行のほうが本体**で、消えていたら説明から書き直せばいい
（全 68 行 / 77 行、コメントと空行を除くと 47 行 / 41 行。
**この文書は残るが、指している先は残らない**）。

```
duplabel.mjs   1 つの BulletML の中の重複ラベルを、種別つき・入れ子か兄弟かで数える
anchors.mjs    md の文書内リンクが、その文書の見出しに届くかを見る
```

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

★★**この段落の「`fire` が勝つ」は出どころが無い。** 2026-08-31 に公式リファレンスを
日英 2 版 当てたが、**優先順位はどちらにも書かれていない。**
`<bullet>` は "Defines the direction, the speed and the action of bullet."、
`<fire>` は "Fires a bullet to `<direction>` degrees at `<speed>`." で、
**両方に書いたときの規則は無い。** → 下の節で実物から決めた。

---

## 1 を直した —— `speed` と同じ形に揃えた（2026-08-31）

### 不具合が 2 つ 重なっていた

```
1  createTask が読んだ direction を、fireCommand が ★無条件で上書きしていた
2  createTask 側の direction が ★度からラジアンへ直していなかった
```

1 だけ直したら `d=23.717` が出た（期待は 30 度 = `0.524`）。
**2 は「届いていなかったので誰も気づかなかった」欠陥。**
`fireCommand` の 4 分岐は全部 `* revise` している。**`createTask` だけ落ちていた。**

★**1 つ直して初めて 2 つめが見える。** 到達しない枝は、中身も検算されていない。

### 向きは実物から決めた —— `speed` に揃える

仕様が優先順位を決めていないので、**同じ実装の `speed` がどうしているか**で決めた。

```fsharp
// fireCommand。createTask のあとで bullet の speed を読み直している
match bulletElm with
| ProcessableBulletml.Bullet(attr,_,speed,_) ->
  match speed with
  | Some (Speed(attr,s)) -> newBullet.Speed <- getValue s     ★bullet が勝つ
```

**`speed` は既に「`bullet` に書いてあればそちらが勝つ」。** `direction` だけ違っていた。
**同じ `fire`/`bullet` の対で、片方だけ規則が違うほうが不自然。**

★★**根拠に引いたこの `speed` 自身も、それ自体 不完全だった。**
上の `Some (Speed(attr,s))` は **`attr` を束縛して 1 度も読んでいない** ——
つまり**どちらが勝つかは正しく、`type`（`absolute` / `relative` / `sequence`）を
見ていなかった。** 同じ節の[新しい札](#新しい札--bullet-直下の-speed-の-type-も見ていなかった)で直した。
★**「隣を あるべき姿の根拠に引く前に、その隣を検算する」** ——
ここは引いたあとに気づいた。**引用は直す前の姿のまま残してある**（根拠の記録なので）。

★**当てる先を数えた。**

```
一意 227 本 / fire 要素 1513 個 のうち
  fire と bullet の両方に direction   ★11 個  ← 優先順位が効く（bullet が勝つ側へ）
  fire に無く bullet にだけ direction ★12 個  ← 効かなかったものが効くようになる
  fire と bullet の両方に speed       ★24 個  ← ここは前から bullet が勝っている
```

### 測り直した結果

```
                              直す前      直した後
inline literal (bullet 内)   d=2.850  →  ★d=0.524
ref literal    (bullet 内)   d=2.850  →  ★d=0.524
ref param      (bullet 内)   d=2.850  →  ★d=0.524
fire 側        (fire 内)     d=0.524     d=0.524
```

★**控えの但し書き「30 度 = 0.524」に 4 条件とも揃った。**
**`bullet` に書いても `fire` に書いても同じ向きになる**（非対称が消えた）。

```
corpus-smoke   撃った 218 / 撃たなかった 5 / 落ちた 4 / 避けた 0   変わらない
```

★**ただしこの行は波及の根拠にならない**（corpus-smoke は向きを見ていない）。
落ちた控えは `bullet-direction-where` と `bullet-ref-params` の 2 本。

---

## 新しい札 —— `bullet` 直下の `speed` の `type` も見ていなかった

固めた場所: `FireShapes.fs` / 控え `bullet-speed-type.txt`

見つけたのは planner。**1 を直すときに「`speed` は既に正しい」と引いた、その `speed` 自身。**

### 現物 —— ★3 か所に散っていた

```
createTask         speed の attrs を束縛して ★1 度も読んでいない
fireCommand 前半   createTask の直後に、同じく type を無視して代入し直している
fireCommand 末尾   最後に SrcSpeed で ★全部 上書きしている
```

★**1 か所 直しても効かない。3 か所とも直して初めて動いた。**
1 で `direction` を直したときと同じ形（**上書きが後ろに在る**）が、`speed` 側にもあった。

### 直した結果

```
親 speed=3   bullet speed absolute 1  ->  s=1.000
親 speed=3   bullet speed relative 1  ->  ★s=4.000（親の 3 + 1）
親 speed=3   bullet speed sequence 1  ->  s=1.000
```

### 当てる先 —— ★planner と数が食い違い、両方 網が漏れていた

```
planner   relative ★4 個 / absolute 8 個 / ★3 本
engineer  relative ★1 個 / absolute 3 個 / ★1 本（air_elemental のみ）
```

★**現物で当てた。** planner が挙げた `kunekune_plus_homing` はこう。

```xml
<bullet label="...hmgLsr">
    <speed type="absolute">2</speed>          ← bullet 直下。ここは数える
    <action>
        <changeSpeed>
            <speed type="absolute">0.3</speed>  ← ★changeSpeed の中。数えない
```

**`changeSpeed` の中は `createTask` を通らない**
（[7](#7-fire-の-speed-typerelative-が親の速さを見ていない) で「外に在る 7 本だけ」と切ったのと同じ線）。

★★**engineer は 1 回 誤爆している。** 最初 `<fire>` の中を落とし忘れて
**`sequence` 18 個**が出た。`ellipse_bomb` の現物を開いて気づいた。
★**当時の網を復元して当て直したら、18 がそのまま再現した**（記録は正しかった）。

### ★★★ この食い違いの説明を、engineer が 2 回 続けて間違えた

**数の食い違い（4 対 1）そのものは実在した。**
**間違えたのは、毎回「なぜ割れたか」の説明のほう。** 2 回とも、直後に検算して出た。

**1 回目。**「`bullet` 直下は `absolute` 1 個だけで、
`relative` は `changeSpeed` の中」と便にも文書にも書いた。

```
grep 'type="relative"' [Bulletsmorph]_kunekune_plus_homing.xml   →  ★0 件
```

★**あのファイルに `relative` は 1 個も無い。**
引いた現物は正しく、**その行が説明するのは `absolute` の側**だった。

**2 回目。**「planner の網が `changeSpeed` を落としていなかった」と書いた。
★**planner の網は `changeSpeed` を落としている**（本人が現物を出した）。

★★★**本当の原因は、網の漏れではなく ★数え方が 3 通り混ざっていたこと。**

```
planner の初報   relative 4 個 / absolute 8 個 / 3 本

  「個」   ★複製を潰していない（同じ弾幕が sample 4 か所に居る。1 個 × 4 / 2 個 × 4）
  「本」   ★型をまたいだ合計（relative 1 本 ＋ absolute 2 本）
           しかもファイル名で潰した数
```

★**1 つの表の中に、潰し方の違う 3 つの数が並んでいた。**
**engineer は「4 と 1 が割れている」だけを見て、網の漏れを探しに行った。**
★★**潰し方が違うだけなら、網はどちらも正しい。**

### 数え直し —— 3 つの網が一致した

★**engineer は道具を 2 本 書いていて、その 2 本も食い違っていた。**

```
旧（当時）  一意 227 本   relative 1 / absolute ★3
新（訂正時）一意 ★245 本  relative 1 / absolute ★2
```

★**旧に偽陽性が 1 個**。`<bullet\b[^>]*>` が**自己閉じの `<bullet/>` から始まり、
遠くの `</bullet>` まで飲み込んで**、途中の兄弟 `<fire label="white2">` の
`speed` を拾っていた（`[XII_STAG]_3b.xml`）。
★**新に分母の間違い**。Unity の `Library` / `Temp` を落とし忘れて 227 → 245 になっていた
（数そのものは変わらなかったが、**`corpus-smoke` の分母と揃っていない数を公表していた**）。

両方 直して当て直し、planner の網とも突き合わせた。

```
              engineer      planner
relative      1 個 / 1 本    1 個      [Original]_air_elemental.xml
absolute      2 個 / 2 本    2 個      kunekune_plus_homing（indent 版と 2 つ。中身は同じ弾幕）
sequence      0 個           0 個
type 無し   158 個          158 個
一意        ★227 本                    ← corpus-smoke と同じ分母
```

★**直しの当てる先は `relative` の 1 本。3 つの網すべてで一致し、最初から動いていない。**

★★**教訓は「網が漏れていた」ではない** ——
**数が割れたとき、engineer は 2 回とも「網の漏れ」を探しに行った。**
**実際は 1 回目が潰し方の違い、2 回目が自分の道具の偽陽性。**
★★★**「数が割れた ＝ どちらかの網が漏れている」が、そもそも間違った当たり方だった。**
**割れ方には、網の漏れ・潰し方の違い・分母の違い・偽陽性の 4 通りがある。**

### ★★★ 対になるもの —— 一致したときは、誰も調べない

planner の指摘。**この文書には既に、逆側の実例が載っている。**

```
★割れたとき    割れ方は 4 通り。1 通り目（網の漏れ）だけを探さない
★一致したとき  一致は「同じものを見た」の証拠にならない。★網の形を突き合わせる
```

★**割れたときは調べる。一致したときは調べない。そこが非対称。**

逆側の実例は [9 の 23 本](#-9-の当てる先を数えた--複数の-top-を持つのは-23-本)。
**二人が別の道具で数えて 23 本で一致したが、両方の網に穴が在った。**

```
planner の網   XML パーサ（引用符は無事） ／ ★木の形が違った（直下だけ数えた）
engineer の網  正規表現 ／ ★単一引用符で 11 本 落ちた ／ 木の形は合っていた
```

★★**同じ穴で一致したのではなく、違う穴で一致した。**
23 が動かなかったのは、**engineer が漏らした 11 本が全部「1 本だけ」側で、
planner が数え落とした入れ子の `top*` が 0 本だった**から。**偶然が 2 つ重なった。**

★**この節の 4 つの一致も、3 つの網を全部 直したあとなので本物**だが、
**直す前に「一致した」で止めていたら偽物だった。**

### ★★ この偽陽性の形は使い回せる

```
<bullet\b[^>]*> は ★自己閉じの <bullet/> にも当たる
そこから「対応する </bullet> まで」を切ると、★区間が遠くまで伸びる
伸びた区間の中では <fire>...</fire> の対応が取れないので、strip が効かない
```

★**開きタグを正規表現で拾うと、自己閉じタグも開きタグに見える。**
★★**しかも症状は「多く出る」なので、網が広い＝丁寧、に見える。**
**漏れは 0 件で気づけるが、飲み込みは数が増えるだけなので気づきにくい。**

### ★★★ 「2 点で目盛りを合わせた」の限界

engineer は当時、`air_elemental` の `relative` と
`kunekune` の `absolute` の 2 点で目盛りを合わせてから当てた。
**それでも偽陽性が残った。**

★**「2 点で合わせた」は、その 2 点が通ったという意味しかない。**
★★**校正点を「答えを知っているもの」から選ぶ以上、
自分が想定していない形（ここでは `<bullet/>` の自己閉じ）は
★原理的に校正点に入らない。** **2 点でも 4 点でも同じ。**

★★★**今回 効いたのは校正ではなく、道具を 2 本 書いて突き合わせたほう。**
**校正は「道具が動くか」を見る。突き合わせは「道具が何を見ているか」を割る。**

### ★★ 同じ罠を 3 回 踏んだ

**直したのに控えが `1.000 1.000 1.000` のまま**で、
★**「まだ効いていない」と 3 回 読んで、3 回 別の場所を直した。**

★**原因は実装ではなくテストの形** —— **`root` から撃っていたので親の速さが 0。**
`0 + 1` と `1` が同じ値になる。**弾の中から撃つ形に変えたら 1 回で出た。**

★★★**7 で planner に「残り 5 本は親の速さが 0」と言われて直した、
まさにその形を、自分でもう一度 踏んだ。**
**`relative` を測るときは、基準が 0 でないことを先に確かめる。**

---

## 2. `wait` の最初の 1 周だけ 1 フレーム短い（★直した）

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

★(a) は下の節で直した。**残るのは (b) だけ**になり、上の表は 1 行に畳まれた。

---

## 2 を直した —— `wait` の初期化の入口が 2 つあった（2026-08-31）

### 現物

**同じ 1 つのフィールド `pw.term` に、初期化の入口が 2 つあって値が違った。**

```fsharp
// IntermediateParser.fs:1103  木を作るとき（1 周目が読む）
{ initTerm = s; term = Processable.getValue s; finish = false }

// Processable.fs:231  Init()（2 周目以降が読む）
pw.term <- getValue pw.initTerm + 1.f     // ★こちらにだけ + 1
```

`repeat` は 1 周ごとに子を `Init()` するので、**1 周目だけ生成側の値、
2 周目からは Init 側の値**を読む。それが「最初の 1 周だけ短い」の正体。

### 直す向きは、仕様ではなく実装の中の対称性で決めた

[公式ドキュメントとの突き合わせ](#2479-を公式ドキュメントと突き合わせた2026-08-31直していない)で
**「仕様は 1 フレームをどこで数えるかを決めていない」**と書いた。字面からは降ろせない。

決め手は**同じ `runCommand` の中の兄弟 3 つ**。`term` を持つ命令は 4 つあり、
`wait` 以外の 3 つは**入口が 1 つで、`+1` を付けていない**。

```
accel            pa.term <- getValue pa.initTerm     +1 なし   きっかり term フレーム占める
changeDirection  pd.term <- getValue pd.initTerm     +1 なし   きっかり term フレーム占める
changeSpeed      ps.term <- getValue ps.initTerm     +1 なし   きっかり term フレーム占める
wait  生成       term = getValue s                   +1 なし   ★兄弟と揃っている
wait  Init       term = getValue initTerm + 1.f      ★ここだけ違う
```

**`changeDirection term=2` は 2 フレームで 30 度を配り切る**（`changeDir = 差 / term`
を `term` 回 足す）。**`term` が占めるフレーム数の意味は、この 3 つが既に決めている。**

もう 1 つ。**`+1` 側では `wait 0` が 1 フレーム食う。** 生成側では食わない。

★**そろえる先は生成側**。`Processable.fs:231` の `+ 1.f` を消した。

### 測った結果 —— 2 つ出ていた規則が 1 つに畳まれた

```
                     直す前              いま
wait=1 repeat=on   gaps=[1; 2; 2]  →  gaps=[1; 1; 1]
wait=2 repeat=on   gaps=[2; 3; 3]  →  gaps=[2; 2; 2]
wait=3 repeat=on   gaps=[3; 4; 4]  →  gaps=[3; 3; 3]
wait=4 repeat=on   gaps=[4; 5; 5]  →  gaps=[4; 4; 4]

wait=1 repeat=off  gaps=[2; 3; 3…] →  gaps=[2; 2; 2…]
wait=4 repeat=off  gaps=[5; 6; 6…] →  gaps=[5; 5; 5…]
```

| | 最初の間隔 | 以降 |
|---|---|---|
| `repeat` あり | `w` | `w` |
| `repeat` なし | `w+1` | `w+1` |

**(a) は消えた。残った `+1` は (b)** ——
`top` action は End で 1 周を閉じてから `Init()` して次のフレームで撃ち直すので、
`repeat`（同じフレームの中で次の周に入る）より毎周 1 フレーム多い。
**これは 2 の札ではない。** 直すなら別の判断が要る。

### ★★ 控えが動いた数が、凍らせた予測と食い違った

★**planner の予測（測る前に凍らせたもの）**:
「f 番号を持つ控え 35 本のうち **30 本以上**が動く」。

```
控え 86 本 のうち 動いた   ★17 本
  そのうち f 番号を持つ    ★12 本 / 35   ← 予測は 30 本以上
```

★**予測は大きく外れた。理由は「wait を持つか」ではなかった。**

**この不具合は 2 周目からしか出ない。** ★**逃げ道が 2 本ある。**

```
smoke-fire-wait   wait 3・repeat なし・窓 6 フレーム
                  撃つのは f00 と f04 の 2 発だけ。★2 周目の待ちは f04 で始まっている
                  （Init 側の値を読んでいる）が、★終わるのが窓の外（f08/f09）
                  → ★2 周目が窓に入らない

cycle-action-self-with-wait
                  wait 2・自己参照で回る形。1 周ごとに ★参照を解き直して並びを作る
                  → ★2 周目が Init() を通らない
```

★★**理由が別々**（planner の指摘）。**片方は「2 周目が在るのに窓に入らない」、
片方は「2 周目が Init を通らない」。**
★**engineer は最初これを「1 周しか回らないから」の 1 本にまとめて書いたが、
`smoke-fire-wait` は 2 周 回っている。窓が短いだけだった。**

★★★**軸は 2 本 ——「2 周目が窓に入るか」と「2 周目が Init を通るか」。**

★**「wait を持つ控えの数」で当てる先を数えると、大きく外れる。**
**数えるべきは、この 2 本をどちらも抜ける控えの数だった。**

★★**そして今日の他の 3 件（7 の 3 本 / air_elemental の 220 フレーム /
corpus-smoke の 60 フレーム）は ★全部 前者（窓）**。
**今日 4 回目の「窓が短い」。**
[9 の 23 本](#-9-の当てる先を数えた--複数の-top-を持つのは-23-本) のときと同じで、
**網が数えている対象と、不具合が当たる対象がずれている。**

### ★ corpus-smoke は 1 つも動かなかった（これも予測を外した）

```
撃った 218 / 撃たなかった 5 / 落ちた 4 / 避けた 0   ★直す前と同じ
```

engineer は「同じ 60 フレームの窓で余分な待ちが消えるので**撃った数が増える**」と
予測していた。**増えなかった。** corpus-smoke は**速さも向きもフレームも見ておらず、
「60 フレームの中で 1 発でも撃ったか」しか数えない。**
**撃たなかった 5 本の待ちは 60 フレームよりずっと長い**ので、
1 周あたり 1 フレーム縮んでも窓には入ってこない。

★**この門は 2 の当てる先ではない。** 弾の数が 4 倍 近くになった弾幕もある
（`real-speed-relative-7` の `kunekune` 246 → 366 発、
`real-unresolved-param` の `kunekune_plus_homing` 174 → 258 発）が、
**corpus-smoke の 3 つの数はどれも動かない。**

### ★ 直したら、控え 1 本が測る力を失った

`freeze-rand-within-loop` は
「ひと回りの中で 3 発 撃つと `$rand` が何回 転がるか」を測る控え。
毎フレーム `rand` を動かして、3 発の速さが割れるかを見ている。

```
直す前  f0=0.2  f1=0.5  f3=0.9  に動かして、撃つのは f0/f1/f3  → 0.2 0.5 0.9
いま    撃つのが 1 フレームずつ早まって f0/f1/f2 → ★0.2 0.5 0.5
```

★**3 発のうち 2 発が同じ値になり、「毎フレーム転がる」ことが読めなくなった。**
控えは緑にできるが、**割れる形が消えている。**
`rand` を動かすフレームを `f3` → `f2` に詰め直して、3 発とも割れる形に戻した。

★**「直したとき、網が探していた形のほうが消える」**型。
控えが動いたときは、**新しい値が正しいかだけでなく、
その控えがまだ同じものを測っているか**を見ること。

### 動いた控え 17 本

```
f 番号を持つ（1 フレームずつ早まった）   12 本
  action-ref-params  bullet-fires-bullet  cycle-bullet-mutual  cycle-bullet-self
  isolation-repeat-term  isolation-vanish  nested-repeat  rand-0  rand-1
  rank-in-expr  repeat-sequence-fire  vanish

f 番号を持たない                          5 本
  wait-intervals               ★この札そのもの
  isolation-sequence-across-loop  同じ窓で 7 発 → 11 発
  freeze-rand-within-loop         ★上の「測る力を失った」1 本
  real-speed-relative-7           発射数だけ増える。速さの顔ぶれは同じ
  real-unresolved-param           発射数だけ増える。向き/速さの顔ぶれは同じ
```

★**`vanish` の控えも直った。** 2 の節に
「`wait 2` のあと f04 で消える。(a) と同じ現象」と書いてあったとおり、
**f03 で消えるようになった**（`wait 2` がきっかり 2 フレーム）。

★**`isolation-repeat-term`（`repeat` の中の `changeDirection term=2` を 3 周）は、
着く先が変わっていない。** 30 度 × 3 = 90 度 = `1.571` に届くのは前も後も同じで、
**届くまでのフレームだけが縮んだ**（f10 → f08）。

```
網    Core 86 件 ＋ Parser 346 件 すべて緑。控え 86 本
本体  src/FsBulletML.Core/Processable.fs  Init() の `+ 1.f` を消した 1 行
控え  17 本 焼き直し ＋ RefParamFreeze.fs の窓を 1 フレーム詰めた
```

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
足すので、(a) か (b) を通ると**座標が膨らむ**。
★**膨らむ量は呼ぶ側の係数しだいで、同梱の 3 経路で違う**（下の「6 を直した」の隣、
「3 を直した」節に表がある）。ここに「2 倍」と書いていたのは MonoGame だけの話だった。

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

## 3 を直した —— 届いても壊れない形にした（2026-08-31）

### 「直すべきものではない」を 1 段 変えた

上の節は「いまは死んだ枝。直すべきものではないが、リファクタリングで届くようにした
瞬間に壊れる」で止まっていた。**届いても壊れない形にすれば、その懸念ごと消える。**

```
直す前   RunResult(true, bullet.X, bullet.Y)   絶対値
直した後 RunResult(true, 0.f, 0.f)             差分 0（run は差分を返す契約）
```

契約が差分であることは、**生きている枝で確認できる**
（`AccelerationX + sin(dir)*speed` を返している ＝ 1 フレームぶんの差分）。

### ★ 死んでいる理由は、測定より構造のほうが強い（planner）

上の節は「直に呼んで測ったら届かなかった」で止めていた。
**`run` を呼ぶ箇所は 4 つで、4 つとも `Task` を先に見ている。**

```
src/FsBulletML.MonoGame/BaseBullet.fs:76        match this.self.Task with | None -> ()
src/FsBulletML.Unity2D/DefaultBullet.fs:89      同じ形
samples/…Unity2D.CSharp/BaseBullet.cs:41        Monad.OptionExtentions.Action(self.Task, ...)
samples/…ECS/BulletSimulationSystem.cs:126      同じ形
```

**「まだ届いていない」ではなく「呼ぶ側が全員 先に弾いている」。**
★**そしてそれが直す理由も強くする** —— **ガードを 1 つ外したら、その瞬間に届く。**

### ★★★ 「2 倍になる」は 1 経路だけの話だった

上の節には **「そのフレームで座標が 2 倍になる」** と書いてあった。
**呼ぶ側の係数を数えたら 3 通りあった。**

```
MonoGame   BaseBullet.fs:71     X <- X + x                       係数 1      → ★2 倍
Unity2D    DefaultBullet.fs:79  X <- X + (x / PixcelsToUnits)    係数 1/100  → ★1.01 倍
C# sample  BaseBullet.cs:44     X = X + (result.X / 100)         係数 1/100  → ★1.01 倍
```

（`Settings.fs:11` に `PixcelsToUnits = 100.f`）

★★**engineer は「2 倍」を note から検算せずに写し、planner は「1.01 倍」を
C# サンプルだけ見て出した。二人とも 1 経路を見て全体へ広げていた。**
**正確には「呼ぶ側の係数しだい」で、`apply` に何を渡すかは呼ぶ側が決める。**

★**「2 倍」と書くと、次に読む人が「見た目で分かるはず」と思う。**
1% のずれは 1 フレームでは見えない。

### 測った / 測っていない

```
(a) Task = None      ★測った。テストからガードを迂回して届かせた
(b) Tasks = null     ★測っていない
```

(b) は `Tasks` の setter が internal で、`FsBulletML.Core.Tests` は
`InternalsVisibleTo` に入っていない（`Parser.Tests` は入っている）。
**本番でも `convertBulletmlTask` が必ずリストを入れるので、到達する作り方が無い。**
直しは (a) と同じ形で入れたが、**落ちるところを見ていない。**

★**(a) の控えは「テストからしか通らない形」を固定している。**
doc に本番の 4 経路がガードしていることを書いた。

### 直す前の値

★**控えを焼き直したので、控えからは消えた**（未追跡なので git にも無い）。ここに残す。

```
直す前   Task=None  Processed=true X=7.000000 Y=11.000000  絶対値
直した後 Task=None  Processed=true X=0.000000 Y=0.000000   差分 0
```

### ★ (a) にはもう 1 行 死んでいたので消した

```fsharp
| None ->
    bullet.Task |> Option.iter(fun task -> task.Finish <- true)   ★None なので必ず何もしない
    RunResult(true, bullet.X, bullet.Y)
```

`bullet.Task` は `None` に確定しているので、`Option.iter` は 1 度も走らない。消した。
**(b) の側の `Option.iter` は `Some` の中なので生きている。残してある。**

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

## 4 を直した —— `\$d*` を `\$\d*` に（2026-08-31）

### 向きは 1 本だった

**仕様は未解決の `$N` について何も言っていない**（日英 2 版とも）。
**ただし実装の意図は字から読める** —— `\$\d*` のつもりで `\$d*` と書いてある。

```fsharp
// 直す前
Regex.Replace(s, "\$d*", "0")     $ のあとに ★文字 d が 0 個以上
// 直した後
Regex.Replace(s, "\$\d*", "0")    $ のあとに ★数字が 0 個以上
```

```
        直す前              直した後
$1      "01" → ★1.000       "0" → 0.000
$2      "02" → ★2.000       "0" → 0.000
$12     "012" → ★12.000     "0" → 0.000
1+$1    "1+01" → ★2.000     "1+0" → 1.000
```

**未解決の `$N` が、静かに `N` として計算されていた。**

### 当てる先を先に測った（7 で踏んだ形を潰した）

`ref` に渡す `param` の数が、参照先の使う `$N` の最大値に足りないものを静的に数えた。

```
一意な xml 227 本 / ref を持つ 207 本 のうち
  ★param が足りない ref を持つ: 2 本
    [Bulletsmorph]_kunekune_plus_homing.xml   actionRef label="impl:30"  param 0 個
    [Bulletsmorph]_satoru4.xml                actionRef label="impl:100" param 0 個
```

どちらも参照先が `<direction type="absolute">$2</direction>`
`<speed type="absolute">$1</speed>` を使う。**`$1` `$2` が未解決のまま残る。**

### 実物 2 本を 200 フレーム 回した —— 2 本とも動く

```
                                直す前                    直した後
kunekune_plus_homing   向き/速さ ★0.035/1.000 …      →  ★0.000/0.000 0.262/1.000 …
satoru4                （末尾が変わる）
```

★**200 フレームで測った。** 7 で 60 フレームの窓が短くて 3 本を取り逃したので、
最初から長い窓にした（控え `real-unresolved-param`）。

```
corpus-smoke   撃った 217 / 撃たなかった 6 / 落ちた 4 / 避けた 0   変わらない
```

★**ただしこの行は「波及していない」の根拠にならない**（corpus-smoke は速さも向きも
見ていない）。言えるのは「撃つ本数は変わらなかった」まで。

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

### ついでに、例外のメッセージが条件と合っていない（★直した）

```fsharp
// IntermediateParser.createBulletml の | None ->
| None -> new BulletmlDTDViolationException("this element should have ShootingDirection attribute.") |> raise
```

条件は `tryFindBulletmlAttrs` が `None` のとき、つまり
**`bulletml` 要素の属性が取れなかったとき**であって、`type` 属性の有無ではない。
`type` を省いても、この例外は飛ばない（上で測ったとおり）。

★**そのあと、この枝が 3 と同じ「届かない枝」だと分かった。**

`tryFindBulletmlAttrs` の中身は `maybe { ... }` だが、
★**`let!` が 1 つも無い**（`Bind` を通らない）。`Util.fs` の `MaybeBuilder` は
`Bind` でしか `None` を作らないので、**必ず `return` に着いて `Some` を返す。**
`let!` は Core 全体で 3 つあるが、3 つとも `actionRef` / `bulletRef` / `fireRef` の
`label` を取る所で、**この関数には 1 つも無い**。

★**いちばん届きそうな入力（属性をぜんぶ省いた `<bulletml>`）で確かめた。**

```
xmlns も type も name も無い <bulletml>
実装: ★落ちない。撃った弾 1 発
```

★**「落ちない」だけだと、黙って 1 つも走らなかった場合と見分けがつかない**ので、
**撃った数のほうを控えに出している**（控え `bulletml-no-attrs.txt`）。

**直したのはメッセージだけ。** 条件のほうに合わせて
`bulletml element attributes could not be read.` にした。
**枝は届かないままなので、挙動は 1 つも変わらない。**
[3](#3-run-に絶対値を返す枝が-2-つあるどちらも届かなかった) と同じで、
**届いたときに嘘を言わない形にした**だけ。

★**5 の本体（既定値が 3 つある／フロントへ繋ぐ）は直していない。**
あちらは不具合の修正ではなく**どう振る舞うと決めるか**なので、判断待ちのまま。

---

## 6. 小数点がカンマのカルチャでは、式の評価が落ちる（★直した）

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
**BulletML を 1 つも走らせられなかった**（値を 1 つでも書けば落ちる）。
壊れ方は「一部の式」ではなく「全部」。
★**測ったのは 2026-08-31 の直す前。** いまは 4 カルチャとも走る（下の「6 を直した」節）。

fr-FR で `2.5` のときだけ `FormatException` が出るのは、
`ToString("F10")` を通らずに `Single.Parse` へ直行する経路があるためと思われる。
**推論。** 直すときに測り直すこと。

---

## 6 を直した —— 不変カルチャに寄せた（2026-08-31）

### ★「出どころは 4 か所」は古くなっていた。触ったのは 11 変換、壊れていたのは 8

上の節を書いたときは 4 か所だった。**着手して名前で当て直したら 11 変換。**
**そのうち実際にカルチャで壊れるのは 8**（planner が直後に測って分けた）。

```
★壊れていた 8
IntermediateParser.fs  toStr の  .ToString("F10")
IntermediateParser.fs  mapEval の .ToString("F10")
IntermediateParser.fs  expandBulletRefOnce の .ToString("F10")   ★8 の段階 2 で engineer が足した
IntermediateParser.fs  expandActionRefOnce の .ToString("F10")   ★8 の段階 3 で engineer が足した
Processable.fs         rand.ToString()
Processable.fs         rank.ToString()
Util.fs                eval の   Single.Parse(ev)
Util.fs                tryEval の Single.TryParse(ev)            ★どこからも呼ばれていない

★壊れていなかった 3（明示に寄せただけ）
IntermediateParser.fs  toStr の  single |> string
Util.fs                eval の   Evaluate(...) |> string
Util.fs                tryEval の Evaluate(...) |> string
```

★★**2 か所は、この節を書いたあとに engineer 自身が足したもの。**
8 の直しで `expandBulletRefOnce` / `expandActionRefOnce` を書いたとき、
既存の `mapEval` と同じ形を写して、**同じ欠陥も一緒に写した。**
（planner も着手前に独立に数えて同じ結論を出した。
**写した側と、読んだ側の両方が同じところを見落としていた。**）

### ★★★ 「`|> string` は数の書式を持つ」は間違いだった

engineer はこの節に **「`|> string` は文字列化に見えて数の書式を持つ」** と書いた。
**planner が測って、事実として違うと分かった。**

```
             string(float32)  string(obj)  string(XPath の obj)  ToString()
en-US        2.5              2.5          2.5                   2.5
de-DE        ★2.5             ★2.5         ★2.5                  2,5
fr-FR        ★2.5             ★2.5         ★2.5                  2,5

陰性対照     de-DE の (2.5f).ToString() = ★2,5   ← 網は , を出せる
```

**F# の `string` は `IFormattable` に `InvariantCulture` を渡す。**
`nav.Evaluate(...)` が返す boxed な `obj` でも同じだった（現物と同じ形で測った）。

★**直しは残す。** `string` の不変性は F# の実装に依るところで、
明示に `CultureInfo.InvariantCulture` を渡すほうが版が変わっても動く。
**変えたのはコードではなく理由のほう。**

★★**理由は走らせられないので落ちない。**
この行を残していたら、次に誰かが別の場所の `|> string` を
「壊れている」と読んで直しに行くところだった。

### 作る側と読む側は、セットで直さないと壊れる

```
作る側  数 → 文字列   ToString / |> string
読む側  文字列 → 数   Single.Parse / Single.TryParse
```

**片方だけ不変にすると、いまより悪くなる。**
たとえば作る側だけ不変にして `"2.5"` を作り、読む側を de-DE のままにすると、
`Single.Parse("2.5")` は de-DE で **`.` を桁区切りと読んで 25 を返す** —— 例外も出ない。

### `$rand` の経路（上の節で「上流を直すまで見えない」としたもの）

見えるようになった。**`$rand` 固有の壊れ方は無かった。**

```
en-US $rand      0.500
de-DE $rand      0.500      ← en-US と同じ
de-DE $rank      0.500
de-DE 1+$rand*2  2.000      ← 1 + 0.5*2 = 2
fr-FR $rand      0.500
```

### ★ `2.5` の `FormatException` の推論は、測る前に直してしまった

上の節に **「推論。直すときに測り直すこと」** と書いてあったのに、
**測る前に直した。** 直ったあとは再現しないので、その経路は直接 測れない。

代わりに材料を控えに残した（`culture-numberformat`）。

```
en-US      小数点 .   桁区切り ,
ja-JP      小数点 .   桁区切り ,
de-DE      小数点 ,   桁区切り .          ← "2.5" は . が桁区切りなので ★25 になる（例外なし）
fr-FR      小数点 ,   桁区切り U+202F     ← . はどちらでもないので ★FormatException
Invariant  小数点 .   桁区切り ,
```

**推論はこの表と整合する。** `.` を含む文字列が `Single.Parse` へ直行する経路があるなら、
fr-FR は落ち、**de-DE は落ちずに値が 10 倍になる。**
ただし**その経路を直接 測ってはいない。** 表からの読みである。

★★★**planner が測った。読みは 1 字も違わず当たっていた。**

```
en-US   Single.Parse "2.5" = 2.500000
de-DE   Single.Parse "2.5" = ★25.000000       ← . を桁区切りと読む。★例外なし
fr-FR   Single.Parse "2.5" = ★FormatException
```

**これで「推論」から「測定」に変わった。**
下の「作る側だけ直すと、落ちるバグが静かに値が狂うバグに変わる」も、これで裏づいた。
（probe は planner の scratchpad の `culture-probe.fsx` / `culture-probe2.fsx`。
**この repo には入っていないので、いつか消える。** 上の 3 行が本体）

### 測り直した結果

```
              直す前                         直した後
en-US / ja-JP 正常                           ★1 文字も変わらない
de-DE         全部 XPathException            en-US と同じ値
fr-FR         FormatException / XPathException  en-US と同じ値
corpus-smoke  撃った 217 / 落ちた 4          ★変わらない
```

**`,` が小数点のカルチャでも BulletML が走るようになった。**

控え 8 本（`culture-decimal` `culture-integer` `culture-literal` `culture-messages`
`culture-which-value` `culture-rand-rank` `culture-rand-isolated` `culture-numberformat`）。

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
→ ★**2026-08-31 に直した。下の節。**

---

## 7 を直した —— `direction` と同じ割り方に揃えた（2026-08-31）

### 直した形

`if` の 2 分岐を、`direction` 側と同じ 3 分岐にした。

```fsharp
match speed.speedType with
| SpeedType.Sequence -> SrcSpeed <- SrcSpeed + pf.changeSpeed        そのまま
| SpeedType.Relative -> SrcSpeed <- pf.changeSpeed + ★bullet.Speed   ここだけ変えた
| _                  -> SrcSpeed <- pf.changeSpeed                   そのまま
```

`bullet.Speed` は同じスコープに居る（60 行 上で `direction relative` が
`pf.changeDir * revise + bullet.Dir` と書いている。**同じ位置に同じ形が入る**）。

★**`sequence` と `absolute` は 1 文字も変えていない。**
壊れていたのは `relative` を `sequence` の枝に相乗りさせていたことだけ。

### 測り直した結果

★**控えが自分で書いていた期待値と一致した。**

```
              直す前          直した後      控えの但し書き
親 speed=1    s=1.000    →    ★s=2.000     「親を基準なら 2」
親 speed=3    s=1.000    →    ★s=4.000     「親を基準なら 4」
親 speed=5    s=2.000    →    ★s=7.000     「親を基準なら 7」
```

`bullet-action-relative` も `s=1.000` → `s=2.000`。
**向きのほう（`d=2.356`）は 1 文字も動いていない。**

```
corpus-smoke   撃った 217 / 撃たなかった 6 / 落ちた 4 / 避けた 0   変わらない
```

★**ただし、この行を「波及していない」の根拠にしてはいけない。**
**corpus-smoke は「撃ったか」しか数えておらず、速さを見ていない**（下の節）。
**この直しは速さを変えるので、そもそもこの門には映らない。**
言えるのは「**撃つ本数は変わらなかった**」までである。

### 当てる先（同じ分母で数えたもの）

```
一意な xml 227 本のうち
  speed type="relative" を changeSpeed の ★外 に持つ   ★7 本（3.1%）
                            changeSpeed の 中 に持つ      7 本         ← 動かない
```

`changeSpeed` の中の `relative` は仕様も実装も「現在の弾の速度との差分」で
一致しているので触っていない。

### ★★★ 「当てる先が在る」と「値が動く」は別だった —— 7 本のうち 2 本

最初この節に **「corpus-smoke が変わらないので波及していない」** と書いた。
**planner に「corpus-smoke は撃ったかしか数えていない。速さは見ていない」と指摘された。**
**変化が無かった証拠ではなく、その門が速度を測っていない証拠だった。**

実物 7 本を 60 フレーム 回して、直す前と直した後で並べた（控え `real-speed-relative-7`）。

```
                          直す前                              直した後
aba_1.xml       速さ  ★0.000 1.500                        →  1.500
aba_3.xml       速さ  ★0.000 0.800 1.300 1.450            →  0.800 1.300 1.450
aba_7.xml       速さ  ★0.300 0.600 0.900 1.100 1.200 1.500 →  1.100 1.400 1.700 2.000 2.300 2.600
hajike.xml      速さ  （末尾が変わる）
kunekune.xml    速さ  ★-0.500 … -13.000, 2.000            →  1.500 2.000
（残り 2 本）         変わらない
```

★**`kunekune.xml` は直す前に速さが負の方向へ伸び続けていた**（負＝逆向きに飛ぶ）。
`relative -0.5` が `sequence` の枝で**累積**していたため。
planner が XML から裏を取った —— `<fire label="src"><speed>2</speed>` の下で
`repeat 10 × 2 か所 = ひと回り 20 発`、`-0.5 × 20 = -10.0`。
**走らせなくても repeat の数から出る。** 直したあとの `1.5` も `2 + (-0.5)` で合う。

### ★★★ 「変わらない」の理由を 1 回 間違えた —— 窓が短かった

最初この節に **「動いたのは 7 本中 2 本。残り 5 本は root から撃つので親の速さが 0」**
と書いた。**planner が現物を見て、親の速さは 0 ではないと指摘した。**

```
hajike    relative は <bullet label="cross"> の中。cross は ★<speed>0.3</speed> で撃たれる
aba_7     relative は <bullet label="bit"> の中。  bit   は ★<speed>1.1</speed> で撃たれる
aba_3     relative は <bullet label="changecolor"> の中。★<speed>1.3</speed> で撃たれる
```

**本当の理由は「60 フレームの窓に、その fire がまだ入っていない」だった。**
到達フレームは `wait` を足せば **XML から読める**（走らせなくても分かる）。

```
aba_7   top→dummy(0) → wait 60 → bit(60) → wait 20  → ★80 フレーム目
hajike  top→wave(0) → cross(0) → wait 100           → ★100 フレーム目
aba_3   top→bomb(0) → wait 50 → bombbit(50) → wait 120 → ★170 フレーム目
```

**窓を 60 → 200 に伸ばして測り直した。**

```
60 フレーム    ★2 / 7 本が動く
200 フレーム   ★5 / 7 本が動く
```

★**`self-0012` は走行を伸ばしても届かない。** 当てる先 2 か所が
`<action label="6way">` の中で、**`top6` からしか呼ばれず、いまの実装は `top*` を
1 つしか回さない**（[9](#9-top-が複数あっても先頭に-wait-があると後ろが-1-度も走らない)）。
しかもその手前に `<wait>9999</wait>` が在る。
★★**未修正の 9 が、7 の検証を塞いでいる。**
（`air_elemental` は追い切れていない。**未確認**）

★**控えは「直した後」しか持てない**ので、上の表がこの変化の唯一の記録。

### この節が踏んだ形 —— 門は在るが、届いていなかった

```
1  ★門が覆っていない先を「変わらない」と読んだ（corpus-smoke は速度を測らない）
2  ★門を足したが、窓が 60 フレームで当てる先の 3 本に到達していなかった
3  ★到達していない理由を「親の速さが 0」と読んだ（現物と合っていない）
```

**1 と 3 は planner が指摘した。**
★**planner の推薦（`hajike` が濃い / `aba_7` なら 1 本で両方見える）も外れていた** ——
**「fire 経路の数」で選ぶと、入れ子が深いものが選ばれる。深い＝到達が遅い。**
**濃さの物差しが、測定の窓と逆相関していた。**

★★**今日 3 回目の形。**

```
8   長時間走行の門が self しか回していない   覆っていない先に本物のクラッシュが在った
7   corpus-smoke が速さを見ていない          門が測る対象が違った
7'  速さの門が 60 フレームしか回さない       ★門が当てる先に到達していない
```

**3 つとも「門は在る」。違うのは、対象 / 種類 / ★届く距離。**

---

## 8. 相互参照でスタックが溢れ、プロセスごと落ちる（★段階 1 を直した）

固めた場所: `SelfReference.fs`（**隔離を外した。控え 6 本**）

★**2026-08-31、zecl の判断で段階 1 を入れた。以下は直す前の記録。**
直した内容と測り直した結果は [この節の末尾](#段階-1-を直した--打ち止めを入れた)。

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

### この件だけテストの置き方を変えてあった（★もう戻した）

直す前は控えを取っていなかった。**落ちると控えが書けない**ため。
fixture は `[<Explicit>]` で、名指しでなければ回らないようにしていた。
通常の走行（48 件）に混ぜると、他の 47 件を巻き添えにして全部落ちた。
実際に 1 回巻き添えにしてから隔離した。

**段階 1 が入って例外で戻るようになったので、隔離を外して控えを 6 本 取った。**

---

## ★段階 1 を直した —— 打ち止めを入れた

`IntermediateParser.convertRefBulletml` に、**展開中の参照を種別つきの集合で持たせた。**
再訪したら `BulletmlDTDViolationException` を投げる。

```fsharp
let internal refKey kind label = kind + ":" + (label: string)

let rec private convertRefBulletmlIn visiting topRecBulletml recBulletml =
  let enter key =
    if Set.contains key visiting then
      new BulletmlDTDViolationException(
            sprintf "circular reference detected:[%s] …") |> raise
    Set.add key visiting
  ...
  | RecBulletml.ActionRef (attrs, prams) ->
    let visiting = enter (refKey "action" attrs.actionRefLabel)
    ...
```

**集合はパススコープ**（immutable な `Set` を再帰呼び出しに渡すだけ）なので、
展開し終えて戻れば消える。**兄弟に同じ label が何度出ても輪にはならない。**
外側のシグネチャ（`convertRefBulletml topRecBulletml recBulletml`）は変えていないので、
`toProcessable` の呼び出しは無変更。

### 何が変わったか

```
                    直す前                          直したあと
7 本を走らせる      ★プロセスごと落ちる            BulletmlDTDViolationException
SelfReference       [<Explicit>] で隔離。控えなし   ★隔離を外して控え 6 本
Corpus の known再帰  7 本を避けていた                ★空にした
```

`corpus-smoke` の分母で測り直した結果。

```
              直す前    直したあと
撃った        210 本    ★210 本    ← 変わらない
撃たなかった    6 本      ★6 本    ← 変わらない
落ちた          4 本      11 本     ← +7（避けていた 7 本が例外として出た）
避けた          7 本       0 本
```

★**撃てていた 210 本が 1 本も減っていない。** 打ち止めが正当な弾幕を巻き込んでいない、
の分母つきの証拠。`Parser.Tests` の 346 件も緑のまま。

### 輪の中身が出た。**7 本中 6 本は `bulletRef` の自己参照**

```
[ESP_RADE]_round_123_boss_izuna_fan   action:fan
[Original]_cont_circle                bullet:bit
[Original]_light_lv10                 bullet:bit
[Original]_light_lv25                 bullet:bit
[Original]_light_max                  bullet:bitaim
[Original]_water_lv10                 bullet:longbit
[OtakuTwo]_accel_jump                 bullet:bound
```

★**これは壊れた BulletML ではない。** 例えば `cont_circle` の `bullet label="bit"` は、
`wait 40` を挟んで `bulletRef label="bit"` を撃つ —— **弾が自分を撃つ連鎖**で、
BulletML では正当な書き方。実行時には `wait` があるので有限に進む。
**展開時にだけ無限になる。**

### 控えは 7 本。**実物で当たっている形と、守りで置いた形を分けてある**

planner が控えの中身とコーパスの実例を突き合わせて、**入れ替わり**を見つけた。
最初は「実例のある mutual に控えが無く、控えのある mutual に実例が無い」状態だった。

| 控え | 形 | コーパスの実例 |
|---|---|---|
| `cycle-action-self` | `action` の自己参照 | ★1 種（`action:fan`） |
| `cycle-action-mutual` | `action` の相互参照 | 0 件（守りで置いた） |
| `cycle-bullet-self` | `bullet` の自己参照 | ★3 種（`bit` / `bitaim` / `bound`） |
| `cycle-bullet-mutual` | `bullet` の 2 段相互参照 | ★1 種（`longbit` ⇄ `shortbit`）**あとから足した** |
| `cycle-fire-self` | `fire` の自己参照 | 0 件（守りで置いた） |
| `cycle-siblings-not-a-cycle` | 兄弟に同じ label | 陰性対照 |
| `cycle-deep-chain-ok` | 輪でない深い鎖 | 陰性対照 |

`bullet` の 2 段相互参照は `[Original]_water_lv10.xml` に在る。

```
:40  <bullet label="longbit">   … :52  <bulletRef label="shortbit">
:80  <bullet label="shortbit">  … :92  <bulletRef label="longbit">
```

★**凍結予測（経路つき）を書いてから測って、当たった。**
打ち止めの集合は**種別つきのキーを 1 つの `Set` に入れている**ので、
`bullet:longbit` → `bullet:shortbit` → `bullet:longbit` の 3 手目で再訪になる。
**種別ごとに別々の集合を持っていたら、ここは止まらない**（planner の推論。実装は前者）。

実測も `circular reference detected:[bullet:longbit]` で一致。

### ★数える単位が 2 つある —— 「7 本」はファイル、「8 個」は輪

planner が静的な軸（部分木 ＋ 種別つき）で独立に測り、**5/7 は label まで一致**した。
残り 2 本で数が割れて、**片方は見せかけ、片方は本物の差**だった。

```
                      planner の軸                   例外に出た label
water_lv10   bullet:longbit , bullet:shortbit   bullet:longbit    ★同じもの
                （1 つの輪を両端から名指しただけ）
light_max    bullet:bit , bullet:bitaim         bullet:bitaim     ★本物の差
                （★独立した輪が 2 つ在る）
```

`light_max.xml` は輪を 2 つ持っている。

```
:166  <bullet label="bit">      … :178  <bulletRef label="bit">      ← 輪 1
:200  <bullet label="bitaim">   … :219  <bulletRef label="bitaim">   ← 輪 2
```

`top` からの辿り順で `bitaim` に先に着く（`:11 dummyaim` → `:117` → `:124 bitaim`）ので、
**`bit` の輪へ着く前に例外で止まる。**

★★**例外は一度に 1 つしか出ないので、この控えからは 2 つめが見えない。**
「件数で止めず当たった行を出す」の裏側で、
**1 件出たら残りが隠れる**形。**静的に全部並べる軸は、ここでだけ効く。**

```
ファイル   7 本
輪         ★8 個   action self 1 / bullet self 6（bit ×4・bitaim・bound）/ bullet mutual 1
```

★**段階 2 で「いくつ直ったか」を数えるなら、分母は輪の 8 個のほう。**
ファイル単位だと、`light_max` が半分だけ直った状態を数えられない
（`bitaim` を動くようにしても、次は `bit` で止まる）。

### 段階 1 で止まっていたこと（段階 2 で解いた）

段階 1 の時点では、**7 本は「落ちない」ようになっただけで「動く」ようにはならなかった。**
例外で弾かれるので、弾幕自体はいまも走らない状態だった。

段階 2（輪のところは展開せず `Ref` を残し、実行時に 1 段ずつ解決する）が要る。
測った範囲では、そこは 3 か所に分かれる。

```
convertRefBulletml    輪なら展開せず Ref ノードのまま返す（★ここは書く）
convertRecBulletmlEx  ★3 種とも変換済み（:1065 BulletRef / :1066 FireRef / :1108 ActionRef）
BulletRunner          ★Ref のケースが 0 件。実行時に解決する側を新しく書くことになる
```

**`ProcessableBulletml` の DU には `ActionRef` / `FireRef` / `BulletRef` が既にある**
（`Processable.fs:165` `:173` `:196`）。`cloneProcessable` も 3 つとも通す。
**型も変換も揃っていて、実行側だけが空**という状態。

★**最初これを「変換は半分」と書いていた。範囲を狭く取っていた**（planner の指摘）。
`convertRecBulletmlEx` は `:1023-1122` なのに `:1004-1085` に当てていて、
`ActionRef`（`:1108`）が範囲の外に落ちていた。**開始も終わりも外していた。**
「鍵が合っていても、当てる範囲が狭いと出ない」。
★**しかもその材料を段階 2 の計画に使おうとしていた。** 計画になる前に止まった。

★**6/7 が `bulletRef` の自己参照**なので、段階 2 でいちばん効くのは `BulletRef` の実行時解決。

### ★実行側は「0 件」ではなく「黙って飲む」形だった

`BulletRunner` に `Ref` の `match` ケースが無いのは事実だが、**受け皿は在る。**
しかも **3 通りに割れる**（planner が見つけ、こちらで当て直した）。

```
Ref が居る場所            いま起きること                    出典
top のタスクとして         failwith "error"                 :361
repeat の直下の子として    failwith "error"                 :140
★action の子として        ★黙って RunState.End            :343   ← いちばん多い経路
```

★★**3 つめが危ない。** `runCommand` の `| _ -> bullet, RunState.End` に落ちるので、
**`Ref` は「もう終わった命令」として素通りする。例外も警告も出ない。弾が出ないだけ。**

さらに `finish` の向きが噛み合っていない。**同じ `Ref` に 3 つの関数が 3 つ違う答えを返す。**

```
:89   getFinish   | _ -> false                  ★終わっていない、と答え続ける
:101  setFinish   | _ -> ()                     ★終わった、にできない
:343  runCommand  | _ -> bullet, RunState.End   ★終わった、を返す
```

`getFinish` が `false` を返し続けるので、**呼ぶ側が「まだ終わっていない」と読んで
回し続ける種**にもなる。**段階 2 で `Ref` を残す設計にするなら、ここを最初に揃える。**

★**段階 2 の危なさは「書き忘れ」ではない。**

```
書き忘れなら   落ちる。すぐ分かる
いまの形なら   ★黙って弾が出なくなる
               corpus-smoke の「撃たなかった」に静かに移るだけ
```

★★**その事故は、既にある分母でそのまま捕まる**（planner の指摘）。
`corpus-smoke` は 撃った / 撃たなかった / 落ちた / 避けた に分けているので、
**いまの「撃たなかった 6 本」が増えた瞬間に赤くなる。網を新しく作る必要は無い。**

### 段階 2 で触るなら、ついでに直せるもの

`BulletRunner` の `failwith` は **4 か所あって、文言が 2 種類しかない。**

```
:46   failwith "convert error"
:140  failwith "error"
:361  failwith "error"
:405  failwith "convert error"
```

**どちらが飛んだか区別できない。** いま直す必要は無いが、**触るときに分ける。**

---

## 8 の段階 2 —— 輪を残して実行時に 1 段ずつ解いた

2026-08-31、zecl の判断で段階 2 まで入れた。

### 直したもの（3 か所）

```
IntermediateParser.convertRefBulletmlIn   bullet の輪は例外にせず BulletRef のまま残す
IntermediateParser.expandBulletRefOnce    その BulletRef を 1 段だけ解く入口（新規）
Processable.BulletmlTask.ResolveBulletRef 走らせる側から解くための入口（新規のフィールド）
BulletRunner.convertBulletmlTask          文書全体を closure で捕まえて上に挿す
BulletRunner.fireCommand                  bulletElm が BulletRef なら 1 段解いてから使う
BulletRunner.createTask                   撃たれた弾の task にも入口を引き継ぐ
```

**`action` / `fire` の輪は例外のまま。** 実行時に新しいタスクができないので、
1 段ずつ解いても進まない。ここは段階 1 の打ち止めが残る。

★**この 2 行のうち `action` の分は取り消し**（[段階 3](#8-の段階-3--action-の輪も解いた輪-8-個ぜんぶ)）。
新しいタスクができないのは合っているが、**走らせる並びを差し替えれば進む。**
`fire` の輪は段階 3 でも触っていないので、いまも例外のまま
（コーパスに実例 0 本。控え `cycle-fire-self` が例外を控えている）。

### なぜ 1 段で足りるか

`bulletRef` は必ず `fire` の中にあり、**`fire` が走ると新しい弾と新しい `BulletmlTask` ができる。**
だから実行時に要るのは「撃たれるたびに 1 段」だけで、状態を持ち回る必要が無い。
解いた中にまた同じ `BulletRef` が残るので、**次に撃たれたときに次の 1 段が解かれる。**

`createTask` で引き継ぐのを忘れると、**外側の 1 段だけ解けて内側が解けない。**
実際に 1 回そうなった（`convert error` のまま）。

### 受け皿は 3 通りではなく 4 通りだった

段階 2 に入る前に数えた受け皿の一覧に、**`createTask` が抜けていた。**

```
:374  run             failwith         top のタスクとして
:143  repeatCommand   failwith         repeat の直下の子として
:343  runCommand      黙って End       action の子として
:46   createTask      failwith         fire の子として     ← 4 つめ
```

`Ref` が `fire` の子として残ると `createTask` に届く。**段階 2 の途中で実際にここへ落ちた。**

`:46` は同じ日の `failwith` の一覧（文言が 2 種類しかない）には出ていた。
**文言の話としては目に入っていて、経路の話としては数えなかった。**
「道具が名乗る分母は、道具が自分で削ったあとの数」の、
**見出しを分けたせいで分母から外れた**形。

### 測り直した結果

```
              段階 1 の前   段階 1   段階 2
撃った          210 本      210 本   216 本
撃たなかった      6 本        6 本     6 本
落ちた            4 本       11 本     5 本
避けた            7 本        0 本     0 本
```

**7 本のうち 6 本が動くようになった。** 残る 1 本は `[ESP_RADE]_..._izuna_fan.xml` で、
輪が `action:fan`（★「`action` の輪なので実行時に解けない」と書いていたが、
[段階 3](#8-の段階-3--action-の輪も解いた輪-8-個ぜんぶ) で解けた。この表は段階 2 時点の数）。
他の 4 本は 8 とは別件（`repeat` の DTD 違反 3 本と `2wayLeft` の `XmlException`）。

**「撃たなかった 6 本」が増えていない。** `Ref` が黙って素通りする事故（`runCommand` の
`| _ -> RunState.End`）が起きていれば、ここが増える。増えていないので起きていない。
`Parser.Tests` の 346 件も緑のまま。

### 動いている形

`SelfReference.fs` の `cycle-bullet-self` が、弾の連鎖をそのまま控えにしている。

```
f00  +b1
f01  +b2      b1 が撃った
f02  +b3      b2 が撃った
f03  +b4      b3 が撃った
```

**展開時には無限だったものが、実行時には 1 フレームに 1 段ずつ進む。**
`cycle-bullet-mutual`（`longbit` と `shortbit`）も同じく走る。

### 輪の数で数えると

段階 1 の節に書いた分母（ファイル 7 本 / 輪 8 個）で数えると、こうなる。

```
bullet self    6 個   すべて解けた
bullet mutual  1 個   解けた
action self    1 個   解けない（例外のまま）  ← 段階 3 で解いた
```

`[Original]_light_max.xml` は輪を 2 つ持つが、**どちらも `bullet` なので両方解けた。**
ファイル単位でも輪単位でも、残るのは `action:fan` の 1 個だけ。

### 残った 1 個（`action:fan`）について（★段階 3 で解いた）

`[ESP_RADE]_..._izuna_fan.xml` の `action label="fan"` は、
**`wait` を挟んで自分を `actionRef` している**（`:54` と `:65` に `wait`、`:86` で自分を呼ぶ）。

**「`action` の輪は解けない」は、まだ測っていない読み。engineer が立てたもので、
planner は これについて何も言っていない**（planner が `action:fan` に触れたのは
表の中のデータ点が 3 か所だけで、主張ではない）。
一度これを planner の読みとして便に書き、本人の指摘で取り消した。
**後で誰かが planner に聞いても答えられないので、出どころをここに残す。**

`wait` があるので、1 段ずつ解けば実時間で進む形ではある。
控えは `cycle-action-self-with-wait` に置いた（いまは例外になる）。

ただし `bulletRef` と同じようにはいかない。

```
bulletRef   fire が新しい弾と新しい task を作る   古い木は弾が消えれば消える
actionRef   同じ task の中で子として実行される   解くたびに木が 1 段ずつ深くなる
```

`fireCommand` に足した分岐では届かない。`runCommand` / `actionCommand` の側で、
**解いた結果をどこに置き、いつ捨てるか**を決める話になる。

実例はこの 1 本だけ（輪 8 個のうち 1 個）。**やるかどうかは判断待ち。**
→ ぜくるの「輪をやり切る」で着手し、下の段階 3 で解いた。

---

## 8 の段階 3 —— `action` の輪も解いた（輪 8 個ぜんぶ）

### 実物を読んだら、設計の分かれ道が 1 本に絞れた

`[ESP_RADE]_..._izuna_fan.xml` の `fan` は、こういう形をしている。

```xml
<action label="fan">
  <wait>2</wait>
  <changeSpeed>...</changeSpeed>
  <wait>1-$1</wait>
  <actionRef label="Stop"/>
  <fire>...</fire>
  <actionRef label="XWay">...</actionRef>
  <actionRef label="fan">...</actionRef>   ← 輪。末尾にある
</action>
```

読めたのは 2 つ。

- **輪の `actionRef` は末尾にある**
- **終了条件が無い**。`param` が `$1-1` で減っていくだけで、`fan` は永遠に自分を呼ぶ

2 つめが効いた。**「解くたびに 1 段 深くする」は使えない。**
`wait` があるので 1 フレームに 1 段だとしても、60fps で 1 分 = 3600 段。
StackOverflow に戻るだけになる。

### 解き方 —— 走らせる並びを差し替える

`ProcessableAction` に `loop` を足した。走らせる側が `actionRef` の輪に届いたら、
1 段だけ解いて **この `action` が次のフレームから走らせる並び**をそこへ置く。

```
届いた並び   [fire; wait; actionRef(fan)]
                              ↑ ここに届いた
置く並び     [解いた fan の中身...] @ [actionRef より後ろの兄弟]
```

- **済んだ手前は捨てる**ので、並びの長さは解くたびに伸びない
- **後ろの兄弟を繋ぐ**ので、輪が末尾でなくても後続が落ちない
- 差し替えたらそのフレームは `Stop` で終える。だから **1 フレームに解くのは 1 段まで**

`wait` の無い輪（`<action label="top"><actionRef label="top"/></action>`）も、
これなら固まらない。何も撃たずに毎フレーム 1 段ずつ進む弾になる。
BulletML として無限ループの正しい記述なので、例外を投げるより忠実。

### ★「1 段だけ解く」と書いた入口が、2 段 解いていた

段階 2 で書いた `expandBulletRefOnce` は、こうなっていた。

```fsharp
refBulletml bullet (Some label) evaluated
|> convertRefBulletml topRecBulletml      // ← visiting が空から始まる
```

`convertRefBulletml` は `Set.empty` から始まる。だから
**解いた中身に残っている同じ参照が、もう 1 段 展開される。**
名前も doc も「1 段だけ解く」と言っているのに、2 段 解いていた。

`action` 側は同じ形をそのまま写したので、同じ穴を持って生まれた。
そして `action` では **1 フレームごとに `actionCommand → runCommand → actionCommand`
が 1 段 深くなり**、1000 フレームでテストのホストプロセスごと落ちた。

★**`bullet` 側では症状が出ていなかった。** `fire` が新しい弾と新しい task を作って
段を切るので、余分な 1 段は「無駄に大きい木」になるだけで軌跡に出ない。
控えは緑のままだった。**同じ欠陥が、置かれた場所によって見える／見えないに分かれる。**

直しは `visiting` に自分の key を入れてから展開する 1 行。

```fsharp
|> convertRefBulletmlIn (Set.singleton (refKey "bullet" label)) topRecBulletml
```

★**`bullet` 側を直しても控えは 2 本とも緑のままだった** ——
これが「余分な段は軌跡に出ない」の裏取りになっている。
一方 `action` 側は `cycle-action-mutual` の軌跡が変わった。

```
2 段版   f00 f02 f03 f05 で発射   （不規則）
1 段版   f00 f02 f04     で発射   （wait 1 なので 2 フレーム周期）
```

**規則的なほうが正しい。** 控えが「直した」ことを見せてくれた形。

### 測り直した結果

```
              直す前   段階 1   段階 2   段階 3
撃った        210 本   210 本   216 本   217 本
撃たなかった    6 本     6 本     6 本     6 本
落ちた          4 本    11 本     5 本     4 本
避けた          7 本     0 本     0 本     0 本
```

- **輪 8 個ぜんぶ解けた**（`bullet self` 6 / `bullet mutual` 1 / `action self` 1）
- 落ちた 4 本は**直す前と同じ 4 本**で、輪と無関係
  （`repeat` の DTD 違反 3 本 ＋ XML のパースエラー 1 本）
- **撃たなかった 6 本が動いていない**。段階 2 と同じで、事故が起きていない証拠

### 触ったところ

```
Processable.fs        ProcessableAction に loop を足した
                      loop が ProcessableBulletml を参照するので、型を相互再帰の組へ移した
                      BulletmlTask に ResolveActionRef
IntermediateParser.fs ActionRef の輪を残す（bulletRef と同じ形に）
                      expandActionRefOnce
                      ★expandBulletRefOnce の 2 段 展開を直した
BulletRunner.fs       actionCommand が pa を受け取り、輪に届いたら loop を差し替える
                      runCommand / repeatCommand が loop を見る
                      createTask で ResolveActionRef を引き継ぐ
```

`Init()` と `cloneProcessable` で `loop` を `None` に戻している。
戻し忘れると、`repeat` の 2 周めが 1 周めの解いた並びから始まる。

### 控えを強くした

焼き直した `cycle-action-self` と `cycle-action-mutual` を読んだら、
**どちらも「何も撃たず、落ちず、走る」だけ**だった。これは弱い。
「輪を黙って捨てる」実装に変えても同じ控えが出るので、**解けたことを担保していない。**

`cycle-action-long-run` を足した。**輪を 1000 フレーム 回して落ちないこと**を見る。
`withTimeout` の 1MB スタックの上なので、解いた段を積む実装だとここで落ちる ——
**実際に落ちた。** 上の 2 段 展開はこれで出た。

### ★★★ その門が、輪の片方しか回していなかった（planner の指摘）

**「段を積む事故を見ているのは `long-run` だけで、それは `self` です」。**
`cycle-action-mutual` は 6 フレームしか回していない、と指摘された。

そのとおりだった。**`mutual` を 1000 フレームで回したら、ホストプロセスごと落ちた。**
直したばかりの「1 段のつもりが 2 段」と、同じ形の穴が 2 段の輪に残っていた。

★**なぜ `self` だけ通っていたか。**

```
self     解いた結果の並びの ★トップレベル に、同じ actionRef がそのまま現れる
         → 同じ pa の並びを差し替え続けられる。深さ一定

mutual   解いた結果に ★別の action が挟まる（top を解くと Action(b, [actionRef top])）
         → 差し替わるのは内側の pa。外側は古い並びを指したまま
         → 辿る道がフレームごとに 1 段 伸びる
```

**同じ「輪を残して 1 段ずつ解く」でも、輪の長さで別の仕組みになっていた。**
`self` の控えが緑なので、`mutual` も同じだと思い込んでいた。

### 線引き —— 自己参照だけ解く

`convertRefBulletmlIn` に「直近に展開した `action` の label」を持ち回らせ、
**輪がその label 自身へ戻るときだけ残す。** 別の `action` を経由する輪は展開時に落とす。

```
fan     top → fanMain → fan → ★fan     直近展開 = fan、輪の相手も fan   → 残す
mutual  top → b → top → ★b            直近展開 = top、輪の相手は b     → 落とす
```

`repeat` の中の自己参照も残る。差し替え先の `pa` が `repeatCommand` の側で固定なので、
そこも深さが伸びない。

★**種別をまたぐ輪も落とす。** `bulletRef` / `fireRef` を展開するときは
`lastAction` を `None` に戻しているので、`action:a → bullet:b → action:a` は
**同じ label へ戻っていても自己参照として扱われない**。

```
cycle-action-via-bullet   circular reference detected:[action:a]
```

これは planner に「どちらになるか読めない」と聞かれて、予測を凍結してから測った。
**間に `bullet` が入ると `fire` が段を切るので、たぶん解ける形**だが、
**測っていないので落としている。** 実例 0 本
（planner が `samples` 925 本を走査して「種別をまたぐ輪 0」）。

**コーパスの数は変わらない。** `action` の輪は `fan` の自己参照 1 個だけで、
2 段の `action` 輪は実例 0 本。**輪 8 個ぜんぶ解ける、は維持されている。**

### ★足した門を 1 本 消した

`cycle-action-long-run-mutual`（2 段の輪を 1000 フレーム）を足したが、
線引きを入れたあとは**展開時に例外が出るので 1 フレームも走らない。**
名前は「1000 フレーム回しても落ちない」なのに、そこへ到達していない。
`cycle-action-mutual` と同じ例外を控えるだけなので消した。

★**2 段の輪を解けるようにするなら、この門を戻すこと。**

### 代わりに、実物の骨格で長く回す門を足した

`cycle-action-long-run` は `<action label="top"><actionRef label="top"/></action>` の
最小形だけだった。実物の `fan` は **`fire` と、輪でない `actionRef`** を持つ。
輪でない `actionRef` は解くと `action` として並びに入るので、そこも長く回して見る。

```
cycle-action-long-run-fan   1000 フレーム走って落ちなかった。撃った弾 334 発
```

334 は `wait 2` で 3 フレームに 1 発（1000 / 3 ≒ 333）。
**落ちないことと、回り続けていることの両方が 1 行に出る。**
撃った弾は `vanish` させてあるので、測っているのは段の積み方だけになる。

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

直していない。→ ★**2026-08-31 に直した。下の節。**

---

## 9 を直した —— `Stop` を task ごとに閉じた（2026-08-31）

### 直した形は 1 語

```fsharp
while i < len && not stop do     ← 直す前。★どれか 1 本が Stop したら while ごと抜ける
while i < len do                 ← 直した後
```

`top*` は 1 本ずつ独立した task。**ある `top` が `wait` で止まっても、
それはその `top` の話**なので、後ろの `top*` はこのフレームでも回す。
`stop` は返り値の組み立てにだけ使い、**走査は止めない。**

`endCount` は済んだ task を `else` の枝で数えているので、`Finish` の判定は崩れない
（planner が読んで確認）。

### ★★ 「番号順 / 文書順」は、直した時点で決着していた

材料の節に「番号順 / 文書順に全部」の 2 本があると書いたが、
**`while i < len` にした時点で `top*` は全部 走る。**
**「どれを走らせるか」はもう問いではない。** 残るのは**触る順**だけで、
`getAction` が行きがけ順なので **いま既に文書順**（planner の指摘）。

★**追加の変更はしない。** 番号順にする理由が残っていない ——

```
1  いま既に文書順。番号順にするには手を足す
2  4 形（宣言が逆 / tops・topt / 番号が飛ぶ / 裸と同居）を文書順は全部 覆える
3  番号順にすると wana.xml の 1 本目が入れ替わる
4  番号順にすると「tops / topt を番号のどこに置くか」を新しく決める必要が出る
   —— ★判断を減らす直しではなく、増やす直しになる
```

### 測り直した結果

```
                直す前   直した後
撃った          217 本   ★218 本
撃たなかった      6 本   ★5 本      ← 塞がれていた 1 本が動き出した
落ちた            4 本    4 本
避けた            0 本    0 本
```

```
multiple-top          +b1 のみ  →  ★+b1 +b2 +b3（top / top1 / top2 が全部 走る）
multiple-top-blocked  先頭に wait ありでも ★+b2 が出る
```

### 死んだ mutable が 2 つ残った（planner の指摘）

```fsharp
| RunState.Stop     -> stop   <- true      ★この直しで死んだ（もう誰も読まない）
| RunState.Continue -> break' <- true      ★直す前から誰も読んでいない
```

`break'` が元から死んでいるということは、**`top` の段で `RunState.Continue` が
返っても、いま何も起きていない**。**挙動は変わっていない**（直す前も読まれていない）。
[3](#3-run-に絶対値を返す枝が-2-つあるどちらも届かなかった) と同じ家族 ——
**書いてあるのに効いていない枝。**

★**2 つとも消した。消しても控えは 84 本すべて緑のまま** ——
**それ自体が「読まれていなかった」の裏取りになっている。**

### ★★★ 9 の直しが 7 の当てる先を開けた —— ただし半分

planner は「`self-0012` の当てる先は `6way` の中。`top6` からしか呼ばれないうえ、
手前に `<wait>9999</wait>` が在るので、**9 を直しても測れない**」と読んだ。
engineer の実測では **204 発 → 523 発** に増え、新しい速さが 2 つ出た。

**現物を開いたら、両方 正しかった。** `speed type="relative"` は **4 か所** ある。

```
:75  :89   ★<action label="top5"> の中     → 9 を直して届いた
:117 :133  ★<action label="6way"> の中     → wait 9999 の後ろ。届かない
```

```
                直す前          直した後
self-0012      撃った 204 発   ★撃った 523 発
               速さ 0.000 1.275 1.328  →  ★0.000 1.116 1.275 1.328 1.487
```

★**planner は `6way` の 2 か所だけを見て「測れない」と読み、
engineer は数が動いたのを見て「届いた」と読んだ。**
**見ていた場所が違っただけで、どちらの観測も正しい。**

★★**「届いた / 届かない」を 1 つの答えにできない。** 同じファイルの中で
**届く 2 か所と届かない 2 か所が同居している。**

---

## 2・4・7・9 を公式ドキュメントと突き合わせた（2026-08-31・直していない）

ぜくるの指示で**直さず材料だけ**出した。引いたのは
`bulletml_ref.html`（日本語）と `bulletml_ref_e.html`（英語）の 2 つ。

### ★★★ 7 は仕様が決めていて、実装が違う。しかも同じ関数の中に正解がある

`speed` と `direction` の `type` は、仕様では**同じ言い回しで対になっている**。

```
                 仕様（日本語 / 英語）                        実装                  一致
direction relative  その弾の方向が基準 / this bullet          changeDir + bullet.Dir   ★合う
direction sequence  前の fire の方向が基準 / previous fire    SrcDir + changeDir       ★合う
direction absolute  絶対値 / absolute                         changeDir                ★合う
speed     relative  ★その弾の速度が基準 / ★this bullet       ★SrcSpeed + changeSpeed  ★違う
speed     sequence  前の fire の速度が基準 / previous fire    SrcSpeed + changeSpeed   ★合う
speed     absolute  絶対値                                    changeSpeed              ★合う
```

**実装は `speed` の `relative` を `sequence` と同じ 1 本にまとめている。**

```fsharp
if (speed.speedType = SpeedType.Sequence || speed.speedType = SpeedType.Relative) then
  SrcSpeed <- SrcSpeed + changeSpeed
```

★**`sequence` のほうは正しい。壊れているのは `relative` を巻き込んでいること。**
`direction` 側は 3 つに分けてあって 3 つとも合っている。**同じ関数の中に正解が写っている。**

★★**日英で記述は一致している**（`this bullet` / `その弾の速度`）。
**仕様が割れているわけではない。** 直す向きは 1 本。

### 2 は仕様の字面と合わない。ただし「フレーム」の数え方は書いていない

```
日本語  「NUMBER フレーム待ちます」
英語    "Waits for NUMBER frames. 1 frame is 1/60 seconds."
```

**最初の 1 周だけ短い、とは書いていない。** いまの実装は最初が `w`、以降が `w+1`
（`repeat` あり）なので、**どちらの数え方でも字面とは合わない。**

★ただし**仕様は「1 フレームをどこで数えるか」を決めていない**（`run` を呼んだ回数か、
経過フレームか）。**「1 フレーム短い」が不具合かどうかは、そこを決めないと言えない。**

★**この節の結論は [2 を直した](#2-を直した--wait-の初期化の入口が-2-つあった2026-08-31) で更新した。**
**仕様からは降ろせないままだが、決め手が別に見つかった** ——
`accel` / `changeDirection` / `changeSpeed` は同じ `term` を持ちながら
**入口が 1 つで `+1` を付けておらず、きっかり `term` フレーム占める。**
**`wait` だけ入口が 2 つあって、片方が兄弟と揃っていなかった。**
★**どちらが正しいかは、仕様ではなく実装の中の対称性が決めた。**

### 4 と 9 は、仕様が何も言っていない

```
4  ref に param を足りなく渡したとき、未解決の $N をどうするか   ★2 版とも定義なし
9  top / top1 / top2 が複数あるときどう走るか                    ★2 版とも "top" の記述なし
```

★**`top` というラベルそのものが仕様に無い。** `bulletml_ref.html` にも
`bulletml_applet.html` にも出てこない。**実装の慣習である。**

**だから 4 と 9 は「仕様違反」ではない。** 直すなら、
**この実装がどう振る舞うと決めるか**の話になる（仕様から降ろせない）。

★4 については、仕様ではなく**実装の意図**が読める ——
`Regex.Replace(s, "\$d*", "0")` は `\$\d*` のタイポで、
**`$N` を `0` に潰す意図だったことは字から分かる。**

### ★★★ 9 の当てる先を数えた —— 複数の `top*` を持つのは 23 本

planner が数え、engineer が別の道具で数え直した。**23 本で一致。**

```
一意な xml          227 本（中身のハッシュで潰したあと。corpus-smoke と同じ分母）
  top* を持つ       ★227 本（＝ 1 本も欠けていない）
    1 本だけ        204
    ★2 本           13
    ★3 本            9
    ★4 本            1
    ────────────────
    複数持ち        ★23 本（227 本の 10.1%）
```

★★★**この表は 1 回 間違えた。** 最初は「top* を持つ 216 本 / 1 本だけ 193」と数えた。
**11 本 落としていた。** 原因は網のほうで、`label` を二重引用符で囲ったものしか見ていなかった。
`10Way.xml` ほか 11 本は `<action label='top'>` と**単一引用符**で書いてある。

★**落ちた 11 本は全部「1 本だけ持つ」ものだったので、複数持ちの 23 本は動かなかった。**
**分母だけが 216 → 227 に動いた**（10.6% → 10.1%）。

★★**planner は「その 11 本は Library の .NET 文書 XML では」と読み、
「corpus-smoke の分母なら 0 本になるはず」と予測した。**
**予測は当たったが、理由は外れていた**（現物は `PlayerBullet/basic.xml` などの弾幕）。
**当たった予測の理由が正しいとは限らない。**

★★★**二人とも二重引用符だけの網で数えて 23 本が一致した。**
漏れがたまたま「1 本だけ持つ」側にしか当たらなかったので、**23 は無事だった。**
**数の一致は、網が同じであることの証拠にならない。**

★**あとで分かったが、穴は engineer 側だけではなかった** ——
planner の網は XML パーサなので引用符は無事だったが、**木の形が違った**（直下だけ数えた）。
★★**同じ穴で一致したのではなく、違う穴で一致していた。**
**engineer の漏れた 11 本が全部「1 本だけ」側、planner が数え落とす入れ子の `top*` が 0 本。
偶然が 2 つ重なっただけ。**

★**割れたときの読み方は
[こちら](#-対になるもの--一致したときは誰も調べない)**（割れ方は 4 通りある）。
**割れたときは調べ、一致したときは調べない —— そこが非対称。**

### ★★ 直し方を決める材料。「top1 から順に」では覆えない形が 4 つある

```
1  ★宣言が逆        [Original]_wana.xml            ["top2","top1"]
2  ★番号でない      [Daiouzyou]_hibachi_2.xml      ["top","tops"]
                    [Daiouzyou]_hibachi_4.xml      ["top","tops","topt"]
3  ★番号が飛ぶ      [OtakuTwo]_self-0012.xml       ["top1","top5","top6"]
4  ★裸の top と同居 [OtakuTwo]_self-1020.xml       ["top","top2"]
```

**`tops` / `topt` は本当に入口**（`<bulletml>` の直下にあり、`actionRef` から
参照されていない。planner が現物で確かめた）。**拾いすぎではない。**

★★★**「`top1`, `top2`, … を順に」という直し方は、この 4 つを覆えない。**
**「文書順に全部」なら 4 つとも覆える。**
ただし `wana.xml` は**いま `top2` が走っている**ので、
「番号順」に変えるとこの 1 本だけ挙動が変わる。**どちらを選ぶかで動く先が違う。**

★**同じ名前のファイルが 2 本ある**（`[Psyvariar]_X-A_boss_winder.xml`）。
中身が違うのでハッシュでは潰れない。**ファイル名で数えると 22 本に見える。**

### 7 の当てる先も、同じ物差しで数えた —— 7 本

9 を 23 本と数えたのに 7 を数えていなかったので、**同じ分母で当て直した。**

```
一意な xml 227 本のうち
  speed type="relative" を changeSpeed の ★外 に持つ   ★7 本（3.1%）  ← 直すと動く先
                            changeSpeed の 中 に持つ      7 本         ← 動かない
                            両方持つ                       2 本
```

★**`changeSpeed` の中の `relative` は動かない。** 仕様も実装も
「現在の弾の速度との差分」で一致しているので、直す対象は**外に在る 7 本だけ。**

```
[Bulletsmorph]_aba_1.xml          外 2
[Bulletsmorph]_aba_3.xml          外 1
[Bulletsmorph]_aba_7.xml          外 1（中にも 1）
[Original]_air_elemental.xml      外 3
[Original]_hajike.xml             外 4
[Original]_kunekune.xml           外 1
[OtakuTwo]_self-0012.xml          外 2（中にも 2）
```

### 突き合わせた結果のまとめ

```
7  ★仕様が決めていて実装が違う。直す向きは 1 本。同じ関数に正解が写っている
2  ★仕様の字面とは合わない。ただし「フレームの数え方」を先に決める必要がある
4  仕様は無言。実装の意図（タイポ）は読める
9  仕様は無言。top というラベル自体が仕様に無い
   ★ただし当てる先は 23 本（10%）あり、直し方の選択で動く先が変わる
```

★**7 と 9 で「直しやすさ」の意味が違う。** 同じ分母で数えるとこうなる。

```
      向き                          当てる先（一意 227 本のうち）
7     ★1 本（仕様が決めている）      ★7 本（3.1%）
9     ★2 本以上（仕様が無言）        ★23 本（10.1%）
```

**「直しやすい」と「効く」は別。** 7 は迷わず直せるが動く先が小さく、
9 は動く先が 3 倍あるが、**どう直すかを先に決めないといけない。**

### ★ 道具の出力を 1 回で信じかけた

英語版の `speed` を最初に引いたとき、要約が
**「relative は previous fire を基準」**と返してきた。日本語版の「その弾の速度」と
食い違うので、**「日英で仕様が割れている」と書きかけた。**

同じ URL に「全文を引用して」と当て直したら、こう返ってきた。

```
"In case of the type is "relative" ... If not, the speed is relative to the speed of this bullet.
 In case of the type is "sequence" ... If not, the speed is relative to the speed of the previous fire."
```

★★**1 回目の要約が relative と sequence を取り違えていた。**
**日英は一致していた。** 割れていたのは道具の出力のほうだった。

★**「2 つの資料が食い違った」と思ったら、まず同じ資料を別の引き方で 2 回 読む。**
（[[verify-by-breaking]] の「新しい道具は既知の 2 点で目盛りを合わせる」の、
要約する道具に当たる形）

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
              ★いま      直す前（8 の段階 1 より前）
一意な弾幕     227 本      227 本
  撃った       210 本      210 本   ← 変わらない
  撃たなかった   6 本        6 本   ← 変わらない
  落ちた        11 本        4 本
  避けた         0 本        7 本（StackOverflow で捕まえられなかったもの）
```

### 避けていた 7 本 —— 8 が実物で当たった（★いまは例外で出る）

**合成した入力ではなく、同梱のサンプルが 8 の無限再帰を踏んでいた。**
`[ESP_RADE]_round_123_boss_izuna_fan.xml` ほか 6 本。

見つけ方は「処理する前にファイル名を控えへ書き、落ちたあと最後の行を読む」。
`StackOverflow` は捕まえられないので、これ以外に特定する道が無かった。

そのあと静的に閉路を数える道具を書いて 7 本を先に出し、避けるリストに入れた。
**静的に出た 7 本が、実際に落ちるものを過不足なく覆っていた。**

★**[8 の段階 1](#段階-1-を直した--打ち止めを入れた) を入れたので、リストは空にした。**
7 本は「落ちた」に移り、**輪になっている label まで例外のメッセージに出る**ようになった。
**まだ動くようにはなっていない**（段階 2 待ち）。

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

`mapEval` の `ToString("F10")` は [6](#6-小数点がカンマのカルチャでは式の評価が落ちる直した) の
出どころの 1 つでもあった。**同じ 1 行に 2 つの不具合が載っている。**

★**6 のほうは直した**（`CultureInfo.InvariantCulture` を渡した）。
**11 はそのまま残っている** —— 6 で直したのは「どの書き方で文字列にするか」で、
11 は「そもそもここで数へ潰すか」の話。**同じ行に居るが別の判断になる。**

控え: `freeze-direct-rank` / `freeze-direct-rand` / `freeze-ref-noparam-rank` /
`freeze-param-rank` / `freeze-param-rank-expr` / `freeze-param-rand` /
`freeze-rand-within-loop` / `freeze-original-flag`（`RefParamFreeze.fs`）

---

## 11 を直した —— `param` を文字のまま渡す（2026-08-31）

### 向きは「型の形」が決めていた

仕様は評価のタイミングを書いていない（planner が日英 2 版 当てて、
動詞は「置き換える / replaced」で評価とは書いていないと確認）。
**決め手は型のほうだった。**

```fsharp
type Params = string list          ★文字のまま持ち回ると宣言している
Param.replace                      ★文字の置き換え
mapEval                            ★getValue で数へ潰してから replace に渡していた
```

**型と実装が食い違っていた。** 潰さないほうが型の形と合う。

### 直した形

`mapEval` を恒等関数にした。`expandBulletRefOnce` / `expandActionRefOnce`（8 で足した
2 か所）も同じ形だったので、**5 か所とも文字のまま渡すようにした。**

### ★★★ 心配していた代償は、起きなかった

この節に **「`$1` を何度も使う `action` では、いまは 1 回ぶんの値が共有されているが、
文字のまま渡すと使うたびに転がることになる」** と書いていた。
**直す前に控えを足して固定し、直したあとで見た。**

```
                直す前                    直した後
+b1 d=0.873  →  ★d=0.175
+b2 d=0.873  →  ★d=0.175
+b3 d=0.873  →  ★d=0.175
```

★**扇はばらけなかった。** 3 発とも揃ったまま、値だけが変わった。
**同じフレームの中では `$rand` が 1 回しか転がらない**ので、
「使うたびに転がる」は起きない。

★★**代償を測る控えを、直す前に置いておいたから分かった。**
置いていなければ「たぶんばらける」のまま直すか、直さないかのどちらかだった。

### 測り直した結果

```
                          直す前        直した後
freeze-param-rank         0.200 のまま → ★0.700 に追随
freeze-param-rank-expr    3.000 のまま → ★8.000 に追随
freeze-rand-within-loop   0.200 0.200  → ★0.500 0.900（ひと回りの中でも転がる）
freeze-param-reused       揃ったまま値が変わった（上）
corpus-smoke              撃った 218 / 撃たなかった 5 / 落ちた 4   変わらない
```

### ★ `$rand` を特別扱いする仕組みが丸ごと要らなくなった

`existRandomParam` は「`$rand` を含む `ref` があれば `Original` に生の XML を持たせ、
`Init()` が毎周 作り直す」ための仕組みだった。**`$rand` だけを助ける迂回路。**

**消して測った。**

```
挙動の控え 85 本   ★全部 緑のまま
落ちた控え  1 本   ★freeze-original-flag（機構そのものを測っているもの）
```

★**挙動の控えが 1 本も動かない＝もう仕事をしていない。** `existRandomParam`（36 行）を
消した。呼び出しは 0 件になっていた。`Original` プロパティは公開 API なので残してある
（`Some` になる経路が消えただけ）。

★★**「消しても赤くならない」を、消す前に測ってから消した。**

### 当てる先

```
一意 227 本 / param を持つ 135 本 のうち
  param に $rank だけ       ★32 本  ← 追随するようになった
  param に $rank と $rand    ★6 本  ← 同上
  param に $rand だけ        24 本  （前から Original で毎周 作り直されていた）
  どちらも無い               73 本  （数なので潰しても同じ）

★動く先の合計 38 本（16.7%）
```

**これまでの札でいちばん大きい**（9 の 23 本を超える）。

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
置いた理由は**リファクタリングで解決の順序が変わっても誰も気づかないから**で、
控えの名前に `now-` を付けてある。

### 実例の数 —— 範囲を締める（最初「同梱に 0 本」と書いていた）

planner の指摘で数え直した。**0 本は正しいが、範囲は `samples/` だけだった。**

```
samples/ だけ      906 ファイル → 一意 227 本 → 重複ラベル ★0 件
リポジトリ全体     1079 ファイル → 一意 376 本 → 重複ラベル ★2 件
```

★**「同梱の 227 本には 0 本」は `samples/` の話。**「どこにも無い」とは書けない。
道具は `atelier-shared/duplabel.mjs`（種別つきで数え、入れ子か兄弟かも出す）。

出てきた 2 本はどちらも**パース用の fixture**で、しかも**形が違う**。

```
tests/TestData/xml/bulletRef/elements/success/bulletRef-param-nothing.xml
tests/TestData/xml/fireRef/elements/success/fireRef-param-nothing.xml
  どちらも <action label="top"> の中に <action label="top">   ★入れ子
```

**上の控えは兄弟に 2 つ置いた形**なので、実在するほうを覆っていなかった。

### 入れ子のほうも固めた（控え `now-duplicate-label-nested`）

この 2 本を使っているのは `XmlParse.fs` と `OtherParse.fs` の**パース経路だけ**で、
**どちらの `top` が走るかは既存の 346 件が 1 件も見ていない**（planner は未確認としていた。測った）。

凍結した予測（経路つき）と実測。

```
予測  ★外側が勝つ
      getAction（IntermediateParser.fs:721）が list@[recBulletml]@getChildren2 と
      自分を子より先に置く行きがけ順なので、平らにした並びで外側が先に来る
      tryFindAction（:737）はそこへ List.tryFind を当てるだけ

実測  +b1 d=0.000 s=1.000   ← 外側にしかない fire
      +b2 d=1.571 s=9.000   ← 内側の fire
      ★両方。外側が解決され、その中身として内側も走った
```

**外側が勝つと、内側は「到達しない」のではなく「外側の一部として走る」。**
兄弟のとき（後ろが 1 度も走らない）とは結果が違う。**同じ「先勝ち」でも見え方が別。**

★**帰結（planner）。内側の `label` は、どこからも指せない。**
`convertRefBulletml` は `tryFindAction` に**文書全体**（`topRecBulletml`）を渡すので、
`<actionRef label="top"/>` をどこに書いても必ず外側へ解決される。
**書いてあるのに使えない識別子。** 1 回の解決ではなく恒久的。

★★**そして `action` だけではない。3 つとも同じ形だった**（planner が母集団を数えた。
こちらでも数え直して、`IntermediateParser.fs` にこの形は**ちょうど 3 か所**）。

```
721  getAction   list@[recBulletml]@getChildren2   ← 最初に読んでいた 1 本
765  getFire     ★同じ
809  getBullet   ★同じ
```

**入れ子の同名 `label` は、種別によらず内側が指せない。**
最初は 1 本だけ読んで `action` の話として書いていた。
[[observation-vs-inference]] でいう **1 経路を測って経路全体へ広げる**の、逆の形 ——
**広げてよい所を、母集団を数えずに狭く書いていた。**

```
その actionRef が 外側の部分木の中   → 外側へ解決 → ★自己参照。8（輪）に落ちる
その actionRef が 外側の外           → 外側へ解決 → 輪にはならないが内側は指せない
```

**どちらもコーパスに実例が無い**（この 2 本に `actionRef label="top"` は無い）ので、
控えは足していない。**上の枝は走らせていない** —— 走らせると
[8](#8-相互参照でスタックが溢れプロセスごと落ちる段階-1-を直した) でプロセスごと落ちるため。
**コードから言えること**であって、測ったものではない。

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

## 13 は 1 つの札ではなく 2 つだった（2026-08-31）

### ★★★ 原典のスキーマが同梱に入っていた

`license/bulletml/relax/bulletml.rlx`（ABA Games の RELAX。ライブラリと一緒に配られたもの）。

```
<tag name="bulletml">
  <attribute name="xmlns" type="string"/>
  <attribute name="type" type="string">   （none / vertical / horizontal）
</tag>
★これで終わり。name も description も宣言が無い
```

**この repo の `DTD.fs` のコメントと完全に一致する。**
★**「DTD が緩い」のではなく「原典がそう決めている」。**
**`name` / `description` は BulletML の属性ではない。**

★★**落としているのは writer ではない。writer が原典どおりで、parser が緩い。**

### 当てる先を数え直した —— 「サンプル 4 本」ではなかった

```
<bulletml name="…"> に値を持つ弾幕        ★3 本（2wayRight / 5way / homing）
<bulletml description="…"> に値を持つ弾幕 ★0 本
.Description の呼び出し（repo 全体）      ★0 件（定義 1 件のみ）
.Name の呼び出し                          サンプル 4 本
```

★**`<bulletml>` タグは複数行にまたがるので、行単位の grep では落ちる**
（planner が行で 2 本・タグ全体で 3 本と食い違った。engineer も数え直して 3 本で一致）。

★★**この節に「サンプル 4 本とも名前が空になる」と書いていたが、正確ではない。**

```
言える   ★3 本の弾幕を選んだときだけ、画面の題名が消える
言えない サンプル 4 本の表示が壊れる（残り 224 本はいまでも空文字が出ている）
```

**当てる先は「サンプルの本数」ではなく「値を持つ弾幕の本数」だった。**

### `description` は直さないことにした —— ★直しても出力が変わらない

「parser が読むのをやめる」を一度 入れて、**測って戻した。**

```
直す前   parser が読む     → Description = Some → writer は書かない → ★往復で消える
直した後 parser が読まない → Description = None → writer は書かない → ★往復で消える
```

★**出力は同じ。** 変わるのは `Description` の返り値だけで、
**repo 内に呼び出しは 0 件、repo の外には破壊的。** **害だけあって益が無い。**

★**「向きが 1 本」と「直す価値が在る」は別だった。**
向きは 1 本に絞れたが、**その 1 本を通しても何も良くならない。**

★★**この節の前提が、あとで変わった。** 下の「2 つとも writer が書く」を見ること。

### `name` だけが判断として残る

```
name  値を持つ弾幕 ★3 本 / サンプル 4 本のコードが .Name を使う / 原典に宣言 0 件

  A  parser が読むのをやめる  → ★3 本の題名が空になる。往復は一致する
  B  writer が書く            → 原典に無い属性を出力する。往復は一致する
  C  いまのまま               → 往復で消える。3 本の題名は出る
```

★**`description` と違って、`name` は A と B で出力が変わる。** ここは判断が要る。

---

## 13 を直した —— `name` と `description` を writer が書く（2026-08-31）

### ぜくるの決定

**B（`parser` に合わせる ＝ `writer` が書く）。** 外への影響が無い側。

★**そして、この節が「原典に無い属性を出力する」と書いていた前提が外れた。**
ぜくるが `DTD.fs` と `IntermediateParser` に書いた ——

> `description` は BulletML 公式の属性ではない。
> **BulletML の名前/説明文を格納するための属性として追加した。**

★★**原典に無いのは事実だが、「無いのに出す」のではなく
「この実装が意図して足した拡張」だった。** それなら書くのが筋になる。

### `description` も揃えた

`name` を入れた時点で、**同じ parser が読む兄弟なのに片方だけ往復する形**になった。

```
name         parser が読む → writer が書く      ★往復する
description  parser が読む → writer は書かない  ★往復で消える
```

★**入れた理由（「parser が読む属性は writer も書く」）を、そのまま隣に当てると
`description` も書くことになる。** ぜくるの判断で 2 つとも書くようにした。

★★**上の「`description` は直さないことにした」は取り消し。**
あのときの根拠は「直しても出力が変わらない」だったが、
**測っていたのは `parser` を止める向きだけ**で、`writer` に書かせる向きは測っていない。
★★★**3 択のうち 1 つしか測らずに「直す価値が無い」と締めていた。**

### 測った結果 —— 4 条件とも往復する

```
入力にある属性        直す前の DTD 経由       いま
xmlns + type          一致                   一致
+ name                ★ちがう（name が消える）  ★一致
+ description         ★ちがう                 ★一致
+ name + description  ★ちがう                 ★一致
```

`XmlNode` 経由は前から 4 条件とも一致していた（**経路で割れていた**のが揃った）。

書く順は `xmlns` → `type` → `name` → `description`。
**同梱の弾幕が `type` のうしろに `name` を置いている**ので、そこに合わせた。

```
網    Core 87 件 ＋ Parser 346 件 すべて緑。控え 87 本
本体  src/FsBulletML.Core/DTD.fs  8 行
控え  now-roundtrip-bulletml-attrs.txt
```

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

## リファクタリング前の網は足りているか（2026-08-31・測った）

ぜくるの問い。**測ってから答えた。結論は「機構ごとには強いが、実物の弾幕には弱い」。**

### いちばん大きい穴 —— 走らせた軌跡を捨てていた

```fsharp
// 直す前の corpus-smoke
let t = Trace.run xml 60
if t.Contains "  +b" then ok <- ok + 1 else silent.Add name
```

★**227 本ぶんの軌跡を計算してから、真偽値 1 個に潰して捨てていた。**

```
実物の弾幕          227 本
  値まで固めていた   ★十数本   real-speed-relative-7（7）/ real-unresolved-param（2）/ FireShapes の数本
  「撃ったか」だけ    ★残り 200 本超
```

★★**推測ではなく実測がある** —— [2 を直した](#2-を直した--wait-の初期化の入口が-2-つあった2026-08-31)とき、
**控えが 17 本 動いたのに `corpus-smoke` の 3 つの数は 1 つも動かなかった。**
**Core の挙動が全面的に変わっても、この門は緑のまま通る。**

### 直した —— 同じ走行から指紋を残す（走行は増えていない）

`corpus-smoke` と `corpus-trace` が **1 回の走行を共有する**（`lazy`）。

```
corpus-smoke  撃った／撃たなかった／落ちた／避けた の数と名前   広く浅い
corpus-trace  1 本ずつの 弾数・生存数・軌跡の指紋              広く深い
```

★**追加コストは測って 0 秒**（`corpus-smoke` 単体で 8 秒、Core 全体 11〜12 秒。
`corpus-trace` を足したあとも 11〜12 秒）。**新しく走らせるものが 1 つも無い。**

★**指紋だけだと「何が変わったか」が読めない**ので、弾の数と最終フレームの生存数を
先に出す。**2 つが同じで指紋だけ動いたら、弾の数は変わらず値が動いた**と分かる。

### ★★★ 壊して確かめた —— 門が本当に落ちるところを見た

`wait` の `+1` を戻して（[2](#2-wait-の最初の-1-周だけ-1-フレーム短い直した) を再発させて）当てた。

```
corpus-trace  ★失敗   ← 捕まえた
corpus-smoke   成功    ← 前と同じく無反応
```

★**2 行目で影響範囲が読める。**

```
控え  撃った弾の合計 76929 発 ／ 最終フレームに残っていた合計 73755 本
いま  撃った弾の合計 72244 発 ／ 最終フレームに残っていた合計 69374 本   ★6% 減
```

**「17 本の控えが動くのに smoke は無反応」だった変更が、`corpus-trace` では赤くなる。**
★**これが入る前と後で、リファクタリングの安全網の意味がいちばん変わる。**

### ついでに踏んだ —— 並べ直す鍵で順序が変わる

控えを作り直したら `corpus-smoke` の「撃たなかったもの」の並びが変わった。

```
フルパス（\ 区切り）  ...\Enemy\move.xml  vs  ...\EnemyBullet\...   '\'=92 > 'B'=66  → EnemyBullet が先
相対パス（/ 区切り）  ...Enemy/move.xml   vs  ...EnemyBullet/...    '/'=47 < 'B'=66  → move.xml が先
```

★**元のコードは「フルパス順で回して、出すときに相対パスで並べ直す」形**だった。
回した順のまま出したら逆になる。**同じ集合でも、並べ直す鍵の区切り文字で順序が変わる。**
`Name`（`/` 区切り）で 1 回 並べ直して、両方の控えで揃えた。

### 残っている穴（直していない）

```
フロント（MonoGame / Unity2D / C# サンプル）  ★網 0 件
    run が返すのは差分で、呼ぶ側の係数が 1 倍と 1/100 で違う（3 で見つけた形）
    Core を安全に直しても、ここで壊れたら出ない

性能                                          ★網 0 件。遅くなっても分からない

カバレッジ                                     ★測れていない
    coverlet.collector が入っていないので --collect の出力が空になる
    どこが掛かっていないかを数字で持っていない（推測で話している）

明示的に未測定の枝                              2 つ
    BulletRunner の Tasks=null（3）／ IntermediateParser の maybe の | None ->（5）
```

---

## 測り方について

- 継ぎ目は既にインターフェースとして空いていたので、**網を張るあいだ本体は 1 行も触っていない**（8 の段階 1 は、網が揃ったあとで入れた）
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
