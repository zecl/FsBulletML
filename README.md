FsBulletML
========
`FsBulletML`は、弾幕記述言語`BulletML`の`F#`実装です。

判別共用体(Discriminated Unions)で、シューティングゲームの弾幕を記述するためのinternal DSLを提供します。
また、`XML`形式、`SXML`形式、`FSB`形式(オフサイドルールの独自形式)のexternal DSLを読み込んで実行することもできます。

詳しくは、[プロジェクトのホームページ](http://zecl.github.io/FsBulletML/)をご覧ください。
（サイトの一部は Unity Web Player 時代の記述が残っています。現行のビルド手順はこの README を優先してください。）

## 動作環境

現行のビルドは **.NET 10** です。SDK は `global.json` で `10.0.302`（`rollForward: latestFeature`）を指定しています。

| 対象 | 内容 |
| --- | --- |
| ライブラリ | `FsBulletML.Library.sln` (Core, Parser, Parser.Tests, MonoGame, Bullets) |
| Core | `net10.0` と `netstandard2.1` |
| Parser | `net10.0` |
| MonoGame | `MonoGame.Framework.DesktopGL` **3.8.5**、`net10.0` |
| Unity 2D サンプル | **Unity 6**（6000.x）。C# サンプルは Editor で Play 可能 |
| TypeProviders / Docs | まだ旧 .NET Framework 向け（未移行） |

旧 `FsBulletML.sln` は packages.config / .NET Framework 時代のソリューションです。普段のビルドとテストには使いません。

Unity Web Player は廃止済みです。ブラウザ埋め込みデモではなく、Unity Editor でサンプルプロジェクトを開いてください。

## ビルドとテスト

```
dotnet test FsBulletML.Library.sln
```

## サンプル

- MonoGame (F#): `samples/FsBulletML.Sample.MonoGame.FSharp`
  `dotnet run --project samples/FsBulletML.Sample.MonoGame.FSharp`
- MonoGame (C#): `samples/FsBulletML.Sample.MonoGame.CSharp`
- Unity 2D (C#): `samples/FsBulletML.Sample.Unity2D.CSharp`。Unity 6 でこのフォルダを開く。本編シーンは `Assets/Senes/FsBulletML.Sample.Unity2D.unity`（フォルダ名 `Senes` は元からの誤記）
- Unity 2D (F#): `samples/FsBulletML.Sample.Unity2D.FSharp`。ゲームプレイ DLL を `Assets/FsBulletML` に入れて使う

Unity の `Logs/` と `UserSettings/` は git 管理対象外です。
