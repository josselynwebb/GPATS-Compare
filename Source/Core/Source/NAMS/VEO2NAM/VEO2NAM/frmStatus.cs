using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VEO2_Library;

namespace NAM
{
    public partial class frmStatus : Form
    {
        
        public frmStatus()
        {
            InitializeComponent();
            
        }

        private void frmStatus_Load(object sender, EventArgs e)
        {
            updateControls();
            updateLocation();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateControls();
            
        }

        private void updateControls()
        {
            switch (VEO2.VEO2Status)
            {
                case "Success":
                    this.BackColor = Color.GreenYellow;
                    lblCmd.Text = "";
                    lblCmd.Update();
                    lblResponse.Text = VEO2.VEO2Status;
                    lblResponse.Update();
                    break;

                case "Failed":
                    this.BackColor = Color.Salmon;
                    lblCmd.Text = "";
                    lblCmd.Update();
                    lblResponse.Text = VEO2.VEO2Status;
                    lblResponse.Update();
                    break;

                default:
                    lblHeader.Text = VEO2.VEO2Config;
                    lblHeader.Update();
                    lblOperation.Text = VEO2.VEO2Operation;
                    lblOperation.Update();
                    progressBar1.Value = VEO2.VEO2Progress;
                    progressBar1.Update();
                    lblProgress.Text = progressBar1.Value.ToString() + " %";
                    lblProgress.Update();
                    lblCmd.Text = VEO2.VEO2Query;
                    lblCmd.Update();
                    lblResponse.Text = VEO2.VEO2Status;
                    lblResponse.Update();
                    break;
            }

            this.Update();
            this.Refresh();
        }

        private void updateLocation()
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            Location = new Point((screenWidth - Width),screenHeight - Height);

        }

    }
}
