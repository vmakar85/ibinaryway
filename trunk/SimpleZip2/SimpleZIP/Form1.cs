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
                OpenFileDialog fileDialog1 = new OpenFileDialog();

                fileDialog1.Title = "Выберите файл для архивации";
                fileDialog1.Filter = "DarkBasicPro files (*.dba)|*.dba|All files (*.*)|*.*";
                fileDialog1.FilterIndex = 1;
                fileDialog1.RestoreDirectory = true;

                if (fileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string zipFileName = fileDialog1.SafeFileName;
                    string zipFilePath__Name = fileDialog1.FileName;
                    string name = zipFileName.Substring(0, zipFileName.LastIndexOf('.'));

                    string CurrentDirectory = Directory.GetCurrentDirectory();
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    folderBrowserDialog.Description = "Задайте папку куда следует сохранить ZIP архив. \nЕсли папка не задана, то утилита сохранить \nполученный файл в папку рядом с исходным файлом.";

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        string saveZipFilePath = folderBrowserDialog.SelectedPath;
                        PuckFile(zipFilePath__Name, saveZipFilePath + "/" + name + ".zip");
                        MessageBox.Show("Архивация завершена \n" + "MD5SUM : " + MD5Sum(saveZipFilePath + "/" + name + ".zip") + "\n" + "Внимание после закрытия данного окноа значение MD5Sum окажется в Вашем буфере обмена.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        CCopy(MD5Sum(saveZipFilePath + "/" + name + ".zip"));
                    }
                    else
                    {
                        MessageBox.Show("Папка не выбрана", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        PuckFile(zipFilePath__Name, Directory.GetCurrentDirectory() + "/" + name + ".zip");
                        MessageBox.Show("Архивация завершена\n" + "MD5SUM : " + MD5Sum(Directory.GetCurrentDirectory() + "/" + name + ".zip") + "\n" + "Внимание после закрытия данного окноа значение MD5Sum окажется в Вашем буфере обмена.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        CCopy(MD5Sum(Directory.GetCurrentDirectory() + "/" + name + ".zip"));
                    }

                }
                else
                {
                    MessageBox.Show("Файл не выбран", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
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

        #region Работа с архивами
            /// <summary>
            /// Распаковка файла ZIP
            /// </summary>
            /// <param name="ExistingZipFile"></param>
            /// <param name="TargetDirectory"></param>
            public void Unpack(String ExistingZipFile, String TargetDirectory)
            {
                if (File.Exists(ExistingZipFile))
                {
                    using (ZipFile zip = ZipFile.Read(ExistingZipFile))
                    {
                        foreach (ZipEntry e in zip)
                        { e.Extract(TargetDirectory, ExtractExistingFileAction.OverwriteSilently ); }
                    }
                }
            }
            /// <summary>
            /// Запаковать 1 файл
            /// </summary>
            /// <param name="FileToZip_name"></param>
            /// <param name="ZipFileName"></param>
            public void PuckFile(String FileToZip_name, String ZipFileName)
            {

                String name = Path.GetFileName(FileToZip_name);

                String directoryName = Path.GetDirectoryName(FileToZip_name);
                Directory.SetCurrentDirectory(directoryName);

                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(name);
                    zip.AddProgress += new EventHandler<AddProgressEventArgs>(zip_AddProgress);
                    zip.SaveProgress += new EventHandler<SaveProgressEventArgs>(zip_SaveProgress);
                    zip.Save(ZipFileName);
                }
            }

            void zip_SaveProgress(object sender, SaveProgressEventArgs e)
            {
 
                progressBar1.Visible = true;
                //progressBar1.Value = Convert.ToInt32(100 * e.BytesTransferred / e.TotalBytesToTransfer); 
            }

            void zip_AddProgress(object sender, AddProgressEventArgs e)
            {
                progressBar1.Visible = true; 
            }
            /// <summary>
            /// запаковать 1 папку
            /// </summary>
            /// <param name="FolderToZip_name"></param>
            /// <param name="ZipFileName"></param>
            public void PuckFolder(String FolderToZip_name, String ZipFileName)
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(FolderToZip_name);
                    zip.Save(ZipFileName); 
                }
            }


        #endregion 

        #region Работа с буфером обмена / выуживание MD5 из файла
            // Выкоыриваем MD5 из файла архива
            public string MD5Sum(string fileName)
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            // копируем текст в буфер обмена
            public void CCopy(string text)
            {
                Clipboard.SetText(text);
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
