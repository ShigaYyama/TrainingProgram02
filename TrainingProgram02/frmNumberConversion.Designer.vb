<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNumberConversion
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnExecution = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnExecution
        '
        Me.btnExecution.Location = New System.Drawing.Point(67, 146)
        Me.btnExecution.Name = "btnExecution"
        Me.btnExecution.Size = New System.Drawing.Size(193, 76)
        Me.btnExecution.TabIndex = 0
        Me.btnExecution.Text = "実行"
        Me.btnExecution.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(337, 146)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(193, 76)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "終了"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmNumberConversion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(602, 331)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnExecution)
        Me.Name = "frmNumberConversion"
        Me.Text = "識別番号変換"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnExecution As Button
    Friend WithEvents btnClose As Button
End Class
