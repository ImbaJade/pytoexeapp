namespace PyToExe
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnCheckStatus = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnSound = new System.Windows.Forms.CheckBox();
            this.btnTop = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.delete = new System.Windows.Forms.Button();
            this.btnOut = new System.Windows.Forms.Button();
            this.btncopydownload = new System.Windows.Forms.Button();
            this.txtNotif = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUpload
            // 
            this.btnUpload.AllowDrop = true;
            this.btnUpload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnUpload.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.ForeColor = System.Drawing.Color.White;
            this.btnUpload.Location = new System.Drawing.Point(9, 102);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(173, 41);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "UPLOAD";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            this.btnUpload.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.btnUpload.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            // 
            // btnCheckStatus
            // 
            this.btnCheckStatus.AllowDrop = true;
            this.btnCheckStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCheckStatus.Enabled = false;
            this.btnCheckStatus.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckStatus.ForeColor = System.Drawing.Color.White;
            this.btnCheckStatus.Location = new System.Drawing.Point(9, 149);
            this.btnCheckStatus.Name = "btnCheckStatus";
            this.btnCheckStatus.Size = new System.Drawing.Size(173, 41);
            this.btnCheckStatus.TabIndex = 1;
            this.btnCheckStatus.Text = "CHECK STATUS";
            this.btnCheckStatus.UseVisualStyleBackColor = false;
            this.btnCheckStatus.Click += new System.EventHandler(this.btnCheckStatus_Click);
            this.btnCheckStatus.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.btnCheckStatus.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            // 
            // btnDownload
            // 
            this.btnDownload.AllowDrop = true;
            this.btnDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDownload.Enabled = false;
            this.btnDownload.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.Color.White;
            this.btnDownload.Location = new System.Drawing.Point(9, 196);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(173, 41);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "DOWNLOAD";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            this.btnDownload.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.btnDownload.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            // 
            // txtStatus
            // 
            this.txtStatus.AllowDrop = true;
            this.txtStatus.BackColor = System.Drawing.Color.Black;
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.ForeColor = System.Drawing.Color.Lime;
            this.txtStatus.Location = new System.Drawing.Point(281, 102);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(301, 105);
            this.txtStatus.TabIndex = 3;
            this.txtStatus.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.txtStatus.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            // 
            // btnSound
            // 
            this.btnSound.AutoSize = true;
            this.btnSound.Checked = true;
            this.btnSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnSound.Location = new System.Drawing.Point(9, 8);
            this.btnSound.Name = "btnSound";
            this.btnSound.Size = new System.Drawing.Size(57, 17);
            this.btnSound.TabIndex = 4;
            this.btnSound.Text = "Sound";
            this.btnSound.UseVisualStyleBackColor = true;
            this.btnSound.CheckedChanged += new System.EventHandler(this.btnSound_CheckedChanged);
            // 
            // btnTop
            // 
            this.btnTop.AutoSize = true;
            this.btnTop.Location = new System.Drawing.Point(9, 31);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(98, 17);
            this.btnTop.TabIndex = 5;
            this.btnTop.Text = "Always On Top";
            this.btnTop.UseVisualStyleBackColor = true;
            this.btnTop.CheckedChanged += new System.EventHandler(this.btnTop_CheckedChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.Yellow;
            this.linkLabel1.Location = new System.Drawing.Point(476, 11);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(106, 14);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "By: UNAMED666";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.Black;
            this.delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delete.ForeColor = System.Drawing.Color.White;
            this.delete.Location = new System.Drawing.Point(281, 214);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(301, 23);
            this.delete.TabIndex = 7;
            this.delete.Text = "DELETE YOUR UPLOADED .PY FILE HERE";
            this.delete.UseVisualStyleBackColor = false;
            this.delete.Visible = false;
            this.delete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnOut
            // 
            this.btnOut.AllowDrop = true;
            this.btnOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(255)))));
            this.btnOut.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOut.ForeColor = System.Drawing.Color.White;
            this.btnOut.Location = new System.Drawing.Point(9, 66);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(173, 29);
            this.btnOut.TabIndex = 8;
            this.btnOut.Text = "DOWNLOAD  FOLDER";
            this.btnOut.UseVisualStyleBackColor = false;
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // btncopydownload
            // 
            this.btncopydownload.AllowDrop = true;
            this.btncopydownload.BackColor = System.Drawing.Color.Black;
            this.btncopydownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncopydownload.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncopydownload.ForeColor = System.Drawing.Color.White;
            this.btncopydownload.Location = new System.Drawing.Point(428, 66);
            this.btncopydownload.Name = "btncopydownload";
            this.btncopydownload.Size = new System.Drawing.Size(154, 29);
            this.btncopydownload.TabIndex = 9;
            this.btncopydownload.Text = "Copy Download Link";
            this.btncopydownload.UseVisualStyleBackColor = false;
            this.btncopydownload.Visible = false;
            this.btncopydownload.Click += new System.EventHandler(this.btncopydownload_Click);
            // 
            // txtNotif
            // 
            this.txtNotif.AllowDrop = true;
            this.txtNotif.BackColor = System.Drawing.Color.Black;
            this.txtNotif.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNotif.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotif.ForeColor = System.Drawing.Color.Lime;
            this.txtNotif.Location = new System.Drawing.Point(9, 252);
            this.txtNotif.Multiline = true;
            this.txtNotif.Name = "txtNotif";
            this.txtNotif.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotif.Size = new System.Drawing.Size(603, 53);
            this.txtNotif.TabIndex = 10;
            this.txtNotif.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNotif.TextChanged += new System.EventHandler(this.txtNotif_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Monotype Corsiva", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.Location = new System.Drawing.Point(215, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 45);
            this.label1.TabIndex = 11;
            this.label1.Text = "PY to EXE";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(594, 310);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNotif);
            this.Controls.Add(this.btncopydownload);
            this.Controls.Add(this.btnOut);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnTop);
            this.Controls.Add(this.btnSound);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnCheckStatus);
            this.Controls.Add(this.btnUpload);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "PyToExe";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnCheckStatus;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.CheckBox btnSound;
        private System.Windows.Forms.CheckBox btnTop;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button btnOut;
        private System.Windows.Forms.Button btncopydownload;
        private System.Windows.Forms.TextBox txtNotif;
        private System.Windows.Forms.Label label1;
    }
}

