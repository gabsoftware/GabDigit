Imports System.Threading

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Me.GabDigit1.Text = Me.TextBox1.Text
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        For i As Integer = 0 To 200
            Me.GabDigit1.Text = i
            Thread.Sleep(50)
            Application.DoEvents()
        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Me.Timer1.Enabled = False Then
            Timer1.Start()
            Button2.Text = "Stop clock"
        Else
            Timer1.Stop()
            Button2.Text = "Clock"
        End If


    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim d As Date = Now
        GabDigit1.Text = IIf(d.Hour < 10, "0" & d.Hour, d.Hour) & ":" & _
                       IIf(d.Minute < 10, "0" & d.Minute, d.Minute) & ":" & _
                       IIf(d.Second < 10, "0" & d.Second, d.Second) & "." & _
                       IIf(d.Millisecond < 10, "0", "") & IIf(d.Millisecond < 100, "0", "") & d.Millisecond

    End Sub
End Class
