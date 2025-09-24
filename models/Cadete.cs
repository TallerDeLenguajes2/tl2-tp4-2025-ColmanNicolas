using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CadeteClass
{
    public class Cadete
    {
        private static int Incremental = 1;
        public int Id { get; }
        public string Nombre { get; }
        public string Telefono { get; }
        public string Direccion { get; }

        public Cadete(string nombre, string telefono, string direccion)
        {
            this.Id = Incremental++;
            this.Nombre = nombre;
            this.Telefono = telefono;
            this.Direccion = direccion;
        }
        public static Cadete CrearCadete(string nombre, string telefono, string direccion)
        {

            return new Cadete(nombre, telefono, direccion);
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