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
        DirectBitmap img;
        Point ponto_ini; //ponto inicial do click
        bool flag_down = false; //saber se está clickado
        bool flag_ctrl = false; //saber se o control está pressionado
        char botao = 'n'; //determina o botao do mouse

        public frmMain()
        {
            InitializeComponent();
            pbCanvas.Size = new Size((int)(this.Size.Width * 0.8), this.Size.Height);
            pbCanvas.MouseWheel += pbCanvas_MouseWheel;
            _control = new ControlMain();
        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abrir();
        }

        private void abrir()
        {
            ofdAbrir = new OpenFileDialog()
            {
                FileName = "*.obj",
                Filter = "Arquivos de Objeto (*.obj)|*.obj",
                Title = "Abrir"
            };

            if (ofdAbrir.ShowDialog() == DialogResult.OK)
            {
                img = new DirectBitmap(pbCanvas.Width, pbCanvas.Height);
                pbCanvas.Image = img.Bitmap;
                _control.lerObjeto(ofdAbrir.FileName, img);
                pbCanvas.Refresh();
            }
        }

        #region MouseEvents

        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            flag_down = true;

            if(_control.getObj() != null)
                if (e.Button == MouseButtons.Right)
                {
                    botao = 'r';
                    ponto_ini = e.Location;
                }else if(e.Button == MouseButtons.Left)
                {
                    botao = 'l';
                    ponto_ini = e.Location;
                }
                
        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(flag_down && _control.getObj() != null)
            {
                img.Dispose();
                img = new DirectBitmap(pbCanvas.Width, pbCanvas.Height);

                if (botao == 'r')
                {
                    int dx = e.X - ponto_ini.X, dy = e.Y - ponto_ini.Y, dz = 0;
                    
                    ponto_ini = e.Location;
                    _control.translacao(dx, dy, dz, img);
                }
                else if(botao == 'l')
                {
                    int dx = e.X - ponto_ini.X, dy = e.Y - ponto_ini.Y;

                    ponto_ini = e.Location;
                    if (flag_ctrl)
                        _control.rotacao(0, 0, 3, img);
                    else
                        _control.rotacao(dy, dx, 0, img);
                }

                pbCanvas.Image.Dispose();
                pbCanvas.Image = img.Bitmap;
            }
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            flag_down = false;
            botao = 'n';
        }

        private void pbCanvas_MouseWheel(object sender, MouseEventArgs e)
        {
            if(_control.getObj() != null)
            {
                img.Dispose();
                img = new DirectBitmap(pbCanvas.Width, pbCanvas.Height);

                if (flag_ctrl)
                {
                    _control.translacao(0, 0, 1, img);
                }
                else
                {
                    if (e.Delta > 0)
                        _control.escala(1.1, img);
                    else
                        _control.escala(0.9, img);
                }
                

                pbCanvas.Image.Dispose();
                pbCanvas.Image = img.Bitmap;
            }
        }

        private void pbCanvas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_control.getObj() == null)
                abrir();
        }


        #endregion

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                flag_ctrl = true;
        }

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (!e.Control)
                flag_ctrl = false;
        }

        private void eixoZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.Dispose();
            img = new DirectBitmap(pbCanvas.Width, pbCanvas.Height);
            _control.setProj(img, '1');
            pbCanvas.Image.Dispose();
            pbCanvas.Image = img.Bitmap;
        }

        private void eixoYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.Dispose();
            img = new DirectBitmap(pbCanvas.Width, pbCanvas.Height);
            _control.setProj(img, '2');
            pbCanvas.Image.Dispose();
            pbCanvas.Image = img.Bitmap;
        }

        private void eixoXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.Dispose();
            img = new DirectBitmap(pbCanvas.Width, pbCanvas.Height);
            _control.setProj(img, '3');
            pbCanvas.Image.Dispose();
            pbCanvas.Image = img.Bitmap;
        }

        private void cabinetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.Dispose();
            img = new DirectBitmap(pbCanvas.Width, pbCanvas.Height);
            _control.setProj(img, '4');
            pbCanvas.Image.Dispose();
            pbCanvas.Image = img.Bitmap;
        }

        private void cavaleiraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.Dispose();
            img = new DirectBitmap(pbCanvas.Width, pbCanvas.Height);
            _control.setProj(img, '5');
            pbCanvas.Image.Dispose();
            pbCanvas.Image = img.Bitmap;
        }

        private void PontoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.Dispose();
            img = new DirectBitmap(pbCanvas.Width, pbCanvas.Height);
            _control.setProj(img, '6');
            pbCanvas.Image.Dispose();
            pbCanvas.Image = img.Bitmap;
        }
    }    
}
