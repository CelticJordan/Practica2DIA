using System.Text;
using Practica2DIA.Core.Camiones;

namespace Practica2DIA.Core {
    using System;
    public class Cargamento
    {
        public string idCargamento;
        public int numContenedores;
        public Cargamento(string idCarga, Camion.TipoCamion tcam, string idCam, Contenedor.TipoCont tcon, string idCon)
        {
            if ((tcam.Equals(Camion.TipoCamion.grande) && tcon.Equals(Contenedor.TipoCont.grande)) || 
                 (tcam.Equals(Camion.TipoCamion.grande) && tcon.Equals(Contenedor.TipoCont.pequeno)) || 
                  (tcam.Equals(Camion.TipoCamion.pequeno) && tcon.Equals(Contenedor.TipoCont.pequeno)))
            {
                this.IdCargamento = idCarga;
                this.Camion = Camion.Crea(tcam, idCam);
                this.Contenedor1 = Contenedor.Crea(tcon, idCon);
                this.numContenedores = 1;
            }
            else
            {
                throw new System.ArgumentException("Los parámetros introducidos son incompatibles");
            }
        }

        public Cargamento(string idCarga, Camion.TipoCamion tcam, string idCam, Contenedor.TipoCont tcon1, string idCon1,
            Contenedor.TipoCont tcon2, string idCon2)
        {
            if (tcam.Equals(Camion.TipoCamion.grande) && tcon1.Equals(Contenedor.TipoCont.pequeno) && 
                tcon2.Equals(Contenedor.TipoCont.pequeno))
            {
                this.IdCargamento = idCarga;
                this.Camion = Camion.Crea(tcam, idCam);
                Contenedor1 = Contenedor.Crea(tcon1, idCon1);
                Contenedor2 = Contenedor.Crea(tcon2, idCon2);
                this.numContenedores = 2;
                
            }
            else
            {
                throw new System.ArgumentException("Los parámetros introducidos son incompatibles");
            }
        }

        public string IdCargamento
        {
            get => idCargamento;
            set => idCargamento = value;
        }

        public Camion Camion
        {
            get;
            private set;
        }

        public Contenedor Contenedor1
        {
            get;
            private set;
        }
        
        public Contenedor Contenedor2
        {
            get;
            private set;
        }

        public override string ToString()
        {
            StringBuilder toret = new StringBuilder("Cargamento ");
            toret.Append(IdCargamento);
            toret.Append(" ,formado por Camion ");
            toret.Append(Camion.ToString());

            if (numContenedores == 1)
            {
                toret.Append(" cargando Contenedor ");
                toret.Append(Contenedor1.ToString());
            }

            if (numContenedores == 2)
            {
                toret.Append(" cargando Contenedor ");
                toret.Append(Contenedor1.ToString());
                toret.Append(" y Contenedor ");
                toret.Append(Contenedor2.ToString());
            }

            return toret.ToString();

        }
    }
}