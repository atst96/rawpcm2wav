# RawPCM2WAVTool

PCMデータ(44.1kHz／16bit)のデータをWAVEファイルに変換します。

## 使い方
コマンドライン引数に変換対象データのファイルパスを指定して実行してください。  
実行後にファイルと同じディレクトリにwavファイルが作成されます。    
  
例: `aaa.bin`、`bbb.bin`を変換する場合。
```bat
./RawPCM2WAVTool.exe C:\aaa.bin D:\bbb.bin
```

出力:
```
C:\aaa.wav
D:\bbb.wav
```
