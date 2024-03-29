﻿# Ken Burns Slideshow Touch Version
Instant, portable, full-screen slideshow application with the Ken Burns effect (**and more**)! Touch Version.
## Motivation
This idea came to me while watching a slideshow on iPad. How nice would it be if we can create instant slideshows on a Windows machine with only a single executable and a folder of images.

From this original version (does not support touch screen): https://github.com/changbowen/Ken-Burns-Slideshow/ Modified touch version.

此程序可用Ken Burns等三种效果播放文件夹中的图像，Ken Burns效果可参照iPad照片图库。

来源于这个原始版本（不支持触摸屏）：https://github.com/changbowen/Ken-Burns-Slideshow/ 修改的触摸屏版本。

Always get the latest development build from [here](https://github.com/honentan/kbstouch/blob/master/bin/Release/Ken%20Burns%20Slideshow%20Touch.zip).

最新开发版[传送门](https://github.com/honentan/kbstouch/blob/master/bin/Release/Ken%20Burns%20Slideshow%20Touch.zip)。

If you really like it [![PayPal](https://img.shields.io/badge/%24-PayPal-blue.svg)](https://www.paypal.me/honentan) or [支付宝](https://github.com/honentan/kbstouch/blob/master/images/preview/alipay.jpg) or [微信](https://github.com/honentan/kbstouch/blob/master/images/preview/wechat.jpg)

## Preview
- <img src="http://i.imgur.com/nbznvOh.gif" title="Ken Burns effect preview"/>
- <img src="http://i.imgur.com/A97UmCm.gif" title="Breath effect preview"/>
- <img src="http://i.imgur.com/d7Ap7t5.gif" title="Throw effect preview"/>
- <img src="https://github.com/honentan/kbstouch/blob/master/images/preview/path.jpg" width="600" title="Show path is default, double-clicke to cancel"/>
- <img src="https://github.com/honentan/kbstouch/blob/master/images/preview/controlPad.png" width="600" title="Control Pad(touch/click to display)"/>
- <img src="https://github.com/honentan/kbstouch/blob/master/images/preview/clock.png" width="600" title="Clock & Weather"/>
- <img src="https://github.com/honentan/kbstouch/blob/master/images/preview/option.png" width="600" title="New option for break"/>

## Language &amp; Requirement
- WPF application written in VB.net
- .Net framework 4.5 or above.
- 此程序基于WPF，使用VB.net编写。需要.Net framework 4.5或以上。

## Features
- Create instant full-screen slideshow with an imitation of the Ken Burns effect (**and more!**) for a set of folders that contains images. Images will be looped.
- Support displaying date for the images. Date value is parsed from the file name with format <em>yyyy-MM-dd</em>. If you want a date to be displayed, please rename the image files to the format for this to work. A valid example: <em>2015-12-02.jpg</em>
- Support audio files to be played in loop at background.
- Options to load large images at lower resolution to improve performance.
- Options for each slide (Edit Mode) is now available by pressing F11.
- Support launching slideshow directly from a folder containing images and audio files.
- Support EXIF rotation.
- Simplified Chinese language support.
- New in touch version
   - Support touch screen, touch/click to display control pad (with four buttons: settings, clock, mute, exit);
   - Added clock & weather, multiple styles of graphic display (restart application after modification);
   - Set the start and end time to switch to the clock & weather;
   - The file path is displayed by default and double-clicked to cancel.

## 功能特性
- 首次使用需要设置图片文件夹和音乐文件夹（可选）。
- 支持在每张图片上面显示日期。如果图片文件名是2015-12-02.jpg这种格式，屏幕显示的日期即为2015.12。
- 支持背景循环播放音频文件。
- 提供降低分辨率载入图像的选项以改善性能。
- 提供单张幻灯片设置（编辑模式），可添加自定义文本。
- 可关联Windows资源管理器右键菜单，右键点击文件夹直接播放。
- 支持EXIF旋转。
- 支持简体中文语言。
- 触屏版新增功能
  - 支持触屏，触摸/点击出现控制板（设置、时钟、静音、退出四个按钮）；
  - 新增时钟和天气，多种风格图形显示（修改后需重启应用）；
  - 设置切换为时钟显示的起止时间；
  - 默认显示文件路径，双击取消。

## Note
- Press **ESC** to fade out and quit.
- Press **F1** to re-open control panel after it is closed.
- Press **F12** at runtime for options.
- Press **F11** at runtime for Edit Mode dialog.
- Press **Ctrl+P** at runtime to hold off transition to the next image (i.e. pause).
- Press **Shift+P** at runtime to fade out and pause music.
- Press **Ctrl+R** at runtime to restart slideshow.
- Press **Ctrl+Q** to quit immediately without animations (otherwise fade to a black screen).
- Press **Ctrl+N** to jump to the next music item.
- Animation might not be as smooth when not using Windows Aero themes on Windows 7.
- Preferably a Windows 7 or above PC with a modern discrete GPU gives better / smoother performance. Any configuration that affect Aero performance will also affect animation playback. If you have multiple monitors, set to duplicate or single monitor mode for better performance.
- Framedrops may occur when certain programs are opened such as Potplayer and Foobar 2000.
- Choose "All at Once" under Load Mode to load all images at program start. It uses more memory but eliminates frame-drops in transition animation.
- Audio files with URI escape marks in the file name (e.g. This%20is%20a%20song%28I%20am%20kidding%29.mp3) will not be recognized due to .Net won't take strings in Media.MediaPlayer.Open and the only URI it takes just keeps unescaping the file name whenever it can.

## 使用说明
- 按 **ESC** 键淡出并退出。
- 按 **F1** 键打开/关闭控制窗口。
- 按 **F12** 键打开选项窗口。
- 按 **F11** 键打开编辑模式窗口。
- 按 **Ctrl+P** 可暂停/播放动画。
- 按 **Shift+P** 可淡出并暂停音乐，再按即恢复播放。
- 按 **Ctrl+R** 重新开始幻灯片。
- 按 **Ctrl+Q** 直接跳过动画淡出到桌面（否则则淡出到黑屏）。
- 按 **Ctrl+N** 下一首音乐。
- 在Windows 7系统里，如果Aero特效未开启，动画效果可能受影响。
- 使用独立显卡和Windows 7（或以上）动画效果会更流畅。任何降低Aero性能的配置都会影响动画效果。如果你连接了多个显示器，设置为双显示复制或者只使用一个显示器会改善动画效果。
- 部分应用程序同样会影响帧率。比如Potplayer和Foobar 2000。
- 载入模式选项中，选择“一次性载入”会在程序启动时载入所有图片。此选项会占用大量内存，但会改善换页动画的帧率。
- 文件名中包含URI转义符的音频文件（如：This%20is%20a%20song%28I%20am%20kidding%29.mp3）不会被识别。

## Updates
- 2019-06-12
  - First touch version.
