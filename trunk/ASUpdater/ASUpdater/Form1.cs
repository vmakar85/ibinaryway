#region Using
// основные библиотеки
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;

// вспомогательные библиотеки.
using Kent.Boogaart.KBCsv;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

#endregion
///
/// Незабыть перенести остольные методы из ранней версии.
/// не забыть добавить отдельное окно для выбора насителя после нажатия кнопки установить обновление 
///


namespace ASUpdater
{
    
    public partial class Form1 : Form
    {

        public List<String> Links;
        List<string> links = new List<string>();

    #region Инициализация формы
        public Form1()
        {
            InitializeComponent();

            Thread Thre1 = new Thread(testOS);
            Thre1.Start();

            Testinet();

            //Usb_drive_scane();

            conf_File_loading("osm maps.csv", "http://s1.autosputnik.com/maps/osm_maps.csv");
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

    #endregion


    #region работа с архивами
        // мет. распаковка файлов из zip архива и удаление зип файла за собой.
        public static void UNFastzip(object UzipFileName)
        {
            try
            {
                if (File.Exists(UzipFileName.ToString()))
                {
                    FastZip NFastZip = new FastZip();
                    NFastZip.ExtractZip(UzipFileName.ToString(), @"Temp", null);
                    File.Delete(UzipFileName.ToString());
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Упс что то не то!!! в блоке UNFastzip сообщите програмисту \n" + ex); }
            
        }
    #endregion


    #region работа над файлами

        // Мет. копирование файлов, с предворительной проверкой на наличие файла
        public void FileCopy(object FromPath,object ToPath)
        {
            try
            {
                if (File.Exists(FromPath.ToString()))
                {
                    File.Copy(FromPath.ToString(), ToPath.ToString(), true);
                }
            }
            catch(Exception ex)
            { MessageBox.Show("Упс что то не то!!! в блоке FileCopy сообщите програмисту \n" + ex); }
        }

    #endregion


    #region Работа с визуальной частью
        // кнопка обновить
        void update_button_Click(object sender, EventArgs e)
        {
            update_button.Enabled = false;
            toolStripProgressBar1.Enabled = true;

            //Thread Thre = new Thread(UNFastzip);
            //Thre.Start("Temp.zip");
            //Thread Thre2 = new Thread(FileCopy);
            //Thre2.Start("list", "list2");
            Read_Data_Frome_CSV();

            ///
            /// Создать кнопку для обновления после чтения всего этого говна
            ///
            Button UPBT = new Button();
            flowLayoutPanel1.Controls.Add(UPBT);
            UPBT.Click += new System.EventHandler(Update_Click);
            UPBT.Name =  "Update";
            UPBT.Size = new Size(500, 60);
            UPBT.Font = new Font("Comic Sans MS", 25f);
            UPBT.Text = "Обновить" ; 

        }

        /// <summary>
        /// кнопка настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settings_button_Click(object sender, EventArgs e)
        {
            Usb_drive_scane();
        }

        /// <summary>
        /// Кнопка обновить все это нафиг!!!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Update_Click(object sender, EventArgs e)
        {
            ASuncFile_load("http://s1.autosputnik.com/maps/as5_osm-bulgaria_2010-09-06_030343.zip", @"as5_osm-bulgaria_2010-09-06_030343.zip");
            foreach (string link in links)
            {
                MessageBox.Show(link);
            }
        }
        
        /// <summary>
        ///  зкщцентаж загрузки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.ProgressBar.Value = e.ProgressPercentage;
        }

        void DownloadFileCallBack2(object sender, AsyncCompletedEventArgs c)
        {
            MessageBox.Show("Download Completed");
            toolStripProgressBar1.Visible = false;
            toolStripProgressBar1.Value = 0;
            toolStripStatusLabel2.Visible = false;
        }


    #endregion


    #region Всякие проверки
        // Проверка ОСи от пользюков решивших запустить утилиту на PND
        public void testOS() 
        {
            OperatingSystem os = Environment.OSVersion;
            PlatformID pid = os.Platform;

            if (pid == PlatformID.WinCE)
            { this.Close();}
        }

        public void Testinet()
        {
            if (InternetState.IsConnected == true)
            {
                toolStripStatusLabel1.Text = "Доступ к www : есть";
            }
            else
            {
                toolStripStatusLabel1.Text = "Доступ к www : нет";
            }
        }
        
    #endregion


    #region Работа с сетью
        // метод для загрузки файла асинхронна ( без блокировки действий ) КОНФИГ ФАЙЛ
        void conf_File_loading(string filename, string wwwFilePath)
        {
            try
            {
                if (System.IO.File.Exists(@filename))
                {
                    File.Delete(filename);
                    WebClient myWebClient = new WebClient();
                    Uri sysU = new Uri(wwwFilePath);
                    myWebClient.DownloadFileAsync(sysU, filename);
                }
                else
                {
                    WebClient myWebClient = new WebClient();
                    Uri sysU = new Uri(wwwFilePath);
                    myWebClient.DownloadFileAsync(sysU, filename);
                }
            }
            catch
            {
                MessageBox.Show("Возможно нет инета!!!");
            }
        }
        // метод для загрузки файла асинхронна ( без блокировки действий ) КАРТЫ
        void ASuncFile_load(string wFilePath, string fileName)
        {
            WebClient myWEbClient = new WebClient();
            Uri sysU = new Uri(wFilePath);
            myWEbClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallBack2);
            myWEbClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
            toolStripStatusLabel2.Text = "Загружаю : " + fileName;
            toolStripStatusLabel2.Visible = true;
            myWEbClient.DownloadFileAsync(sysU, @"Temp\" + fileName);
            
             //.ToolTipText = "" + fileName + ".zip";
        }
    #endregion


    #region Тестовый полигон
       
    #endregion


    #region Работа в usb насителями

        void Usb_drive_scane()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady && d.DriveType == DriveType.Removable)
                {
                    Label lb1 = new Label();
                    Label lb2 = new Label();
                    Label lb3 = new Label();

                    lb1.Width = 320;
                    lb2.Width = 320;
                    lb3.Width = 320;

                    flowLayoutPanel1.Controls.Add(lb1);
                    flowLayoutPanel1.Controls.Add(lb2);
                    flowLayoutPanel1.Controls.Add(lb3);
                    lb1.Text = "Name: " + d.Name;
                    lb2.Text = "TotalFreeSpace: " + (d.TotalFreeSpace / 1049078);
                    lb3.Text = "RootDirectory: " + d.RootDirectory;
                }
            }
        
        }


    #endregion


    #region чтение данных из таблици

        public void Read_Data_Frome_CSV()
        {
            using (CsvReader reader = new CsvReader(@"osm maps.csv"))
            {
                reader.ValueSeparator = ';';
                DataRecord record = null;
               
                
                
                
                while ((record = reader.ReadDataRecord()) != null)
                {

                    Label l = new Label();
                    flowLayoutPanel1.Controls.Add(l);
                    l.Text = record[0];
                    //if (record.Values != null)
                    if ( record.Values[1] != "") 
                    {

                        
                        Label l2 = new Label();
                        flowLayoutPanel1.Controls.Add(l2);
                        l2.Width = 370;
                        l2.Text = "Размер архива : " + record[2] + "Mb" + " размер карты после распаковки : " + record[3] + "Mb" ;
                        Label l3 = new Label();
                        flowLayoutPanel1.Controls.Add(l3);
                        l3.Width = 370;
                        l3.Text =  "Описание: " + record[4];
                        CheckBox ch = new CheckBox();
                        ch.Width = 370; 
                        flowLayoutPanel1.Controls.Add(ch);
                        ch.Text = "Загрузить для последующей установки ?"; 

                        links.Add(record[1]);
                        
                    }
                }
            }
        }
     #endregion

    }
  }
