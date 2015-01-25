<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AttachmentsControl
    Inherits System.Windows.Forms.UserControl

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
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.chkInline = New System.Windows.Forms.CheckBox()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.txtFilename = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.btnDetach = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lbAttachments = New System.Windows.Forms.ListBox()
        Me.txtFormat = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'chkInline
        '
        Me.chkInline.Location = New System.Drawing.Point(394, 8)
        Me.chkInline.Name = "chkInline"
        Me.chkInline.Size = New System.Drawing.Size(130, 24)
        Me.chkInline.TabIndex = 3
        Me.chkInline.Text = "Store file inline"
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(354, 7)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(28, 23)
        Me.btnLoad.TabIndex = 2
        Me.btnLoad.Text = "..."
        '
        'txtFilename
        '
        Me.txtFilename.Location = New System.Drawing.Point(74, 8)
        Me.txtFilename.Name = "txtFilename"
        Me.txtFilename.Size = New System.Drawing.Size(280, 22)
        Me.txtFilename.TabIndex = 1
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(10, 8)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(58, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "Name"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnDetach
        '
        Me.btnDetach.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnDetach.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnDetach.Location = New System.Drawing.Point(251, 170)
        Me.btnDetach.Name = "btnDetach"
        Me.btnDetach.Size = New System.Drawing.Size(75, 28)
        Me.btnDetach.TabIndex = 10
        Me.btnDetach.Text = "Detach"
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnClear.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnClear.Location = New System.Drawing.Point(170, 170)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 28)
        Me.btnClear.TabIndex = 9
        Me.btnClear.Text = "Clear"
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnRemove.Location = New System.Drawing.Point(89, 170)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(75, 28)
        Me.btnRemove.TabIndex = 8
        Me.btnRemove.Text = "Remove"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAdd.Location = New System.Drawing.Point(8, 170)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 28)
        Me.btnAdd.TabIndex = 7
        Me.btnAdd.Text = "Add"
        '
        'lbAttachments
        '
        Me.lbAttachments.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lbAttachments.HorizontalExtent = 800
        Me.lbAttachments.HorizontalScrollbar = true
        Me.lbAttachments.ItemHeight = 16
        Me.lbAttachments.Location = New System.Drawing.Point(8, 64)
        Me.lbAttachments.Name = "lbAttachments"
        Me.lbAttachments.Size = New System.Drawing.Size(522, 100)
        Me.lbAttachments.TabIndex = 6
        '
        'txtFormat
        '
        Me.txtFormat.Location = New System.Drawing.Point(74, 36)
        Me.txtFormat.Name = "txtFormat"
        Me.txtFormat.Size = New System.Drawing.Size(280, 22)
        Me.txtFormat.TabIndex = 5
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(10, 36)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(58, 23)
        Me.label2.TabIndex = 4
        Me.label2.Text = "Format"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AttachmentsControl
        '
        Me.Controls.Add(Me.txtFormat)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.btnDetach)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lbAttachments)
        Me.Controls.Add(Me.chkInline)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.txtFilename)
        Me.Controls.Add(Me.label1)
        Me.Name = "AttachmentsControl"
        Me.Size = New System.Drawing.Size(538, 202)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents chkInline As System.Windows.Forms.CheckBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents txtFilename As System.Windows.Forms.TextBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents btnDetach As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lbAttachments As System.Windows.Forms.ListBox
    Friend WithEvents txtFormat As System.Windows.Forms.TextBox
    Friend WithEvents label2 As System.Windows.Forms.Label

End Class
