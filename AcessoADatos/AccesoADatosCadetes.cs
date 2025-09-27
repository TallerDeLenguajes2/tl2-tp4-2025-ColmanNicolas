using System.Text.Json;
using CadeteClass;

namespace tl2_tp4_2025_ColmanNicolas.AccesoADatos
{
    public class AccesoADatosCadetes : IAccesoADatos<Cadete>
    {
        public AccesoADatosCadetes(){}

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public (bool, string, List<Cadete>) Obtener()
        {
            string ruta = Path.Combine(Directory.GetCurrentDirectory(), "almacenamiento", "cadetes.json");

            if (File.Exists(ruta))
            {
                try
                {
                    string json = File.ReadAllText(ruta);
                    List<Cadete> cadetes = JsonSerializer.Deserialize<List<Cadete>>(json, options);
                    if (cadetes != null)
                    {
                        return (true, $"Datos obtenidos con exito", cadetes);
                    }
                    else
                    {
                        return (false, $"Base de datos Vacia", new List<Cadete>());

                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error al leer cadetes.json: {ex.Message}", new List<Cadete>());
                }
            }
            else
            {
                return (false, "Error. No se encontró el archivo cadetes.json.", new List<Cadete>());
            }

        }

        public (bool, string) Guardar(List<Cadete> cadetes)
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

                return (true, "Datos de cadetes guardados correctamente en cadetes.json.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al guardar cadetes.json: {ex.Message}");
            }
        }
    }
}