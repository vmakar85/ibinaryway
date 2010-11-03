#region Using
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.IO;
    using System.Security.Cryptography;
    using Ionic.Zip;
#endregion



#region Пустой_регион
#endregion


namespace SimpleZIP
{
    public partial class SimpleZIP : Form
    {

        #region Инициализация

             public SimpleZIP()
             {
                   InitializeComponent();
                   OS_ch();
             }

        #endregion

        #region Работа с кнопками
            /// <summary>
            /// зазиповать файл
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void button_zipFile_Click(object sender, EventArgs e)
            {

            }
            /// <summary>
            /// зазиповать папку
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void button_zipFolder_Click(object sender, EventArgs e)
            {

            }
            /// <summary>
            /// раззиповать архив
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void button_UnZip_Click(object sender, EventArgs e)
            {

            }
        #endregion


        #region Перетягивание формачки
             // постоянные переменные
             private const int WM_NCHITTEST = 0x84;
             private const int HTCLIENT = 0x1;
             private const int HTCAPTION = 0x2;
             // перетаскивание формы дергая ее за любую часть
             protected override void WndProc(ref Message m)
             {
                 switch (m.Msg)
                 {
                     case WM_NCHITTEST:
                         base.WndProc(ref m);
                         if ((int)m.Result == HTCLIENT)
                             m.Result = (IntPtr)HTCAPTION;
                         return;
                 }
                 base.WndProc(ref m);
             }
        #endregion
        #region Разные проверки
             // узнаем версию винды для последующей коректировки отображения окна ПО. 
             public void OS_ch()
             {
                 System.OperatingSystem os_ver = System.Environment.OSVersion;

                 if (os_ver.Version.Major <= 5)
                 {
                     TransparencyKey = System.Drawing.Color.Empty;
                     BackColor = System.Drawing.SystemColors.Control;
                 }
             }
        #endregion

    }
}
