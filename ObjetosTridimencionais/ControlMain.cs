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
        private char proj = '1'; //define o tipo de projeção
        // proj
        // 1 == projeção paralela ortográfica no eixo Z (x, y)
        // 2 == projeção paralela ortográfica no eixo Y (x, z)
        // 3 == projeção paralela ortográfica no eixo X (y, z)

        public ControlMain() { }

        public Obj getObj() { return this.obj; }
        public void setProj(DirectBitmap img, char proj) { this.proj = proj; desenha(img); }

        public void lerObjeto(string caminho, DirectBitmap img)
        {
            obj = new Obj();
            obj.carregar(caminho);
            desenha(img);
        }

        public void translacao(int tx, int ty, int tz, DirectBitmap img)
        {
            obj.translacao(tx, ty, tz);
            obj.aplica_transformacoes();
            desenha(img);
        }

        public void escala(double value, DirectBitmap img)
        {
            obj.escala(value, img);
            obj.aplica_transformacoes();
            desenha(img);
        }

        public void rotacao(int ang_x, int ang_y, int ang_z, DirectBitmap img)
        {
            obj.rotacaoX((ang_x * Math.PI) / 180);
            obj.rotacaoY((ang_y * Math.PI) / 180);
            obj.rotacaoZ((ang_z * Math.PI) / 180);
            obj.aplica_transformacoes();
            desenha(img);
        }

        private void desenha(DirectBitmap img)
        {
            switch(proj)
            {
                case '1':
                    obj.projecao_po(img, proj);
                    break;
                case '2':
                    obj.projecao_po(img, proj);
                    break;
                case '3':
                    obj.projecao_po(img, proj);
                    break;
                case '4':
                    obj.projecao_pb(img, proj);
                    break;
                case '5':
                    obj.projecao_pb(img, proj);
                    break;
                default:
                    obj.projecao_po(img, '1');
                    break;
            }
        }
    }
}
