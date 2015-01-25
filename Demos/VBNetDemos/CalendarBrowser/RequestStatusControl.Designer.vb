<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RequestStatusControl
    Inherits EWSoftware.PDI.Windows.Forms.BrowseControl

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified Imports the Windows Form Designer.
    'Do not modify it Imports the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtStatusCode = New System.Windows.Forms.TextBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.txtExtData = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'txtStatusCode
        '
        Me.txtStatusCode.Location = New System.Drawing.Point(120, 8)
        Me.txtStatusCode.Name = "txtStatusCode"
        Me.txtStatusCode.Size = New System.Drawing.Size(128, 22)
        Me.txtStatusCode.TabIndex = 1
        '
        'label5
        '
        Me.label5.Location = New System.Drawing.Point(26, 8)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(88, 23)
        Me.label5.TabIndex = 0
        Me.label5.Text = "Status Code"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMessage
        '
        Me.txtMessage.Location = New System.Drawing.Point(120, 40)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(264, 22)
        Me.txtMessage.TabIndex = 3
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(29, 40)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(85, 23)
        Me.label1.TabIndex = 2
        Me.label1.Text = "Message"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtExtData
        '
        Me.txtExtData.Location = New System.Drawing.Point(120, 72)
        Me.txtExtData.Name = "txtExtData"
        Me.txtExtData.Size = New System.Drawing.Size(264, 22)
        Me.txtExtData.TabIndex = 5
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(4, 72)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(110, 23)
        Me.label2.TabIndex = 4
        Me.label2.Text = "Extended Data"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'RequestStatusControl
        '
        Me.Controls.Add(Me.txtExtData)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.txtMessage)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.txtStatusCode)
        Me.Controls.Add(Me.label5)
        Me.Name = "RequestStatusControl"
        Me.Size = New System.Drawing.Size(400, 136)
        Me.Controls.SetChildIndex(Me.label5, 0)
        Me.Controls.SetChildIndex(Me.txtStatusCode, 0)
        Me.Controls.SetChildIndex(Me.label1, 0)
        Me.Controls.SetChildIndex(Me.txtMessage, 0)
        Me.Controls.SetChildIndex(Me.label2, 0)
        Me.Controls.SetChildIndex(Me.txtExtData, 0)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents txtStatusCode As System.Windows.Forms.TextBox
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents txtExtData As System.Windows.Forms.TextBox
    Friend WithEvents label2 As System.Windows.Forms.Label

End Class
