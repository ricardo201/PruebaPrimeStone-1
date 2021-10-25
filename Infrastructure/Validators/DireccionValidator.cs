using Common.Const.ErrorMessages;
using Common.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class DireccionValidator : AbstractValidator<DireccionDto>
    {
        public DireccionValidator()
        {
            RuleFor(direccionDto => direccionDto.Direccion)
                .NotNull().WithMessage(DireccionErrorMessages.AddressCannotBeNull)
                .NotEmpty().WithMessage(DireccionErrorMessages.AddressCannotBeEmpty);

            RuleFor(direccionDto => (int)direccionDto.TipoDireccion)
                .NotNull().WithMessage(DireccionErrorMessages.AddressTypeCannotBeNull)
                .GreaterThanOrEqualTo(0).WithMessage(DireccionErrorMessages.AddressTypeCannotBeLessThanZero);
        }
    }
}
