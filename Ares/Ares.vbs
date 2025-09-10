Set WshShell = CreateObject("WScript.Shell")
WshShell.CurrentDirectory = WshShell.ExpandEnvironmentStrings("%APPDATA%") & "\Ares"
WshShell.Run chr(34) & "Ares_Launcher.exe" & Chr(34), 0
Set WshShell = Nothing