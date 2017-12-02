using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DarkGDK;
using DarkGDK.Basic2D;
using DarkGDK.Basic3D;

namespace SpaceInvasion
{
    public partial class GameForm : Form
    {
        private SpaceInvasion g;

        public GameForm()
        {
            InitializeComponent();
            this.g = new SpaceInvasion(800, 600);
        }

        private void fMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Engine.StopGDKLoop();
        }

        private void fMain_Shown(object sender, EventArgs e)
        {
            g.Setup();
            g.Run();
        }

        private void MClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!g.fire)
            {
                g.fire = (e.Button == MouseButtons.Left);
            }
        }
    }
}
