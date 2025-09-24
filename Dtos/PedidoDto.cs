namespace tl2_tp4_2025_ColmanNicolas.Dtos
{
    public class PedidoDto
    {
        public string DetallePedido { get; set; }
        public string Observacion { get; set; }
        public string NombreCliente { get; set; }
        public string DireccionCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public string DatosRefDireccion { get; set; }
        public void Deconstruct(out string detallePedido, out string observacion, out string nombreCliente, out string telefonoCliente, out string direccionCliente, out string datosRefDireccion)
        {
            detallePedido = this.DetallePedido;
            observacion = this.Observacion;
            nombreCliente = this.NombreCliente;
            telefonoCliente = this.TelefonoCliente;
            direccionCliente = this.DireccionCliente;
            datosRefDireccion = this.DatosRefDireccion;
        }
    }
}