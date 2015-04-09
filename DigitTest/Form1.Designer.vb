Imports GabSoftware.WinControls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Button2 = New System.Windows.Forms.Button
        Me.GabDigit1 = New GabDigit
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 70)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(1148, 20)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(47, 157)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(161, 109)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Go !"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 1
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(214, 157)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(146, 109)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Clock"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GabDigit1
        '
        Me.GabDigit1.ActiveColor = System.Drawing.Color.DodgerBlue
        Me.GabDigit1.ActiveColorBorder = System.Drawing.Color.MidnightBlue
        Me.GabDigit1.BackColor = System.Drawing.Color.Black
        Me.GabDigit1.BackgroundColor = System.Drawing.Color.Black
        Me.GabDigit1.DigitHeight = 48
        Me.GabDigit1.DigitSegmentSpace = 0
        Me.GabDigit1.DigitSegmentThickness = 5
        Me.GabDigit1.DigitSmoothingMode = GabDigit.eDigitSmoothingMode.Antialiased
        Me.GabDigit1.DigitSpace = 3
        Me.GabDigit1.DigitWidth = 24
        Me.GabDigit1.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.GabDigit1.Location = New System.Drawing.Point(12, 12)
        Me.GabDigit1.Name = "GabDigit1"
        Me.GabDigit1.Padding = New System.Windows.Forms.Padding(2)
        Me.GabDigit1.Size = New System.Drawing.Size(1148, 52)
        Me.GabDigit1.TabIndex = 0
        Me.GabDigit1.Text = "0123456789:abc.def"
        Me.GabDigit1.TextAlign = System.Drawing.ContentAlignment.TopLeft
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1172, 475)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.GabDigit1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GabDigit1 As GabDigit
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Button2 As System.Windows.Forms.Button

End Class
