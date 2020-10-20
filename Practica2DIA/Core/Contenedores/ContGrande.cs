namespace Practica2DIA.Core.Contenedores
{
    public class ContGrande: Contenedor
    {
        public const int PesoKg = 1500;
        private string id;
        
        public ContGrande(string id) : base(PesoKg, id)
        {
        }

        public override string ToString()
        {
            return Id + base.ToString();
        }
    }
}