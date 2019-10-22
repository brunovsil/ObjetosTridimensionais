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

        #endregion
    }
}
