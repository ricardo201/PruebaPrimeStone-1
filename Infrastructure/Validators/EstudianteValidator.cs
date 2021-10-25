using Common.DTOs;
using Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class EstudianteValidator : AbstractValidator<EstudianteDto>
    {
        private readonly IEstudianteService _estudianteService;

        public EstudianteValidator(IEstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
        }
    }
}
