using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CadeteClass
{
    public class Cadete
    {
        public int Id { get; }
        public string Nombre { get; }
        public string Telefono { get; }
        public string Direccion { get; }
        public Cadete()
        {
            this.Id = 0;
            this.Nombre = "Sin cadete";
            this.Telefono = "Sin telefono";
            this.Direccion = "Sin direccion"; 
        }

        [JsonConstructor]
        public Cadete(int id, string nombre, string telefono, string direccion)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Telefono = telefono;
            this.Direccion = direccion;
        }
        public int ObtenerId()
        {
            return this.Id;
        }
        public string ObtenerNombre()
        {
            return this.Nombre;
        }
        public string ObtenerTelefono()
        {
            return this.Telefono;
        }
        public string ObtenerDireccion()
        {
            return this.Direccion;
        }
        public override string ToString()
        {
            return $"{Id} - {Nombre} - {Telefono} - {Direccion}";
        }
        /*            Console.WriteLine($"\nCadete: {this.Nombre}");
            Console.WriteLine($"\nJornal a cobrar: ${cobro} por la cantidad de {cobro/500} envios entregados.");*/
    }
}