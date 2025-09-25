using System.Text.Json;
using CadeteClass;
using CadeteriaClass;

namespace AccesoADatosJsonClass
{
    public class AccesoADatosJSON : IAccesoADatos
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public List<Cadete> AccesoADatosCadetes()
        {
            string ruta = Path.Combine(Directory.GetCurrentDirectory(), "almacenamiento", "cadetes.json");

            if (File.Exists(ruta))
            {
                try
                {
                    string json = File.ReadAllText(ruta);
                    List<Cadete> cadetes = JsonSerializer.Deserialize<List<Cadete>>(json, options);
                    return cadetes ?? new List<Cadete>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al leer cadetes.json: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("No se encontró el archivo cadetes.json.");
            }

            return new List<Cadete>();
        }

        public void GuardarDatosDeCadetes(List<Cadete> cadetes)
        {
            string ruta = Path.Combine(Directory.GetCurrentDirectory(), "almacenamiento", "cadetes.json");

            try
            {
                string carpeta = Path.GetDirectoryName(ruta);
                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                string json = JsonSerializer.Serialize(cadetes, options);
                File.WriteAllText(ruta, json);

                Console.WriteLine("Datos de cadetes guardados correctamente en cadetes.json.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar cadetes.json: {ex.Message}");
            }
        }
    }
}