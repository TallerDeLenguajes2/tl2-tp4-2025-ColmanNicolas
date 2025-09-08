using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteClass
{
    public class Cliente
    {
        private static int Incremental = 1;
        private int Id { get; }
        public string Nombre { get; }
        public string Direccion { get; }
        public string Telefono { get; }
        public string DatosReferenciaDireccion { get; }

        public Cliente(string nombre, string direccion, string telefono, string datosDireccion)
        {
            this.Id = Incremental++;
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Telefono = telefono;
            this.DatosReferenciaDireccion = datosDireccion;
        }

        public string[] MostrarCliente()
        {
            return [this.Nombre, this.Telefono];
        }

        public string[] MostrarDireccion()
        {
            return [this.Direccion, this.DatosReferenciaDireccion];

        }

    }
}