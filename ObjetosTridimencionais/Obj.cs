using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetosTridimencionais
{
    class Obj
    {
        List<Vertice> list_v = new List<Vertice>();
        List<Face> list_f = new List<Face>();

        public Obj() { }

        public List<Vertice> getVertices() { return list_v; }
        public void addVertice(Vertice v) { this.list_v.Add(v); }

        public List<Face> getFaces() { return list_f; }
        public void addFace(Face f) { this.list_f.Add(f); }

        public void carregar()
        {

        }
    }
}
