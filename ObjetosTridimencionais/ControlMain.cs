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

        public void translacao(int dx, int dy, int dz, Bitmap img)
        {
            obj.translacao(dx, dy, dz);
            obj.aplica_transformacoes();
            obj.desenha_pp(img);
        }

        public void escala(double value, Bitmap img)
        {
            obj.escala(value);
            obj.aplica_transformacoes();
            obj.desenha_pp(img);
        }
    }
}
