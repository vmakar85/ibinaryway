using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace ASUpdater
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    static class some_data
    {
        public static String Val { get; set; }
    }

    static class Flag
    {
        public static Boolean CH_flag { get; set; }
    }

    #region Какая то непонятная фигня для получения инфы о достпности сети
        static class InternetState
        { 

        [DllImport("wininet.dll")]
            extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        public static bool IsConnected
        {
            get
            {
                int Desc;
                return InternetGetConnectedState(out Desc, 0);
            }
        }
    #endregion

    }
}
