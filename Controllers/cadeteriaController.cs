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

    // GET: api/cadeteria/getPedidos
    [HttpGet("getPedidos")]
    public ActionResult<List<Pedido>> GetPedidos()
    {
        var pedidos = Cadeteria.ObtenerPedidos();
        if (pedidos == null || pedidos.Count == 0)
        {
            return NotFound(new { mensaje = "No hay pedidos registrados" });
        }
        return Ok(pedidos);
    }
        // GET: api/cadeteria/getPedidos/1
    [HttpGet("getPedidos/{nroPedido}")]
    public ActionResult<List<Pedido>> GetPedidoPorNro(int nroPedido)
    {
        Pedido pedido = Cadeteria.BuscarPedidoPorId(nroPedido);
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
        var cadetes = Cadeteria.ObtenerCadetes();
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
        Cadete cadete = Cadeteria.BuscarCadetePorId(idCadete);
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
        string[] informeDeCadetes = Cadeteria.Informe();
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
        Pedido nuevoPedido = Cadeteria.CrearPedido(pedido);

        if (nuevoPedido == null)
        {
            return BadRequest(new { mensaje = "Los datos del pedido están incompletos o son inválidos." });
        }

        Cadeteria.Pedidos.Add(nuevoPedido);
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
        var (Nombre, Telefono, Direccion) = cadete;
        Cadete nuevoCadete = Cadete.CrearCadete(Nombre,Direccion, Telefono );
        Cadeteria.IncorporarCadete(nuevoCadete);

        return Ok(new { nuevoCadete });        
    }

    // PUT: api/cadeteria/asignar/1/2
    [HttpPut("asignar/{idCadete}/{idPedido}")]
    public ActionResult AsignarPedido(int idCadete, int idPedido)
    {
        var (exito, mensaje) = Cadeteria.AsignarCadeteAPedido(idCadete, idPedido);

        return exito ? Ok(new { mensaje }) : BadRequest(new { mensaje });
    }

    // PUT: api/cadeteria/estado/1/2
    [HttpPut("estado/{idPedido}/{nuevoEstado}")]
    public ActionResult CambiarEstadoPedido(int idPedido, int nuevoEstado)
    {
        bool exito = Cadeteria.CambiarEstadoDePedido(idPedido, nuevoEstado);
        return exito ? Ok("Estado cambiado") : BadRequest("No se pudo cambiar el estado");
    }

    // PUT: api/cadeteria/cambiarcadete/1/3
    [HttpPut("cambiarcadete/{idPedido}/{idNuevoCadete}")]
    public ActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        var (exito, mensaje) = Cadeteria.ReasignarPedido(idPedido, idNuevoCadete);

        return exito ? Ok(new {mensaje}) : BadRequest(new {mensaje});
    }

}
