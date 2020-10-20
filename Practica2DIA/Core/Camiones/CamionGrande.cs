namespace Practica2DIA.Core.Camiones
{
    public class CamionGrande: Camion
    {
        public const int PesoKg = 2000;
        private string id; 
        
        public CamionGrande(string id) : base(PesoKg, id)
        {
        }
        
        public override string ToString()
        {
            return Id + base.ToString();
        }
    }
}