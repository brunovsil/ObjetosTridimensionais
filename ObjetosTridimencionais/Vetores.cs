using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetosTridimencionais
{
    abstract class Vetores
    {
        static public double[] normalizaVetor(double[] v)
        {
            double[] r = new double[3];

            double norma = Math.Sqrt(Math.Pow(v[0], 2) + Math.Pow(v[1], 2) + Math.Pow(v[2], 2));

            r[0] = v[0] / norma;
            r[1] = v[1] / norma;
            r[2] = v[2] / norma;

            return r;
        }

        static public double prodEscalar(double[] u, double[] v)
        {
            return u[0] * v[0] + u[1] * v[1] + u[2] * v[2];
        }

        static public double[] prodVetorial(double[] u, double[] v)
        {
            double[] r = new double[3];

            r[0] = u[1] * v[2] - u[2] * v[1];
            r[1] = u[2] * v[0] - u[0] * v[2];
            r[2] = u[0] * v[1] - u[1] * v[0];

            return r;
        }
    }
}
