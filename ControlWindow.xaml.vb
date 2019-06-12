﻿Public Class ControlWindow

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Owner = Application.Current.MainWindow
    End Sub

    Private Sub Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        If Not MainWindow.nextcloseaction = MainWindow.CloseAction.DirectQuit Then
            e.Cancel = True
            Hide() 'if close, next F1 will throw exception
        End If
    End Sub

    Friend Sub Btn_SwitchImage_Click(sender As Object, e As RoutedEventArgs) Handles Btn_SwitchImage.Click
        Task.Run(Sub()
                     Dispatcher.Invoke(Sub()
                                           Btn_SwitchImage.IsEnabled = False
                                           Btn_Restart.IsEnabled = False
                                       End Sub)
                     Threading.Thread.Sleep(3000)
                     Dispatcher.Invoke(Sub()
                                           Btn_SwitchImage.IsEnabled = True
                                           Btn_Restart.IsEnabled = True
                                       End Sub)
                 End Sub)
        DirectCast(Owner, MainWindow).SwitchImage()
    End Sub

    Friend Sub Btn_SwitchAudio_Click(sender As Object, e As RoutedEventArgs) Handles Btn_SwitchAudio.Click
        Task.Run(Sub()
                     Dispatcher.Invoke(Sub()
                                           Btn_SwitchAudio.IsEnabled = False
                                           Btn_Restart.IsEnabled = False
                                           Btn_NextSong.IsEnabled = False
                                       End Sub)
                     Threading.Thread.Sleep(3000)
                     Dispatcher.Invoke(Sub()
                                           Btn_SwitchAudio.IsEnabled = True
                                           Btn_Restart.IsEnabled = True
                                           Btn_NextSong.IsEnabled = True
                                       End Sub)
                 End Sub)
        DirectCast(Owner, MainWindow).SwitchAudio()
    End Sub

    Friend Sub Btn_Restart_Click(sender As Object, e As RoutedEventArgs) Handles Btn_Restart.Click
        Task.Run(Sub()
                     Dispatcher.Invoke(Sub()
                                           Btn_SwitchImage.IsEnabled = False
                                           Btn_SwitchAudio.IsEnabled = False
                                           Btn_Restart.IsEnabled = False
                                           Btn_NextSong.IsEnabled = False
                                       End Sub)
                     Threading.Thread.Sleep(3000)
                     Dispatcher.Invoke(Sub()
                                           Btn_SwitchImage.IsEnabled = True
                                           Btn_SwitchAudio.IsEnabled = True
                                           Btn_Restart.IsEnabled = True
                                           Btn_NextSong.IsEnabled = True
                                       End Sub)
                 End Sub)
        DirectCast(Owner, MainWindow).RestartAll()
    End Sub

    Friend Sub Btn_NextSong_Click(sender As Object, e As RoutedEventArgs) Handles Btn_NextSong.Click
        If MainWindow.ListOfMusic?.Count = 0 Then Return
        Task.Run(Sub()
                     Dispatcher.Invoke(Sub()
                                           Btn_SwitchAudio.IsEnabled = False
                                           Btn_Restart.IsEnabled = False
                                           Btn_NextSong.IsEnabled = False
                                       End Sub)
                     Threading.Thread.Sleep(3000)
                     Dispatcher.Invoke(Sub()
                                           Btn_SwitchAudio.IsEnabled = True
                                           Btn_Restart.IsEnabled = True
                                           Btn_NextSong.IsEnabled = True
                                       End Sub)
                 End Sub)
        DirectCast(Owner, MainWindow).NextSong()
    End Sub

    Private Sub Btn_Options_Click(sender As Object, e As RoutedEventArgs) Handles Btn_Options.Click
        Dim optwin As New OptWindow
        optwin.ShowDialog()
        optwin.Close()
    End Sub

    Private Sub Btn_EditSlide_Click(sender As Object, e As RoutedEventArgs) Handles Btn_EditSlide.Click
        Dim editwin As New EditWindow
        editwin.ShowDialog()
        editwin.Close()
    End Sub

    Private Sub Btn_Exit_Click(sender As Object, e As RoutedEventArgs) Handles Btn_Exit.Click
        DirectCast(Owner, MainWindow).Close()
    End Sub

    Private Sub Window_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Escape Then
            Hide()
        ElseIf e.Key = Key.P Then
            If Keyboard.Modifiers = ModifierKeys.Control AndAlso Btn_SwitchImage.IsEnabled Then 'pause image
                Btn_SwitchImage_Click(Nothing, Nothing)
            ElseIf Keyboard.Modifiers = ModifierKeys.Shift AndAlso Btn_SwitchAudio.IsEnabled Then 'fadeout audio only
                Btn_SwitchAudio_Click(Nothing, Nothing)
            End If
        ElseIf e.Key = Key.R AndAlso Keyboard.Modifiers = ModifierKeys.Control AndAlso Btn_Restart.IsEnabled Then
            Btn_Restart_Click(Nothing, Nothing)
        ElseIf e.Key = Key.F12 Then
            Dim optwin As New OptWindow
            optwin.ShowDialog()
            optwin.Close()
        ElseIf e.Key = Key.F11 Then
            Dim editwin As New EditWindow
            editwin.ShowDialog()
            editwin.Close()
        ElseIf e.Key = Key.Q AndAlso Keyboard.Modifiers = ModifierKeys.Control Then 'immediately quit
            MainWindow.nextcloseaction = MainWindow.CloseAction.FadeToDesktop
            DirectCast(Owner, MainWindow).Close()
        ElseIf e.Key = Key.N AndAlso Keyboard.Modifiers = ModifierKeys.Control Then
            Btn_NextSong_Click(Nothing, Nothing)
        End If
    End Sub
End Class
