using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CadeteClass;
using ClienteClass;


namespace PedidoClass
{
    public class Pedido
    {
        private static int Incremental = 1; //variable para toda la clase
        private int Nro { get; }
        private string DetallePedido { get; }
        private string Obs { get; }
        private Cliente Cliente { get; }
        private Cadete? Cadete { get; set; }
        private EstadoPedido Estado { get; set; }
        private DateTime Fecha { get; set; }

        public enum EstadoPedido
        {
            AprobacionPendiente,
            Procesando,
            EnViaje,
            Entregado,
            Cancelado
        }

        public Pedido(Pedido pedido)
        {
            this.Nro = Incremental++;
            this.DetallePedido = pedido.DetallePedido;
            this.Obs = pedido.Obs;
            this.Estado = EstadoPedido.AprobacionPendiente;
            this.Fecha = DateTime.Now;
            this.Cliente = new Cliente(pedido.Cliente.Nombre, pedido.Cliente.Direccion, pedido.Cliente.Telefono, pedido.Cliente.DatosReferenciaDireccion); // revisar esto
            this.Cadete = null;
        }

        public string[] VerDireccionCliente()
        {
            return this.Cliente.MostrarDireccion();
        }
        public string[] VerDatosCliente()
        {
            return this.Cliente.MostrarCliente();
        }
        public void AvanzarEstadoPedido()
        {
            if (Estado == EstadoPedido.AprobacionPendiente) Estado = EstadoPedido.Procesando;
            else if (Estado == EstadoPedido.Procesando) Estado = EstadoPedido.EnViaje;
            else if (Estado == EstadoPedido.EnViaje) Estado = EstadoPedido.Entregado;
        }
        public void CancelarPedido()
        {
            Estado = EstadoPedido.Cancelado;
        }
        public int ObtenerNro()
        {
            return this.Nro;
        }
        public EstadoPedido ObtenerEstado()
        {
            return this.Estado;
        }
        public Cadete? ObtenerCadete()
        {
            return this.Cadete;
        }
        public void AsignarCadete(Cadete unCadete)
        {
            this.Cadete = unCadete;
        }
        public void RemoverCadete(Cadete unCadete)
        {
            this.Cadete = null;
        }

    }
}