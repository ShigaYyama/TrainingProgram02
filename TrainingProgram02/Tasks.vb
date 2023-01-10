﻿Imports System.Configuration
Imports System.Text

Public Class Tasks

    'ファイル名の配列
    Shared FileName() As String = {"En.txt", "FF4.txt", "Fn.txt", "Kn.csv"}

    'ファイルの存在確認
    Public Shared Function FileExistCheck() As Boolean

        FileExistCheck = True

        Dim FileCount As Integer = 0

        'ファイル名を確認
        For Each Names In FileName

            Dim FilePath As String
            FilePath = ConfigurationManager.AppSettings("filePath") & "\" & Names

            'File.Existsでファイルの存在確認
            If System.IO.File.Exists(FilePath) Then

                '変数でカウントを増やす
                FileCount = FileCount + 1

            End If

        Next

        'カウントが0 = ファイルが一つも存在していない時、処理を中断する
        If FileCount = 0 Then

            MessageBox.Show("処理対象のファイルが存在しません")
            Return False

        End If

    End Function


    'データ出力処理
    Public Shared Sub Output()

        'ファイルの出力中にエラーが発生するとプログレスバーが表示されたままにならないように例外処理
        Using frmProg As New frmProgress

            'プログレスバーを表示
            frmProg.Visible = True
            frmProg.lblWaiting.Text = "処理中です・・・しばらくおまちください"

            'プログレスバーの最大値を設定(ファイル名を設定した配列の長さで取得)
            Dim MaxProgNum As Integer = FileName.Length
            frmProg.progressBar.Maximum = MaxProgNum

            Dim ProgNum As Integer

            'ファイル名ごとに処理を繰り返す
            For Each Names In FileName

                'フォルダのパスをそれぞれ変数定義
                Dim CopyPath As String
                CopyPath = ConfigurationManager.AppSettings("filePath") & "\" & Names

                Dim OPPath As String
                OPPath = ConfigurationManager.AppSettings("outputPath") & "\" & Names

                Dim BUPath As String
                BUPath = ConfigurationManager.AppSettings("bk_DirPath") & "\" & Names

                Dim ConnectRow As String = Nothing
                Dim ConnectData As New StringBuilder

                'File.Existsでファイルの存在確認
                If Not System.IO.File.Exists(CopyPath) Then

                    '存在しなければ、プログレスバーの値を+1して処理を次に回す
                    MessageBox.Show(Names & "ファイルは存在していません。")
                    ProgNum = ProgNum + 1

                Else

                    Try

                        '存在したら、データを読み込む
                        Using ReadFile As New System.IO.StreamReader(CopyPath, System.Text.Encoding.GetEncoding("shift_jis"))

                            Dim StrSplit As String = Nothing
                            '拡張子の判別
                            '.csvの場合は、テキストを","で区切る
                            If System.IO.Path.GetExtension(CopyPath).ToLower = ".csv" Then
                                StrSplit = ","
                            Else
                                '.csv以外の場合は、テキストをタブで区切る
                                StrSplit = vbTab
                            End If

                            '内容を一行ずつ読み込む
                            While ReadFile.Peek() > -1

                                Dim ReadRow As String = ReadFile.ReadLine()
                                Dim StrArray() As String = Nothing

                                Dim ChangeStr As New System.Text.StringBuilder()

                                '文字列を分割して配列にセット
                                StrArray = ReadRow.Split(StrSplit)

                                '配列数(分割したテキスト数)が2つ以上の時に、数値の変換処理を行う
                                If StrArray.Length >= 2 Then

                                    '数値の変換処理(1～9までを反転して置き換える)
                                    'StringBuilderクラを用いることで処理を高速化
                                    For c As Integer = 0 To StrArray(1).Length - 1
                                        Dim SeachInt As Integer

                                        '数値を判別、文字の場合はそのまま文字として返す
                                        If StrArray(1)(c) >= "1" And StrArray(1)(c) <= "9" Then
                                            SeachInt = Integer.Parse(StrArray(1)(c))

                                            ChangeStr.Append(CStr(10 - SeachInt))
                                        Else
                                            ChangeStr.Append(StrArray(1)(c))
                                        End If

                                    Next

                                    '変換した文字列を配列の2番目に置き換える
                                    StrArray(1) = ChangeStr.ToString

                                End If

                                '配列を文字列に再度結合
                                ConnectRow = String.Join(StrSplit, StrArray)

                                '結合した文字列を、改行して表として格納
                                'StringBuilderクラスを用いることで処理を高速化
                                ConnectData.AppendLine(ConnectRow)

                            End While

                            Try

                                '格納した文字列の表をテキストへ出力
                                Using FileWrite As New System.IO.StreamWriter(OPPath, False, System.Text.Encoding.GetEncoding("shift_jis"))

                                    FileWrite.Write(ConnectData)

                                End Using

                            Catch Ex As Exception

                                MessageBox.Show("ファイルの保存に失敗しました。" & vbLf & "ファイル名：" & OPPath)
                                Exit Sub

                            End Try

                        End Using

                    Catch Ex As Exception

                        MessageBox.Show(Ex.Message & vbLf & Names & "の内容を確認してください。")
                        Exit Sub

                    End Try

                    Try

                        'INPUTフォルダのデータを、BACKUPフォルダに移動して、ファイル名の前に日付を足して保存
                        Dim buFileName As String = DateTime.Now.ToString("yyyyMMddHHmmss") & "_" & Names
                        System.IO.File.Move(CopyPath, ConfigurationManager.AppSettings("bk_DirPath") & "\" & buFileName)

                    Catch Ex As Exception

                        MessageBox.Show("ファイルの移動に失敗しました。" & vbLf & "移動元ファイル名：" & CopyPath & vbLf & "移動先ファイル名：" & BUPath)
                        Exit Sub

                    End Try


                    'プログレスバーの値を+1して処理を次に回す
                    ProgNum = ProgNum + 1

                End If

                'プログレスバーの値を代入
                frmProg.progressBar.Value = ProgNum

            Next

            'プログレスバーの値が4(最大値)に達した時、プログレスフォームを閉じる
            System.Threading.Thread.Sleep(1500)
            MessageBox.Show("ファイルの変換が完了しました")
            frmProg.Close()

        End Using

    End Sub

End Class
