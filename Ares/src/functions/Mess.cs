namespace Ares;

public class MessC{
    public static void ShowBalloonTip(
        string title,
        string text,
        EventHandler evt=null,
        string logopath="logo.ico",
        int time=3000,
        ToolTipIcon type=ToolTipIcon.Info
    ){
		NotifyIcon Notify = new NotifyIcon();
		Notify.Icon = new System.Drawing.Icon(logopath);
		Notify.BalloonTipText = text;
		Notify.BalloonTipTitle = title;
		Notify.BalloonTipIcon = type;
        if(evt != null) Notify.BalloonTipClicked += evt;
		Notify.Visible = true;
		Notify.ShowBalloonTip(time);
	}

    
    public static void ShowBalloonTipIcon(
        string title,
        string text,
        NotifyIcon Notify,
        EventHandler evt=null,
        int time=3000,
        ToolTipIcon type=ToolTipIcon.Info
    ){
		Notify.BalloonTipText = text;
		Notify.BalloonTipTitle = title;
		Notify.BalloonTipIcon = type;
        if(evt != null) Notify.BalloonTipClicked += evt;
		Notify.ShowBalloonTip(time);
	}
}