using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Drawing;

namespace OpenCatStoreApp
{
    internal static class Notificator
    {
        public static void SendNotification<T>(T message)
        {
            if (message == null)
            {
                return;
            }
            try
            {
                new ToastContentBuilder().AddText(message.ToString()).Show();
            }
            catch (PlatformNotSupportedException)
            {
                System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
                notifyIcon.Icon = SystemIcons.Information;
                notifyIcon.Visible = true;
                notifyIcon.Text = message.ToString();
                notifyIcon.BalloonTipTitle = "AppTimeControl";
                notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
                notifyIcon.ShowBalloonTip(3000);
            }
        }
    }
}