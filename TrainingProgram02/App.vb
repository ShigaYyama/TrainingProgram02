﻿Imports System.Collections.Specialized
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

        'すべてのキーとその値を取得して存在を確認
        Dim key As String
        For Each key In System.Configuration.ConfigurationManager.AppSettings.AllKeys

            For Each Keys As String In KeyNameArry

                If Array.IndexOf(System.Configuration.ConfigurationManager.AppSettings.AllKeys, Keys) = -1 Then

                    MessageBox.Show("ファイルに指定されたキーが存在しません" & vbLf & "configファイルの内容を確認してください" & vbLf & "キー：" & Keys)

                End If

            Next

            If System.Configuration.ConfigurationManager.AppSettings.AllKeys.Length > KeyNameArry.Length Then
                MessageBox.Show("ファイルに指定されたキーが指定数より多いです" & vbLf & "configファイルの内容を確認してください")
            End If

            'フォルダの存在を確認
            Dim FolderName As String = ConfigurationManager.AppSettings("filePath")
            If Not System.IO.Directory.Exists(System.Configuration.ConfigurationManager.AppSettings(key)) Then
                MessageBox.Show("ファイルに指定されたキーが存在しません" & vbLf & "configファイルの内容を確認してください" & vbLf & "キー：" & key)
            End If
        Next key

        'フォルダの存在を確認
        Dim FolderName As String = ConfigurationManager.AppSettings("filePath")
        If System.IO.Directory.Exists(FolderName) Then
            MessageBox.Show("'" + FolderName + "'は存在します。")
        Else
            MessageBox.Show("configファイルに指定されたフォルダが存在しません。" & vbLf & "configファイルを確認してください" & vbLf & "パス：" & FolderName)
            End If

    End Sub

End Class
