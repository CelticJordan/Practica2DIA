namespace Practica2DIA.Core.Camiones
{
    public class CamionPequeno: Camion
    {
        public const int PesoKg = 1000;
        private string id;       
        
        public CamionPequeno(string id) : base(PesoKg, id)
        {
        }
        
        public override string ToString()
        {
            return Id + base.ToString();
        }
    }
}