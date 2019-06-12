Option Explicit On

Imports System.Windows.Threading
Imports System.Windows.Media.Imaging
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Windows.Interop
Imports System.Diagnostics
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class ClockWeather
    Private digitImgs(11) As BitmapImage
    Private weatherImgs(11) As BitmapImage
    Private dTimer As DispatcherTimer
    Private screenWidth, screenHeight As Double

    Private Const Const_ClockImgPath = "pack://application:,,,/images/clock/"
    Private Const Const_WeatherImgPath = "pack://application:,,,/images/weather/"

    Private Structure WeatherInfo
        Dim high As String
        Dim low As String
        Dim aqi As String
        Dim fx As String
        Dim fl As String
        Dim type As String
    End Structure

    Private Enum WEATHER_TYPE As Integer
        DuoYun = 0
        LeiDian = 1
        LeiYu = 2
        Qing = 3
        TaiFeng = 4
        Xue = 5
        Yin = 6
        Yu = 7
        ZhenYu = 8
        DuoYun_ye = 9
        Qing_ye = 10
    End Enum


    '设置Owner后才可以显示在最前面
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Owner = Application.Current.MainWindow

        'AddHandler Me.Activated, AddressOf WindowActivated

#If DEBUG Then
        System.Diagnostics.Debug.Print(Format(Now, "hh:mm:ss") & " New...")
#End If

        screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight
        screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth

        'NewImgSource("paint")
        NewImgSource()

        TimerInit()

        ClockInit()
        WeatherInit()
    End Sub

    Private Sub Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        If Not MainWindow.nextcloseaction = MainWindow.CloseAction.DirectQuit Then
            e.Cancel = True
            Hide() 'if close, next F1 will throw exception
        End If
    End Sub

    'Private Sub NewImgSource(Optional ByVal clockStyle = "crystal", Optional ByVal weatherStyle = "cartoon")
    Private Sub NewImgSource()

        If weatherImgs(0) IsNot Nothing Then Exit Sub '已经初始化

        Dim baseUriStr As String

        Select Case MainWindow.weatherStyle
            Case MainWindow.WEATHER_STYLE.PAINT
                baseUriStr = Const_WeatherImgPath & "paint/"
            Case MainWindow.WEATHER_STYLE.CARTOON
                baseUriStr = Const_WeatherImgPath & "cartoon/"
            Case Else
                baseUriStr = Const_WeatherImgPath & "paint/"
        End Select

        weatherImgs(WEATHER_TYPE.DuoYun) = New BitmapImage(New Uri(baseUriStr & "duoyun-ye.png"))
        weatherImgs(WEATHER_TYPE.DuoYun_ye) = New BitmapImage(New Uri(baseUriStr & "duoyun.png"))
        weatherImgs(WEATHER_TYPE.LeiDian) = New BitmapImage(New Uri(baseUriStr & "leidian.png"))
        weatherImgs(WEATHER_TYPE.LeiYu) = New BitmapImage(New Uri(baseUriStr & "leiyu.png"))
        weatherImgs(WEATHER_TYPE.Qing) = New BitmapImage(New Uri(baseUriStr & "qing.png"))
        weatherImgs(WEATHER_TYPE.Qing_ye) = New BitmapImage(New Uri(baseUriStr & "qing-ye.png"))
        weatherImgs(WEATHER_TYPE.TaiFeng) = New BitmapImage(New Uri(baseUriStr & "taifeng.png"))
        weatherImgs(WEATHER_TYPE.Xue) = New BitmapImage(New Uri(baseUriStr & "xue.png"))
        weatherImgs(WEATHER_TYPE.Yin) = New BitmapImage(New Uri(baseUriStr & "yin.png"))
        weatherImgs(WEATHER_TYPE.Yu) = New BitmapImage(New Uri(baseUriStr & "yu.png"))
        weatherImgs(WEATHER_TYPE.ZhenYu) = New BitmapImage(New Uri(baseUriStr & "zhenyu.png"))

        Select Case MainWindow.clockStyle
            Case MainWindow.CLOCK_STYLE.PAINT
                baseUriStr = Const_ClockImgPath & "paint/"
            Case MainWindow.CLOCK_STYLE.CRYSTAL
                baseUriStr = Const_ClockImgPath & "crystal/"
            Case MainWindow.CLOCK_STYLE.PAGE
                baseUriStr = Const_ClockImgPath & "page/"
            Case MainWindow.CLOCK_STYLE.LCD
                baseUriStr = Const_ClockImgPath & "lcd/"
            Case MainWindow.CLOCK_STYLE.NEON
                baseUriStr = Const_ClockImgPath & "neon/"
            Case Else
                baseUriStr = Const_ClockImgPath & "paint/"
        End Select

        For i = 0 To 9
            digitImgs(i) = New BitmapImage(New Uri(baseUriStr & CStr(i) & ".png"))
        Next i
        digitImgs(10) = New BitmapImage(New Uri(baseUriStr & "c.png"))

    End Sub

    Private Sub ClockInit()

        If digitImgs(0) Is Nothing Then NewImgSource()

        img_colon.Source = digitImgs(10)
        img_hour_single.Source = digitImgs(CInt(Format(Now, "HH")) Mod 10)
        img_minute_single.Source = digitImgs(CInt(Format(Now, "mm")) Mod 10)
        img_hour_ten.Source = digitImgs(CInt(Format(Now, "HH")) \ 10)
        img_minute_ten.Source = digitImgs(CInt(Format(Now, "mm")) \ 10)

    End Sub

    Private Sub WeatherInit()
        WeatherInfUpdate()

    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        'ClockInit()
        'WeatherInit()
        Me.Topmost = True

    End Sub

    Private Sub TimerInit()

        If dTimer Is Nothing Then dTimer = New DispatcherTimer()

        AddHandler dTimer.Tick, AddressOf dispatcherTimer_Tick

        dTimer.Interval = New TimeSpan(0, 0, 1)
        dTimer.IsEnabled = True

        dTimer.Start()

#If DEBUG Then
        System.Diagnostics.Debug.Print(Format(Now, "hh:mm:ss") & " TimerInit...")
#End If
    End Sub

    Public Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        Static lastMinuteTen, lastMinuteSingle, lastHourTen, lastHourSingle As Integer
        Static breakScheduleEnd = False '是否刚刚结束break
        Dim nowHour, nowMinuteTen, nowMinuteSingle, nowHourTen, nowHourSingle As Integer

#If DEBUG Then
        'System.Diagnostics.Debug.Print(Format(Now, "hh:mm:ss") & " dispatcherTimer_Tick...")
#End If

        nowHour = CInt(Format(Now, "HH"))
        nowHourSingle = nowHour Mod 10
        nowHourTen = nowHour \ 10
        nowMinuteTen = CInt(Format(Now, "mm")) \ 10
        nowMinuteSingle = CInt(Format(Now, "mm")) Mod 10

        If nowHour >= MainWindow.breakStartHour Or nowHour < MainWindow.breakEndHour Then '休息时间
            If Not MainWindow.breakSchedule Then MainWindow.breakSchedule = True
        Else
            If MainWindow.breakSchedule Then
                MainWindow.breakSchedule = False
                breakScheduleEnd = True
            End If
        End If

        If MainWindow.breakSchedule Then    '休息时间
            Select Case MainWindow.breakMode
                Case MainWindow.BREAK_MODE.CLOCK
                    If Not IsVisible Then DirectCast(Owner, MainWindow).ShowClock()

                Case MainWindow.BREAK_MODE.SCROFF
                    If MainWindow.screenIsOn Then DirectCast(Owner, MainWindow).ScreenToggle()

                Case Else    'MainWindow.BREAK_MODE.NONE
                    Exit Sub
            End Select
        End If

        If breakScheduleEnd Then '刚刚结束break，恢复状态
            breakScheduleEnd = False

            Select Case MainWindow.breakMode
                Case MainWindow.BREAK_MODE.CLOCK
                    If IsVisible Then DirectCast(Owner, MainWindow).ShowClock()

                Case MainWindow.BREAK_MODE.SCROFF
                    If Not MainWindow.screenIsOn Then DirectCast(Owner, MainWindow).ScreenToggle(False)

                Case Else    'MainWindow.BREAK_MODE.NONE
                    Exit Sub
            End Select

        End If

        If Not IsVisible Then Exit Sub

        If lastHourSingle <> nowHourSingle Then '时变化
            WeatherInfUpdate()

            lastHourSingle = nowHourSingle
            img_hour_single.Source = digitImgs(lastHourSingle)
        End If
        If lastHourTen <> nowHourTen Then
            lastHourTen = nowHourTen
            img_hour_ten.Source = digitImgs(lastHourTen)
        End If
        If lastMinuteTen <> nowMinuteTen Then
            lastMinuteTen = nowMinuteTen
            img_minute_ten.Source = digitImgs(lastMinuteTen)
        End If

        If lastMinuteSingle <> nowMinuteSingle Then
            Dim ran = New System.Random()

            Me.Top = ran.Next(0, screenHeight - Me.Height)
            Me.Left = ran.Next(0, screenWidth - Me.Width)

            lastMinuteSingle = nowMinuteSingle
            img_minute_single.Source = digitImgs(lastMinuteSingle)
        End If

        'CommandManager.InvalidateRequerySuggested()

    End Sub


    Private Sub WeatherInfUpdate()
        Dim url As String = "http://t.weather.sojson.com/api/weather/city/101020600"
        Dim httpReq As System.Net.HttpWebRequest
        Dim httpResp As System.Net.HttpWebResponse
        Dim httpURL As New System.Uri(url)

        httpReq = CType(WebRequest.Create(httpURL), HttpWebRequest)
        httpReq.Method = "GET"
        httpResp = CType(httpReq.GetResponse(), HttpWebResponse)
        httpReq.KeepAlive = False

        'Dim reader As StreamReader = New StreamReader(httpResp.GetResponseStream, System.Text.Encoding.Default)
        Dim reader As StreamReader = New StreamReader(httpResp.GetResponseStream, System.Text.Encoding.UTF8)
        Dim respHTML As String = reader.ReadToEnd()  'respHTML就是网页内容

        'MsgBox("return[" & respHTML & "]")

        weatherInfoDecode(respHTML)
    End Sub

    Private Sub weatherInfoDecode(ByVal wInfJson As String)
        Dim p As JObject = CType(JsonConvert.DeserializeObject(wInfJson), JObject)

        Dim wInf As List(Of WeatherInfo)
        Dim tempText As String

        wInf = JsonConvert.DeserializeObject(Of List(Of WeatherInfo))(p("data")("forecast").ToString)

        weather_text.Text = wInf(0).type
        'txt_temp_low.Text = wInf(0).low.Substring(2).Trim
        tempText = wInf(0).low.Substring(2).Trim
        txt_temp_low.Text = tempText.Substring(0, tempText.Length - 3) + " ℃"
        'txt_temp_high.Text = wInf(0).high.Substring(2).Trim + "℃"
        tempText = wInf(0).high.Substring(2).Trim
        txt_temp_high.Text = tempText.Substring(0, tempText.Length - 3) + " ℃"

        Select Case wInf(0).type
            Case "多云"
                If CInt(Format(Now, "HH")) > 18 And CInt(Format(Now, "HH")) < 6 Then
                    img_weather.Source = weatherImgs(WEATHER_TYPE.DuoYun_ye)
                Else
                    img_weather.Source = weatherImgs(WEATHER_TYPE.DuoYun)
                End If
            Case "晴"
                If CInt(Format(Now, "HH")) > 18 And CInt(Format(Now, "HH")) < 6 Then
                    img_weather.Source = weatherImgs(WEATHER_TYPE.Qing_ye)
                Else
                    img_weather.Source = weatherImgs(WEATHER_TYPE.Qing)
                End If
            Case "阴"
                img_weather.Source = weatherImgs(WEATHER_TYPE.Yin)
            Case "雨", "小雨", "中雨", "大雨"
                img_weather.Source = weatherImgs(WEATHER_TYPE.Yu)
            Case "雷电"
                img_weather.Source = weatherImgs(WEATHER_TYPE.LeiDian)
            Case "雷雨"
                img_weather.Source = weatherImgs(WEATHER_TYPE.LeiYu)
            Case "雪"
                img_weather.Source = weatherImgs(WEATHER_TYPE.Xue)
            Case Else
                img_weather.Source = weatherImgs(WEATHER_TYPE.TaiFeng)
        End Select

        'MsgBox(wInf(0).type & " / " & wInf(0).low & " - " & wInf(0).high)

    End Sub

    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Const WM_SYSCOMMAND = &H112&
    Const SC_MONITORPOWER = &HF170&

    Public Sub ScreenOff()
#If DEBUG Then
        System.Diagnostics.Debug.Print(Format(Now, "hh:mm:ss") & " ScreenOff...")

        MsgBox("Wait 5 seconds... Screen will on again...")
#End If
        Dim wndHelper = New WindowInteropHelper(Me)
        Dim wpfHwnd = wndHelper.Handle

        SendMessage(wpfHwnd, WM_SYSCOMMAND, SC_MONITORPOWER, 2&)    '关闭显示器

        MainWindow.screenIsOn = False

#If DEBUG Then
        Threading.Thread.Sleep(5000)

        ScreenOn()
#End If
    End Sub


    Public Sub ScreenOn()
#If DEBUG Then
        System.Diagnostics.Debug.Print(Format(Now, "hh:mm:ss") & " Screen On...")
#End If
        Dim wndHelper = New WindowInteropHelper(Me)
        Dim wpfHwnd = wndHelper.Handle

        SendMessage(wpfHwnd, WM_SYSCOMMAND, SC_MONITORPOWER, -1&)    '打开显示器

        MainWindow.screenIsOn = True
    End Sub

End Class