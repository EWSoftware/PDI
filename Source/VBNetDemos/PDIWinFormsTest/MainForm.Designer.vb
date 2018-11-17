<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.btnHolidays = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnRRULE = New System.Windows.Forms.Button()
        Me.btnTestCalRecur = New System.Windows.Forms.Button()
        Me.btnTestVTimeZone = New System.Windows.Forms.Button()
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.SuspendLayout
        '
        'btnHolidays
        '
        Me.btnHolidays.Location = New System.Drawing.Point(84, 18)
        Me.btnHolidays.Name = "btnHolidays"
        Me.btnHolidays.Size = New System.Drawing.Size(138, 40)
        Me.btnHolidays.TabIndex = 0
        Me.btnHolidays.Text = "Test &Holidays"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(84, 293)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(138, 40)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "E&xit"
        '
        'btnRRULE
        '
        Me.btnRRULE.Location = New System.Drawing.Point(84, 73)
        Me.btnRRULE.Name = "btnRRULE"
        Me.btnRRULE.Size = New System.Drawing.Size(138, 40)
        Me.btnRRULE.TabIndex = 1
        Me.btnRRULE.Text = "Test RR&ULE"
        '
        'btnTestCalRecur
        '
        Me.btnTestCalRecur.Location = New System.Drawing.Point(84, 128)
        Me.btnTestCalRecur.Name = "btnTestCalRecur"
        Me.btnTestCalRecur.Size = New System.Drawing.Size(138, 40)
        Me.btnTestCalRecur.TabIndex = 2
        Me.btnTestCalRecur.Text = "Test C&al Recur"
        '
        'btnTestVTimeZone
        '
        Me.btnTestVTimeZone.Location = New System.Drawing.Point(84, 183)
        Me.btnTestVTimeZone.Name = "btnTestVTimeZone"
        Me.btnTestVTimeZone.Size = New System.Drawing.Size(138, 40)
        Me.btnTestVTimeZone.TabIndex = 3
        Me.btnTestVTimeZone.Text = "Test &VTimeZone"
        '
        'btnAbout
        '
        Me.btnAbout.Location = New System.Drawing.Point(84, 238)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(138, 40)
        Me.btnAbout.TabIndex = 4
        Me.btnAbout.Text = "A&bout"
        '
        'MainForm
        '
        Me.ClientSize = New System.Drawing.Size(307, 353)
        Me.Controls.Add(Me.btnAbout)
        Me.Controls.Add(Me.btnTestVTimeZone)
        Me.Controls.Add(Me.btnTestCalRecur)
        Me.Controls.Add(Me.btnRRULE)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnHolidays)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EWSoftware.PDI Test"
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents btnHolidays As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnRRULE As System.Windows.Forms.Button
    Friend WithEvents btnTestCalRecur As System.Windows.Forms.Button
    Friend WithEvents btnTestVTimeZone As System.Windows.Forms.Button
    Friend WithEvents btnAbout As System.Windows.Forms.Button

End Class
