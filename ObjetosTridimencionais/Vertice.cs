using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetosTridimencionais
{
    class Vertice
    {
        private double x, y, z;

        public Vertice() { }

        public Vertice(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        //getters e setters
        public double getX() { return this.x; }
        public void setX(double x) { this.x = x; }

        public double getY() { return this.y; }
        public void setY(double y) { this.y = y; }

        public double getZ() { return this.z; }
        public void setZ(double z) { this.z = z; }
    }
}
