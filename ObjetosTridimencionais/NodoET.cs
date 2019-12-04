using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetosTridimencionais
{
    class NodoET
    {
        private double yMax, xMin, zMin, incX, incZ;

        public NodoET() { }
        public NodoET(double y, double x, double z, double incX, double inxZ)
        {
            this.yMax = y;
            this.xMin = x;
            this.incX = incX;
            this.incZ = incZ;
        }

        //getters e setters
        public double getYMax() { return yMax; }
        public double getXMin() { return xMin; }
        public double getZMin() { return zMin; }
        public double getIncX() { return incX; }
        public double getIncZ() { return incZ; }

        public void setYMax(double ymax) { this.yMax = ymax; }
        public void setXMin(double xmin) { this.xMin = xmin; }
        public void setZMin(double zmin) { this.zMin = zmin; }
        public void setIncX(double inc) { this.incX = inc; }
        public void setIncZ(double inc) { this.incZ = inc; }
    }
}
