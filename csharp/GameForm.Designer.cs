using System.Windows.Forms;

namespace SpaceInvasion
{
    partial class GameForm
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
            this.ctlDarkGDKViewport1 = new DarkGDK.ctlDarkGDKViewport();
            this.SuspendLayout();
            // 
            // GameForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctlDarkGDKViewport1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Space Invaders";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.fMain_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fMain_FormClosed);
            this.ClientSize = new System.Drawing.Size(1280,800);
            // 
            // ctlDarkGDKViewport1
            // 
            this.ctlDarkGDKViewport1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlDarkGDKViewport1.GraphicsMode = "1280x800x32";
            this.ctlDarkGDKViewport1.Location = new System.Drawing.Point(0, 0);
            this.ctlDarkGDKViewport1.Name = "ctlDarkGDKViewport1";
            this.ctlDarkGDKViewport1.Size = new System.Drawing.Size(1280, 800);
            this.ctlDarkGDKViewport1.TabIndex = 0;
            this.ctlDarkGDKViewport1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MClick);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkGDK.ctlDarkGDKViewport ctlDarkGDKViewport1;
    }
}

