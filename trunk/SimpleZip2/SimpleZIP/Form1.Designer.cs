namespace SimpleZIP
{
    partial class SimpleZIP
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleZIP));
            this.button_zipFile = new System.Windows.Forms.Button();
            this.button_zipFolder = new System.Windows.Forms.Button();
            this.button_UnZip = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_zipFile
            // 
            this.button_zipFile.Image = ((System.Drawing.Image)(resources.GetObject("button_zipFile.Image")));
            this.button_zipFile.Location = new System.Drawing.Point(12, 8);
            this.button_zipFile.Name = "button_zipFile";
            this.button_zipFile.Size = new System.Drawing.Size(107, 57);
            this.button_zipFile.TabIndex = 0;
            this.button_zipFile.UseVisualStyleBackColor = true;
            this.button_zipFile.Click += new System.EventHandler(this.button_zipFile_Click);
            // 
            // button_zipFolder
            // 
            this.button_zipFolder.Image = ((System.Drawing.Image)(resources.GetObject("button_zipFolder.Image")));
            this.button_zipFolder.Location = new System.Drawing.Point(12, 71);
            this.button_zipFolder.Name = "button_zipFolder";
            this.button_zipFolder.Size = new System.Drawing.Size(107, 57);
            this.button_zipFolder.TabIndex = 1;
            this.button_zipFolder.UseVisualStyleBackColor = true;
            this.button_zipFolder.Click += new System.EventHandler(this.button_zipFolder_Click);
            // 
            // button_UnZip
            // 
            this.button_UnZip.Image = ((System.Drawing.Image)(resources.GetObject("button_UnZip.Image")));
            this.button_UnZip.Location = new System.Drawing.Point(12, 134);
            this.button_UnZip.Name = "button_UnZip";
            this.button_UnZip.Size = new System.Drawing.Size(107, 57);
            this.button_UnZip.TabIndex = 2;
            this.button_UnZip.UseVisualStyleBackColor = true;
            this.button_UnZip.Click += new System.EventHandler(this.button_UnZip_Click);
            // 
            // SimpleZIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(130, 199);
            this.Controls.Add(this.button_UnZip);
            this.Controls.Add(this.button_zipFolder);
            this.Controls.Add(this.button_zipFile);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SimpleZIP";
            this.Text = "SimpleZIP";
            this.TransparencyKey = System.Drawing.SystemColors.ActiveCaption;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_zipFile;
        private System.Windows.Forms.Button button_zipFolder;
        private System.Windows.Forms.Button button_UnZip;
    }
}

