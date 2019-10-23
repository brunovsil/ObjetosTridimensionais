using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetosTridimencionais
{
    class Face
    {
        private List<int> listIndex = new List<int>();
        private double[] vetNormal = new double[3];

        public Face(){ }

        #region Getters e Setters

        public void addIndex(int i) { listIndex.Add(i); }
        public List<int> getVet() { return listIndex; }
        public double[] getNormal() { return vetNormal; }
        public void setNormal(double[] v) { vetNormal = v; }

        #endregion

        #region Métodos

        public void scanLine(Vertice[] list_v, Color c, Bitmap img)
        {
            List<List<NodoET>> et = new List<List<NodoET>>();
            List<NodoET> aet = new List<NodoET>();

            for(int i = 0; i < 3; i++)
            {
                int prox = i + 1;
                if (i == 2) prox = 0;

                //definindo valores
                double ymin, ymax, xmax, xmin;
                if (list_v[listIndex[i]].getY() < list_v[listIndex[prox]].getY())
                {
                    ymin = list_v[listIndex[i]].getY();
                    ymax = list_v[listIndex[prox]].getY();
                }
                else
                {
                    ymin = list_v[listIndex[prox]].getY();
                    ymax = list_v[listIndex[i]].getY();
                }

                if (list_v[listIndex[i]].getX() > list_v[listIndex[prox]].getX())
                {
                    xmin = list_v[listIndex[i]].getX();
                    xmax = list_v[listIndex[prox]].getX();
                }
                else
                {
                    xmin = list_v[listIndex[prox]].getX();
                    xmax = list_v[listIndex[i]].getX();
                }

                //definindo o nodo e adicionando a ET
                NodoET nodo = new NodoET(ymax, xmin, (ymax - ymin) / (xmax - xmin));
                et[(int)Math.Round(ymin)].Add(nodo);
            }
        }

        public void calcNormal(List<Vertice> list_v)
        {
            //vertices
            Vertice A = list_v[0]; 
            Vertice B = list_v[1]; 
            Vertice C = list_v[list_v.Count - 1];

            //vetores
            double[] vet1 = new double[3];
            double[] vet2 = new double[3];
            vet1[0] = B.getX() - A.getX(); vet1[1] = B.getY() - A.getY(); vet1[2] = B.getZ() - A.getZ(); //define x, y e z
            vet2[0] = C.getX() - A.getX(); vet2[1] = C.getY() - A.getY(); vet2[2] = C.getZ() - A.getZ(); //define x, y e z

            double[] vn = prodVetorial(vet1, vet2);

            vn = normalizaVetor(vn);

            vetNormal = vn;
        }

        #endregion

        #region Métodos auxiliares

        private double[] prodVetorial(double[] u, double[] v)
        {
            double[] r = new double[3];

            r[0] = u[1] * v[2] - u[2] * v[1];
            r[1] = u[2] * v[0] - u[0] * v[2];
            r[2] = u[0] * v[1] - u[1] * v[0];

            return r;
        }

        private double prodEscalar(double[] u, double[] v)
        {
            return u[0] * v[0] + u[1] * v[1] + u[2] * v[2];
        }

        private double[] normalizaVetor(double[] v)
        {
            double[] r = new double[3];

            double norma = Math.Sqrt(Math.Pow(v[0], 2) + Math.Pow(v[1], 2) + Math.Pow(v[2], 2));

            r[0] = v[0] / norma;
            r[1] = v[1] / norma;
            r[2] = v[2] / norma;

            return r;
        }

        #endregion
    }
}
