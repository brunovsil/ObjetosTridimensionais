using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjetosTridimencionais
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            pbCanvas.Size = new Size((int)(this.Size.Width * 0.8), this.Size.Height);
        }
    }
}
