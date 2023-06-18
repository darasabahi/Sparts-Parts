namespace spare_parts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.analogueClock1 = new GHNet.Windows.Forms.AnalogueClock();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.analogueClock1)).BeginInit();
            this.SuspendLayout();
            // 
            // analogueClock1
            // 
            this.analogueClock1.BackColor = System.Drawing.Color.Transparent;
            this.analogueClock1.Location = new System.Drawing.Point(12, 12);
            this.analogueClock1.Name = "analogueClock1";
            this.analogueClock1.Size = new System.Drawing.Size(222, 205);
            this.analogueClock1.TabIndex = 4;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("B Mehr", 14.25F, System.Drawing.FontStyle.Bold);
            this.button5.Image = global::spare_parts.Properties.Resources.f1;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.Location = new System.Drawing.Point(1122, 311);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(187, 46);
            this.button5.TabIndex = 3;
            this.button5.Text = "مدیریت مالی";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("B Mehr", 14.25F, System.Drawing.FontStyle.Bold);
            this.button4.Image = global::spare_parts.Properties.Resources.simple_online_shop_icons_vector_19781542;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.Location = new System.Drawing.Point(1122, 238);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(187, 46);
            this.button4.TabIndex = 3;
            this.button4.Text = "ثبت خرید";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("B Mehr", 14.25F, System.Drawing.FontStyle.Bold);
            this.button3.Image = global::spare_parts.Properties.Resources.produsts1;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.button3.Location = new System.Drawing.Point(1122, 171);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(187, 46);
            this.button3.TabIndex = 2;
            this.button3.Text = "محصولات";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("B Mehr", 14.25F, System.Drawing.FontStyle.Bold);
            this.button2.Image = global::spare_parts.Properties.Resources.sellers2;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.Location = new System.Drawing.Point(1122, 96);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(187, 46);
            this.button2.TabIndex = 1;
            this.button2.Text = "اطلاعات فروشندگان";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("B Mehr", 14.25F, System.Drawing.FontStyle.Bold);
            this.button1.Image = global::spare_parts.Properties.Resources.customer;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(1122, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(187, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "اطلاعات خریداران";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("B Nazanin", 32.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(200)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Image = global::spare_parts.Properties.Resources.cart21;
            this.label1.Location = new System.Drawing.Point(0, 542);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 79);
            this.label1.TabIndex = 6;
            this.label1.Text = "0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1336, 630);
            this.Controls.Add(this.analogueClock1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.RightToLeftLayout = true;
            this.Text = "صفحه اصلی";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.analogueClock1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Timer timer1;
        private GHNet.Windows.Forms.AnalogueClock analogueClock1;
        public System.Windows.Forms.Label label1;
 
    }
}

