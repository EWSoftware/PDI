<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PhotoControl
    Inherits System.Windows.Forms.UserControl

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                bmImage.Dispose()

                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlPhoto = New System.Windows.Forms.Panel()
        Me.label1 = New System.Windows.Forms.Label()
        Me.txtFilename = New System.Windows.Forms.TextBox()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.chkInline = New System.Windows.Forms.CheckBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.cboImageType = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout
        '
        'pnlPhoto
        '
        Me.pnlPhoto.AutoScroll = true
        Me.pnlPhoto.BackColor = System.Drawing.SystemColors.Window
        Me.pnlPhoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlPhoto.Location = New System.Drawing.Point(8, 4)
        Me.pnlPhoto.Name = "pnlPhoto"
        Me.pnlPhoto.Size = New System.Drawing.Size(200, 200)
        Me.pnlPhoto.TabIndex = 6
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(218, 19)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(63, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "&Name"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFilename
        '
        Me.txtFilename.Location = New System.Drawing.Point(287, 17)
        Me.txtFilename.Name = "txtFilename"
        Me.txtFilename.Size = New System.Drawing.Size(280, 26)
        Me.txtFilename.TabIndex = 1
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(573, 16)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(35, 28)
        Me.btnLoad.TabIndex = 2
        Me.btnLoad.Text = "..."
        '
        'chkInline
        '
        Me.chkInline.Location = New System.Drawing.Point(287, 49)
        Me.chkInline.Name = "chkInline"
        Me.chkInline.Size = New System.Drawing.Size(280, 24)
        Me.chkInline.TabIndex = 3
        Me.chkInline.Text = "Store image in vCard"
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(219, 81)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(62, 23)
        Me.label2.TabIndex = 4
        Me.label2.Text = "Type"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboImageType
        '
        Me.cboImageType.Items.AddRange(New Object() {"GIF", "CGM", "WMF", "BMP", "MET", "PMB", "DIB", "PICT", "TIFF", "PS", "PDF", "JPEG", "MPEG", "MPEG2", "AVI", "QTIME", "PNG"})
        Me.cboImageType.Location = New System.Drawing.Point(287, 79)
        Me.cboImageType.MaxDropDownItems = 16
        Me.cboImageType.Name = "cboImageType"
        Me.cboImageType.Size = New System.Drawing.Size(152, 28)
        Me.cboImageType.TabIndex = 5
        '
        'PhotoControl
        '
        Me.Controls.Add(Me.cboImageType)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.chkInline)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.txtFilename)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.pnlPhoto)
        Me.Name = "PhotoControl"
        Me.Size = New System.Drawing.Size(619, 208)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents pnlPhoto As System.Windows.Forms.Panel
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents txtFilename As System.Windows.Forms.TextBox
    Friend WithEvents chkInline As System.Windows.Forms.CheckBox
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents cboImageType As System.Windows.Forms.ComboBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button

End Class
