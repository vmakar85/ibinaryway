using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

///To Do
/// ДОделать архивацию папок
///

namespace SimpleZIP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // тестируем ось
            OS_ch();
            //Вывод подсказки для кнопок
            helpTip(button1, "Запаковать файл в *.Zip архив");
            helpTip(button2, "Не работет, можно не жать... НЕ РАБОТАЕТ Я ТЕБЕ ГОВОРЮ!!!.");
            helpTip(button3, "Распаковать *.Zip файл");
        }

        ///
        /// Кнопки и действия для кнопок
        /// Кнопка 1 -запаковать 1н файл *готово
        /// Кнопка 2 -запаковать папку и файлы в ней *в работе
        /// Кнопка 3 -распаковать zip архив *готово
        ///

        // кнопка для сжатия 1го файла.. 
        private void button1_Click(object sender, EventArgs e)
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
                folderBrowserDialog.Description = "Задайте папку куда следует сохранить ZIP архив. \nЕсли папка не задана, то утилита сохранить \nполученный файл в корневой папке.";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string saveZipFilePath = folderBrowserDialog.SelectedPath;
                    addZipFile(zipFilePath__Name, zipFileName, name, saveZipFilePath + "/");
                    MessageBox.Show("Архивация завершена \n" + "MD5SUM : " + MD5Sum(saveZipFilePath + "/" + name + ".zip") + "\n" + "Внимание после закрытия данного окноа значение MD5Sum окажется в Вашем буфере обмена.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    CCopy(MD5Sum(saveZipFilePath + "/" + name + ".zip"));
                }
                else
                {
                    MessageBox.Show("Папка не выбрана", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    addZipFile(zipFilePath__Name, zipFileName, name, Directory.GetCurrentDirectory() + "/");
                    MessageBox.Show("Архивация завершена\n" + "MD5SUM : " + MD5Sum(Directory.GetCurrentDirectory() + "/" + name + ".zip") + "\n" + "Внимание после закрытия данного окноа значение MD5Sum окажется в Вашем буфере обмена.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    CCopy(MD5Sum(Directory.GetCurrentDirectory() + "/" + name + ".zip"));
                }
            
            }
            else
            {
                MessageBox.Show("Файл не выбран", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Только в платной версии =P", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        // Кнопка для распаковки файла архива
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog1 = new OpenFileDialog();

            fileDialog1.Title = "Распаковать фойл";
            fileDialog1.Filter = "Simpl zip (*.zip)|*.zip|All files (*.*)|*.*";
            fileDialog1.FilterIndex = 1;
            fileDialog1.RestoreDirectory = true;

            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
                string zipFileName = fileDialog1.SafeFileName;

                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

                UnZip(zipFileName, Directory.GetCurrentDirectory());
            }
        }

        ///
        /// Работа с архивом(и) 
        /// Запаковка\распаковка файлов/папок - общие методы. 
        ///
        // запоковка 1го файла в архив
        public void addZipFile(string zipFilePath, string zipFileName,string name ,string saveZipFilePath)
        {
            FastZip fz = new FastZip();
            Directory.CreateDirectory("Temp");
            File.Copy(zipFilePath, "Temp\\" + zipFileName , true);
            fz.CreateZip(saveZipFilePath + name + ".zip", "Temp", true, null);
            Directory.Delete("Temp", true);
        }
        // распаковка файлов
        public void UnZip(string zipFileName, string targetDir)
        {
            FastZip fz = new FastZip();
            fz.ExtractZip(zipFileName, targetDir, null);
        }
        // запаковка папки с файломи ( общий метод ) 
        public void AddZipFolders(string zipFileName, string dir)
        {
            //ZipFile zipFile = ZipFile.Create(zipFileName);

            //string fileToAdd = "";

            //zipFile.BeginUpdate();

            //zipFile.AddDirectory(dir);

            //zipFile.Add(fileToAdd);

            //zipFile.CommitUpdate();
        }



        ///
        /// Не очень важные куски кода или дополнительные методы
        ///

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
        // узнаем версию винды для последующей коректировки отображения окна ПО. 
        public void OS_ch()
        {
            System.OperatingSystem os_ver = System.Environment.OSVersion;

            if (os_ver.Version.Major <= 5)
            {
                TransparencyKey = System.Drawing.Color.Empty;
                BackColor = System.Drawing.SystemColors.Control;
            }
           // return os_ver;
        }

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

        // Визуальные подсказки для кнопок
        public void helpTip(Control button, string TextForTIPS)
        {
            ToolTip t = new ToolTip();
            t.UseAnimation = true;
            //t.ToolTipIcon = ToolTipIcon.Info;
            t.SetToolTip(button, TextForTIPS);
        }

    }
}
