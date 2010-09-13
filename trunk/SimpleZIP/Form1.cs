using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

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
            
        }

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
                folderBrowserDialog.Description = "Задайте папку куда следует сохранить полученный ZIP архив. \n Если папка не задана, то утилита сохранить полученный файл в корневой папке.";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string saveZipFilePath = folderBrowserDialog.SelectedPath;
                    addZipFile(zipFilePath__Name, zipFileName, name, saveZipFilePath + "/");
                }
                else
                {
                    MessageBox.Show("Папка не выбрана", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    addZipFile(zipFilePath__Name, zipFileName, name, Directory.GetCurrentDirectory() + "/");
                }
                MessageBox.Show("Архивация завершена", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        public void addZipFile(string zipFilePath, string zipFileName,string name ,string saveZipFilePath)
        {
            FastZip fz = new FastZip();
            Directory.CreateDirectory("Temp");
            File.Copy(zipFilePath, "Temp\\" + zipFileName , true);
            fz.CreateZip(saveZipFilePath + name + ".zip", "Temp", true, null);
            Directory.Delete("Temp", true);
        }

        public void UnZip(string zipFileName, string targetDir)
        {
            FastZip fz = new FastZip();
            fz.ExtractZip(zipFileName, targetDir, null);
        }

    }
}
