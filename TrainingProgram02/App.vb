Imports System.Configuration

Public Class App

    '2重起動確認
    Public Shared Function DuplicateCheck() As Boolean
        DuplicateCheck = False

        'ミューテックスインスタンス生成
        Dim AppMutex As New System.Threading.Mutex(False, Application.ProductName)

        If AppMutex.WaitOne(0, False) Then

            MessageBox.Show("2重起動確認完了。問題ありません。")
        Else

            MessageBox.Show("既にアプリケーションが起動しています。")
            Application.Exit()
        End If

        'ガベージコレクションの対象から除外
        GC.KeepAlive(AppMutex)

        'ミューテックスオブジェクトを破棄
        AppMutex.Close()

        DuplicateCheck = True
    End Function

    'キーの存在確認
    Public Shared Sub ConfigCheck()

        '取得するキー名を配列で配置
        Dim KeyNameArry As String() = {"filePath", "bk_DirPath", "outputPath"}

        'すべてのキーを取得して、配列のキー名の存在を確認
        Dim AllKeys As String
        For Each AllKeys In System.Configuration.ConfigurationManager.AppSettings.AllKeys

            If AllKeys <> KeyNameArry(0) And AllKeys <> KeyNameArry(1) And AllKeys <> KeyNameArry(2) Then

                MessageBox.Show("ファイルに指定されたキーが存在しません" & vbLf & "configファイルの内容を確認してください" & vbLf & "キー：" & AllKeys)

            End If

        Next

        'フォルダの存在を確認
        Dim FolderName As String = ConfigurationManager.AppSettings("filePath")
        If System.IO.Directory.Exists(FolderName) Then
            MessageBox.Show("'" + FolderName + "'は存在します。")
        Else
            MessageBox.Show("configファイルに指定されたフォルダが存在しません。" & vbLf & "configファイルを確認してください" & vbLf & "パス：" & FolderName)
        End If

    End Sub

End Class
