using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadeteClass;
using ClienteClass;
using PedidoClass;
using tl2_tp4_2025_ColmanNicolas.Dtos;

namespace CadeteriaClass
{
    public class Cadeteria
    {
        private const int CobroPorPedido = 500;
        public int Id { get; }
        public string Nombre { get; }
        public string Telefono { get; }
        public string Direccion { get; }
        public List<Cadete> Cadetes { get; set; }
        public List<Pedido> Pedidos { get; set; }

        public Cadeteria() { }

        public void IncorporarCadete(Cadete cadete)
        {
            Cadetes.Add(cadete);
        }
        public void IncorporarPedido(Pedido pedido)
        {
            Pedidos.Add(pedido);
        }
        public void IncorporarListadoDeCadetes(List<Cadete> cadetes)
        {
            Cadetes = cadetes;

        }
        public void IncorporarListadoDePedidos(List<Pedido> pedidos)
        {
            Pedidos = pedidos;
        }
        public Pedido CrearPedido(PedidoDto pedido)
        {
            var (detallePedido, observacion, nombreCliente, telefonoCliente, direccionCliente, datosRefDireccion) = pedido;

            /* if (string.IsNullOrWhiteSpace(detallePedido) || string.IsNullOrWhiteSpace(observacion) ||
                 string.IsNullOrWhiteSpace(nombreCliente) || string.IsNullOrWhiteSpace(telefonoCliente) ||
                 string.IsNullOrWhiteSpace(direccionCliente) || string.IsNullOrWhiteSpace(datosRefDireccion))
             {
                 return null;
             }*/
            int nroPedido = 1;
            if (this.Pedidos.Count > 0)
            {
                nroPedido = this.Pedidos.Last().ObtenerNro() + 1;
            }
            return new Pedido(nroPedido, detallePedido, observacion, nombreCliente, telefonoCliente, direccionCliente, datosRefDireccion);
        }
        public Cadete CrearCadete(string nombre, string telefono, string direccion)
        {

            /* if (string.IsNullOrWhiteSpace(detallePedido) || string.IsNullOrWhiteSpace(observacion) ||
                 string.IsNullOrWhiteSpace(nombreCliente) || string.IsNullOrWhiteSpace(telefonoCliente) ||
                 string.IsNullOrWhiteSpace(direccionCliente) || string.IsNullOrWhiteSpace(datosRefDireccion))
             {
                 return null;
             }*/
            int idCadete = 1;
            if (this.Cadetes.Count > 0)
            {
                idCadete = this.Cadetes.Last().ObtenerId() + 1;
            }
            return new Cadete(idCadete, nombre, telefono, direccion);
        }
        public Cadete BuscarCadetePorId(int id)
        {
            return Cadetes.Find(c => c.ObtenerId() == id);
        }
        public Pedido BuscarPedidoPorId(int nroPedido)
        {
            return Pedidos.Find(c => c.ObtenerNro() == nroPedido);
        }

        public List<Cadete> ObtenerCadetes()
        {
            return Cadetes;
        }
        public List<Pedido> ObtenerPedidos()
        {
            return Pedidos;
        }
        public void QuitarPedidoPorNro(int nro)
        {
            Pedido pedido = Pedidos.Find(p => p.ObtenerNro() == nro);
            if (pedido != null)
            {
                Pedidos.Remove(pedido);
            }
        }
        public int JornalACobrar(int idCadete)
        {
            return PedidosCompletadosDeUnCadete(idCadete).Count * CobroPorPedido;
        }
        public string[] Informe()
        {
            string[] respuesta = new string[Cadetes.Count];
            int cantPedidos, jornalACobrar, i = 0;

            foreach (var cadete in Cadetes)
            {
                jornalACobrar = JornalACobrar(cadete.ObtenerId());
                cantPedidos = jornalACobrar / CobroPorPedido;

                respuesta[i] = cadete.ToString() + "\n" + $". Envios de la jornada: {cantPedidos}. Cobro jornal: ${jornalACobrar}";
                i++;
            }
            return respuesta;
        }
        public List<Pedido> PedidosCompletadosDeUnCadete(int idCadete)
        {
            return Pedidos.FindAll(p =>
                p.ObtenerEstado() == Pedido.EstadoPedido.Entregado &&
                p.ObtenerCadete() != null &&
                p.ObtenerCadete().ObtenerId() == idCadete
            );
        }
        public (bool resultado, string mensaje) AsignarCadeteAPedido(int idCadete, int idPedido)
        {
            Cadete existeCadete = Cadetes.Find(p => p.ObtenerId() == idCadete);
            if (existeCadete == null)
            {
                return (false, $"Error. El cadete de id {idCadete} no se encotró en el sistema.");
            }

            Pedido existePedido = BuscarPedidoPorId(idPedido);

            if (existePedido == null)
            {
                return (false, $"Error. El pedido de id {idPedido} no se encotró en el sistema.");
            }
            if (existePedido.ObtenerCadete() != null && existePedido.ObtenerCadete().ObtenerId() != 0)
            {
                return (false, $"Error. Pedido Nro {existePedido.ObtenerNro()} ya asignado al cadete {existePedido.ObtenerCadete().ObtenerNombre()}.");
            }

            if (existePedido.ObtenerEstado() == Pedido.EstadoPedido.AprobacionPendiente)
            {
                return (false, $"Error. Pedido Nro {existePedido.ObtenerNro()} se encuentra en estado de aprobacion pendiente, avance su estado a procesando para poder asignar a un cadete");
            }
            existePedido.CambiarEstadoPedido(1);
            existePedido.AsignarCadete(existeCadete);

            return (true, $"Pedido Nro {existePedido.ObtenerNro()} asignado al cadete {existeCadete.ObtenerNombre()}");

        }
        public (bool resultado, string mensaje) ReasignarPedido(int idCadete2, int nroPedido)
        {
            Cadete cadete2 = BuscarCadetePorId(idCadete2);
            Pedido existePedido = Pedidos.Find(p => p.ObtenerNro() == nroPedido);
            Cadete cadete1 = existePedido.ObtenerCadete();

            if (cadete2 == null)
            {
                return (false, $"Error. No se encontro el cadete de id {idCadete2}");
            }

            if (existePedido == null)
            {
                return (false, $"Error. No se encontro el pedido de numero: ({existePedido.ObtenerNro()}).");
            }

            existePedido.AsignarCadete(cadete2);

            if (cadete1 != null)
            {
                return (true, $"Pedido {nroPedido} reasignado desde el cadete {cadete1.ObtenerNombre()} al cadete {cadete2.ObtenerNombre()}");
            }
            else
            {
                return (true, $"Pedido {nroPedido} asignado al cadete {cadete2.ObtenerNombre()}");
            }
        }
        public bool CambiarEstadoDePedido(int nro, int codigo)
        {
            Pedido pedidoBuscado = BuscarPedidoPorId(nro);

            if (pedidoBuscado == null)
            {
                return false;
            }
            else
            {
                pedidoBuscado.CambiarEstadoPedido(codigo);
                return true;
            }
        }
        /*public static string ToString()
         {
             return $"{Nombre} - {Telefono} - {Direccion}";
         }
         //informes*/
    }
}