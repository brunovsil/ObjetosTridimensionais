using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetosTridimencionais
{
    class NodoET
    {
        private double yMin, xMax, incX;

        public NodoET() { }
        public NodoET(double y, double x, double inc)
        {
            this.yMin = y;
            this.xMax = x;
            this.incX = inc;
        }

        //getters e setters
        public double getYMin() { return yMin; }
        public double getXMax() { return xMax; }
        public double getIncX() { return incX; }

        public void setYMin(double ymin) { this.yMin = ymin; }
        public void setXMax(double xmax) { this.xMax= xmax; }
        public void setIncX(double inc) { this.incX = inc; }
    }
}
