(**
FsBulletML(Beta)
==================================

これは何？
-------------
``FsBulletML``は、弾幕記述言語``BulletML``の``F#``実装です。  
  
判別共用体(Discriminated Unions)で弾幕を記述できる内部DSLを提供します。
また、``XML``形式、``SXML``形式、``FSB``形式(オフサイドルールの独自形式)の外部DSLを読み込んで実行することもできます。

  ![sample1](content/images/sample1.png "sample1")![sample2](content/images/sample2.png "sample2")![sample3](content/images/sample3.png "sample3")
*)

(*** hide ***)
#I "bin/Debug"
#r "FsBulletML.Core.dll"

(**
特徴
-------------
 - 判別共用体で弾幕を記述できます。(内部DSL)

*)
open FsBulletML

/// 全方位弾
let sample = 
    Bulletml({ bulletmlXmlns = None; bulletmlType = Some ShootingDirection.BulletVertical;},
        [BulletmlElm.Action ({actionLabel = Some "circle";},
            [Action.Repeat
                (Times "$1",
                Action ({actionLabel = None;},
                    [Fire ({fireLabel = None;},
                      Some (Direction (Some {directionType = DirectionType.Sequence;},"360/$1")),
                      None,
                      Bullet ({bulletLabel = None;}, None, None,[]))]))]);
          BulletmlElm.Action ({actionLabel = Some "top";},
            [Action.Repeat
                (Times "30",
                ActionElm.Action ({actionLabel = None;},
                    [Action.ActionRef ({actionRefLabel = "circle";}, ["20"]); Wait "20"]))])])

(**
 - ``XML``形式で弾幕を記述できます。 (外部DSL)
*)

<!--全方位弾-->
<bulletml>
  <action label="circle">
    <repeat>
      <times>$1</times>
      <action>
        <fire>
          <direction type="sequence">360/$1</direction>
          <bullet/>
        </fire>
      </action>
    </repeat>
  </action>
  <action label="top">
    <repeat>
      <times>30</times>
      <action>
        <actionRef label="circle">
          <param>20</param>
        </actionRef>
        <wait>20</wait>
      </action>
    </repeat>
  </action>
</bulletml>


(**
 - ``SXML``形式で弾幕を記述できます。(外部DSL)
*)

(bulletml
  (action (@ (label "circle"))
    (repeat
      (times "$1")
      (action
        (fire
          (direction (@ (type "sequence")) "360/$1")
          (bullet)))))
  (action (@ (label "top"))
    (repeat
      (times "30")
      (action
        (actionRef (@ (label "circle"))
          (param "20"))
        (wait "20")))))

(**
 - ``FSB``形式(オフサイドルールの独自形式)で弾幕を記述できます。(外部DSL)
*)

bulletml
  action label="circle"
    repeat
      times:"$1"
      action
        fire
          direction type="sequence":"360/$1"
          bullet
  action label="top"
    repeat
      times:"30"
      action
        actionRef label="circle"
          param:"20"
        wait:"20"

(**
 - 敵弾、敵機の動作に加えて、自機弾も``BulletML``で記述することができます(互換性を保った仕様拡張)。
*)

(**
 - Bulletml判別共用体の記述を容易にするためのコードスニペットを提供しています。
  
<div style="text-align:center;">
    <object width="560" height="315">
        <param name="movie" value="http://www.youtube.com/v/nJYQqAm_iD0"></param>
        <embed src="http://www.youtube.com/v/nJYQqAm_iD0" width="560" height="315" />
        <noembed>プラグインが必要です。</noembed>
    </object>
</div>

   **※拡張機能 F# snippetを導入する必要があります**  
   [F# snippet extension - Visual Studio Gallery - Microsoft] [1]  
   [F# code snippet + snippet management for Visual Studio 2012 Addon - Apollo 13 - Tao Liu's blog] [2]  
   [すべての F# ユーザーが今すぐ導入すべき拡張機能 F# snippet を導入しよう - Bug Catharsis] [3]  

*)


(**
 - 外部DSLは``C#``や``VB``からも使えます。``F#``で定義した内部DSLを``C#``や``VB``から呼び出すこともできます。
*)

(**
インストール
-------------

内部DSLを利用するには、``FsBulletML.Core``をインストールします。

<div class="row">
  <div class="span1"></div>
  <div class="span6">
    <div class="well well-small" id="nuget">
      FsBulletML.Coreは<a href="https://nuget.org/packages/FsBulletML.Core" target="_blunk">NuGet</a>からインストールすることができます。
      <pre>PM> Install-Package FsBulletML.Core</pre>
    </div>
  </div>
  <div class="span1"></div>
</div>


外部DSLを利用するには、``FsBulletML.Core``に加えて、``FsBulletML.Parser``をインストールします。

<div class="row">
  <div class="span1"></div>
  <div class="span6">
    <div class="well well-small" id="nuget">
      FsBulletML.Parserは<a href="https://nuget.org/packages/FsBulletML.Parser" target="_blunk">NuGet</a>からインストールすることができます。
      <pre>PM> Install-Package FsBulletML.Parser</pre>
    </div>
  </div>
  <div class="span1"></div>
</div>
*)

(**
Demo
-------------
<div style="text-align:center;">
  <object width="560" height="315">
      <param name="movie" value="http://www.youtube.com/v/sfgqZYDrWG8"></param>
      <embed src="http://www.youtube.com/v/sfgqZYDrWG8" width="560" height="315" />
      <noembed>プラグインが必要です。</noembed>
  </object><br>
  サンプルプログラム(<a href="https://github.com/zecl/FsBulletML/tree/master/samples" target="_blunk">source</a>)は<a href="http://monogame.codeplex.com/" target="_blunk">MonoGame</a>で動かしています。弾の破棄はガベージコレクションに丸投げです。
</div>
*)

(**
ご利用上の注意
-------------
 - パフォーマンスがよろしいとは言えないので鬼畜系弾幕は厳しいかもしれません。
 - ``ActionRef``, ``FireRef``, ``BulletRef``要素の相互参照に対応していません。
 - APIが固まっていないBeta版扱い。なので、バージョンアップによって破壊的変更がされる可能性があります。
*)

(**
今後の課題とか（予定は未定）
-------------
 - テストが雑。現状ざっくりとしたパーサのテストしかない。
 - ``BulletML``の構文チェックが中途半端。
 - より型安全な弾幕記述のサポート。
 - モナディックに弾幕を構築する仕組みとか。``CustomOperationAttribute``を利用したコンピュテーション式とか。
 - ``SXML``形式のパーサの改善。行コメントのサポート。
 - ``FSB``形式のパーサの改善。行コメントのサポート。
 - APIの改善とか。``C#``等からの使いやすさの向上とか。
 - パフォーマンスの向上とか。
 - ``BulletML``の仕様と互換を持たせつつ、独自仕様を追加。
 - ``BulletML`` 型プロバイダーとか。(``F#``の型を作れるようにならないとツライ)
 - 別枠でカスタム ビジュアライザーの提供とか。

 [1]: http://visualstudiogallery.msdn.microsoft.com/d19080ad-d44c-46ae-b65c-55cede5f708b  "F# snippet extension - Visual Studio Gallery - Microsoft"
 [2]: http://apollo13cn.blogspot.jp/2012/06/f-code-snippet-visual-studio-2012-addon.html        "F# code snippet + snippet management for Visual Studio 2012 Addon - Apollo 13 - Tao Liu's blog"
 [3]: http://zecl.hatenablog.com/entry/20130317/p1 "すべての F# ユーザーが今すぐ導入すべき拡張機能 F# snippet を導入しよう - Bug Catharsis"
 
*)
