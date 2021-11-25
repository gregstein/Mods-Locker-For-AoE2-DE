﻿
namespace ModLocker
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.fp = new System.Windows.Forms.FlowLayoutPanel();
            this.listPROFILE = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.applychange = new System.Windows.Forms.Button();
            this.locker = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // fp
            // 
            this.fp.AutoScroll = true;
            this.fp.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.fp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fp.Location = new System.Drawing.Point(13, 74);
            this.fp.MaximumSize = new System.Drawing.Size(356, 222);
            this.fp.MinimumSize = new System.Drawing.Size(356, 222);
            this.fp.Name = "fp";
            this.fp.Size = new System.Drawing.Size(356, 222);
            this.fp.TabIndex = 0;
            // 
            // listPROFILE
            // 
            this.listPROFILE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listPROFILE.FormattingEnabled = true;
            this.listPROFILE.Location = new System.Drawing.Point(126, 18);
            this.listPROFILE.Name = "listPROFILE";
            this.listPROFILE.Size = new System.Drawing.Size(143, 21);
            this.listPROFILE.TabIndex = 1;
            this.listPROFILE.SelectedIndexChanged += new System.EventHandler(this.listPROFILE_SelectedIndexChanged);
            this.listPROFILE.Click += new System.EventHandler(this.listPROFILE_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Choose Steam Profile";
            // 
            // applychange
            // 
            this.applychange.Location = new System.Drawing.Point(275, 12);
            this.applychange.Name = "applychange";
            this.applychange.Size = new System.Drawing.Size(96, 30);
            this.applychange.TabIndex = 3;
            this.applychange.Text = "Apply Changes";
            this.applychange.UseVisualStyleBackColor = true;
            this.applychange.Click += new System.EventHandler(this.applychange_Click);
            // 
            // locker
            // 
            this.locker.BackColor = System.Drawing.Color.Transparent;
            this.locker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.locker.FlatAppearance.BorderSize = 0;
            this.locker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.locker.ImageIndex = 1;
            this.locker.ImageList = this.imageList1;
            this.locker.Location = new System.Drawing.Point(160, 302);
            this.locker.Name = "locker";
            this.locker.Size = new System.Drawing.Size(48, 43);
            this.locker.TabIndex = 4;
            this.locker.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.locker.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.locker.UseVisualStyleBackColor = false;
            this.locker.Click += new System.EventHandler(this.locker_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "lock.png");
            this.imageList1.Images.SetKeyName(1, "unlock.png");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(29, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(299, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mods that you Uncheck will be REMOVED!";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 64);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(356, 10);
            this.progressBar1.TabIndex = 6;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 358);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.locker);
            this.Controls.Add(this.applychange);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listPROFILE);
            this.Controls.Add(this.fp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Mods Locker for AoE2 DE (By GregStein)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel fp;
        private System.Windows.Forms.ComboBox listPROFILE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button applychange;
        private System.Windows.Forms.Button locker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

