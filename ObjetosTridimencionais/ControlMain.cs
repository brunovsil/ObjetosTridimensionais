using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetosTridimencionais
{
    class ControlMain
    {
        private Obj obj;

        public ControlMain() { }

        public void lerObjeto()
        {
            obj = new Obj();
            obj.carregar();
        }
    }
}
