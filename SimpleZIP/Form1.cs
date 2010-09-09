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
                addZipFile(zipFilePath__Name, zipFileName);
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Только в платной версии =P");
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
                UnZip(zipFileName, Directory.GetCurrentDirectory());
            }
        }

        public void addZipFile(string zipFilePath, string zipFileName)
        {
            FastZip fz = new FastZip();
            Directory.CreateDirectory("Temp");
            File.Copy(zipFilePath, "Temp\\" + zipFileName);
            fz.CreateZip(zipFileName + ".zip", "Temp", true, null);
            Directory.Delete("Temp", true);
        }

        public void UnZip(string zipFileName, string targetDir)
        {
            FastZip fz = new FastZip();
            fz.ExtractZip(zipFileName, targetDir, null);
        }

    }
}
