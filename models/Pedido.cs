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
        public int Nro { get; }
        public string DetallePedido { get; }
        public string Obs { get; }
        public Cliente Cliente { get; }
        public Cadete Cadete { get; set; }
        public EstadoPedido Estado { get; set; }
        public DateTime Fecha { get; set; }

        public enum EstadoPedido
        {
            AprobacionPendiente,
            Procesando,
            EnViaje,
            Entregado,
            Cancelado
        }

        public Pedido(string detallePedido, string observacion, string nombreCliente, string direccionCliente, string telefonoCliente, string datosRefDireccion)
        {
            this.Nro = Incremental++;
            this.DetallePedido = detallePedido;
            this.Obs = observacion;
            this.Estado = EstadoPedido.AprobacionPendiente;
            this.Fecha = DateTime.Now;
            this.Cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente, datosRefDireccion);
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
        public bool CambiarEstadoPedido(int codigo)
        {
            bool resultado;
            switch (codigo)
            {
                case 1:
                    if (Estado == EstadoPedido.AprobacionPendiente)
                        Estado = EstadoPedido.Procesando;
                    resultado = true;
                    break;

                case 2:
                    if (Estado == EstadoPedido.Procesando)
                        Estado = EstadoPedido.EnViaje;
                    resultado = true;
                    break;

                case 3:
                    if (Estado == EstadoPedido.EnViaje)
                        Estado = EstadoPedido.Entregado;
                    resultado = true;
                    break;

                case 4:
                    Estado = EstadoPedido.Cancelado;
                    resultado = true;
                    break;

                case 5: // reiniciar pedido 
                    Estado = EstadoPedido.AprobacionPendiente;
                    resultado = true;
                    break;

                default:
                    resultado = false;
                    break;
            }
            return resultado;
        }

        public int ObtenerNro()
        {
            return this.Nro;
        }
        public EstadoPedido ObtenerEstado()
        {
            return this.Estado;
        }
        public Cadete ObtenerCadete()
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