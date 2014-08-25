(**

Release Notes
==================================

FsBulletML.Core
-------------
0.8.9
-------------
- Xml読込みについて .NET Framework4.0 と .NET Framework3.5 の　APIを揃えた。


0.8.8
-------------
- ``Unity``からの利用を考慮してAPIと機能を変更(breaking change)
    - ``BulletmlRunner.run``にて、弾の座標(X,Y)を直接変更しないようにし、移動分の値を返すよう変更。
    - ``BulletmlRunner.run``にて、Tupleではなく``RunResult``型を返すよう変更。
    - ``BulletmlInfo``型の``BulletmlTask``および``BulletmlTaskOption``をメソッドへ変更。

0.8.7
-------------
- NuGet packageの不具合対応。``FSharp.Core``を特定のバージョンを利用するよう修正。


0.8.6
-------------
- bug fix
- ``BulletmlInfo`` struct追加
- ※``FsBulletML.Bullets``の弾幕定義で``BulletmlInfo`` structを利用するよう修正。
- ※``FsBulletML.Bullets``の弾幕名変更(C#から参照可能)。
- ※MonoGame(C#)サンプル ``FsBulletML.Sample.MonoGame.CSharp``を追加
- ※MonoGame(F#)サンプル ``FsBulletML.Sample``を``FsBulletML.Sample.MonoGame.FSharp``へ変更


0.8.5
-------------
- bug fix
- ※``FsBulletML.Sample``: 弾を進行方向を反映して描画するよう修正。


0.8.4
-------------
- ``XML``形式の外部DSLを``FsBulletML.Core``へ含めるよう修正。``FsBulletML.Parser``では``SXML``形式、``FSB``形式のみ外部DSLを追加拡張する。


0.8.3
-------------
- ``FsBulletML.Core``を.NET Framework3.5以上に対応
- ``FsBulletML.Parser``は.NET Framework4.0以上に対応


0.8.2
-------------
- ``StructuredFormatDisplay``属性による``Bulletml``判別共用体の文字列化対応。Bulletml.ToNodeString()メソッドで文字列を取得可。これにより``XML``形式等から、内部DSLのコードに変換可能に。


0.8.1
-------------
- 最初のリリース



FsBulletML.Parser
-------------
0.8.5
-------------
-- Sxml,FSBのパース処理をModuleからアクセスできるようにした


0.8.1
-------------
- 最初のリリース


FsBulletML.TypeProviders
-------------
0.9.1
-------------
- 最初のリリース


*)
