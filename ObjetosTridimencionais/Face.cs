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

            double[] vn = Vetores.prodVetorial(vet1, vet2);

            vn = Vetores.normalizaVetor(vn);

            vetNormal = vn;
        }

        #endregion

    }
}
