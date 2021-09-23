namespace NAM
{
    partial class frmStatus
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblCmd = new System.Windows.Forms.Label();
            this.lblResponse = new System.Windows.Forms.Label();
            this.lblOperation = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(7, 13);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(264, 23);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Label1";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.Red;
            this.progressBar1.Location = new System.Drawing.Point(59, 140);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(160, 16);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblProgress
            // 
            this.lblProgress.Font = new System.Drawing.Font("Arial", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(10, 159);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(264, 20);
            this.lblProgress.TabIndex = 3;
            this.lblProgress.Text = "0 %";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCmd
            // 
            this.lblCmd.Font = new System.Drawing.Font("Arial", 8.25F);
            this.lblCmd.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblCmd.Location = new System.Drawing.Point(3, 3);
            this.lblCmd.Name = "lblCmd";
            this.lblCmd.Size = new System.Drawing.Size(254, 23);
            this.lblCmd.TabIndex = 2;
            this.lblCmd.Text = "Command/Query";
            this.lblCmd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResponse
            // 
            this.lblResponse.Font = new System.Drawing.Font("Arial", 8.25F);
            this.lblResponse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblResponse.Location = new System.Drawing.Point(3, 26);
            this.lblResponse.Name = "lblResponse";
            this.lblResponse.Size = new System.Drawing.Size(254, 23);
            this.lblResponse.TabIndex = 4;
            this.lblResponse.Text = "Response";
            this.lblResponse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOperation
            // 
            this.lblOperation.Font = new System.Drawing.Font("Arial", 8.25F);
            this.lblOperation.Location = new System.Drawing.Point(10, 53);
            this.lblOperation.Name = "lblOperation";
            this.lblOperation.Size = new System.Drawing.Size(264, 23);
            this.lblOperation.TabIndex = 5;
            this.lblOperation.Text = "Operation";
            this.lblOperation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lblCmd);
            this.panel1.Controls.Add(this.lblResponse);
            this.panel1.Location = new System.Drawing.Point(10, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(264, 53);
            this.panel1.TabIndex = 6;
            // 
            // frmStatus
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(286, 184);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblOperation);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblHeader);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStatus";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VEO2NAM Status";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmStatus_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Label lblHeader;
        public System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.Label lblProgress;
        public System.Windows.Forms.Label lblCmd;
        public System.Windows.Forms.Label lblResponse;
        public System.Windows.Forms.Label lblOperation;
        private System.Windows.Forms.Panel panel1;
    }
}