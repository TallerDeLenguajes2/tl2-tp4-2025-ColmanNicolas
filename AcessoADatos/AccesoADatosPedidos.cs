using System.Text.Json;
using PedidoClass;

namespace tl2_tp4_2025_ColmanNicolas.AccesoADatos
{
    public class AccesoADatosPedidos : IAccesoADatos<Pedido>
    {
        public AccesoADatosPedidos() { }

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public (bool, string, List<Pedido>) Obtener()
        {
            string ruta = Path.Combine(Directory.GetCurrentDirectory(), "almacenamiento", "pedidos.json");

            if (File.Exists(ruta))
            {
                try
                {
                    string json = File.ReadAllText(ruta);
                    List<Pedido> pedidos = JsonSerializer.Deserialize<List<Pedido>>(json, options);
                    if (pedidos.Count > 0)
                    {
                        return (true, $"Datos obtenidos con exito", pedidos);
                    }
                    else
                    {
                        return (false, $"Base de datos Vacia", new List<Pedido>());

                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error al leer pedidos.json: {ex.Message}", new List<Pedido>());
                }
            }
            else
            {
                return (false, "Error. No se encontró el archivo pedidos.json.", new List<Pedido>());
            }

        }

        public (bool, string) Guardar(List<Pedido> pedidos)
        {
            string ruta = Path.Combine(Directory.GetCurrentDirectory(), "almacenamiento", "pedidos.json");

            try
            {
                string carpeta = Path.GetDirectoryName(ruta);
                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                string json = JsonSerializer.Serialize(pedidos, options);
                File.WriteAllText(ruta, json);

                return (true, "Datos de pedidos guardados correctamente en pedidos.json.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al guardar pedidos.json: {ex.Message}");
            }
        }
    }
}