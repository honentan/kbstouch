﻿Imports Ken_Burns_Slideshow.Application

Public Class OptWindow
    Dim lastPath As String

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Owner = Application.Current.MainWindow
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        CbB_ScaleMode.ItemsSource = MainWindow.ScaleMode_Dic
        For Each i In MainWindow.folders_image
            LB_ImgFolder.Items.Add(i)
        Next
        For Each i In MainWindow.folders_music
            LB_BGMFolder.Items.Add(i)
        Next
        CB_VerLk.IsChecked = MainWindow.verticalLock
        CB_ResLk.IsChecked = MainWindow.resolutionLock
        CB_VerOptm.IsChecked = MainWindow.verticalOptimize
        CB_HorOptm.IsChecked = MainWindow.horizontalOptimize
        CB_Fadeout.IsChecked = MainWindow.fadeout
        TB_BreakStart.Text = MainWindow.breakStartHour
        TB_BreakEnd.Text = MainWindow.breakEndHour
        TB_VORatio.Text = MainWindow.verticalOptimizeR
        TB_HORatio.Text = MainWindow.horizontalOptimizeR
        TB_Framerate.Text = MainWindow.framerate
        TB_Duration.Text = MainWindow.duration
        CbB_Transit.SelectedIndex = MainWindow.transit
        CbB_LoadMode.SelectedIndex = MainWindow.loadmode_next
        CbB_BreakMode.SelectedIndex = MainWindow.breakMode
        CbB_ClockStyle.SelectedIndex = MainWindow.clockStyle
        CbB_WeatherStyle.SelectedIndex = MainWindow.weatherStyle
        TB_LoadQuality.Text = MainWindow.loadquality
        CbB_ScaleMode.SelectedItem = New KeyValuePair(Of Integer, String)(MainWindow.scalemode, MainWindow.ScaleMode_Dic(MainWindow.scalemode))
        CbB_BlurMode.SelectedIndex = MainWindow.blurmode
        CB_RandomizeV.IsChecked = MainWindow.randomizeV
        CB_RandomizeA.IsChecked = MainWindow.randomizeA
        CB_Img_Recur.IsChecked = MainWindow.recursive_folder
        CB_BGM_Recur.IsChecked = MainWindow.recursive_music
        CB_ShowControl.IsChecked = MainWindow.showcontrol
        CB_NoLoop.IsChecked = MainWindow.noloop

        If Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Classes\Directory\shell\OpenWithKenBurns\command") Is Nothing Then
            Btn_FolderAsso.Content = Current.Resources("register menu")
        Else
            Btn_FolderAsso.Content = Current.Resources("unregister menu")
        End If
    End Sub

    Private Sub CB_HorOptm_Change() Handles CB_HorOptm.Unchecked, CB_HorOptm.Checked, CB_HorOptm.Loaded
        If Me.IsLoaded Then
            If CB_HorOptm.IsChecked Then
                TB_HORatio.IsEnabled = True
            Else
                TB_HORatio.IsEnabled = False
            End If
        End If
    End Sub

    Private Sub CB_VerOptm_Change() Handles CB_VerOptm.Unchecked, CB_VerOptm.Checked, CB_VerOptm.Loaded
        If Me.IsLoaded Then
            If CB_VerOptm.IsChecked Then
                TB_VORatio.IsEnabled = True
            Else
                TB_VORatio.IsEnabled = False
            End If
        End If
    End Sub

    Private Function CheckConsist(chk_str As String, ByRef target As Double, min As Double, max As Double, inclu_min As Boolean, inclu_max As Boolean, err_msg As String) As Boolean
        Dim tmp As Double = 0
        If Double.TryParse(chk_str, tmp) AndAlso If(inclu_min, tmp >= min, tmp > min) AndAlso If(inclu_max, tmp <= max, tmp < max) Then
            target = tmp
            Return True
        Else
            MessageBox.Show(err_msg, "", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            Return False
        End If
    End Function

    Private Function CheckConsist(chk_str As String, ByRef target As UInteger, min As UInteger, max As UInteger, inclu_min As Boolean, inclu_max As Boolean, err_msg As String) As Boolean
        Dim tmp As UInteger = 0
        If UInteger.TryParse(chk_str, tmp) AndAlso If(inclu_min, tmp >= min, tmp > min) AndAlso If(inclu_max, tmp <= max, tmp < max) Then
            target = tmp
            Return True
        Else
            MessageBox.Show(err_msg, "", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            Return False
        End If
    End Function

    Private Function CheckConsist(chk_str As String, ByRef target As Double, min As Double, inclu_min As Boolean, err_msg As String) As Boolean
        Dim tmp As Double = 0
        If Double.TryParse(chk_str, tmp) AndAlso If(inclu_min, tmp >= min, tmp > min) Then
            target = tmp
            Return True
        Else
            MessageBox.Show(err_msg, "", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            Return False
        End If
    End Function

    Private Function CheckConsist(chk_str As String, ByRef target As UInteger, min As UInteger, inclu_min As Boolean, err_msg As String) As Boolean
        Dim tmp As UInteger = 0
        If UInteger.TryParse(chk_str, tmp) AndAlso If(inclu_min, tmp >= min, tmp > min) Then
            target = tmp
            Return True
        Else
            MessageBox.Show(err_msg, "", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            Return False
        End If
    End Function

    Private Sub Btn_OK_Click(sender As Object, e As RoutedEventArgs) Handles Btn_OK.Click
        If Not CheckConsist(TB_BreakStart.Text, MainWindow.breakStartHour, 0, 23, True, True, Current.Resources("msg_breakHour").ToString) Then Exit Sub
        If Not CheckConsist(TB_BreakEnd.Text, MainWindow.breakEndHour, 0, MainWindow.breakStartHour, True, False, Current.Resources("msg_breakHour").ToString) Then Exit Sub

        If Not CheckConsist(TB_VORatio.Text, MainWindow.verticalOptimizeR, 0, 1, False, True, Current.Resources("msg_invVOR").ToString) Then Exit Sub
                If Not CheckConsist(TB_HORatio.Text, MainWindow.horizontalOptimizeR, 0, 1, False, True, Current.Resources("msg_invHOR").ToString) Then Exit Sub
                If Not CheckConsist(TB_Framerate.Text, MainWindow.framerate, 0, False, Current.Resources("msg_invfps").ToString) Then Exit Sub
                If Not CheckConsist(TB_Duration.Text, MainWindow.duration, 4, False, Current.Resources("msg_invdur").ToString) Then Exit Sub
                If Not CheckConsist(TB_LoadQuality.Text, MainWindow.loadquality, 0, 2, False, True, Current.Resources("msg_invmul").ToString) Then Exit Sub

                MainWindow.folders_image.Clear()
                For Each i As String In LB_ImgFolder.Items
                    MainWindow.folders_image.Add(i)
                Next
                MainWindow.folders_music.Clear()
                For Each i As String In LB_BGMFolder.Items
                    MainWindow.folders_music.Add(i)
                Next
                MainWindow.verticalLock = CB_VerLk.IsChecked
                MainWindow.verticalOptimize = CB_VerOptm.IsChecked
                MainWindow.horizontalOptimize = CB_HorOptm.IsChecked
                MainWindow.resolutionLock = CB_ResLk.IsChecked
                MainWindow.fadeout = CB_Fadeout.IsChecked
                MainWindow.transit = CbB_Transit.SelectedIndex
                MainWindow.loadmode_next = CbB_LoadMode.SelectedIndex
                MainWindow.breakMode = CbB_BreakMode.SelectedIndex
                MainWindow.clockStyle = CbB_ClockStyle.SelectedIndex
                MainWindow.weatherStyle = CbB_WeatherStyle.SelectedIndex
                MainWindow.scalemode = CbB_ScaleMode.SelectedItem.Key
                MainWindow.blurmode = CbB_BlurMode.SelectedIndex
                MainWindow.randomizeV = CB_RandomizeV.IsChecked
                MainWindow.randomizeA = CB_RandomizeA.IsChecked
                MainWindow.recursive_folder = CB_Img_Recur.IsChecked
                MainWindow.recursive_music = CB_BGM_Recur.IsChecked
                MainWindow.showcontrol = CB_ShowControl.IsChecked
                MainWindow.noloop = CB_NoLoop.IsChecked

                'saving to file
                Dim config As New XElement("CfgRoot")
                config.Add(New XElement("Version", FileVersionInfo.GetVersionInfo(Reflection.Assembly.GetExecutingAssembly().Location).FileVersion))
                config.Add(New XElement("PicDir"))
                For Each i As String In LB_ImgFolder.Items
                    config.Element("PicDir").Add(New XElement("dir", New XCData(i)))
                Next
                config.Add(New XElement("Music"))
                For Each i As String In LB_BGMFolder.Items
                    config.Element("Music").Add(New XElement("dir", New XCData(i)))
                Next
                config.Add(New XElement("Framerate", TB_Framerate.Text))
                config.Add(New XElement("Duration", TB_Duration.Text))
                config.Add(New XElement("VerticalLock", CB_VerLk.IsChecked.Value))
                config.Add(New XElement("ResolutionLock", CB_ResLk.IsChecked.Value))
                config.Add(New XElement("VerticalOptimize", CB_VerOptm.IsChecked.Value))
                config.Add(New XElement("HorizontalOptimize", CB_HorOptm.IsChecked.Value))
                config.Add(New XElement("Fadeout", CB_Fadeout.IsChecked.Value))
                config.Add(New XElement("VerticalOptimizeRatio", TB_VORatio.Text))
                config.Add(New XElement("BreakStartHour", TB_BreakStart.Text))
                config.Add(New XElement("BreakEndHour", TB_BreakEnd.Text))
                config.Add(New XElement("HorizontalOptimizeRatio", TB_HORatio.Text))
                config.Add(New XElement("Transit", CbB_Transit.SelectedIndex))
                config.Add(New XElement("LoadQuality", TB_LoadQuality.Text))
                config.Add(New XElement("ScaleMode", CbB_ScaleMode.SelectedItem.Key))
                config.Add(New XElement("BlurMode", CbB_BlurMode.SelectedIndex))
                config.Add(New XElement("LoadMode", CbB_LoadMode.SelectedIndex))
                config.Add(New XElement("BreakMode", CbB_BreakMode.SelectedIndex))
                config.Add(New XElement("ClockStyle", CbB_ClockStyle.SelectedIndex))
                config.Add(New XElement("WeatherStyle", CbB_WeatherStyle.SelectedIndex))
                config.Add(New XElement("RandomizeV", CB_RandomizeV.IsChecked.Value))
                config.Add(New XElement("RandomizeA", CB_RandomizeA.IsChecked.Value))
                config.Add(New XElement("RecursiveFolder", CB_Img_Recur.IsChecked.Value))
                config.Add(New XElement("RecursiveMusic", CB_BGM_Recur.IsChecked.Value))
                config.Add(New XElement("ShowControl", CB_ShowControl.IsChecked.Value))
                config.Add(New XElement("NoLoop", CB_NoLoop.IsChecked.Value))

                'copy slides data if exists
                If My.Computer.FileSystem.FileExists(MainWindow.config_path) Then
                    Dim ori_config = XElement.Load(MainWindow.config_path)
                    If ori_config.Elements("DocumentElement").Any Then
                        config.Add(ori_config.Element("DocumentElement"))
                    End If
                End If

                config.Save(MainWindow.config_path)
                If Interop.ComponentDispatcher.IsThreadModal Then DialogResult = True Else Close()
    End Sub

    Private Function GetTargetLB(sender As Object) As ListBox
        Dim lb As ListBox = Nothing
        Select Case sender.Name
            Case NameOf(Btn_Img_Add), NameOf(Btn_Img_Edit), NameOf(Btn_Img_Rmv), NameOf(LB_ImgFolder)
                lb = LB_ImgFolder
            Case NameOf(Btn_BGM_Add), NameOf(Btn_BGM_Edit), NameOf(Btn_BGM_Rmv), NameOf(LB_BGMFolder)
                lb = LB_BGMFolder
        End Select
        Return lb
    End Function

    Private Sub Btn_Img_Add_Click(sender As Object, e As RoutedEventArgs) Handles Btn_Img_Add.Click, Btn_BGM_Add.Click
        Using dialog As New Forms.FolderBrowserDialog
            dialog.Description = Current.Resources("select folder")
            dialog.SelectedPath = lastPath
            If dialog.ShowDialog = Forms.DialogResult.OK Then
                GetTargetLB(sender).Items.Add(dialog.SelectedPath)
                lastPath = dialog.SelectedPath
            End If
        End Using
    End Sub

    Private Sub Btn_Img_Rmv_Click(sender As Object, e As RoutedEventArgs) Handles Btn_Img_Rmv.Click, Btn_BGM_Rmv.Click
        Dim lb As ListBox = GetTargetLB(sender)
        If lb.SelectedIndex <> -1 Then
            lb.Items.RemoveAt(lb.SelectedIndex)
        End If
    End Sub

    Private Sub Btn_Img_Edit_Click(sender As Object, e As RoutedEventArgs) Handles Btn_Img_Edit.Click, Btn_BGM_Edit.Click, LB_ImgFolder.MouseDoubleClick, LB_BGMFolder.MouseDoubleClick
        Dim lb As ListBox = GetTargetLB(sender)
        If lb.SelectedIndex <> -1 Then
            Using dialog As New Forms.FolderBrowserDialog
                dialog.Description = Current.Resources("select folder")
                dialog.SelectedPath = lb.SelectedItem.ToString
                If dialog.ShowDialog = Forms.DialogResult.OK Then
                    lb.Items(lb.SelectedIndex) = dialog.SelectedPath
                    lastPath = dialog.SelectedPath
                End If
            End Using
        End If
    End Sub

    Private Sub CbB_BreakMode_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles CbB_BreakMode.SelectionChanged
        If Me.IsLoaded Then
            If CbB_BreakMode.SelectedIndex = MainWindow.BREAK_MODE.NONE Then
                Grid_BreakTime.IsEnabled = False
                CbB_ClockStyle.IsEnabled = False
                CbB_WeatherStyle.IsEnabled = False
            Else
                Grid_BreakTime.IsEnabled = True

                If CbB_BreakMode.SelectedIndex = MainWindow.BREAK_MODE.CLOCK Then
                    CbB_ClockStyle.IsEnabled = True
                    CbB_WeatherStyle.IsEnabled = True
                Else
                    CbB_ClockStyle.IsEnabled = False
                    CbB_WeatherStyle.IsEnabled = False
                End If
            End If
        End If
    End Sub

    Private Sub CbB_Transit_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles CbB_Transit.SelectionChanged
        If Me.IsLoaded Then
            If CbB_Transit.SelectedIndex = 2 Then
                CB_VerOptm.IsEnabled = False
                CB_HorOptm.IsEnabled = False
                TB_VORatio.IsEnabled = False
                TB_HORatio.IsEnabled = False
            Else
                CB_VerOptm.IsEnabled = True
                CB_HorOptm.IsEnabled = True
                TB_VORatio.IsEnabled = True
                TB_HORatio.IsEnabled = True
            End If
        End If
    End Sub

    Private Sub CB_ResLk_Checked(sender As Object, e As RoutedEventArgs) Handles CB_ResLk.Checked, CB_ResLk.Unchecked
        If Me.IsLoaded Then
            If CB_ResLk.IsChecked Then
                TB_LoadQuality.IsEnabled = True
            Else
                If CbB_LoadMode.SelectedIndex = 1 AndAlso MsgBox(Current.Resources("msg_reslockwarning"), MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                    e.Handled = True
                    CB_ResLk.IsChecked = True
                Else
                    TB_LoadQuality.IsEnabled = False
                End If
            End If
        End If
    End Sub

    Private Sub CbB_LoadMode_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles CbB_LoadMode.SelectionChanged
        If Me.IsLoaded Then
            If CbB_LoadMode.SelectedIndex = 1 AndAlso Not CB_ResLk.IsChecked Then
                If MsgBox(Current.Resources("msg_reslockwarning"), MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                    CB_ResLk.IsChecked = True
                End If
            End If
        End If
    End Sub

    Private Sub Btn_FolderAsso_Click(sender As Object, e As RoutedEventArgs) Handles Btn_FolderAsso.Click
        If Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Classes\Directory\shell\OpenWithKenBurns\command") Is Nothing Then
            Dim shellkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\Classes\Directory\shell\OpenWithKenBurns")
            shellkey.SetValue("", Current.Resources("ken burns me"), Microsoft.Win32.RegistryValueKind.String)
            shellkey.CreateSubKey("command").SetValue("", """" & Reflection.Assembly.GetExecutingAssembly.Location & """ ""%1")
            Btn_FolderAsso.Content = Current.Resources("unregister menu")
        Else
            Dim dirkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Classes\Directory", True)
            dirkey.DeleteSubKeyTree("shell\OpenWithKenBurns")
            Dim shellkey = dirkey.OpenSubKey("shell", True)
            If shellkey.SubKeyCount = 0 AndAlso shellkey.ValueCount = 0 Then
                dirkey.DeleteSubKeyTree("shell")
            End If
            Btn_FolderAsso.Content = Current.Resources("register menu")
        End If
    End Sub
End Class
