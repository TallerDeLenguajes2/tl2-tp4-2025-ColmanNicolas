using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ClienteClass
{
    public class Cliente
    {
        private int Incremental = 1;
        public int Id { get; }
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
        // Constructor para deserialización desde JSON
        [JsonConstructor]
        public Cliente(int id, string nombre, string direccion, string telefono, string datosReferenciaDireccion)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            DatosReferenciaDireccion = datosReferenciaDireccion;
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