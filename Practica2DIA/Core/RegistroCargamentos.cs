using System;

namespace Practica2DIA.Core {
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Collections;
    using System.Xml.Linq;
    using System.Collections.Generic;
    
    public class RegistroCargamentos: ICollection<Cargamento>
    {
        public const string ArchivoXml = "cargamentos.xml";
        public const string EtqCargamentos = "cargamentos";
        public const string EtqCargamento = "cargamento";
        public const string EtqNumCont = "numContenedores";
        public const string EtqCam = "camion";
        public const string EtqConts = "contenedores";
        public const string EtqCont1 = "contenedor1";
        public const string EtqCont2 = "contenedor2";
        public const string EtqId = "id";
        public const string EtqPeso = "peso";

        public RegistroCargamentos()
        {
            this.cargamentos = new List<Cargamento>();
        }

        public RegistroCargamentos(IEnumerable<Cargamento> cargamentos): this()
        {
            this.cargamentos.AddRange(cargamentos);
        }

        public void Add(Cargamento c)
        {
            this.cargamentos.Add(c);
        }

        public bool Remove(Cargamento c)
        {
            return this.cargamentos.Remove(c);
        }

        public void RemoveAt(int i)
        {
            this.cargamentos.RemoveAt(i);
        }

        public void AddRange(IEnumerable<Cargamento> c)
        {
            this.cargamentos.AddRange(c);
        }

        public int Count
        {
            get { return this.cargamentos.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Clear()
        {
            this.cargamentos.Clear();
        }

        public bool Contains(Cargamento c)
        {
            return this.cargamentos.Contains(c);
        }

        public void CopyTo(Cargamento[] c, int i)
        {
            this.cargamentos.CopyTo( c, i );
        }

        IEnumerator<Cargamento> IEnumerable<Cargamento>.GetEnumerator()
        {
            foreach(var c in this.cargamentos) {
                yield return c;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach(var c in this.cargamentos) {
                yield return c;
            }
        }
        
        public Cargamento this[int i] {
            get { return this.cargamentos[ i ]; }
            set { this.cargamentos[ i ] = value; }
        }
        
        public static Camion.TipoCamion conversionKgCam(string s)
        {
            if (s.Equals("1000"))
            {
                return Camion.TipoCamion.pequeno;
            }
            else if (s.Equals("2000"))
            {
                return Camion.TipoCamion.grande;
            }
            else
            {
                throw new System.ArgumentException("El parámetro no está dentro de los valores aceptados (1000 o 2000)");
            }
        }
        
        public static Contenedor.TipoCont conversionKgCont(string s)
        {
            if (s.Equals("800"))
            {
                return Contenedor.TipoCont.pequeno;
            }
            else if (s.Equals("1500"))
            {
                return Contenedor.TipoCont.grande;
            }
            else
            {
                throw new System.ArgumentException("El parámetro no está dentro de los valores aceptados (800 o 1500)");
            }
        }
        
        public override string ToString()
        {
            var toret = new StringBuilder();

            foreach(Cargamento c in this.cargamentos) {
                toret.AppendLine( c.ToString() );
            }

            return toret.ToString();
        }
        
        public void GuardaXml()
        {
            this.GuardaXml( ArchivoXml );
        }
        
        public void GuardaXml(string nf)
        {
            var doc = new XDocument();
            var root = new XElement( EtqCargamentos );
            
            foreach(Cargamento cargamento in this.cargamentos) {
                
                if (cargamento.numContenedores == 1)
                {
                    root.Add(
                        new XElement( EtqCargamento,
                            new XAttribute( EtqId, cargamento.idCargamento ),
                            new XAttribute( EtqNumCont, cargamento.numContenedores.ToString() ),
                            new XElement( EtqCam,  
                                new XAttribute(EtqId, cargamento.Camion.Id),
                                new XAttribute(EtqPeso, cargamento.Camion.PesoKg.ToString()),
                                new XElement( EtqConts,
                                    new XElement( EtqCont1,
                                        new XAttribute( EtqId, cargamento.Contenedor1.Id),
                                        new XAttribute( EtqPeso, cargamento.Contenedor1.PesoKg.ToString() ) ) ) ) ) );
                }
                
                if (cargamento.numContenedores == 2)
                {
                    root.Add(
                        new XElement( EtqCargamento,
                            new XAttribute( EtqId, cargamento.idCargamento ),
                            new XAttribute( EtqNumCont, cargamento.numContenedores.ToString() ),
                            new XElement( EtqCam,  
                                new XAttribute(EtqId, cargamento.Camion.Id),
                                new XAttribute(EtqPeso, cargamento.Camion.PesoKg.ToString()),
                                new XElement( EtqConts,
                                    new XElement( EtqCont1,
                                        new XAttribute( EtqId, cargamento.Contenedor1.Id),
                                        new XAttribute( EtqPeso, cargamento.Contenedor1.PesoKg.ToString() ) ),
                                    new XElement( EtqCont2,
                                        new XAttribute( EtqId, cargamento.Contenedor2.Id),
                                        new XAttribute( EtqPeso, cargamento.Contenedor2.PesoKg.ToString() ) ) ) ) ) );
                }
            }
            
            doc.Add( root );
            doc.Save( nf );
        }
        
        public static RegistroCargamentos RecuperaXml(string f)
        {
            var toret = new RegistroCargamentos();

            try
            {
                var doc = XDocument.Load(f);

                if (doc.Root != null
                    && doc.Root.Name == EtqCargamentos)
                {
                    var cargamentos = doc.Root.Elements(EtqCargamento);

                    foreach (var cargamentoXml in cargamentos)
                    {

                        if ((int) cargamentoXml.Attribute(EtqNumCont) == 1)
                        {
                            toret.Add(new Cargamento(
                            (string) cargamentoXml.Attribute(EtqId).Value,
                            (Camion.TipoCamion) conversionKgCam(cargamentoXml.Element(EtqCam).Attribute(EtqPeso).Value),
                            (string) cargamentoXml.Element(EtqCam).Attribute(EtqId).Value,
                            (Contenedor.TipoCont) conversionKgCont(cargamentoXml.Element(EtqCam).Element(EtqConts).Element(EtqCont1).Attribute(EtqPeso).Value),
                            (string) cargamentoXml.Element(EtqCam).Element(EtqConts).Element(EtqCont1).Attribute(EtqId).Value));
                        }

                        else if ((int) cargamentoXml.Attribute(EtqNumCont) == 2)
                        {
                            toret.Add(new Cargamento(
                                (string) cargamentoXml.Attribute(EtqId).Value,
                                (Camion.TipoCamion) conversionKgCam(cargamentoXml.Element(EtqCam).Attribute(EtqPeso).Value),
                                (string) cargamentoXml.Element(EtqCam).Attribute(EtqId).Value,
                                (Contenedor.TipoCont) conversionKgCont(cargamentoXml.Element(EtqCam).Element(EtqConts).Element(EtqCont1).Attribute(EtqPeso).Value),
                                (string) cargamentoXml.Element(EtqCam).Element(EtqConts).Element(EtqCont1).Attribute(EtqId).Value,
                                (Contenedor.TipoCont) conversionKgCont(cargamentoXml.Element(EtqCam).Element(EtqConts).Element(EtqCont2).Attribute(EtqPeso).Value),
                                (string) cargamentoXml.Element(EtqCam).Element(EtqConts).Element(EtqCont2).Attribute(EtqId).Value));
                        }
                    }
                }
            }
            
            catch(XmlException)
            {
                toret.Clear();
            }
            catch(IOException)
            {
                toret.Clear();
            }
                
            return toret;
        }
        
        public static RegistroCargamentos RecuperaXml()
        {
            return RecuperaXml( ArchivoXml );
        }
        
        private List<Cargamento> cargamentos;
    }
}