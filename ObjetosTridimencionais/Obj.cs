using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetosTridimencionais
{
    class Obj
    {
        List<Vertice> list_v = new List<Vertice>();
        List<Face> list_f = new List<Face>();
        double[,] mat_a = {
                { 1, 0, 0},
                { 0, 1, 0},
                { 0, 0, 1}
            }; //matriz acumulada de transformacao

        public Obj()
        {
        
        }

        //getters e setters
        public List<Vertice> getVertices() { return list_v; }
        public void addVertice(Vertice v) { this.list_v.Add(v); }

        public List<Face> getFaces() { return list_f; }
        public void addFace(Face f) { this.list_f.Add(f); }

        //carrega o objeto a partir de um arquivo obj
        public void carregar(string caminho)
        {
            StreamReader sr = File.OpenText(caminho);

            string[] linha;
            
            while(!sr.EndOfStream) //para cada linha do arquivo obj
            {
                linha = sr.ReadLine().Split(' ');

                //verificação do tipo da linha - v - vn - f
                if (linha[0].Equals("v"))//define um novo vertice
                {
                    Vertice v = new Vertice();

                    v.setX(Convert.ToDouble(linha[1].Replace('.', ',').Replace('E', 'e')));
                    v.setY(Convert.ToDouble(linha[2].Replace('.', ',').Replace('E', 'e')));
                    v.setZ(Convert.ToDouble(linha[3].Replace('.', ',').Replace('E', 'e')));

                    addVertice(v);
                }
                else if(linha[0].Equals("f"))//define uma nova face
                {
                    Face f = new Face();

                    f.addIndex(int.Parse(linha[1].Split('/')[0]));
                    f.addIndex(int.Parse(linha[2].Split('/')[0]));
                    f.addIndex(int.Parse(linha[3].Split('/')[0]));

                    addFace(f);
                }
            }

            sr.Close();
        }

        //desenha o objeto por projeção paralela
        public void desenha_pp(Bitmap img)
        {
            //acha o ponto médio do picture box em relação ao objeto que vai ser desenhado
            Point meio = new Point(img.Width / 2, img.Height / 2);

            //para cada face
            foreach (Face f in list_f)
            {
                int[] _vet = f.getVet();

                //vertices da face
                Point p1 = new Point((int)list_v[_vet[0] - 1].getX() + meio.X, (int)list_v[_vet[0] - 1].getY() + meio.Y);
                Point p2 = new Point((int)list_v[_vet[1] - 1].getX() + meio.X, (int)list_v[_vet[1] - 1].getY() + meio.Y);
                Point p3 = new Point((int)list_v[_vet[2] - 1].getX() + meio.X, (int)list_v[_vet[2] - 1].getY() + meio.Y);

                //desenha as ligações dos vertices (regra da mão direita)
                desenha_reta(p1, p2, img);
                desenha_reta(p2, p3, img);
                desenha_reta(p3, p1, img);
            }
        }

        public void translacao(int dx, int dy)
        {
            double[,] mat = {
                { 1, 0, dx },
                { 0, 1, dy},
                { 0, 0, 1}
            };

            double[,] result = mult_mat(mat, mat_a);
        }

        //Métodos Auxiliares

        //algoritmo do ponto médio para traçado de retas
        private void desenha_reta(Point ini, Point fim, Bitmap img)
        {
            int declive = 1;
            int dx, dy, incE, incNE, d, x, y;

            int x1 = ini.X, x2 = fim.X, y1 = ini.Y, y2 = fim.Y;
            dx = x2 - x1;
            dy = y2 - y1;

            if (Math.Abs(dx) >= Math.Abs(dy))
            {
                declive = dy < 0 ? -1 : 1;
                if (x1 > x2)
                    desenha_reta(fim, ini, img);

                //constante de bresenham
                dy *= declive;
                incE = 2 * dy;
                incNE = 2 * dy - 2 * dx;
                d = 2 * dy - dx;
                y = y1;

                for (x = x1; x <= x2; x++)
                {
                    if (verifica_borda(new Point(x, y), img))
                        img.SetPixel(x, y, Color.FromArgb(255, 255, 255));

                    if (d <= 0) d += incE;
                    else
                    {
                        d += incNE;
                        y += declive;
                    }
                }
            }
            else
            {
                declive = dx < 0 ? -1 : 1;
                if (y1 > y2)
                    desenha_reta(fim, ini, img);

                //constante de bresenham
                dx *= declive;
                incE = 2 * dx;
                incNE = 2 * dx - 2 * dy;
                d = 2 * dx - dy;
                x = x1;

                for (y = y1; y <= y2; y++)
                {
                    if (verifica_borda(new Point(x, y), img))
                        img.SetPixel(x, y, Color.FromArgb(255, 255, 255));

                    if (d <= 0) d += incE;
                    else
                    {
                        d += incNE;
                        x += declive;
                    }
                }
            }
        }

        //verifica se o ponto x pertence a imagem img
        private bool verifica_borda(Point x, Bitmap img)
        {
            return (x.X > 0 && x.Y > 0 && x.X < img.Width && x.Y < img.Height);
        }

        //multiplacao de matrizes
        private double[,] mult_mat(double[,] m1, double[,] m2)
        {
            double soma = 0;
            double[,] result = new double[m1.GetLength(0), m2.GetLength(1)];
            for (int linha = 0; linha < 3; linha++)
            {
                for (int coluna = 0; coluna < 3; coluna++)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        soma = soma + m1[linha, i] * m2[i, coluna];
                    }
                    result[linha, coluna] = soma;

                    soma = 0;
                }
            }

            return result;
        }
    }
}
