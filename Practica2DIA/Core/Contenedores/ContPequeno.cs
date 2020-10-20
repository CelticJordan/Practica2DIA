namespace Practica2DIA.Core.Contenedores
{
    public class ContPequeno : Contenedor
    {
        public const int PesoKg = 800;
        private string id;

        public ContPequeno(string id) : base(PesoKg, id)
        {
        }

        public override string ToString()
        {
            return Id + base.ToString();
        }
    }
}