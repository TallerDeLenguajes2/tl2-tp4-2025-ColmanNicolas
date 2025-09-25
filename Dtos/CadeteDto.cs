namespace tl2_tp4_2025_ColmanNicolas.Dtos
{
    public class CadeteDto
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public void Deconstruct(out string Nombre, out string Telefono, out string Direccion)
        {
            Nombre = this.Nombre;
            Telefono = this.Telefono;
            Direccion = this.Direccion;
        }
    }
}