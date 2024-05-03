# ![logo](https://raw.githubusercontent.com/bartekmuzyk/PointAndShrink/master/PointAndShrink/Resources/appicon.ico) PointAndShrink
A Windows application to manage your cursor size between screens with different resolutions and scaling factors.

[How do I install this thing???](#install)

# The problem
Did you ever have an issue with your pointer looking out of proportion **depending on which monitor was displaying it**? As an example, here is my setup, which suffers from this exact problem:

|Screen with 125% scaling|Screen with 100% scaling|
|:----------------------:|:----------------------:|
|![](https://raw.githubusercontent.com/bartekmuzyk/PointAndShrink/master/PointAndShrink/screenshots/cursor125.png)|![](https://raw.githubusercontent.com/bartekmuzyk/PointAndShrink/master/PointAndShrink/screenshots/cursor100.png)|
|3 lines occupied|4 lines occupied|

What you see here is **the same pointer** against a background of equally spaced lines (they may not look like that on screenshots, there is an explanation later). As you can see, even though they should cover the same amount of space, the cursor is actually **bigger** on the screen where **smaller** scaling is applied!

This happens, because when Windows renders images/bitmaps/videos, they get rendered at **their target resolutions**, as to not blur them. UI elements however do not have this limitations and so they **get scaled** according to the monitor settings. Sadly, this rule applies to browsers too, which is why on the screenshots above it's the **lines that shorten** (since they are not an image) and not the **cursor that grows** (since the cursor *is* an image).

This means that the cursor will have **the same size** when compared to an image or a frame of video, but **a different size** when compared to interfaces. The result is an overgrown (or an overly shrinked) cursor appearing on some monitors, and a normal cursor on other monitors.

# The solution
This application provides a solution in the form of dynamically switching the *cursor scheme* using the registry depending on where the pointer is.

You can make the pointer have a `Default`, `Large` or `Extra large` size on a certain screen. Your configuration will be automatically saved whenever you change a setting.

# <a id="install"></a>Installation
Go to the [latest release](https://github.com/bartekmuzyk/PointAndShrink/releases/latest) and download the newest `.msi` file under **Assets**.

# How to use?
When you launch the app, nothing happens. That's because it is running in the background.

To bring its configuration window to foreground, click the <img src="https://raw.githubusercontent.com/bartekmuzyk/PointAndShrink/master/PointAndShrink/Resources/appicon.ico" width=16 alt="app icon" /> icon in the system tray.

## Launch at startup
There is no setting for this at the moment. To launch the app automatically on boot, you can go through this short process:
1. Press `Win`+`R` to open **Run**.
1. Type in `shell:startup` and press `Enter`.
1. Create a shortcut to the *PointAndShrink* executable file (`PointAndShrink.exe`) inside the directory that just opened.