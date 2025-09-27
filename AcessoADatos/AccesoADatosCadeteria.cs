using System.Text.Json;
using CadeteriaClass;

namespace tl2_tp4_2025_ColmanNicolas.AccesoADatos
{
    public class AccesoADatosCadeteria : IAccesoADatos<Cadeteria>
    {
        public AccesoADatosCadeteria(){}

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public (bool, string, List<Cadeteria>) Obtener()
        {
            string ruta = Path.Combine(Directory.GetCurrentDirectory(), "almacenamiento", "cadeteria.json");

            if (File.Exists(ruta))
            {
                try
                {
                    string json = File.ReadAllText(ruta);
                    List<Cadeteria> cadeteria = JsonSerializer.Deserialize<List<Cadeteria>>(json, options);
                    if (cadeteria != null)
                    {
                        return (true, $"Datos obtenidos con exito", cadeteria);
                    }
                    else
                    {
                        return (false, $"Base de datos Vacia", new List<Cadeteria>());

                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error al leer cadeteria.json: {ex.Message}", new List<Cadeteria>());
                }
            }
            else
            {
                return (false, "Error. No se encontró el archivo cadeteria.json.", new List<Cadeteria>());
            }

        }

        public (bool, string) Guardar(List<Cadeteria> cadeteria)
        {
            return (false, "Error. No se tienen permisos para modificar cadeteria .");
        }
    }
}