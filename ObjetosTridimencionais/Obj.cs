using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObjetosTridimencionais
{
    class Obj
    {
        List<Vertice> list_v = new List<Vertice>(); //lista de vertices originais
        List<Vertice> list_va = new List<Vertice>(); //lista de vertices atuais
        List<Face> list_f = new List<Face>();

        Vertice centro = new Vertice();
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
            }

            sr.Close();

            calculaCentro();
            atualizaNormais();
        }

        #region Modelos de Iluminação e Sombreamento e preenchimento

        public void scanLine(Color c, DirectBitmap img)
        {
            List<NodoET>[] et;
            List<NodoET> aet;
            Point meio = new Point(img.Width / 2, img.Height / 2);
            int pos_y = img.Height + 1; //posição do primeiro Y
            double[,] z_buffer = new double[img.Width, img.Height];

            //inicializa o z-buffer
            for (int i = 0; i < img.Width; i++)
                for (int j = 0; j < img.Height; j++)
                    z_buffer[i, j] = 9999;

            //INFORMAÇÕES DO AMBIENTE
            Vertice luz = new Vertice(10, 10, 1);
            Vertice obs = new Vertice(0, 0, 1);
            Color cor_luz = Color.FromArgb(0, 0, 255);
            int no = 10; //coeficiente da reflexão especular

            foreach (Face f in list_f) //para cada face adiciona os vertices na ET
            {
                List<int> _vet = f.getVet();
                List<Vertice> list_p = new List<Vertice>();
                Vertice pMin, pMax;
                double yMax, xMin, zMin, incZ, incX, dx, dy, dz;
                NodoET nodo;
                pos_y = img.Height + 1;

                //variáveis de iluminação
                double[] k_a = new double[3], //superficie ambiente
                         k_d = new double[3], //superficie difusa
                         k_e = new double[3]; //superficie especular

                double[] l_a = new double[3], //ponto de luz ambiente
                         l_d = new double[3], //ponto de luz difusa
                         l_e = new double[3]; //ponto de luz especular

                double[] vet_luz = new double[3];
                vet_luz[0] = luz.getX();
                vet_luz[1] = luz.getY();
                vet_luz[2] = luz.getZ();
                vet_luz = Vetores.div_esc(vet_luz, Vetores.modulo(vet_luz));

                double[] vet_olho = new double[3];
                vet_olho[0] = list_va[_vet[0] - 1].getX() - obs.getX();
                vet_olho[1] = list_va[_vet[0] - 1].getY() - obs.getY();
                vet_olho[2] = list_va[_vet[0] - 1].getZ() - obs.getZ();
                vet_olho = Vetores.div_esc(vet_olho, Vetores.modulo(vet_olho));

                //luz ambiente
                double mod_amb = 0.5; //modificador para luz ambiente
                l_a[0] = cor_luz.R / 255 * mod_amb;
                l_a[1] = cor_luz.G / 255 * mod_amb;
                l_a[2] = cor_luz.B / 255 * mod_amb;

                //luz difusa
                double mod_dif = 1;
                l_d[0] = cor_luz.R / 255 * mod_dif;
                l_d[1] = cor_luz.G / 255 * mod_dif;
                l_d[2] = cor_luz.B / 255 * mod_dif;

                //luz especular
                double mod_esp = 1;
                l_e[0] = cor_luz.R / 255 * mod_esp;
                l_e[1] = cor_luz.G / 255 * mod_esp;
                l_e[2] = cor_luz.B / 255 * mod_esp;

                //superficie ambiente
                double mod_samb = 1; //modificador para superficie ambiente
                k_a[0] = c.R / 255 * mod_samb;
                k_a[1] = c.G / 255 * mod_samb;
                k_a[2] = c.B / 255 * mod_samb;

                //superficie difusa
                double mod_sdif = 0.5;
                k_d[0] = c.R / 255 * mod_sdif;
                k_d[1] = c.G / 255 * mod_sdif;
                k_d[2] = c.B / 255 * mod_sdif;

                //superficie especular
                double mod_sesp = 1;
                k_e[0] = c.R / 255 * mod_sesp;
                k_e[1] = c.G / 255 * mod_sesp;
                k_e[2] = c.B / 255 * mod_sesp;

                //componentes de reflexão
                //ambiente
                double[] c_a = Vetores.mult(l_a, k_a);

                //difusa
                double[] c_d = new double[3];
                double cos = Vetores.prodEscalar(vet_luz, f.getNormal()) / Vetores.modulo(vet_luz) * Vetores.modulo(f.getNormal());
                c_d = Vetores.mult(l_d, k_d);
                c_d = Vetores.mult_esc(c_d, cos);
                c_d = Vetores.adc(c_a, c_d);

                //especular
                double[] c_e = new double[3];
                double[] H = Vetores.adc(vet_luz, vet_olho);
                H = Vetores.div_esc(H, Vetores.modulo(H));
                c_e = Vetores.mult(l_e, k_e);
                c_e = Vetores.mult_esc(c_e, Math.Pow(Vetores.prodEscalar(f.getNormal(), H), no));

                //phong
                double cor_r = (c_d[0] + c_e[0]) * 255;
                double cor_g = (c_d[1] + c_e[1]) * 255;
                double cor_b = (c_d[2] + c_e[2]) * 255;
                cor_r = cor_r > 0 ? cor_r : 0; cor_r = cor_r < 255 ? cor_r : 255;
                cor_g = cor_g > 0 ? cor_g : 0; cor_g = cor_g < 255 ? cor_g : 255;
                cor_b = cor_b > 0 ? cor_b : 0; cor_b = cor_b < 255 ? cor_b : 255;

                Color nova_c = Color.FromArgb((int)cor_r, (int)cor_g, (int)cor_b);

                //zerar as estruturas para a nova face
                et = new List<NodoET>[img.Height];
                aet = new List<NodoET>();
                for (int i = 0; i < et.Count(); i++)
                    et[i] = new List<NodoET>();

                //criar lista de pontos da face
                for (int i = 0; i < _vet.Count; i++)
                    list_p.Add(new Vertice(list_va[_vet[i] - 1].getX() + meio.X, list_va[_vet[i] - 1].getY() + meio.Y, (int)list_va[_vet[i] - 1].getZ()));

                for (int i = 0; i < list_p.Count - 1; i++)
                {
                    //atualiza a posição do menor Y
                    if ((int)list_p[i].getY() < pos_y)
                        pos_y = (int)list_p[i].getY();

                    //define os pontos de menor e maior Y (ponto mínimo e máximo)
                    pMin = list_p[i].getY() >= list_p[i + 1].getY() ? list_p[i + 1] : list_p[i];
                    pMax = list_p[i].getY() >= list_p[i + 1].getY() ? list_p[i] : list_p[i + 1];
                    zMin = pMin.getZ();
                    yMax = pMax.getY();
                    xMin = pMin.getX();
                    dx = pMax.getX() - pMin.getX();
                    dy = pMax.getY() - pMin.getY();
                    dz = pMax.getZ() - pMin.getZ();
                    incX = dy == 0 ? 0 : dx / dy;
                    incZ = dy == 0 ? 0 : dz / dy;

                    //cria e adiciona o nodo da aresta a ET
                    nodo = new NodoET(yMax, xMin, zMin, incX, incZ);
                    et[(int)pMin.getY()].Add(nodo);
                }
                if ((int)list_p[list_p.Count - 1].getY() < pos_y)
                    pos_y = (int)list_p[list_p.Count - 1].getY();

                //replica o processo para criar a aresta entre o último e o primeiro elemento
                pMin = list_p[list_p.Count - 1].getY() >= list_p[0].getY() ? list_p[0] : list_p[list_p.Count - 1];
                pMax = list_p[list_p.Count - 1].getY() >= list_p[0].getY() ? list_p[list_p.Count - 1] : list_p[0];
                zMin = pMin.getZ();
                yMax = pMax.getY();
                xMin = pMin.getX();
                dx = pMax.getX() - pMin.getX();
                dy = pMax.getY() - pMin.getY();
                dz = pMax.getZ() - pMin.getZ();
                incX = dy == 0 ? 0 : dx / dy;
                incZ = dy == 0 ? 0 : dz / dy;

                nodo = new NodoET(yMax, xMin, zMin, incX, incZ);
                et[(int)pMin.getY()].Add(nodo);
                //--------------------------------------

                do
                {
                    //para cada nodo da ET na posição do Y atual o adiciona na AET
                    foreach (NodoET n in et[pos_y])
                        aet.Add(n);

                    //retira as arestas que tem o Ymax como Y atual (arestas que ja chegaram no Y máximo)
                    List<NodoET> list_aux = new List<NodoET>();
                    for (int i = aet.Count - 1; i >= 0; i--)
                        if ((int)aet[i].getYMax() == pos_y)
                            aet.RemoveAt(i);

                    //desenha a reta em relação aos pontos na AET
                    for (int i = 0; i < aet.Count - 1; i += 2)
                    {
                        Vertice ini = new Vertice(aet[i].getXMin() , pos_y, aet[i].getZMin());
                        Vertice fim = new Vertice(aet[i + 1].getXMin(), pos_y, aet[i + 1].getZMin());

                        //recalculo os valores (variáveis auxiliares)
                        double zMin_ = ini.getZ();
                        double dx_ = fim.getX() - ini.getX();
                        double dz_ = fim.getZ() - ini.getZ();
                        double incZ_ = dx_ == 0 ? 0 : dz_ / dx_;

                        //percorre a reta 
                        for (int j = (int)ini.getX(); j <= (int)fim.getX(); j++)
                        {
                            if(zMin_ <= z_buffer[j, pos_y])
                            {
                                //atualiza o z_buffer
                                z_buffer[j, pos_y] = zMin_;
                                //atualiza o color_buffer
                                img.SetPixel(j, pos_y, nova_c);
                            }
                            zMin_ += incZ_; //incrementa o z
                        }
                    }

                    //incrementa o Y atual
                    pos_y++;

                    //atualiza o X dos Nodos da AET com o seu incremento
                    foreach (NodoET n in aet)
                        n.setXMin(n.getXMin() + n.getIncX());

                    //atualiza o Z dos Nodos da AET com o seu incremento
                    foreach (NodoET n in aet)
                        n.setZMin(n.getZMin() + n.getIncZ());

                    //ordena os elementos em relação ao novo X
                    for (int i = 0; i < aet.Count - 1; i++)
                        for (int j = i + 1; j < aet.Count; j++)
                        {
                            if (aet[i].getXMin() > aet[j].getXMin())
                            {
                                NodoET nodo_aux = aet[j];
                                aet[j] = aet[i];
                                aet[i] = nodo_aux;
                            }
                        }

                } while (aet.Count != 0);
            }
        }

        #endregion

        #region Projecoes

        //desenha o objeto por projeção paralela ortográfica em X, Y ou Z
        public void projecao_po(DirectBitmap img, char proj)
        {
            //acha o ponto médio do picture box em relação ao objeto que vai ser desenhado
            Point meio = new Point(img.Width / 2, img.Height / 2);

            //vetor de observação
            double[] obs = new double[3];
            obs[0] = 0; //x
            obs[1] = 0; //y
            obs[2] = 1; //z

            if (proj == '3')
            {
                obs[0] = 1; //x
                obs[1] = 0; //y
                obs[2] = 0; //z
            }else if(proj == '2')
            {
                obs[0] = 0; //x
                obs[1] = 1; //y
                obs[2] = 0; //z
            }else 
            {
                obs[0] = 0; //x
                obs[1] = 0; //y
                obs[2] = 1; //z
            }

            //para cada face
            foreach (Face f in list_f)
            {
                if (Vetores.prodEscalar(f.getNormal(), obs) > 0) //backface culling
                {
                    List<int> _vet = f.getVet();
                    List<Point> list_p = new List<Point>();

                    //vertices da face
                    for (int i = 0; i < _vet.Count; i++)
                        //verifica qual o eixo de projeção
                        if(proj == '1')
                            list_p.Add(new Point((int)list_va[_vet[i] - 1].getX() + meio.X, (int)list_va[_vet[i] - 1].getY() + meio.Y));
                        else if(proj == '2')
                            list_p.Add(new Point((int)list_va[_vet[i] - 1].getX() + meio.X, (int)list_va[_vet[i] - 1].getZ() + meio.Y));
                        else if(proj == '3')
                            list_p.Add(new Point((int)list_va[_vet[i] - 1].getY() + meio.X, (int)list_va[_vet[i] - 1].getZ() + meio.Y));

                    //desenha as ligações dos vertices (regra da mão direita)
                    for (int i = 0; i < list_p.Count - 1; i++)
                        desenha_reta(list_p[i], list_p[i + 1], img);
                    desenha_reta(list_p[list_p.Count - 1], list_p[0], img);
                }
                
            }
        }

        //desenha o objeto por projeção paralela oblíqua cabinet e cavaleira
        public void projecao_pb(DirectBitmap img, char proj)
        {
            //acha o ponto médio do picture box em relação ao objeto que vai ser desenhado
            Point meio = new Point(img.Width / 2, img.Height / 2);
            double alfa = 30, L = 0.5;

            //vetor de observação
            double[] obs = new double[3];
            obs[0] = 0; //x
            obs[1] = 0; //y
            obs[2] = 1; //z

            if (proj == '4') //cabinet
            {
                L = 0.5;
                alfa = 30;
            }
            else if (proj == '5') //cavaleira
            {
                L = 1.0;
                alfa = 63.4;
            }

            //para cada face
            foreach (Face f in list_f)
            {
                List<int> _vet = f.getVet();
                List<Point> list_p = new List<Point>();

                //vertices da face
                for (int i = 0; i < _vet.Count; i++)
                {
                    double x = list_va[_vet[i] - 1].getX(), y = list_va[_vet[i] - 1].getY(), z = list_va[_vet[i] - 1].getZ();
                    double xp, yp;

                    //pontos projetados
                    xp = x + z * L * Math.Cos(alfa * Math.PI / 180);
                    yp = y + z * L * Math.Sin(alfa * Math.PI / 180);

                    list_p.Add(new Point((int)xp + meio.X, (int)yp + meio.Y));
                }

                //desenha as ligações dos vertices (regra da mão direita)
                for (int i = 0; i < list_p.Count - 1; i++)
                    desenha_reta(list_p[i], list_p[i + 1], img);
                desenha_reta(list_p[list_p.Count - 1], list_p[0], img);
            }
        }

        //desenha o objeto por projeção perspectiva
        public void projecao_pe(DirectBitmap img, char proj)
        {
            //acha o ponto médio do picture box em relação ao objeto que vai ser desenhado
            Point meio = new Point(img.Width / 2, img.Height / 2);

            //vetor de observação
            double[] obs = new double[3];
            obs[0] = 0; //x
            obs[1] = 0; //y
            obs[2] = 1; //z

            //para cada face
            foreach (Face f in list_f)
            {
                List<int> _vet = f.getVet();
                List<Point> list_p = new List<Point>();

                double d = 400; //distancia entre o ponto de observação e o plano de projeção

                //vertices da face
                for (int i = 0; i < _vet.Count; i++)
                {
                    double x = list_va[_vet[i] - 1].getX(), y = list_va[_vet[i] - 1].getY(), z = list_va[_vet[i] - 1].getZ();
                    double xp, yp;

                    //pontos projetados
                    xp = x * d / (z + d);
                    yp = y * d / (z + d);

                    list_p.Add(new Point((int)xp + meio.X, (int)yp + meio.Y));
                }

                //desenha as ligações dos vertices (regra da mão direita)
                for (int i = 0; i < list_p.Count - 1; i++)
                    desenha_reta(list_p[i], list_p[i + 1], img);
                desenha_reta(list_p[list_p.Count - 1], list_p[0], img);
            }
        }

        #endregion

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

            calculaCentro();
            atualizaNormais();
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

        public void escala(double value, DirectBitmap img)
        {
            translacao(-(int)centro.getX(), -(int)centro.getY(), 0);

            double[,] mat = {
                {value, 0, 0, 0},
                {0, value, 0 ,0},
                {0, 0, value, 0},
                { 0,  0,  0,  1}
            };

            mat_a = mult_mat(mat, mat_a);

            translacao((int)centro.getX(), (int)centro.getY(), 0);
        }

        public void rotacaoX(double ang)
        {
            translacao(-(int)centro.getX(), -(int)centro.getY(), 0);

            double[,] mat = {
                {1, 0, 0, 0},
                {0, Math.Cos(ang), -Math.Sin(ang), 0},
                {0, Math.Sin(ang), Math.Cos(ang), 0 },
                {0, 0, 0, 1}
            };

            mat_a = mult_mat(mat_a, mat);

            translacao((int)centro.getX(), (int)centro.getY(), 0);
        }

        public void rotacaoY(double ang)
        {
            translacao(-(int)centro.getX(), -(int)centro.getY(), 0);

            double[,] mat = {
                {Math.Cos(ang), 0, Math.Sin(ang), 0},
                {0, 1, 0, 0},
                {-Math.Sin(ang), 0, Math.Cos(ang), 0},
                {0, 0, 0, 1}
            };

            mat_a = mult_mat(mat_a, mat);

            translacao((int)centro.getX(), (int)centro.getY(), 0);
        }

        public void rotacaoZ(double ang)
        {
            translacao(-(int)centro.getX(), -(int)centro.getY(), 0);

            double[,] mat = {
                {Math.Cos(ang), -Math.Sin(ang), 0, 0},
                {Math.Sin(ang), Math.Cos(ang), 0, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 1}
            };

            mat_a = mult_mat(mat, mat_a);

            translacao((int)centro.getX(), (int)centro.getY(), 0);
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

        //algoritmo do ponto médio para traçado de retas
        private void desenha_reta(Point ini, Point fim, Color c,  DirectBitmap img)
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
                    desenha_reta(fim, ini, c, img);

                //constante de bresenham
                dy *= declive;
                incE = 2 * dy;
                incNE = 2 * dy - 2 * dx;
                d = 2 * dy - dx;
                y = y1;

                for (x = x1; x <= x2; x++)
                {
                    if (verifica_borda(new Point(x, y), img))
                        img.SetPixel(x, y, c);

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
                    desenha_reta(fim, ini, c, img);

                //constante de bresenham
                dx *= declive;
                incE = 2 * dx;
                incNE = 2 * dx - 2 * dy;
                d = 2 * dx - dy;
                x = x1;

                for (y = y1; y <= y2; y++)
                {
                    if (verifica_borda(new Point(x, y), img))
                        img.SetPixel(x, y, c);

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
            double x = 0, y = 0, z = 0;

            foreach (Vertice v in list_va)
            {
                x += v.getX();
                y += v.getY();
                z += v.getZ();
            }

            x /= list_va.Count;
            y /= list_va.Count;
            z /= list_va.Count;

            centro.setX(x);
            centro.setY(y);
            centro.setZ(z);
        }

        private void atualizaNormais()
        {
            foreach(Face f in list_f)
            {
                List<Vertice> lv = new List<Vertice>();
                List<int> _vet = f.getVet();

                foreach(int i in _vet)
                    lv.Add(list_va[i - 1]);

                f.calcNormal(lv);
            }
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
