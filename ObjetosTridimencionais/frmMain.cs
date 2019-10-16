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
        Point ponto_ini; //ponto inicial do click
        bool flag_down = false; //saber se está clickado
        char botao = 'n'; //determina o botao do mouse

        public frmMain()
        {
            InitializeComponent();
            pbCanvas.Size = new Size((int)(this.Size.Width * 0.8), this.Size.Height);
            pbCanvas.MouseWheel += pbCanvas_MouseWheel;
            _control = new ControlMain();

            pbCanvas.Image = img;

        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
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

        #region MouseEvents

        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            flag_down = true;

            if (e.Button == MouseButtons.Right)
            {
                botao = 'r';
                ponto_ini = e.Location;
            }
                
        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(flag_down)
            {
                img = new Bitmap(pbCanvas.Width, pbCanvas.Height);

                if (botao == 'r')
                {
                    int dx = e.X - ponto_ini.X, dy = e.Y - ponto_ini.Y, dz = 0;
                    ponto_ini = e.Location;
                    _control.translacao(dx, dy, dz, img);
                }

                pbCanvas.Image.Dispose();
                pbCanvas.Image = img;
            }
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            flag_down = false;
            botao = 'n';
        }

        private void pbCanvas_MouseWheel(object sender, MouseEventArgs e)
        {
            img = new Bitmap(pbCanvas.Width, pbCanvas.Height);

            if (e.Delta > 0)
                _control.escala(1.1, img);
            else
                _control.escala(0.9, img);

            pbCanvas.Image.Dispose();
            pbCanvas.Image = img;
        }

        #endregion
    }    
}
