using MediatR;
using Microsoft.AspNetCore.Mvc;
using Invoicing.Application.Accounts.Commands.CreateAccount;
using Invoicing.Application.Accounts.Queries.GetAccountsList;
using Invoicing.Domain.ValueObjects;

namespace Invoicing.API.Controllers;

/// <summary>
/// Controlador para la gestión de cuentas externas (integración con API Novasoft)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene todas las cuentas desde el API externo de Novasoft
    /// </summary>
    /// <returns>Lista de cuentas externas sincronizadas</returns>
    /// <response code="200">Cuentas obtenidas exitosamente</response>
    /// <response code="401">Token de autenticación inválido o expirado</response>
    /// <response code="502">Error de comunicación con el servicio externo</response>
    /// <response code="500">Error interno del servidor</response>
    /// <remarks>
    /// Este endpoint realiza automáticamente la autenticación con el servicio externo
    /// antes de obtener las cuentas.
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExternalAccount>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(object), StatusCodes.Status502BadGateway)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ExternalAccount>>> GetAccounts()
    {
        var query = new GetAccountsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Crea una nueva cuenta en el API externo de Novasoft
    /// </summary>
    /// <param name="command">Datos de la cuenta a crear</param>
    /// <returns>True si la cuenta se creó exitosamente</returns>
    /// <response code="200">Cuenta creada exitosamente en el servicio externo</response>
    /// <response code="400">Datos de la cuenta inválidos o cuenta ya existe</response>
    /// <response code="401">Token de autenticación inválido o expirado</response>
    /// <response code="502">Error de comunicación con el servicio externo</response>
    /// <response code="500">Error interno del servidor</response>
    /// <remarks>
    /// Ejemplo de request:
    /// 
    ///     POST /api/accounts
    ///     {
    ///       "code": "456",
    ///       "name": "Empresa XYZ S.A.S",
    ///       "identification": "900123456",
    ///       "email": "contacto@empresa.com",
    ///       "address": "Calle 100 #50-25",
    ///       "phone": "6012345678",
    ///       "lastName": "García",
    ///       "firstName": "Juan",
    ///       "externalClientCode": "EXT001",
    ///       "webPage": "https://empresa.com"
    ///     }
    /// 
    /// Campos con valores constantes (configurados automáticamente):
    /// - Ciudad: 11001 (Bogotá)
    /// - Departamento: 11 (Bogotá D.C.)
    /// - País: 057 (Colombia)
    /// - Tipo de cliente: 2
    /// - Tipo de identificación: 06
    /// - Tipo de persona: 2
    /// - Estado: 1 (Activo)
    /// - Fecha de registro: Fecha/hora actual (UTC)
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(object), StatusCodes.Status502BadGateway)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> CreateAccount([FromBody] CreateAccountCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
