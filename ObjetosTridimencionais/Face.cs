using System;
using System.Collections.Generic;
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
        
        public void addIndex(int i) { vetIndex[this.TL++] = i; }
        public int[] getVet() { return vetIndex; }
    }
}
