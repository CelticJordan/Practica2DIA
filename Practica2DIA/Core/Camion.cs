using System;

namespace Practica2DIA.Core {
    using Camiones;
    public abstract class Camion
    {
        public enum TipoCamion { grande, pequeno };
        protected Camion(int pesoKg, string id)
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

        public static Camion Crea(TipoCamion t, string id)
        {
            Camion toret = null;

            if (t.Equals(TipoCamion.grande))
            {
                toret = new CamionGrande(id);
            }
            else if (t.Equals(TipoCamion.pequeno))
            {
                toret = new CamionPequeno(id);
            }
            
            return toret;
        }

        public override string ToString()
        {
            return string.Format("(Camion de {0} Kilos)", PesoKg);
        }
    }
}