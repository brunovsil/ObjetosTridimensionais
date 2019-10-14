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
        ControlMain _control;
        Bitmap img;
        Point pos_click;
        bool flag_m = false;
        char botao;

        public frmMain()
        {
            InitializeComponent();
            pbCanvas.Size = new Size((int)(this.Size.Width * 0.9), this.Size.Height);
            _control = new ControlMain();

            pbCanvas.Image = img;

        }

        private void BtnAbrir_Click(object sender, EventArgs e)
        {
        }

        private void btAbrir_Click(object sender, EventArgs e)
        {
            ofdAbrir = new OpenFileDialog()
            {
                FileName = "*.obj",
                Filter = "Arquivos de Objeto (*.obj)|*.obj",
                Title = "Abrir"
            };

            if (ofdAbrir.ShowDialog() == DialogResult.OK)
            {
                img = new Bitmap(pbCanvas.Width, pbCanvas.Height);
                pbCanvas.Image = img;
                _control.lerObjeto(ofdAbrir.FileName, img);
                pbCanvas.Refresh();
            }
        }

        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if(!flag_m)
            {
                flag_m = true;
                pos_click = e.Location;

                if (e.Button == MouseButtons.Left)
                    botao = 'l';
                else if (e.Button == MouseButtons.Right)
                    botao = 'r';
                else
                    botao = 'n';
            }
        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(flag_m)
            {
                if(botao == 'l')
                {
                    _control.translacao(e.X - pos_click.X, e.Y - pos_click.Y);
                }
            }
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            flag_m = false;
            botao = 'n';
        }
    }
}
