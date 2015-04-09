
'Imports System
'Imports System.Collections
Imports System.ComponentModel
'Imports System.Drawing
'Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Threading

Namespace GabSoftware.WinControls

    ''' <summary>
    ''' GabDigit is a control who transforms numbers and a few other characters in calculator-screen-like ones.
    ''' Remarks : Only the following characters are valid yet(all other will be ignored) : 0123456789-,.:ABCDEFGHIJLNOPQSTUY (case insensitive) and the blank space.
    ''' </summary>
    ''' <remarks>Only the following characters are valid (all other will be ignored) :
    ''' 0123456789-,.:ABCDEFGHIJLNOPQSTUY (case insensitive) and the blank space.</remarks>
    Public Class GabDigit

        'Private _myText As String
        Private _myActiveColor As Color
        Private _myActiveColorBorder As Color
        Private _myInactiveColor As Color
        Private _myBackgroundColor As Color
        Private _myDigitSmoothingMode As eDigitSmoothingMode
        Private _myDigitWidth As Integer
        Private _myDigitHeight As Integer
        Private _myDigitSpace As Integer
        Private _myDigitSegmentSpace As Integer
        Private _myDigitSegmentThickness As Integer
        Private _myDigits() As Digit
        Private _thread As Thread
        Private _myContentAlignment As ContentAlignment

        Private _isloaded As Boolean

        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.

            'spécifie que l'on dessine soit-même
            SetStyle(ControlStyles.ResizeRedraw, True)
            SetStyle(ControlStyles.UserPaint, True)
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            SetStyle(ControlStyles.Selectable, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            SetStyle(ControlStyles.UserMouse, True)

            Me._isloaded = False

            Me._myActiveColor = Color.DodgerBlue
            Me._myActiveColorBorder = Color.MidnightBlue
            Me._myInactiveColor = Color.Navy
            Me._myBackgroundColor = Color.Black
            Me.BackColor = Me._myBackgroundColor
            Me.Padding = New Padding(2)
            Me._myDigitWidth = 24
            Me._myDigitHeight = 48
            Me._myDigitSpace = 3
            Me._myDigitSegmentSpace = 1
            Me._myDigitSegmentThickness = 5
            Me._myDigitSmoothingMode = eDigitSmoothingMode.Antialiased
            Me._myContentAlignment = ContentAlignment.TopLeft
            Me.Text = "0123456789:abc.def"
            'Me.Width = (Me._myText.Length) * (Me._myDigitWidth + Me._myDigitSpace)
            'Me.Width = GetTextWidth() + Padding.Left + Padding.Right
            Me.Width = GetTextWidth() + Padding.Left + Padding.Right
            Me.Height = Me._myDigitHeight + Padding.Top + Padding.Bottom



            Refresh()

        End Sub

#Region " Enums "
        ''' <summary>
        ''' Type du knob
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum eDigitSmoothingMode

            Normal = 1
            Antialiased = 2

        End Enum

#End Region

#Region " Properties "
        <Category("Digits")> _
        <Browsable(True)> _
        <Bindable(True)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
        Public Overrides Property Text() As String
            Get
                Return MyBase.Text
            End Get
            Set(ByVal value As String)
                MyBase.Text = value
                ReDim _myDigits(value.Length - 1)
                For i As Integer = 0 To value.Length - 1
                    _myDigits(i) = New Digit(value(i), Me)
                Next

                Refresh()
            End Set
        End Property

        <Category("Digits")> Public Property ActiveColor() As Color
            Get
                Return _myActiveColor
            End Get
            Set(ByVal value As Color)
                _myActiveColor = value
                Refresh()
            End Set
        End Property

        <Category("Digits")> Public Property ActiveColorBorder() As Color
            Get
                Return _myActiveColorBorder
            End Get
            Set(ByVal value As Color)
                _myActiveColorBorder = value
                Refresh()
            End Set
        End Property

        <Category("Digits")> Public Property InactiveColor() As Color
            Get
                Return _myInactiveColor
            End Get
            Set(ByVal value As Color)
                _myInactiveColor = value
                Refresh()
            End Set
        End Property

        <Category("Digits")> Public Property BackgroundColor() As Color
            Get
                Return _myBackgroundColor
            End Get
            Set(ByVal value As Color)
                _myBackgroundColor = value
                Me.BackColor = value
                Refresh()
            End Set
        End Property

        <Category("Digits")> Public Property DigitSmoothingMode() As eDigitSmoothingMode
            Get
                Return _myDigitSmoothingMode
            End Get
            Set(ByVal value As eDigitSmoothingMode)
                _myDigitSmoothingMode = value
                Refresh()
            End Set
        End Property

        <Category("Digits")> Public Property DigitWidth() As Integer
            Get
                Return _myDigitWidth
            End Get
            Set(ByVal value As Integer)
                _myDigitWidth = value
                Refresh()
            End Set
        End Property

        <Category("Digits")> Public Property DigitHeight() As Integer
            Get
                Return _myDigitHeight
            End Get
            Set(ByVal value As Integer)
                _myDigitHeight = value
                Refresh()
            End Set
        End Property

        <Category("Digits")> Public Property DigitSpace() As Integer
            Get
                Return _myDigitSpace
            End Get
            Set(ByVal value As Integer)
                _myDigitSpace = value
                Refresh()
            End Set
        End Property

        <Category("Digits")> Public Property DigitSegmentSpace() As Integer
            Get
                Return _myDigitSegmentSpace
            End Get
            Set(ByVal value As Integer)
                _myDigitSegmentSpace = value
                Refresh()
            End Set
        End Property

        <Category("Digits")> Public Property DigitSegmentThickness() As Integer
            Get
                Return _myDigitSegmentThickness
            End Get
            Set(ByVal value As Integer)
                _myDigitSegmentThickness = value
                Refresh()
            End Set
        End Property

        <Category("Digits")> Public Property TextAlign() As ContentAlignment
            Get
                Return _myContentAlignment
            End Get
            Set(ByVal value As ContentAlignment)
                _myContentAlignment = value
                Refresh()
            End Set
        End Property

        <Browsable(False)> Public ReadOnly Property TextWidth() As Integer
            Get
                Return GetTextWidth()
            End Get
        End Property

#End Region

#Region " Private methods "
        Private Sub ThreadedPaint(ByVal e As System.Windows.Forms.PaintEventArgs)

            Dim OffsetX As Integer
            Dim OffsetY As Integer

            Select Case _myContentAlignment
                Case ContentAlignment.TopLeft
                    OffsetX = Padding.Left
                    OffsetY = Padding.Top
                Case ContentAlignment.TopCenter
                    OffsetX = (Me.Width - GetTextWidth()) / 2
                    OffsetY = Padding.Top
                Case ContentAlignment.TopRight
                    OffsetX = (Me.Width - GetTextWidth()) - Padding.Right
                    OffsetY = Padding.Top
                Case ContentAlignment.MiddleLeft
                    OffsetX = Padding.Left
                    OffsetY = (Me.Height - Me._myDigitHeight) / 2
                Case ContentAlignment.MiddleCenter
                    OffsetX = (Me.Width - GetTextWidth()) / 2
                    OffsetY = (Me.Height - Me._myDigitHeight) / 2
                Case ContentAlignment.MiddleRight
                    OffsetX = (Me.Width - GetTextWidth()) - Padding.Right
                    OffsetY = (Me.Height - Me._myDigitHeight) / 2
                Case ContentAlignment.BottomLeft
                    OffsetX = Padding.Left
                    OffsetY = (Me.Height - Me._myDigitHeight) - Padding.Bottom
                Case ContentAlignment.BottomCenter
                    OffsetX = (Me.Width - GetTextWidth()) / 2
                    OffsetY = (Me.Height - Me._myDigitHeight) - Padding.Bottom
                Case ContentAlignment.BottomRight
                    OffsetX = (Me.Width - GetTextWidth()) - Padding.Right
                    OffsetY = (Me.Height - Me._myDigitHeight) - Padding.Bottom
                Case Else
                    OffsetX = Padding.Left
                    OffsetY = Padding.Top
            End Select



            e.Graphics.SmoothingMode = IIf(Me.DigitSmoothingMode = eDigitSmoothingMode.Antialiased, SmoothingMode.AntiAlias, SmoothingMode.None)
            Try
                For i As Integer = 0 To _myDigits.Length - 1
                    If Not _myDigits(i).IsNothing Then
                        _myDigits(i).Paint(e, OffsetX, OffsetY)
                        OffsetX += IIf(_myDigits(i).IsSpecialChar = True, _myDigitSegmentThickness, _myDigitWidth) + _myDigitSpace
                    End If

                Next
            Catch
                Exit Sub
            End Try

        End Sub

        Private Sub GabDigit_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
            'redraw
            If (Not _thread Is Nothing) Then
                If (_thread.IsAlive) Then
                    _thread.Abort()
                End If
            End If


            _thread = New Thread(AddressOf ThreadedPaint)
            _thread.IsBackground = True
            _thread.Start(e)
            _thread.Join()
        End Sub

        Private Sub GabDigit_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
            Refresh()
        End Sub

        Private Sub GabDigit_PaddingChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PaddingChanged
            Refresh()
        End Sub
#End Region

#Region " Public methods "
        Public Overrides Sub Refresh()
            Try
                MyBase.Refresh()
            Finally

            End Try

        End Sub

        Public Function GetTextWidth() As Integer

            Dim cumulativeWidth As Integer = 0


            Try
                For i As Integer = 0 To Text.Length - 1
                    cumulativeWidth += _myDigits(i).GetRealWidth()
                    If i > 0 And i <= Text.Length - 1 Then
                        cumulativeWidth += _myDigitSpace
                    End If
                Next
            Catch
                cumulativeWidth = 0
            End Try

            Return cumulativeWidth
        End Function


#End Region

    End Class
End Namespace
