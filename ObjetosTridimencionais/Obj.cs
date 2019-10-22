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
        List<Vertice> list_v = new List<Vertice>(); //lista de vertices originais
        List<Vertice> list_va = new List<Vertice>(); //lista de vertices atuais
        List<VetorNormal> list_vn = new List<VetorNormal>(); //lista de vetores normais
        List<Face> list_f = new List<Face>();

        double[] centro = new double[3];
        double[,] mat_a = {
                {1, 0, 0, 0},
                {0, 1, 0, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 1}
            }; //matriz acumulada de transformacao

        public Obj()
        {
        
        }

        #region Getters e Setters
        public List<Vertice> getVertices() { return list_va; }
        public void addVertice(Vertice v) { this.list_v.Add(v); this.list_va.Add(v); }
        public void addVetorNormal(VetorNormal vn) { this.list_vn.Add(vn); }

        public List<Face> getFaces() { return list_f; }
        public void addFace(Face f) { this.list_f.Add(f); }

        #endregion

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
                else if (linha[0].Equals("vn"))//define uma nova face
                {
                    VetorNormal vn = new VetorNormal();

                    vn.setX(Convert.ToDouble(linha[1].Replace('.', ',').Replace('E', 'e')));
                    vn.setY(Convert.ToDouble(linha[2].Replace('.', ',').Replace('E', 'e')));
                    vn.setZ(Convert.ToDouble(linha[3].Replace('.', ',').Replace('E', 'e')));

                    addVetorNormal(vn);
                }
            }

            sr.Close();

            calculaCentro();
        }

        //desenha o objeto por projeção paralela
        public void desenha_pp(DirectBitmap img)
        {
            //acha o ponto médio do picture box em relação ao objeto que vai ser desenhado
            Point meio = new Point(img.Width / 2, img.Height / 2);

            //para cada face
            foreach (Face f in list_f)
            {
                int[] _vet = f.getVet();

                //vertices da face
                Point p1 = new Point((int)list_va[_vet[0] - 1].getX() + meio.X, (int)list_va[_vet[0] - 1].getY() + meio.Y);
                Point p2 = new Point((int)list_va[_vet[1] - 1].getX() + meio.X, (int)list_va[_vet[1] - 1].getY() + meio.Y);
                Point p3 = new Point((int)list_va[_vet[2] - 1].getX() + meio.X, (int)list_va[_vet[2] - 1].getY() + meio.Y);

                //desenha as ligações dos vertices (regra da mão direita)
                desenha_reta(p1, p2, img);
                desenha_reta(p2, p3, img);
                desenha_reta(p3, p1, img);
            }
        }

        //desenha o objeto por Backface Culling
        public void desenha_bc(DirectBitmap img)
        {
            //acha o ponto médio do picture box em relação ao objeto que vai ser desenhado
            Point meio = new Point(img.Width / 2, img.Height / 2);

            //para cada face
            foreach (Face f in list_f)
            {
                int[] _vet = f.getVet();

                if(list_vn[_vet[2] - 1].getZ() >= 0)
                {
                    //vertices da face
                    Point p1 = new Point((int)list_va[_vet[0] - 1].getX() + meio.X, (int)list_va[_vet[0] - 1].getY() + meio.Y);
                    Point p2 = new Point((int)list_va[_vet[1] - 1].getX() + meio.X, (int)list_va[_vet[1] - 1].getY() + meio.Y);
                    Point p3 = new Point((int)list_va[_vet[2] - 1].getX() + meio.X, (int)list_va[_vet[2] - 1].getY() + meio.Y);

                    //desenha as ligações dos vertices (regra da mão direita)
                    desenha_reta(p1, p2, img);
                    desenha_reta(p2, p3, img);
                    desenha_reta(p3, p1, img);
                }
            }
        }

        #region Transformacoes

        //multiplica todos os pontos originais pela matriz de tranformação acumulada - definindo os pontos atuais
        public void aplica_transformacoes()
        {
            list_va.Clear();
            foreach(Vertice v in list_v)
            {
                double x = v.getX(), y = v.getY(), z = v.getZ();

                double[,] mat = {
                    {x},
                    {y},
                    {z},
                    {1}
                };

                double[,] result = mult_mat(mat_a, mat);

                Vertice vn = new Vertice(result[0,0], result[1,0], result[2,0]);
                list_va.Add(vn);
            }

            calculaCentro() ;
        }

        public void escala(double value, DirectBitmap img)
        {
            double[,] mat = {
                {value, 0, 0, 0},
                {0, value, 0 ,0},
                {0, 0, value, 0},
                { 0,  0,  0,  1}
            };

            mat_a = mult_mat(mat_a, mat);
        }

        public void translacao(int tx, int ty, int tz)
        {
            double[,] mat = {
                {1, 0, 0, tx},
                {0, 1, 0, ty},
                {0, 0, 1, tz},
                {0, 0, 0, 1}
            };

            mat_a = mult_mat(mat, mat_a);
        }

        public void rotacaoX(double ang)
        {
            double[,] mat = {
                {1, 0, 0, 0},
                {0, Math.Cos(ang), -Math.Sin(ang), 0},
                {0, Math.Sin(ang), Math.Cos(ang), 0 },
                {0, 0, 0, 1}
            };

            mat_a = mult_mat(mat_a, mat);
        }

        public void rotacaoY(double ang)
        {
            double[,] mat = {
                {Math.Cos(ang), 0, Math.Sin(ang), 0},
                {0, 1, 0, 0},
                {-Math.Sin(ang), 0, Math.Cos(ang), 0},
                {0, 0, 0, 1}
            };

            mat_a = mult_mat(mat_a, mat);
        }

        public void rotacaoZ(double ang)
        {
            double[,] mat = {
                {Math.Cos(ang), -Math.Sin(ang), 0, 0},
                {Math.Sin(ang), Math.Cos(ang), 0, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 1}
            };

            mat_a = mult_mat(mat_a, mat);
        }

        #endregion

        #region Metodos Auxiliares

        //algoritmo do ponto médio para traçado de retas
        private void desenha_reta(Point ini, Point fim, DirectBitmap img)
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
        private bool verifica_borda(Point x, DirectBitmap img)
        {
            return (x.X > 0 && x.Y > 0 && x.X < img.Width && x.Y < img.Height);
        }

        //multiplacao de matrizes
        private double[,] mult_mat(double[,] m1, double[,] m2)
        {
            double[,] result = new double[m1.GetLength(0), m2.GetLength(1)];
            double soma = 0;
            for(int lin = 0; lin < m1.GetLength(0); lin++)
            {
                for(int col = 0; col < m2.GetLength(1); col++)
                {
                    for(int i = 0; i < m2.GetLength(0); i++)
                    {
                        soma += m1[lin, i] * m2[i, col];
                    }

                    result[lin, col] = soma;
                    soma = 0;
                }
            }

            return result;
        }

        private void calculaCentro()
        {
            centro[0] = minX() + (maxX() - minX()) / 2;
            centro[1] = minY() + (maxY() - minY()) / 2;
            centro[2] = minZ() + (maxZ() - minZ()) / 2;
        }

        private int prodEscalar(VetorNormal v1, VetorNormal v2)
        {
            

            return 1;
        }

        #region Max e Min

        private double minX()
        {
            double min = 99999;
            foreach (Vertice v in list_v)
                if (v.getX() < min)
                    min = v.getX();

            return min;
        }

        private double minY()
        {
            double min = 99999;
            foreach (Vertice v in list_v)
                if (v.getY() < min)
                    min = v.getY();

            return min;
        }

        private double minZ()
        {
            double min = 99999;
            foreach (Vertice v in list_v)
                if (v.getZ() < min)
                    min = v.getZ();

            return min;
        }

        private double maxX()
        {
            double max = -99999;
            foreach (Vertice v in list_v)
                if (v.getX() > max)
                    max = v.getX();

            return max;
        }

        private double maxY()
        {
            double max = -99999;
            foreach (Vertice v in list_v)
                if (v.getY() > max)
                    max = v.getY();

            return max;
        }

        private double maxZ()
        {
            double max = -99999;
            foreach (Vertice v in list_v)
                if (v.getZ() > max)
                    max = v.getZ();

            return max;
        }
        #endregion

        #endregion
    }
}
