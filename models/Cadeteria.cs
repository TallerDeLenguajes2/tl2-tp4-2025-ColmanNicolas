using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccesoADatosJsonClass;
using CadeteClass;
using ClienteClass;
using PedidoClass;

namespace CadeteriaClass
{
    public class Cadeteria
    {
        private static int Incremental = 1;
        private const int CobroPorPedido = 500;
        private int Id { get; }
        private string Nombre { get; }
        private string Telefono { get; }
        private string Direccion { get; }
        private List<Cadete> Cadetes { get; set;}
        private List<Pedido> Pedidos { get; set; }

        public Cadeteria(string nombre, string telefono, string direccion)
        {
            this.Id = Incremental++;
            this.Nombre = nombre;
            this.Telefono = telefono;
            this.Direccion = direccion;
            this.Cadetes = new List<Cadete>();
            this.Pedidos = new List<Pedido>();
        }

        public void IncorporarCadete(Cadete cadete)
        {
            this.Cadetes.Add(cadete);
        }
        public void IncorporarListadoDeCadetes(List<Cadete> cadetes)
        {
            this.Cadetes = cadetes;
        }
        public Pedido CrearPedido(Pedido pedido)
        {
            return new Pedido(pedido);

        }
        public Cadete? BuscarCadetePorId(int id)
        {
            return Cadetes.Find(c => c.ObtenerId() == id);
        }
        public Pedido? BuscarPedidoPorId(int nroPedido)
        {
            return Pedidos.Find(c => c.ObtenerNro() == nroPedido);
        }

        public List<Cadete> ObtenerCadetes()
        {
            return this.Cadetes;
        }
        public List<Pedido> ObtenerPedidos()
        {
            return this.Pedidos;
        }
        public void QuitarPedidoPorNro(int nro)
        {
            Pedido pedido = this.Pedidos.Find(p => p.ObtenerNro() == nro);
            if (pedido != null)
            {
                this.Pedidos.Remove(pedido);
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
            return this.Pedidos.FindAll(p =>
                p.ObtenerEstado() == Pedido.EstadoPedido.Entregado &&
                p.ObtenerCadete() != null &&
                p.ObtenerCadete().ObtenerId() == idCadete
            );
        }
        public string AsignarCadeteAPedido(int idPedido, int idCadete)
        {
            Cadete? existeCadete = this.Cadetes.Find(p => p.ObtenerId() == idCadete);
            if (existeCadete == null)
            {
                return $"Error. El cadete de id {idCadete} no se encotró en el sistema.";
            }

            Pedido? existePedido = this.Pedidos.Find(p => p.ObtenerNro() == idPedido);
            if (existePedido == null)
            {
                return $"Error. El pedido de id {idPedido} no se encotró en el sistema.";
            }
            if (existePedido.ObtenerCadete() != null)
            {
                return $"Error. Pedido Nro {existePedido.ObtenerNro()} ya asignado al cadete {existePedido.ObtenerCadete().ObtenerNombre()}.";
            }

            if (existePedido.ObtenerEstado() == Pedido.EstadoPedido.AprobacionPendiente)
            {
                return $"Error. Pedido Nro {existePedido.ObtenerNro()} se encuentra en estado de aprobacion pendiente, avance su estado a procesando para poder asignar a un cadete";
            }
            existePedido.AvanzarEstadoPedido();
            existePedido.AsignarCadete(existeCadete);

            return $"Pedido Nro {existePedido.ObtenerNro()} asignado al cadete {existeCadete.ObtenerNombre()}";

        }
        public string ReasignarPedido(int idCadete1, int idCadete2, int nroPedido)
        {
            Cadete? cadete1 = this.Cadetes.Find(c => c.ObtenerId() == idCadete1);
            if (cadete1 == null)
            {
                return $"Error. No se encontro el cadete de id {idCadete1}";
            }

            Cadete? cadete2 = this.Cadetes.Find(c => c.ObtenerId() == idCadete2);
            if (cadete2 == null)
            {
                return $"Error. No se encontro el cadete de id {idCadete2}";
            }

            Pedido? existePedido = Pedidos.Find(p => p.ObtenerNro() == nroPedido);

            if (existePedido != null && existePedido.ObtenerCadete().ObtenerId() == idCadete1)
            {
                existePedido.AsignarCadete(cadete2);
                return $"Pedido {nroPedido} reasignado desde el cadete {cadete1.ObtenerNombre()} al cadete {cadete2.ObtenerNombre()}";
            }
            else
            {
                return $"Error. El cadete {cadete1.ObtenerNombre()} no tiene el pedido de número {nroPedido} para reasignar.";
            }

        }
        public override string ToString()
        {
            return $"{Nombre} - {Telefono} - {Direccion}";
        }
        //informes
    }
}