using System;

namespace Practica2DIA.Core {
    using Contenedores;
    public abstract class Contenedor
    {
        public enum TipoCont { grande, pequeno };
        protected Contenedor(int pesoKg, string id)
        {
            this.PesoKg = pesoKg;
            this.Id = id;
        }

        public int PesoKg
        {
            get;
            private set;
        }

        public string Id
        {
            get;
            private set;
        }

        public static Contenedor Crea(TipoCont t, string id)
        {
            Contenedor toret = null;

            if (t.Equals(TipoCont.grande))
            {
                toret = new ContGrande(id);
            }
            else if (t.Equals(TipoCont.pequeno))
            {
                toret = new ContPequeno(id);
            }
            
            return toret;
        }
        
        public override string ToString()
        {
            return string.Format("(Contenedor de {0} Kilos)", PesoKg);
        }
    }
}