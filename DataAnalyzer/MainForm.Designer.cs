namespace DataAnalyzer
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.inFileTextBox = new System.Windows.Forms.TextBox();
            this.selectButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.importDataWorker = new System.ComponentModel.BackgroundWorker();
            this.labelMessage = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.outFileSelectButton = new System.Windows.Forms.Button();
            this.outFileTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入数据文件:";
            // 
            // inFileTextBox
            // 
            this.inFileTextBox.Location = new System.Drawing.Point(120, 23);
            this.inFileTextBox.Name = "inFileTextBox";
            this.inFileTextBox.Size = new System.Drawing.Size(264, 21);
            this.inFileTextBox.TabIndex = 1;
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(390, 21);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(49, 23);
            this.selectButton.TabIndex = 2;
            this.selectButton.Text = "选择";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(302, 87);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(137, 41);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "开始统计";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(27, 87);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(269, 23);
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;
            // 
            // importDataWorker
            // 
            this.importDataWorker.WorkerReportsProgress = true;
            this.importDataWorker.WorkerSupportsCancellation = true;
            this.importDataWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.importDataWorker_DoWork);
            this.importDataWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.importDataWorker_ProgressChanged);
            this.importDataWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.importDataWorker_RunWorkerCompleted);
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(25, 116);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(0, 12);
            this.labelMessage.TabIndex = 7;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "结果文件:";
            // 
            // outFileSelectButton
            // 
            this.outFileSelectButton.Location = new System.Drawing.Point(390, 50);
            this.outFileSelectButton.Name = "outFileSelectButton";
            this.outFileSelectButton.Size = new System.Drawing.Size(49, 23);
            this.outFileSelectButton.TabIndex = 10;
            this.outFileSelectButton.Text = "选择";
            this.outFileSelectButton.UseVisualStyleBackColor = true;
            this.outFileSelectButton.Click += new System.EventHandler(this.outFileSelectButton_Click);
            // 
            // outFileTextBox
            // 
            this.outFileTextBox.Location = new System.Drawing.Point(120, 52);
            this.outFileTextBox.Name = "outFileTextBox";
            this.outFileTextBox.Size = new System.Drawing.Size(264, 21);
            this.outFileTextBox.TabIndex = 9;
            this.outFileTextBox.Text = "D:\\result.csv";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 151);
            this.Controls.Add(this.outFileSelectButton);
            this.Controls.Add(this.outFileTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.inFileTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据分析";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inFileTextBox;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker importDataWorker;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button outFileSelectButton;
        private System.Windows.Forms.TextBox outFileTextBox;
    }
}

