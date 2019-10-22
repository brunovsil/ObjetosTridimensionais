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

        public Obj getObj() { return this.obj; }

        public void lerObjeto(string caminho, DirectBitmap img)
        {
            obj = new Obj();
            obj.carregar(caminho);
            obj.desenha_pp(img);
        }

        public void translacao(int tx, int ty, int tz, DirectBitmap img)
        {
            obj.translacao(tx, ty, tz);
            obj.aplica_transformacoes();
            obj.desenha_pp(img);
        }

        public void escala(double value, DirectBitmap img)
        {
            obj.escala(value, img);
            obj.aplica_transformacoes();
            obj.desenha_pp(img);
        }

        public void rotacao(int ang_x, int ang_y, int ang_z, DirectBitmap img)
        {
            obj.rotacaoX((ang_x * Math.PI) / 180);
            obj.rotacaoY((ang_y * Math.PI) / 180);
            obj.rotacaoZ((ang_z * Math.PI) / 180);
            obj.aplica_transformacoes();
            obj.desenha_pp(img);
        }
    }
}
