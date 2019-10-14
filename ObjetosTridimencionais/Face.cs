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
        private int[] vetIndex = new int[3];
        private int TL;

        public Face(){ this.TL = 0; }
        
        //getters e setters
        public void addIndex(int i) { vetIndex[this.TL++] = i; }
        public int[] getVet() { return vetIndex; }

        //metodos
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
                if (list_v[vetIndex[i]].getY() < list_v[vetIndex[prox]].getY())
                {
                    ymin = list_v[vetIndex[i]].getY();
                    ymax = list_v[vetIndex[prox]].getY();
                }
                else
                {
                    ymin = list_v[vetIndex[prox]].getY();
                    ymax = list_v[vetIndex[i]].getY();
                }

                if (list_v[vetIndex[i]].getX() > list_v[vetIndex[prox]].getX())
                {
                    xmin = list_v[vetIndex[i]].getX();
                    xmax = list_v[vetIndex[prox]].getX();
                }
                else
                {
                    xmin = list_v[vetIndex[prox]].getX();
                    xmax = list_v[vetIndex[i]].getX();
                }

                //definindo o nodo e adicionando a ET
                NodoET nodo = new NodoET(ymax, xmin, (ymax - ymin) / (xmax - xmin));
                et[(int)Math.Round(ymin)].Add(nodo);
            }
        }
    }
}
