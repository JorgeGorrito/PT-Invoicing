using Invoicing.Application.Invoices.Commands.CreateInvoice;
using Invoicing.Application.Invoices.Queries.GetInvoicesList;
using Invoicing.Application.Invoices.Queries.GetInvoiceById;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing.Api.Controllers;

/// <summary>
/// Controlador para la gestión de facturas
/// </summary>
public class InvoicesController : BaseApiController
{
    /// <summary>
    /// Crea una nueva factura aplicando reglas de precios configuradas (IVA, descuentos)
    /// </summary>
    /// <param name="command">Datos de la factura a crear</param>
    /// <returns>ID de la factura creada</returns>
    /// <response code="200">Factura creada exitosamente</response>
    /// <response code="400">Datos de la factura inválidos (error de validación o regla de negocio)</response>
    /// <response code="500">Error interno del servidor</response>
    /// <remarks>
    /// Ejemplo de request:
    /// 
    ///     POST /api/invoices
    ///     {
    ///       "docNumber": "1006875365",
    ///       "firstName": "Jorge",
    ///       "lastName": "Abella",
    ///       "address": "Calle 123 #45-67",
    ///       "phone": "3001234567",
    ///       "items": [
    ///         {
    ///           "articleCode": 101,
    ///           "quantity": 2,
    ///           "unitPrice": 2500
    ///         }
    ///       ]
    ///     }
    /// 
    /// Reglas aplicadas:
    /// - IVA: 19%
    /// - Descuento: 5% si el valor bruto supera $500,000
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> Create(CreateInvoiceCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    /// <summary>
    /// Obtiene todas las facturas registradas en el sistema
    /// </summary>
    /// <returns>Lista de facturas con información del cliente y totales calculados</returns>
    /// <response code="200">Lista de facturas obtenida exitosamente</response>
    /// <response code="500">Error interno del servidor</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InvoiceDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetAll()
    {
        return Ok(await Mediator.Send(new GetInvoicesListQuery()));
    }

    /// <summary>
    /// Obtiene el detalle completo de una factura específica por su ID
    /// </summary>
    /// <param name="id">ID de la factura a consultar</param>
    /// <returns>Detalle completo de la factura incluyendo cliente e items</returns>
    /// <response code="200">Factura encontrada exitosamente</response>
    /// <response code="404">Factura no encontrada</response>
    /// <response code="500">Error interno del servidor</response>
    /// <remarks>
    /// Retorna información detallada de la factura incluyendo:
    /// - Información completa del cliente
    /// - Lista de todos los artículos con sus cantidades y precios
    /// - Totales desglosados (bruto, descuento, IVA, total)
    /// </remarks>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(InvoiceDetailDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<InvoiceDetailDTO>> GetById(int id)
    {
        var invoice = await Mediator.Send(new GetInvoiceByIdQuery(id));

        if (invoice is null)
            return NotFound(new { error = $"Factura con ID {id} no encontrada." });

        return Ok(invoice);
    }
}