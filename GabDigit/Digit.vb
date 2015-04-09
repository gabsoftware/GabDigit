Imports System.Drawing.Drawing2D
Imports GabSoftware.WinControls

''' <summary>
''' Digit is a subclass of GabDigit and processes character recognizition and painting.
''' </summary>
''' <remarks>Internal use only, use the GabDigit class instead.</remarks>
Public Class Digit
    Private _isSpecialChar As Boolean = False
    Private _isNothing As Boolean = False
    Private _character As Char
    Private _segments As eSegment
    Private _gd As GabDigit

#Region " Constants "
    Const DIGIT_0 As eSegment = eSegment.Top Or eSegment.Bottom Or eSegment.UpLeft Or eSegment.UpRight Or eSegment.Downleft Or eSegment.DownRight
    Const DIGIT_1 As eSegment = eSegment.UpRight Or eSegment.DownRight
    Const DIGIT_2 As eSegment = eSegment.Top Or eSegment.UpRight Or eSegment.Middle Or eSegment.Downleft Or eSegment.Bottom
    Const DIGIT_3 As eSegment = eSegment.Top Or eSegment.UpRight Or eSegment.DownRight Or eSegment.Bottom Or eSegment.Middle
    Const DIGIT_4 As eSegment = eSegment.UpLeft Or eSegment.UpRight Or eSegment.Middle Or eSegment.DownRight
    Const DIGIT_5 As eSegment = eSegment.Top Or eSegment.UpLeft Or eSegment.Middle Or eSegment.DownRight Or eSegment.Bottom
    Const DIGIT_6 As eSegment = eSegment.Top Or eSegment.UpLeft Or eSegment.Downleft Or eSegment.Bottom Or eSegment.DownRight Or eSegment.Middle
    Const DIGIT_7 As eSegment = eSegment.Top Or eSegment.UpRight Or eSegment.DownRight
    Const DIGIT_8 As eSegment = eSegment.Top Or eSegment.UpLeft Or eSegment.UpRight Or eSegment.Middle Or eSegment.Downleft Or eSegment.DownRight Or eSegment.Bottom
    Const DIGIT_9 As eSegment = eSegment.Middle Or eSegment.UpLeft Or eSegment.Top Or eSegment.UpRight Or eSegment.DownRight Or eSegment.Bottom
    Const ALPHA_A As eSegment = eSegment.Top Or eSegment.UpRight Or eSegment.UpLeft Or eSegment.DownRight Or eSegment.Downleft Or eSegment.Middle
    'Const ALPHA_B As eSegment = eSegment.UpLeft Or eSegment.Downleft Or eSegment.Bottom Or eSegment.DownRight Or eSegment.Middle
    Const ALPHA_C As eSegment = eSegment.Top Or eSegment.Downleft Or eSegment.UpLeft Or eSegment.Bottom
    'Const ALPHA_D As eSegment = eSegment.UpRight Or eSegment.DownRight Or eSegment.Bottom Or eSegment.Downleft Or eSegment.Middle
    Const ALPHA_E As eSegment = eSegment.Middle Or eSegment.Top Or eSegment.UpLeft Or eSegment.Downleft Or eSegment.Bottom
    Const ALPHA_F As eSegment = eSegment.Downleft Or eSegment.UpLeft Or eSegment.Middle Or eSegment.Top
    Const ALPHA_G As eSegment = eSegment.Top Or eSegment.UpLeft Or eSegment.Downleft Or eSegment.Bottom Or eSegment.DownRight
    Const ALPHA_H As eSegment = eSegment.Downleft Or eSegment.UpLeft Or eSegment.DownRight Or eSegment.UpRight Or eSegment.Middle
    Const ALPHA_J As eSegment = eSegment.UpRight Or eSegment.DownRight Or eSegment.Bottom
    Const ALPHA_L As eSegment = eSegment.UpLeft Or eSegment.Downleft Or eSegment.Bottom
    Const ALPHA_N As eSegment = eSegment.Downleft Or eSegment.UpLeft Or eSegment.Top Or eSegment.UpRight Or eSegment.DownRight
    'Const ALPHA_O As eSegment = eSegment.Middle Or eSegment.DownRight Or eSegment.Bottom Or eSegment.Downleft
    Const ALPHA_P As eSegment = eSegment.Downleft Or eSegment.UpLeft Or eSegment.Top Or eSegment.UpRight Or eSegment.Middle
    Const ALPHA_Q As eSegment = eSegment.DownRight Or eSegment.UpRight Or eSegment.Top Or eSegment.UpLeft Or eSegment.Middle
    Const ALPHA_R As eSegment = eSegment.Downleft Or eSegment.UpLeft Or eSegment.Top
    Const ALPHA_T As eSegment = eSegment.UpLeft Or eSegment.Downleft Or eSegment.Middle Or eSegment.Bottom
    Const ALPHA_U As eSegment = eSegment.UpLeft Or eSegment.Downleft Or eSegment.Bottom Or eSegment.DownRight Or eSegment.UpRight
    Const ALPHA_Y As eSegment = eSegment.UpLeft Or eSegment.Middle Or eSegment.UpRight Or eSegment.DownRight Or eSegment.Bottom
    Const SPECIAL_NOTHING As eSegment = eSegment.Null
    Const SPECIAL_SPACE As eSegment = eSegment.None
    Const SPECIAL_POINT As eSegment = eSegment.Point
    Const SPECIAL_DOUBLEPOINT As eSegment = eSegment.DoublePoint
    Const SPECIAL_MINUS As eSegment = eSegment.Middle
#End Region

#Region " Enums "
    <FlagsAttribute()> Public Enum eSegment
        None = 0
        Top = 1
        Bottom = 2
        UpLeft = 4
        UpRight = 8
        Downleft = 16
        DownRight = 32
        Middle = 64
        Point = 128
        DoublePoint = 256
        Null = 512
    End Enum
#End Region

#Region " Properties "
    Public Property IsSpecialChar() As Boolean
        Get
            Return _isSpecialChar
        End Get
        Set(ByVal value As Boolean)
            _isSpecialChar = value
        End Set
    End Property

    Public Property IsNothing() As Boolean
        Get
            Return _isNothing
        End Get
        Set(ByVal value As Boolean)
            _isNothing = value
        End Set
    End Property

    Public Property Character() As Char
        Get
            Return _character
        End Get
        Set(ByVal value As Char)
            _character = value
            DetermineSegments()
        End Set
    End Property

    Public Property Segments() As eSegment
        Get
            Return _segments
        End Get
        Set(ByVal value As eSegment)
            _segments = value
        End Set
    End Property
#End Region

#Region " Private methods "
    Private Sub DetermineSegments()
        DetermineSegments(_character)
    End Sub
    Private Sub DetermineSegments(ByVal thisChar As Char)
        Select Case thisChar
            Case "0"
                Segments = DIGIT_0
            Case "1"
                Segments = DIGIT_1
            Case "2"
                Segments = DIGIT_2
            Case "3"
                Segments = DIGIT_3
            Case "4"
                Segments = DIGIT_4
            Case "5"
                Segments = DIGIT_5
            Case "6"
                Segments = DIGIT_6
            Case "7"
                Segments = DIGIT_7
            Case "8"
                Segments = DIGIT_8
            Case "9"
                Segments = DIGIT_9
            Case "a", "A"
                Segments = ALPHA_A
            Case "b", "B"
                Segments = DIGIT_8
            Case "c", "C"
                Segments = ALPHA_C
            Case "d", "D"
                Segments = DIGIT_0
            Case "e", "E"
                Segments = ALPHA_E
            Case "f", "F"
                Segments = ALPHA_F
            Case "g", "G"
                Segments = ALPHA_G
            Case "h", "H", "k", "K"
                Segments = ALPHA_H
            Case "i", "I"
                Segments = DIGIT_1
            Case "j", "J"
                Segments = ALPHA_J
            Case "l", "L"
                Segments = ALPHA_L
            Case "n", "N", "m", "M"
                Segments = ALPHA_N
            Case "o", "O"
                Segments = DIGIT_0
            Case "p", "P"
                Segments = ALPHA_P
            Case "q", "Q"
                Segments = ALPHA_Q
            Case "r", "R"
                Segments = alpha_r
            Case "s", "S"
                Segments = DIGIT_5
            Case "t", "T"
                Segments = ALPHA_T
            Case "u", "U", "v", "V"
                Segments = ALPHA_U
            Case "y", "Y"
                Segments = alpha_y
            Case " "
                Segments = SPECIAL_SPACE
            Case "-"
                Segments = SPECIAL_MINUS
            Case ".", ","
                Segments = SPECIAL_POINT
                IsSpecialChar = True
            Case ":"
                Segments = SPECIAL_DOUBLEPOINT
                IsSpecialChar = True
            Case Else
                Segments = SPECIAL_NOTHING
                IsNothing = True
        End Select

        'Dim s As String
        's = Segments.ToString & vbNewLine
        's += "None : " & AndBits(Segments, eSegment.None) & vbNewLine
        's += "Top : " & OrBits(Segments, eSegment.Top) & vbNewLine
        's += "Bottom : " & OrBits(Segments, eSegment.Bottom) & vbNewLine
        's += "Up-Left : " & OrBits(Segments, eSegment.UpLeft) & vbNewLine
        's += "Up-Right : " & OrBits(Segments, eSegment.UpRight) & vbNewLine
        's += "Down-left : " & OrBits(Segments, eSegment.Downleft) & vbNewLine
        's += "Down-Right : " & OrBits(Segments, eSegment.DownRight) & vbNewLine
        's += "Middle : " & OrBits(Segments, eSegment.Middle) & vbNewLine
        's += "Point : " & OrBits(Segments, eSegment.Point) & vbNewLine
        's += "DoublePoint : " & OrBits(Segments, eSegment.DoublePoint) & vbNewLine
        's += "Nothing : " & OrBits(Segments, eSegment.Null)

        'MsgBox(s)


    End Sub

    Private Function OrBits(ByVal number As Integer, ByVal searched As Integer)
        Return IIf((number Or searched) = number, True, False)
    End Function

    Private Function AndBits(ByVal number As Integer, ByVal searched As Integer)
        Return IIf((number And searched) = number, True, False)
    End Function

#End Region

#Region " Public methods "
    Public Sub New(ByVal thisChar As Char, ByRef thisGabDigitInstance As GabDigit)
        Character = thisChar
        _gd = thisGabDigitInstance
    End Sub

    Public Sub Paint(ByRef e As PaintEventArgs, ByVal OffsetX As Integer, ByVal OffsetY As Integer)

        'Bienvenue dans le merdier

        Dim g As Graphics = e.Graphics
        Dim gp As GraphicsPath

        If IsSpecialChar Then
            Select Case True

                Case OrBits(Segments, eSegment.Point)
                    'on paint le segment du point
                    gp = New GraphicsPath( _
    New Point() { _
          New Point(OffsetX, OffsetY + _gd.DigitHeight * 0.75), _
          New Point(OffsetX + _gd.DigitSegmentThickness - 1, OffsetY + _gd.DigitHeight * 0.75), _
          New Point(OffsetX + _gd.DigitSegmentThickness - 1, OffsetY + _gd.DigitSegmentThickness - 1 + _gd.DigitHeight * 0.75), _
          New Point(OffsetX, OffsetY + _gd.DigitSegmentThickness - 1 + _gd.DigitHeight * 0.75), _
          New Point(OffsetX, OffsetY + _gd.DigitHeight * 0.75)}, _
          New Byte() {PathPointType.Start, _
                      PathPointType.Line, _
                      PathPointType.Line, _
                      PathPointType.Line, _
                      PathPointType.Line Or PathPointType.CloseSubpath}, _
    FillMode.Winding)
                    g.DrawPath(New Pen(_gd.ActiveColorBorder), gp)
                    g.FillPath(New SolidBrush(_gd.ActiveColor), gp)

                Case OrBits(Segments, eSegment.DoublePoint)
                    'on paint le segment du double-point

                    gp = New GraphicsPath( _
New Point() { _
 New Point(OffsetX, OffsetY + _gd.DigitHeight * 0.3), _
 New Point(OffsetX + _gd.DigitSegmentThickness - 1, OffsetY + _gd.DigitHeight * 0.3), _
 New Point(OffsetX + _gd.DigitSegmentThickness - 1, OffsetY + _gd.DigitSegmentThickness - 1 + _gd.DigitHeight * 0.3), _
 New Point(OffsetX, OffsetY + _gd.DigitSegmentThickness - 1 + _gd.DigitHeight * 0.3), _
 New Point(OffsetX, OffsetY + _gd.DigitHeight * 0.3)}, _
 New Byte() {PathPointType.Start, _
             PathPointType.Line, _
             PathPointType.Line, _
             PathPointType.Line, _
             PathPointType.Line Or PathPointType.CloseSubpath}, _
FillMode.Winding)
                    g.DrawPath(New Pen(_gd.ActiveColorBorder), gp)
                    g.FillPath(New SolidBrush(_gd.ActiveColor), gp)

                    gp = New GraphicsPath( _
New Point() { _
 New Point(OffsetX, OffsetY + _gd.DigitHeight * 0.75), _
 New Point(OffsetX + _gd.DigitSegmentThickness - 1, OffsetY + _gd.DigitHeight * 0.75), _
 New Point(OffsetX + _gd.DigitSegmentThickness - 1, OffsetY + _gd.DigitSegmentThickness - 1 + _gd.DigitHeight * 0.75), _
 New Point(OffsetX, OffsetY + _gd.DigitSegmentThickness - 1 + _gd.DigitHeight * 0.75), _
 New Point(OffsetX, OffsetY + _gd.DigitHeight * 0.75)}, _
 New Byte() {PathPointType.Start, _
             PathPointType.Line, _
             PathPointType.Line, _
             PathPointType.Line, _
             PathPointType.Line Or PathPointType.CloseSubpath}, _
FillMode.Winding)
                    g.DrawPath(New Pen(_gd.ActiveColorBorder), gp)
                    g.FillPath(New SolidBrush(_gd.ActiveColor), gp)


                    'Case OrBits(Segments, eSegment.Null)
                    '    'on ne paint rien

            End Select
        Else
            'on paint le segment du haut
            gp = New GraphicsPath( _
                New Point() { _
                      New Point(OffsetX + _gd.DigitSegmentSpace, OffsetY), _
                      New Point(OffsetX + _gd.DigitWidth - _gd.DigitSegmentSpace - 1, OffsetY), _
                      New Point(OffsetX + _gd.DigitWidth - _gd.DigitSegmentSpace - 1 - (_gd.DigitSegmentThickness - 1), OffsetY + (_gd.DigitSegmentThickness - 1)), _
                      New Point(OffsetX + _gd.DigitSegmentSpace + (_gd.DigitSegmentThickness - 1), OffsetY + (_gd.DigitSegmentThickness - 1)), _
                      New Point(OffsetX + _gd.DigitSegmentSpace, OffsetY)}, _
                      New Byte() {PathPointType.Start, _
                                  PathPointType.Line, _
                                  PathPointType.Line, _
                                  PathPointType.Line, _
                                  PathPointType.Line Or PathPointType.CloseSubpath}, _
                FillMode.Winding)
            If OrBits(Segments, eSegment.Top) Then
                g.DrawPath(New Pen(_gd.ActiveColorBorder), gp)
                g.FillPath(New SolidBrush(_gd.ActiveColor), gp)
            Else
                'ce segment est inactif
                g.DrawPath(New Pen(_gd.InactiveColor), gp)
                g.FillPath(New SolidBrush(_gd.InactiveColor), gp)
            End If
            gp.Dispose()

            'on paint le segment du bas
            gp = New GraphicsPath( _
    New Point() { _
          New Point(OffsetX + _gd.DigitSegmentSpace + 1, OffsetY + _gd.DigitHeight - 1), _
          New Point(OffsetX + _gd.DigitSegmentSpace + 1 + (_gd.DigitSegmentThickness - 1), OffsetY + _gd.DigitHeight - 1 - (_gd.DigitSegmentThickness - 1)), _
          New Point(OffsetX + _gd.DigitWidth - _gd.DigitSegmentSpace - 2 - (_gd.DigitSegmentThickness - 1), OffsetY + _gd.DigitHeight - 1 - (_gd.DigitSegmentThickness - 1)), _
          New Point(OffsetX + _gd.DigitWidth - _gd.DigitSegmentSpace - 2, OffsetY + _gd.DigitHeight - 1), _
          New Point(OffsetX + _gd.DigitSegmentSpace + 1, OffsetY + _gd.DigitHeight - 1)}, _
          New Byte() {PathPointType.Start, _
                      PathPointType.Line, _
                      PathPointType.Line, _
                      PathPointType.Line, _
                      PathPointType.Line Or PathPointType.CloseSubpath}, _
    FillMode.Winding)
            If OrBits(Segments, eSegment.Bottom) Then
                g.DrawPath(New Pen(_gd.ActiveColorBorder), gp)
                g.FillPath(New SolidBrush(_gd.ActiveColor), gp)
            Else
                'ce segment est inactif
                g.DrawPath(New Pen(_gd.InactiveColor), gp)
                g.FillPath(New SolidBrush(_gd.InactiveColor), gp)
            End If
            gp.Dispose()

            'on paint le segment du haut-gauche
            gp = New GraphicsPath( _
New Point() { _
New Point(OffsetX, OffsetY + _gd.DigitSegmentSpace + 1), _
New Point(OffsetX + (_gd.DigitSegmentThickness - 1), OffsetY + _gd.DigitSegmentSpace + 1 + (_gd.DigitSegmentThickness - 1)), _
New Point(OffsetX + (_gd.DigitSegmentThickness - 1), OffsetY + _gd.DigitSegmentSpace + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2) - (_gd.DigitSegmentThickness - 1)), _
New Point(OffsetX, OffsetY + _gd.DigitSegmentSpace + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2)), _
New Point(OffsetX, OffsetY + _gd.DigitSegmentSpace + 1)}, _
New Byte() {PathPointType.Start, _
          PathPointType.Line, _
          PathPointType.Line, _
          PathPointType.Line, _
          PathPointType.Line Or PathPointType.CloseSubpath}, _
FillMode.Winding)
            If OrBits(Segments, eSegment.UpLeft) Then
                g.DrawPath(New Pen(_gd.ActiveColorBorder), gp)
                g.FillPath(New SolidBrush(_gd.ActiveColor), gp)
            Else
                'ce segment est inactif
                g.DrawPath(New Pen(_gd.InactiveColor), gp)
                g.FillPath(New SolidBrush(_gd.InactiveColor), gp)
            End If
            gp.Dispose()

            'on paint le segment du haut-droit
            gp = New GraphicsPath( _
New Point() { _
New Point(OffsetX + _gd.DigitWidth - 1, OffsetY + _gd.DigitSegmentSpace + 1), _
New Point(OffsetX + _gd.DigitWidth - 1, OffsetY + _gd.DigitSegmentSpace + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2)), _
New Point(OffsetX + _gd.DigitWidth - 1 - (_gd.DigitSegmentThickness - 1), OffsetY + _gd.DigitSegmentSpace + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2) - (_gd.DigitSegmentThickness - 1)), _
New Point(OffsetX + _gd.DigitWidth - 1 - (_gd.DigitSegmentThickness - 1), OffsetY + _gd.DigitSegmentSpace + 1 + (_gd.DigitSegmentThickness - 1)), _
New Point(OffsetX + _gd.DigitWidth - 1, OffsetY + _gd.DigitSegmentSpace + 1)}, _
New Byte() {PathPointType.Start, _
PathPointType.Line, _
PathPointType.Line, _
PathPointType.Line, _
PathPointType.Line Or PathPointType.CloseSubpath}, _
FillMode.Winding)
            If OrBits(Segments, eSegment.UpRight) Then
                g.DrawPath(New Pen(_gd.ActiveColorBorder), gp)
                g.FillPath(New SolidBrush(_gd.ActiveColor), gp)
            Else
                'ce segment est inactif
                g.DrawPath(New Pen(_gd.InactiveColor), gp)
                g.FillPath(New SolidBrush(_gd.InactiveColor), gp)
            End If
            gp.Dispose()

            'on paint le segment du bas-gauche
            gp = New GraphicsPath( _
New Point() { _
New Point(OffsetX, OffsetY + (_gd.DigitSegmentSpace * 3) + 2 + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2)), _
New Point(OffsetX + (_gd.DigitSegmentThickness - 1), OffsetY + (_gd.DigitSegmentSpace * 3) + 2 + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2) + (_gd.DigitSegmentThickness - 1)), _
New Point(OffsetX + (_gd.DigitSegmentThickness - 1), OffsetY + _gd.DigitHeight - _gd.DigitSegmentSpace - 1 - (_gd.DigitSegmentThickness - 1)), _
New Point(OffsetX, OffsetY + _gd.DigitHeight - _gd.DigitSegmentSpace - 1), _
New Point(OffsetX, OffsetY + (_gd.DigitSegmentSpace * 3) + 2 + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2))}, _
New Byte() {PathPointType.Start, _
          PathPointType.Line, _
          PathPointType.Line, _
          PathPointType.Line, _
          PathPointType.Line Or PathPointType.CloseSubpath}, _
FillMode.Winding)
            If OrBits(Segments, eSegment.Downleft) Then
                g.DrawPath(New Pen(_gd.ActiveColorBorder), gp)
                g.FillPath(New SolidBrush(_gd.ActiveColor), gp)
            Else
                'ce segment est inactif
                g.DrawPath(New Pen(_gd.InactiveColor), gp)
                g.FillPath(New SolidBrush(_gd.InactiveColor), gp)
            End If
            gp.Dispose()

            'on paint le segment du bas-droit
            gp = New GraphicsPath( _
New Point() { _
New Point(OffsetX + _gd.DigitWidth - 1, OffsetY + (_gd.DigitSegmentSpace * 3) + 2 + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2)), _
New Point(OffsetX + _gd.DigitWidth - 1, OffsetY + _gd.DigitHeight - _gd.DigitSegmentSpace - 1), _
New Point(OffsetX + _gd.DigitWidth - 1 - (_gd.DigitSegmentThickness - 1), OffsetY + _gd.DigitHeight - _gd.DigitSegmentSpace - 1 - (_gd.DigitSegmentThickness - 1)), _
New Point(OffsetX + _gd.DigitWidth - 1 - (_gd.DigitSegmentThickness - 1), OffsetY + (_gd.DigitSegmentSpace * 3) + 2 + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2) + (_gd.DigitSegmentThickness - 1)), _
New Point(OffsetX + _gd.DigitWidth - 1, OffsetY + (_gd.DigitSegmentSpace * 3) + 2 + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2))}, _
New Byte() {PathPointType.Start, _
PathPointType.Line, _
PathPointType.Line, _
PathPointType.Line, _
PathPointType.Line Or PathPointType.CloseSubpath}, _
FillMode.Winding)
            If OrBits(Segments, eSegment.DownRight) Then
                g.DrawPath(New Pen(_gd.ActiveColorBorder), gp)
                g.FillPath(New SolidBrush(_gd.ActiveColor), gp)
            Else
                'ce segment est inactif
                g.DrawPath(New Pen(_gd.InactiveColor), gp)
                g.FillPath(New SolidBrush(_gd.InactiveColor), gp)
            End If
            gp.Dispose()

            'on paint le segment du milieu
            gp = New GraphicsPath( _
New Point() { _
New Point(OffsetX + _gd.DigitSegmentSpace, OffsetY + (_gd.DigitSegmentSpace * 2 + 1) + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2)), _
New Point(OffsetX + _gd.DigitSegmentSpace + (_gd.DigitSegmentThickness - 1), OffsetY + (_gd.DigitSegmentSpace * 2 + 1) + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2) - (_gd.DigitSegmentThickness - 1)), _
New Point(OffsetX + _gd.DigitWidth - _gd.DigitSegmentSpace - 1 - (_gd.DigitSegmentThickness - 1), OffsetY + (_gd.DigitSegmentSpace * 2 + 1) + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2) - (_gd.DigitSegmentThickness - 1)), _
New Point(OffsetX + _gd.DigitWidth - _gd.DigitSegmentSpace - 1, OffsetY + (_gd.DigitSegmentSpace * 2 + 1) + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2)), _
New Point(OffsetX + _gd.DigitSegmentSpace, OffsetY + (_gd.DigitSegmentSpace * 2 + 1) + ((_gd.DigitHeight - (_gd.DigitSegmentSpace * 4 + 2)) / 2))}, _
New Byte() {PathPointType.Start, _
PathPointType.Line, _
PathPointType.Line, _
PathPointType.Line, _
PathPointType.Line Or PathPointType.CloseSubpath}, _
FillMode.Winding)
            If OrBits(Segments, eSegment.Middle) Then
                g.DrawPath(New Pen(_gd.ActiveColorBorder), gp)
                g.FillPath(New SolidBrush(_gd.ActiveColor), gp)
            Else
                'ce segment est inactif
                g.DrawPath(New Pen(_gd.InactiveColor), gp)
                g.FillPath(New SolidBrush(_gd.InactiveColor), gp)
            End If
            gp.Dispose()

        End If


    End Sub

    Public Function GetRealWidth() As Integer
        Dim result As Integer
        If IsNothing Then
            result = 0
        Else
            If IsSpecialChar Then
                Return _gd.DigitSegmentThickness
            Else
                Return _gd.DigitWidth
            End If
        End If

        Return result
    End Function
#End Region


End Class
