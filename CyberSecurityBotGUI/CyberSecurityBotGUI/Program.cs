using System;
using System.Windows.Forms;

namespace CyberSecurityBotGUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            VoiceGreeting.PlayGreeting();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChatBotForm());
        }
    }
}