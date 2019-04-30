# unDavah
![動作イメージ](./Document_images/README.md/ScreenImage.gif)
## unDavahって何？
unDavahは、PuTTYやWindows 10でのコマンドプロンプトなど、右クリックが即ペーストな環境で安全に作業するための小さなツールです。
現在のところ、AutoHotkeyから呼び出す形式をとっています。 
## どんな役に立つの？
クリップボードに改行を含む文字列が入った状態でうっかりPuTTYのウィンドウで右クリック→一部コマンドとして実行される、という悲劇を防ぐために、いったんダイアログを表示して確認を求めます。
## 動作
1. 呼び出された直後に、マウスポインタの位置を記憶します。
2. 確認を求めるダイアログを表示します。
3. OKボタンを押されたときのみエラーコード0で正常終了します。その際にマウスポインタの位置を元に戻します。(それ以外はエラーコード1)

これを利用してAHK側で、呼び出す前にunDavahが必要かどうかの判定を、終了後に右クリックする処理を行います。
## なぜ作ったか
元々はAHKスクリプトのみで実現していた機能ですが、マルチモニタ環境でモニタのスケーリングが混在している場合にうまく動作しないことがありました。調べてみると、マウスポインタの座標がうまく扱えていない様子ですし、また長大なテキストがクリップボードに入っているとダイアログが画面からはみ出すようなこともあったため、休暇を利用して作成しました。

## AHKスクリプトのサンプル
バイナリを`C:\opt\unDavah\unDavah-PoC.exe`に配置したなら、次のように.ahkスクリプトから呼び出せます。  
```ahk
$RButton::
MouseGetPos,,, aWin
WinGetClass, aWinClass, ahk_id %aWin%
itsOK := 1

if ((aWinClass == "PuTTY") or (aWinClass == "ConsoleWindowClass") or (aWinClass == "mintty"))
{
    StringLen, length, clipboard 
    if(0 < length and RegExMatch(clipboard, "\n"))
    {
        RunWait, "C:\opt\unDavah\unDavah-PoC.exe"
        if (ErrorLevel > 0)
        {
            itsOK :=0
        }
    }
}
if (itsOK)
{
    MouseClick, RIGHT,,, 1, 0
}

return
```
## 動作環境
Windows 10のみ。
バージョン1809(Windows 10 October 2018 Update)でのみ動作確認しています。
## その他
最初のバージョンは、平成最後の日に書かれ、令和の初日に公開されました。