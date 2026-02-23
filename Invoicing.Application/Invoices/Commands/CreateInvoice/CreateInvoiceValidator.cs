using FluentValidation;

namespace Invoicing.Application.Invoices.Commands.CreateInvoice;

public class CreateInvoiceValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceValidator()
    {
        // Validaciones del Cliente
        RuleFor(x => x.DocNumber)
            .NotEmpty().WithMessage("El número de documento es requerido.")
            .MaximumLength(15).WithMessage("El documento no puede exceder los 15 caracteres.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MaximumLength(100).WithMessage("El nombre es demasiado largo.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("El apellido es requerido.");

        // Validaciones de la lista de ítems
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("La factura debe tener al menos un artículo.");

        // Validaciones dentro de cada ítem
        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ArticleCode)
                .GreaterThan(0).WithMessage("El código de artículo debe ser válido.");

            item.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");

            item.RuleFor(i => i.UnitPrice)
                .GreaterThan(0).WithMessage("El precio unitario debe ser mayor a cero.");
        });
    }
}