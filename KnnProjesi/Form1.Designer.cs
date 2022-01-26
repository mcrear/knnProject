
namespace KnnProjesi
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnEkle = new System.Windows.Forms.Button();
            this.txtYEkseni = new System.Windows.Forms.TextBox();
            this.txtXEkseni = new System.Windows.Forms.TextBox();
            this.lbItemList = new System.Windows.Forms.ListBox();
            this.cbGrup = new System.Windows.Forms.ComboBox();
            this.txtKDegeri = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbEslesmeler = new System.Windows.Forms.ListBox();
            this.txtHedefY = new System.Windows.Forms.TextBox();
            this.txtHedefX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRnd = new System.Windows.Forms.Button();
            this.pnlMatris = new System.Windows.Forms.Panel();
            this.txtRnd = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(262, 38);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(66, 23);
            this.btnEkle.TabIndex = 0;
            this.btnEkle.Text = "Ekle";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // txtYEkseni
            // 
            this.txtYEkseni.Location = new System.Drawing.Point(88, 40);
            this.txtYEkseni.Name = "txtYEkseni";
            this.txtYEkseni.Size = new System.Drawing.Size(76, 20);
            this.txtYEkseni.TabIndex = 1;
            // 
            // txtXEkseni
            // 
            this.txtXEkseni.Location = new System.Drawing.Point(12, 40);
            this.txtXEkseni.Name = "txtXEkseni";
            this.txtXEkseni.Size = new System.Drawing.Size(70, 20);
            this.txtXEkseni.TabIndex = 2;
            // 
            // lbItemList
            // 
            this.lbItemList.FormattingEnabled = true;
            this.lbItemList.Location = new System.Drawing.Point(338, 77);
            this.lbItemList.Name = "lbItemList";
            this.lbItemList.Size = new System.Drawing.Size(274, 238);
            this.lbItemList.TabIndex = 3;
            // 
            // cbGrup
            // 
            this.cbGrup.FormattingEnabled = true;
            this.cbGrup.Items.AddRange(new object[] {
            "Sayı Grubu",
            "Harf Grubu"});
            this.cbGrup.Location = new System.Drawing.Point(170, 40);
            this.cbGrup.Name = "cbGrup";
            this.cbGrup.Size = new System.Drawing.Size(86, 21);
            this.cbGrup.TabIndex = 4;
            // 
            // txtKDegeri
            // 
            this.txtKDegeri.Location = new System.Drawing.Point(416, 341);
            this.txtKDegeri.Name = "txtKDegeri";
            this.txtKDegeri.Size = new System.Drawing.Size(100, 20);
            this.txtKDegeri.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(355, 344);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "K Değeri";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(536, 339);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 7;
            this.btnRun.Text = "Çalıştır";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "X Ekseni";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Y Ekseni";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(167, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Grup";
            // 
            // lbEslesmeler
            // 
            this.lbEslesmeler.FormattingEnabled = true;
            this.lbEslesmeler.Location = new System.Drawing.Point(12, 77);
            this.lbEslesmeler.Name = "lbEslesmeler";
            this.lbEslesmeler.Size = new System.Drawing.Size(316, 238);
            this.lbEslesmeler.TabIndex = 11;
            // 
            // txtHedefY
            // 
            this.txtHedefY.Location = new System.Drawing.Point(228, 341);
            this.txtHedefY.Name = "txtHedefY";
            this.txtHedefY.Size = new System.Drawing.Size(100, 20);
            this.txtHedefY.TabIndex = 12;
            // 
            // txtHedefX
            // 
            this.txtHedefX.Location = new System.Drawing.Point(64, 341);
            this.txtHedefX.Name = "txtHedefX";
            this.txtHedefX.Size = new System.Drawing.Size(100, 20);
            this.txtHedefX.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 344);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Hedef X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(174, 344);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Hedef Y";
            // 
            // btnRnd
            // 
            this.btnRnd.Location = new System.Drawing.Point(536, 38);
            this.btnRnd.Name = "btnRnd";
            this.btnRnd.Size = new System.Drawing.Size(75, 23);
            this.btnRnd.TabIndex = 19;
            this.btnRnd.Text = "Random";
            this.btnRnd.UseVisualStyleBackColor = true;
            this.btnRnd.Click += new System.EventHandler(this.btnRnd_Click);
            // 
            // pnlMatris
            // 
            this.pnlMatris.Location = new System.Drawing.Point(12, 367);
            this.pnlMatris.Name = "pnlMatris";
            this.pnlMatris.Size = new System.Drawing.Size(619, 600);
            this.pnlMatris.TabIndex = 20;
            // 
            // txtRnd
            // 
            this.txtRnd.Location = new System.Drawing.Point(444, 40);
            this.txtRnd.Name = "txtRnd";
            this.txtRnd.Size = new System.Drawing.Size(86, 20);
            this.txtRnd.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(441, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Rnd Adet";
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(338, 40);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(100, 20);
            this.txtSize.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(335, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Matris Size";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 975);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtRnd);
            this.Controls.Add(this.pnlMatris);
            this.Controls.Add(this.btnRnd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtHedefX);
            this.Controls.Add(this.txtHedefY);
            this.Controls.Add(this.lbEslesmeler);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtKDegeri);
            this.Controls.Add(this.cbGrup);
            this.Controls.Add(this.lbItemList);
            this.Controls.Add(this.txtXEkseni);
            this.Controls.Add(this.txtYEkseni);
            this.Controls.Add(this.btnEkle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.TextBox txtYEkseni;
        private System.Windows.Forms.TextBox txtXEkseni;
        private System.Windows.Forms.ListBox lbItemList;
        private System.Windows.Forms.ComboBox cbGrup;
        private System.Windows.Forms.TextBox txtKDegeri;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbEslesmeler;
        private System.Windows.Forms.TextBox txtHedefY;
        private System.Windows.Forms.TextBox txtHedefX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnRnd;
        private System.Windows.Forms.Panel pnlMatris;
        private System.Windows.Forms.TextBox txtRnd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.Label label8;
    }
}

