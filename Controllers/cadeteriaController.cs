using Microsoft.AspNetCore.Mvc;

using CadeteriaClass;
using PedidoClass;
using CadeteClass;
using AccesoADatosCSVClass;
using tl2_tp4_2025_ColmanNicolas.Dtos;

namespace tl2_tp4_2025_ColmanNicolas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CadeteriaController : ControllerBase
{
    private readonly AccesoADatosCSV accesoCSV = new AccesoADatosCSV();
    private readonly AccesoADatosCSV accesoJSON = new AccesoADatosCSV();
    private readonly Cadeteria _cadeteria;

    public CadeteriaController()
    {
        _cadeteria = accesoCSV.AccesoADatosCadeteria();   //recupero cadeteria
        _cadeteria.IncorporarListadoDeCadetes(accesoJSON.AccesoADatosCadetes());  // recupero cadetes

    }

    // GET: api/cadeteria/getPedidos
    [HttpGet("getPedidos")]
    public ActionResult<List<Pedido>> GetPedidos()
    {
        var pedidos = _cadeteria.ObtenerPedidos();
        if (pedidos == null || pedidos.Count == 0)
        {
            return NotFound(new { mensaje = "No hay pedidos registrados" });
        }
        return Ok(pedidos);
    }

    // GET: api/cadeteria/getCadetes
    [HttpGet("getCadetes")]
    public ActionResult<List<Cadete>> GetCadetes()
    {
        var cadetes = _cadeteria.ObtenerCadetes();
        if (cadetes == null || cadetes.Count == 0)
        {
            return NotFound(new { mensaje = "No hay cadetes registrados" });
        }
        return Ok(cadetes);
    }

    // GET: api/cadeteria/getInforme
    [HttpGet("getInforme")]
    public ActionResult<string[]> GetInforme()
    {
        string[] informeDeCadetes = _cadeteria.Informe();
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
        Pedido nuevoPedido = _cadeteria.CrearPedido(pedido);

        if (nuevoPedido == null)
        {
            return BadRequest(new { mensaje = "Los datos del pedido están incompletos o son inválidos." });
        }

        return Ok(new
        {
            mensaje = "Pedido agregado correctamente",
            pedido = nuevoPedido
        });
    }

    // PUT: api/cadeteria/asignar/1/2
    [HttpPut("asignar/{idCadete}/{idPedido}")]
    public ActionResult AsignarPedido(int idCadete, int idPedido)
    {
        var (exito, mensaje) = _cadeteria.AsignarCadeteAPedido( idCadete, idPedido);

        return exito ? Ok(new { mensaje }) : BadRequest(new { mensaje });
    }

    // PUT: api/cadeteria/estado/1/2
    [HttpPut("estado/{idPedido}/{nuevoEstado}")]
    public ActionResult CambiarEstadoPedido(int idPedido, int nuevoEstado)
    {
        bool exito = _cadeteria.CambiarEstadoPedido(idPedido, nuevoEstado);
        return exito ? Ok("Estado cambiado") : BadRequest("No se pudo cambiar el estado");
    }

    /*
            // PUT: api/cadeteria/cambiarcadete/1/3
            [HttpPut("cambiarcadete/{idPedido}/{idNuevoCadete}")]
            public ActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
            {
                bool exito = _cadeteria.CambiarCadetePedido(idPedido, idNuevoCadete);
                return exito ? Ok("Cadete cambiado") : BadRequest("No se pudo cambiar el cadete");
            }
            */
}
