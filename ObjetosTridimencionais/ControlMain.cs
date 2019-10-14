using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetosTridimencionais
{
    class ControlMain
    {
        private Obj obj;

        public ControlMain() { }

        public void lerObjeto(string caminho, Bitmap img)
        {
            obj = new Obj();
            obj.carregar(caminho);
            obj.desenha_pp(img);
        }

        public void translacao(int dx, int dy)
        {
            obj.translacao(dx, dy);
        }
    }
}
