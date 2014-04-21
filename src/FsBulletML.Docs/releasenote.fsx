(**
Release Notes
==================================
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
- ※FsBulletML.Sample : 弾を進行方向を反映して描画するよう修正。


0.8.4
-------------
- ``XML``形式の外部DSLをFsBulletML.Coreへ含めるよう修正。FsBulletML.Parserでは``SXML``形式、``FSB``形式のみ外部DSLを追加拡張する。


0.8.3
-------------
- FsBulletML.Coreを.NET Framework3.5以上に対応
- FsBulletML.Parserは.NET Framework4.0以上に対応


0.8.2
-------------
- StructuredFormatDisplay属性によるBulletml判別共用体の文字列化対応。Bulletml.ToNodeString()メソッドで文字列を取得可。これにより``XML``形式等から、内部DSLのコードに変換可能に。


0.8.1
-------------
- 最初のリリース


*)
