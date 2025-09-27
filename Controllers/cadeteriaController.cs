using Microsoft.AspNetCore.Mvc;

using CadeteriaClass;
using PedidoClass;
using CadeteClass;
using tl2_tp4_2025_ColmanNicolas.AccesoADatos;
using tl2_tp4_2025_ColmanNicolas.Dtos;

namespace tl2_tp4_2025_ColmanNicolas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CadeteriaController : ControllerBase
{
    private Cadeteria cadeteria; //Cadeteria ya no es clase estática
    private AccesoADatosCadeteria ADCadeteria;
    private AccesoADatosCadetes ADCadetes;
    private AccesoADatosPedidos ADPedidos;
    public (bool, string, List<Pedido>) msjPedidos;
    public CadeteriaController()
    {
        ADCadeteria = new AccesoADatosCadeteria();
        ADCadetes = new AccesoADatosCadetes();
        ADPedidos = new AccesoADatosPedidos();

        var (_, _, cadeteriaList) = ADCadeteria.Obtener();
        cadeteria = cadeteriaList[0];

        var tuplaCadetes = ADCadetes.Obtener();
        cadeteria.IncorporarListadoDeCadetes(tuplaCadetes.Item3);

        var tuplaPedidos = ADPedidos.Obtener();
        cadeteria.IncorporarListadoDePedidos(tuplaPedidos.Item3);
    }

    // GET: api/cadeteria/getPedidos
    [HttpGet("getPedidos")]
    public ActionResult<List<Pedido>> GetPedidos()
    {
        var pedidos = cadeteria.ObtenerPedidos();
        if (pedidos == null)
        {
            return NotFound(new { mensaje = "No hay pedidos registrados" });
        }
        return Ok(pedidos);
    }
    // GET: api/cadeteria/getPedidos/1
    [HttpGet("getPedidos/{nroPedido}")]
    public ActionResult<List<Pedido>> GetPedidoPorNro(int nroPedido)
    {
        Pedido pedido = cadeteria.BuscarPedidoPorId(nroPedido);
        if (pedido == null)
        {
            return NotFound(new { mensaje = $"No se encontro el pedido de numero {nroPedido}" });
        }
        return Ok(pedido);
    }


    // GET: api/cadeteria/getCadetes
    [HttpGet("getCadetes")]
    public ActionResult<List<Cadete>> GetCadetes()
    {
        var cadetes = cadeteria.ObtenerCadetes();
        if (cadetes == null || cadetes.Count == 0)
        {
            return NotFound(new { mensaje = "No hay cadetes registrados" });
        }
        return Ok(cadetes);
    }

    // GET: api/cadeteria/getCadetes/1
    [HttpGet("getCadetes/{idCadete}")]
    public ActionResult<List<Cadete>> GetCadetesPorId(int idCadete)
    {
        Cadete cadete = cadeteria.BuscarCadetePorId(idCadete);
        if (cadete == null)
        {
            return NotFound(new { mensaje = $"No se encontro el cadete de id: ({idCadete})" });
        }
        return Ok(cadete);
    }

    // GET: api/cadeteria/getInforme
    [HttpGet("getInforme")]
    public ActionResult<string[]> GetInforme()
    {
        string[] informeDeCadetes = cadeteria.Informe();
        if (informeDeCadetes.Length == 0)
        {
            return NotFound(new { mensaje = "No hay datos de informe para la fecha solicitada" });
        }
        return Ok(informeDeCadetes);
    }

    // POST: api/cadeteria/postPedido
    [HttpPost("postPedido")]
    public ActionResult AgregarPedido([FromBody] PedidoDto pedido)
    {
        Pedido nuevoPedido = cadeteria.CrearPedido(pedido);

        if (nuevoPedido == null)
        {
            return BadRequest(new { mensaje = "Los datos del pedido están incompletos o son inválidos." });
        }

        cadeteria.IncorporarPedido(nuevoPedido); // sumo el pedido a mi lista pedidos
        ADPedidos.Guardar(cadeteria.ObtenerPedidos());  // recupero lista actualizada y la mando a guardar en JSON

        return Ok(new
        {
            mensaje = "Pedido agregado correctamente",
            pedido = nuevoPedido
        });
    }
    // POST: api/cadeteria/postCadete
    [HttpPost("postCadete")]
    public ActionResult AgregarCadete([FromBody] CadeteDto cadete)
    {
        if (cadete == null)
            return BadRequest("Datos de cadete no proporcionados.");

        /*if (string.IsNullOrWhiteSpace(cadete.Nombre) ||
            string.IsNullOrWhiteSpace(cadete.Telefono) ||
            string.IsNullOrWhiteSpace(cadete.Direccion))
            return BadRequest("Todos los campos del cadete son obligatorios.");*/

        var (Nombre, Telefono, Direccion) = cadete;
        Cadete nuevoCadete = cadeteria.CrearCadete(Nombre, Direccion, Telefono);

        /*bool agregado = cadeteria.IncorporarCadete(nuevoCadete);
        if (!agregado)
            return Conflict("Ya existe un cadete con los mismos datos.");*/
        try
        {
            cadeteria.IncorporarCadete(nuevoCadete);
            ADCadetes.Guardar(cadeteria.ObtenerCadetes());
            return Ok(new { mensaje = "Cadete registrado en el sistema", nuevoCadete });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al guardar los datos: {ex.Message}");
        }

    }


    // PUT: api/cadeteria/asignar/1/2
    [HttpPut("asignar/{idCadete}/{idPedido}")]
    public ActionResult AsignarPedido(int idCadete, int idPedido)
    {
        var (exito, mensaje) = cadeteria.AsignarCadeteAPedido(idCadete, idPedido);
        if (exito)
        {
            ADPedidos.Guardar(cadeteria.ObtenerPedidos());  // recupero lista actualizada y la mando a guardar en JSON
            return Ok(new { mensaje });
        }
        else
        {
            return BadRequest(new { mensaje });
        }
    }

    // PUT: api/cadeteria/estado/1/2
    [HttpPut("estado/{idPedido}/{nuevoEstado}")]
    public ActionResult CambiarEstadoPedido(int idPedido, int nuevoEstado)
    {
        bool exito = cadeteria.CambiarEstadoDePedido(idPedido, nuevoEstado);
        if (exito)
        {
            ADPedidos.Guardar(cadeteria.ObtenerPedidos());  // recupero lista actualizada y la mando a guardar en JSON
            return Ok("Estado cambiado");
        }
        else
        {
            return BadRequest("No se pudo cambiar el estado");
        }

    }

    // PUT: api/cadeteria/cambiarcadete/1/3
    [HttpPut("cambiarcadete/{idPedido}/{idNuevoCadete}")]
    public ActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        var (exito, mensaje) = cadeteria.ReasignarPedido(idPedido, idNuevoCadete);

        if (exito)
        {
            ADPedidos.Guardar(cadeteria.ObtenerPedidos());  // recupero lista actualizada y la mando a guardar en JSON
            return Ok(new { mensaje });
        }
        else
        {
            return BadRequest(new { mensaje });
        }
    }

}
