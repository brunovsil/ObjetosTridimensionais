using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetosTridimencionais
{
    class Vertice
    {
        private int x, y, z;

        public Vertice() { }

        public Vertice(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public int getX() { return this.x; }
        public void setX(int x) { this.x = x; }

        public int getY() { return this.y; }
        public void setY(int y) { this.y = y; }

        public int getZ() { return this.z; }
        public void setZ(int z) { this.z = z; }
    }
}
