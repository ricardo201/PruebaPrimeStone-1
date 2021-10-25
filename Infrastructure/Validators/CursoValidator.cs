using Common.Const.ErrorMessages;
using Common.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class CursoValidator : AbstractValidator<CursoDto>
    {
        public CursoValidator()
        {
            RuleFor(cursoDto => cursoDto.NombreCurso)
                .NotNull().WithMessage(CursoErrorMessages.NameCourseCannotBeNull)
                .NotEmpty().WithMessage(CursoErrorMessages.NameCourseCannotBeEmpty);
            RuleFor(cursoDto => cursoDto.FechaInicio)
                .NotNull().WithMessage(CursoErrorMessages.InitialDateCannotBeNull)
                .NotEmpty().WithMessage(CursoErrorMessages.InitialDateCannotBeEmpty)
                .GreaterThan(DateTime.Now.Date).WithMessage(CursoErrorMessages.InitialDateCannotLessThanToday);
            RuleFor(cursoDto => cursoDto.FechaFin)
                .NotNull().WithMessage(CursoErrorMessages.FinalDateCannotBeNull)
                .NotEmpty().WithMessage(CursoErrorMessages.FinalDateCannotBeEmpty)
                .LessThan(DateTime.Now.Date).WithMessage(CursoErrorMessages.FinalDateCannotLessThanToday);
            RuleFor(cursoDto => cursoDto).MustAsync(CompareDate).WithMessage(CursoErrorMessages.FinalDateCannotLessThanInitialDate);
        }

        private async Task<bool> CompareDate(CursoDto registroDto, CancellationToken cancellationToken)
        {
            return registroDto.FechaFin > registroDto.FechaInicio;
        }
    }
}
