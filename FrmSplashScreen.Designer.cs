
namespace EasyCoat
{
    partial class FrmSplashScreen
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSplashScreen));
            this.picBlackground = new System.Windows.Forms.PictureBox();
            this.panBarground = new System.Windows.Forms.Panel();
            this.pnlProgrpoundBar = new System.Windows.Forms.Panel();
            this.timerProgressBar = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picBlackground)).BeginInit();
            this.panBarground.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBlackground
            // 
            this.picBlackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBlackground.Image = ((System.Drawing.Image)(resources.GetObject("picBlackground.Image")));
            this.picBlackground.Location = new System.Drawing.Point(0, 0);
            this.picBlackground.Name = "picBlackground";
            this.picBlackground.Size = new System.Drawing.Size(700, 400);
            this.picBlackground.TabIndex = 0;
            this.picBlackground.TabStop = false;
            this.picBlackground.Click += new System.EventHandler(this.picBlackground_Click);
            // 
            // panBarground
            // 
            this.panBarground.Controls.Add(this.pnlProgrpoundBar);
            this.panBarground.Location = new System.Drawing.Point(0, 335);
            this.panBarground.Name = "panBarground";
            this.panBarground.Size = new System.Drawing.Size(700, 10);
            this.panBarground.TabIndex = 1;
            this.panBarground.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlProgressBar_Paint);
            // 
            // pnlProgrpoundBar
            // 
            this.pnlProgrpoundBar.BackColor = System.Drawing.Color.Cyan;
            this.pnlProgrpoundBar.Location = new System.Drawing.Point(0, 0);
            this.pnlProgrpoundBar.Name = "pnlProgrpoundBar";
            this.pnlProgrpoundBar.Size = new System.Drawing.Size(15, 10);
            this.pnlProgrpoundBar.TabIndex = 2;
            // 
            // timerProgressBar
            // 
            this.timerProgressBar.Enabled = true;
            this.timerProgressBar.Interval = 30;
            this.timerProgressBar.Tick += new System.EventHandler(this.timerProgressBar_Tick);
            // 
            // FrmSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 400);
            this.Controls.Add(this.panBarground);
            this.Controls.Add(this.picBlackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSplashScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBlackground)).EndInit();
            this.panBarground.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBlackground;
        private System.Windows.Forms.Panel panBarground;
        private System.Windows.Forms.Panel pnlProgrpoundBar;
        private System.Windows.Forms.Timer timerProgressBar;
    }
}

