
using CadeteClass;
using CadeteriaClass;

namespace AccesoADatosCSVClass
{
    public class AccesoADatosCSV : IAccesoADatos
    {

        public Cadeteria? AccesoADatosCadeteria()
        {
            string ruta = Path.Combine(Directory.GetCurrentDirectory(), "almacenamiento", "cadeterias.csv");
            Cadeteria? cadeteria = null;

            if (File.Exists(ruta))
            {
                string[] lineas = File.ReadAllLines(ruta);
                if (lineas.Length > 1)
                {
                    string[] campos = lineas[1].Split(',');
                    string nombre = campos[0];
                    string telefono = campos[2];
                    string direccion = campos[1];

                    cadeteria = new Cadeteria(nombre, telefono, direccion);
                }
            }
            else
            {
                Console.WriteLine("Error. No se encontro el archivo CSV con cadeterias.");
            }

            return cadeteria;
        }
        public List<Cadete> AccesoADatosCadetes()
        {
            string ruta = Directory.GetCurrentDirectory();
            ruta = Path.Combine(ruta, "almacenamiento", "cadetes.csv");
            List<Cadete> cadetes = new List<Cadete>();

            if (File.Exists(ruta))
            {
                string[] lineas = File.ReadAllLines(ruta);
                for (int i = 1; i < lineas.Length; i++)
                {

                    string[] campos = lineas[i].Split(',');
                    string nombre = campos[0];
                    string telefono = campos[2];
                    string direccion = campos[1];

                    cadetes.Add(new Cadete(nombre, telefono, direccion));
                }
            }
            else
            {
                Console.WriteLine("Error. No se encontro el archivo CSV con cadetes.");
            }
            return cadetes;
        }
        public void GuardarDatosDeCadetes(List<Cadete> cadetes)
        {
            string ruta = Directory.GetCurrentDirectory();
            ruta = Path.Combine(ruta, "almacenamiento", "cadetes.csv");

            try
            {
                // Creamos el directorio si no existe
                string? carpeta = Path.GetDirectoryName(ruta);
                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                using (StreamWriter writer = new StreamWriter(ruta, false)) // false = sobrescribe
                {
                    // Cabecera del CSV
                    writer.WriteLine("Nombre,Direccion,Telefono");

                    // Escribimos cada cadete en una línea
                    foreach (Cadete cadete in cadetes)
                    {
                        writer.WriteLine($"{cadete.ObtenerNombre()},{cadete.ObtenerDireccion()},{cadete.ObtenerTelefono()}");
                    }
                }

                Console.WriteLine("Datos de cadetes guardados correctamente en cadetes.csv.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar los datos de cadetes: {ex.Message}");
            }
        }
    }
}