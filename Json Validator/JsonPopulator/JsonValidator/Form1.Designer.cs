
namespace JsonValidator
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
        public void InitializeComponent()
        {
            this.GenerateJsonButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.boxIDcomboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.appearanceLabel = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.MysteryBoxTypeCB = new System.Windows.Forms.ComboBox();
            this.ThemeCB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.StyleCB = new System.Windows.Forms.ComboBox();
            this.RibbonLocKeyCB = new System.Windows.Forms.ComboBox();
            this.TitleLocKeyCB = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SubLocKeyCB = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.StorePopsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.PurchaseScrnLbl = new System.Windows.Forms.Label();
            this.PSTitleLocKey = new System.Windows.Forms.TextBox();
            this.PurchasePopsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.MainHubLbl = new System.Windows.Forms.Label();
            this.canShowCarouselBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // GenerateJsonButton
            // 
            this.GenerateJsonButton.Location = new System.Drawing.Point(291, 34);
            this.GenerateJsonButton.Name = "GenerateJsonButton";
            this.GenerateJsonButton.Size = new System.Drawing.Size(144, 46);
            this.GenerateJsonButton.TabIndex = 0;
            this.GenerateJsonButton.Text = "Generate Json";
            this.GenerateJsonButton.UseVisualStyleBackColor = true;
            this.GenerateJsonButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 95);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(199, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // boxIDcomboBox
            // 
            this.boxIDcomboBox.FormattingEnabled = true;
            this.boxIDcomboBox.Location = new System.Drawing.Point(8, 34);
            this.boxIDcomboBox.Name = "boxIDcomboBox";
            this.boxIDcomboBox.Size = new System.Drawing.Size(211, 21);
            this.boxIDcomboBox.TabIndex = 2;
            this.boxIDcomboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Start Date and Time";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "End Date and Time";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 149);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(199, 20);
            this.dateTimePicker1.TabIndex = 9;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(12, 201);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(199, 20);
            this.dateTimePicker2.TabIndex = 10;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "FandomID";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // appearanceLabel
            // 
            this.appearanceLabel.AutoSize = true;
            this.appearanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appearanceLabel.Location = new System.Drawing.Point(9, 224);
            this.appearanceLabel.Name = "appearanceLabel";
            this.appearanceLabel.Size = new System.Drawing.Size(85, 17);
            this.appearanceLabel.TabIndex = 12;
            this.appearanceLabel.Text = "Appearance";
            this.appearanceLabel.Click += new System.EventHandler(this.label4_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 258);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(92, 17);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Is Event Box?";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // MysteryBoxTypeCB
            // 
            this.MysteryBoxTypeCB.FormattingEnabled = true;
            this.MysteryBoxTypeCB.Items.AddRange(new object[] {
            "LuckyMystery"});
            this.MysteryBoxTypeCB.Location = new System.Drawing.Point(122, 278);
            this.MysteryBoxTypeCB.MinimumSize = new System.Drawing.Size(4, 0);
            this.MysteryBoxTypeCB.Name = "MysteryBoxTypeCB";
            this.MysteryBoxTypeCB.Size = new System.Drawing.Size(137, 21);
            this.MysteryBoxTypeCB.TabIndex = 14;
            this.MysteryBoxTypeCB.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // ThemeCB
            // 
            this.ThemeCB.FormattingEnabled = true;
            this.ThemeCB.Items.AddRange(new object[] {
            "\"\""});
            this.ThemeCB.Location = new System.Drawing.Point(122, 304);
            this.ThemeCB.MinimumSize = new System.Drawing.Size(4, 0);
            this.ThemeCB.Name = "ThemeCB";
            this.ThemeCB.Size = new System.Drawing.Size(137, 21);
            this.ThemeCB.TabIndex = 15;
            this.ThemeCB.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 286);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Mystery Box Type";
            this.label4.Click += new System.EventHandler(this.label4_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 312);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Theme";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 343);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "Store Button";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // StyleCB
            // 
            this.StyleCB.FormattingEnabled = true;
            this.StyleCB.Items.AddRange(new object[] {
            "MediumPurple"});
            this.StyleCB.Location = new System.Drawing.Point(105, 366);
            this.StyleCB.MinimumSize = new System.Drawing.Size(4, 0);
            this.StyleCB.Name = "StyleCB";
            this.StyleCB.Size = new System.Drawing.Size(137, 21);
            this.StyleCB.TabIndex = 19;
            // 
            // RibbonLocKeyCB
            // 
            this.RibbonLocKeyCB.FormattingEnabled = true;
            this.RibbonLocKeyCB.Items.AddRange(new object[] {
            "GeneralBoxRibbon"});
            this.RibbonLocKeyCB.Location = new System.Drawing.Point(105, 393);
            this.RibbonLocKeyCB.MinimumSize = new System.Drawing.Size(4, 0);
            this.RibbonLocKeyCB.Name = "RibbonLocKeyCB";
            this.RibbonLocKeyCB.Size = new System.Drawing.Size(137, 21);
            this.RibbonLocKeyCB.TabIndex = 20;
            // 
            // TitleLocKeyCB
            // 
            this.TitleLocKeyCB.FormattingEnabled = true;
            this.TitleLocKeyCB.Items.AddRange(new object[] {
            "LuckyMystery"});
            this.TitleLocKeyCB.Location = new System.Drawing.Point(105, 420);
            this.TitleLocKeyCB.MinimumSize = new System.Drawing.Size(4, 0);
            this.TitleLocKeyCB.Name = "TitleLocKeyCB";
            this.TitleLocKeyCB.Size = new System.Drawing.Size(137, 21);
            this.TitleLocKeyCB.TabIndex = 21;
            this.TitleLocKeyCB.SelectedIndexChanged += new System.EventHandler(this.comboBox6_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 374);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Style";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 400);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Ribbon Loc Key";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 428);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Title Loc Key";
            // 
            // SubLocKeyCB
            // 
            this.SubLocKeyCB.FormattingEnabled = true;
            this.SubLocKeyCB.Items.AddRange(new object[] {
            "LuckyMystery"});
            this.SubLocKeyCB.Location = new System.Drawing.Point(105, 447);
            this.SubLocKeyCB.MinimumSize = new System.Drawing.Size(4, 0);
            this.SubLocKeyCB.Name = "SubLocKeyCB";
            this.SubLocKeyCB.Size = new System.Drawing.Size(137, 21);
            this.SubLocKeyCB.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 455);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "Sub Loc Key";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 488);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "Pop Ids";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(62, 483);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 22);
            this.button1.TabIndex = 31;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // StorePopsPanel
            // 
            this.StorePopsPanel.Location = new System.Drawing.Point(105, 483);
            this.StorePopsPanel.Name = "StorePopsPanel";
            this.StorePopsPanel.Size = new System.Drawing.Size(327, 111);
            this.StorePopsPanel.TabIndex = 32;
            this.StorePopsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // PurchaseScrnLbl
            // 
            this.PurchaseScrnLbl.AutoSize = true;
            this.PurchaseScrnLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PurchaseScrnLbl.Location = new System.Drawing.Point(12, 611);
            this.PurchaseScrnLbl.Name = "PurchaseScrnLbl";
            this.PurchaseScrnLbl.Size = new System.Drawing.Size(198, 17);
            this.PurchaseScrnLbl.TabIndex = 33;
            this.PurchaseScrnLbl.Text = "Purchase Screen Appearance";
            this.PurchaseScrnLbl.Click += new System.EventHandler(this.label12_Click);
            // 
            // PSTitleLocKey
            // 
            this.PSTitleLocKey.Location = new System.Drawing.Point(108, 640);
            this.PSTitleLocKey.Name = "PSTitleLocKey";
            this.PSTitleLocKey.Size = new System.Drawing.Size(120, 20);
            this.PSTitleLocKey.TabIndex = 34;
            this.PSTitleLocKey.TextChanged += new System.EventHandler(this.PSTitleLocKey_TextChanged);
            // 
            // PurchasePopsPanel
            // 
            this.PurchasePopsPanel.Location = new System.Drawing.Point(108, 671);
            this.PurchasePopsPanel.Name = "PurchasePopsPanel";
            this.PurchasePopsPanel.Size = new System.Drawing.Size(327, 117);
            this.PurchasePopsPanel.TabIndex = 37;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(65, 666);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 22);
            this.button2.TabIndex = 36;
            this.button2.Text = "+";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 671);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 35;
            this.label12.Text = "Pop Ids";
            // 
            // MainHubLbl
            // 
            this.MainHubLbl.AutoSize = true;
            this.MainHubLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainHubLbl.Location = new System.Drawing.Point(9, 804);
            this.MainHubLbl.Name = "MainHubLbl";
            this.MainHubLbl.Size = new System.Drawing.Size(149, 17);
            this.MainHubLbl.TabIndex = 38;
            this.MainHubLbl.Text = "Main Hub Appearance";
            // 
            // canShowCarouselBox
            // 
            this.canShowCarouselBox.AutoSize = true;
            this.canShowCarouselBox.Location = new System.Drawing.Point(16, 824);
            this.canShowCarouselBox.Name = "canShowCarouselBox";
            this.canShowCarouselBox.Size = new System.Drawing.Size(137, 17);
            this.canShowCarouselBox.TabIndex = 39;
            this.canShowCarouselBox.Text = "Can Show In Carousel?";
            this.canShowCarouselBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 927);
            this.Controls.Add(this.canShowCarouselBox);
            this.Controls.Add(this.MainHubLbl);
            this.Controls.Add(this.PurchasePopsPanel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.PSTitleLocKey);
            this.Controls.Add(this.PurchaseScrnLbl);
            this.Controls.Add(this.StorePopsPanel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.SubLocKeyCB);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TitleLocKeyCB);
            this.Controls.Add(this.RibbonLocKeyCB);
            this.Controls.Add(this.StyleCB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ThemeCB);
            this.Controls.Add(this.MysteryBoxTypeCB);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.appearanceLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.boxIDcomboBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.GenerateJsonButton);
            this.MinimumSize = new System.Drawing.Size(16, 39);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GenerateJsonButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox boxIDcomboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label appearanceLabel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox MysteryBoxTypeCB;
        private System.Windows.Forms.ComboBox ThemeCB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox StyleCB;
        private System.Windows.Forms.ComboBox RibbonLocKeyCB;
        private System.Windows.Forms.ComboBox TitleLocKeyCB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox SubLocKeyCB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel StorePopsPanel;
        private System.Windows.Forms.Label PurchaseScrnLbl;
        private System.Windows.Forms.TextBox PSTitleLocKey;
        private System.Windows.Forms.FlowLayoutPanel PurchasePopsPanel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label MainHubLbl;
        private System.Windows.Forms.CheckBox canShowCarouselBox;
    }
}

