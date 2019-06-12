Imports System
Imports System.Windows.Threading
Imports System.Windows.Interop
Imports System.Windows.Controls
Imports Ken_Burns_Slideshow.Application


Public Class ControlPad
    Private dispTimer As DispatcherTimer

    Private Sub CreateControlTemplateButton()
        Dim ct As ControlTemplate

        ct = New ControlTemplate()
    End Sub

    Private Sub TimerInit()

        If dispTimer Is Nothing Then dispTimer = New DispatcherTimer()

        AddHandler dispTimer.Tick, AddressOf dispatcherTimer_Tick

        dispTimer.Interval = New TimeSpan(0, 0, 10)
        'dispTimer.IsEnabled = True

        dispTimer.Start()

    End Sub

    Public Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        If Me.IsVisible Then Hide()

        'dispTimer.IsEnabled = False

        dispTimer.Stop()
    End Sub

    Private Sub WindowActivated() Handles Me.Activated

        Me.Topmost = True

        PathText.Content = DirectCast(Owner, MainWindow).ListOfPic.Rows(DirectCast(Owner, MainWindow).position - 2)("Path")

        If dispTimer Is Nothing Then TimerInit()

        If Not dispTimer.IsEnabled Then
            'dispTimer.IsEnabled = True
            dispTimer.Start()
        Else
            dispTimer.Stop()
            dispTimer.Start()
        End If
    End Sub


    Private Sub Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        If Not MainWindow.nextcloseaction = MainWindow.CloseAction.DirectQuit Then
            e.Cancel = True
            Hide() 'if close, next F1 will throw exception
        End If
    End Sub

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Owner = Application.Current.MainWindow

        'AddHandler Me.Activated, AddressOf WindowActivated
        TimerInit()

        'SetPosAndTop()
    End Sub

    Private Sub HasClick()
        If Not dispTimer.IsEnabled Then
            'dispTimer.IsEnabled = True
            dispTimer.Start()
        Else
            dispTimer.Stop()
            dispTimer.Start()
        End If

    End Sub


    Private Sub BtnExit(sender As Object, e As RoutedEventArgs) Handles Btn_Exit.Click
        Hide()
        DirectCast(Owner, MainWindow).nextcloseaction = DirectCast(Owner, MainWindow).CloseAction.FadeToDesktop
        DirectCast(Owner, MainWindow).Close()
    End Sub

    'Private Sub BtnScrOff(sender As Object, e As RoutedEventArgs) Handles Btn_ScrOff.Click
    '    Static lastHashCode = 0 '可能是使用了trigger的原因，会产生两次调用，不过HashCode相同

    '    If lastHashCode = e.GetHashCode Then
    '        Exit Sub
    '    Else
    '        lastHashCode = e.GetHashCode
    '    End If

    '    DirectCast(Owner, MainWindow).ScreenToggle()
    'End Sub

    Private Sub BtnConfig(sender As Object, e As RoutedEventArgs) Handles Btn_Config.Click
        Static lastHashCode = 0 '可能是使用了trigger的原因，会产生两次调用，不过HashCode相同

        If lastHashCode = e.GetHashCode Then
            Exit Sub
        Else
            lastHashCode = e.GetHashCode
        End If

        'MsgBox("Hash Code:" & CStr(e.GetHashCode))

        Dim optwin As New OptWindow

        optwin.ShowDialog()
        optwin.Close()

    End Sub

    Private Sub BtnMute(sender As Object, e As RoutedEventArgs) Handles Btn_Mute.Click
        Static lastHashCode = 0 '可能是使用了trigger的原因，会产生两次调用，不过HashCode相同

        If lastHashCode = e.GetHashCode Then
            Exit Sub
        Else
            lastHashCode = e.GetHashCode
        End If

        DirectCast(Owner, MainWindow).SwitchAudio()

    End Sub

    Private Sub BtnClock(sender As Object, e As RoutedEventArgs) Handles Btn_Clock.Click

        Static lastHashCode = 0 '可能是使用了trigger的原因，会产生两次调用，不过HashCode相同

        If lastHashCode = e.GetHashCode Then
            Exit Sub
        Else
            lastHashCode = e.GetHashCode
        End If

        DirectCast(Owner, MainWindow).ShowClock()

    End Sub

    Private Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Long, ByVal hWndInsertAfter As Long, ByVal X As Long, ByVal Y As Long, ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long
    'SetWindowPos Flags 
    Const SWP_NOSIZE = &H1
    Const SWP_NOMOVE = &H2
    Const SWP_NOZORDER = &H4
    Const SWP_NOREDRAW = &H8
    Const SWP_NOACTIVATE = &H10
    Const SWP_FRAMECHANGED = &H20
    'The frame changed: send WM_NCCALCSIZE
    Const SWP_SHOWWINDOW = &H40
    Const SWP_HIDEWINDOW = &H80
    Const SWP_NOCOPYBITS = &H100
    Const SWP_NOOWNERZORDER = &H200
    'Don't do owner Z ordering 
    Const SWP_DRAWFRAME = SWP_FRAMECHANGED
    Const SWP_NOREPOSITION = SWP_NOOWNERZORDER
    'SetWindowPos() hwndInsertAfter values
    Const HWND_TOP = 0
    Const HWND_BOTTOM = 1
    Const HWND_TOPMOST = -1
    Const HWND_NOTOPMOST = -2

    Public Sub SetPosAndTop()
        Dim wndHelper = New WindowInteropHelper(Me)
        Dim wpfHwnd = wndHelper.Handle

        Left = (Owner.Width - Me.Width) / 2
        Top = Owner.Height - Me.Height - 10

        SetWindowPos(wpfHwnd, HWND_TOP, Left, Top, Me.Width, Me.Height, SWP_SHOWWINDOW)
    End Sub

End Class