Imports System.Configuration
Imports System.IO
Imports System.Reflection.Emit
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class frmNumberConversion

    '画面起動時
    Private Sub frmNumberConversion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '・アプリケーションの2重起動を確認
        App.DuplicateCheck()

        '・設定ファイルとの相違を確認
        App.ConfigCheck()

    End Sub

    '実行ボタン押下時
    Private Sub btnExecution_Click(sender As Object, e As EventArgs) Handles btnExecution.Click

        'ファイルの存在を確認
        If Tasks.FileExistCheck() = False Then
            Exit Sub
        End If

        '変換→出力処理
        Tasks.Output()

    End Sub

    '終了ボタン押下時
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

        'アプリケーションを終了
        Application.Exit()

    End Sub

End Class