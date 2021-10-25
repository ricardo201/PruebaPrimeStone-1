using Common.Const.ErrorMessages;
using Common.DTOs;
using Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class RegistroValidator : AbstractValidator<RegistroDto>
    {
        private readonly IUsuarioService _userService;
        public RegistroValidator(IUsuarioService userService)
        {
            _userService = userService;

            RuleFor(registroDto => registroDto.Password)
                .NotNull().WithMessage(RegistroErrorMessages.PasswordCanNotNull)
                .MinimumLength(10).WithMessage(RegistroErrorMessages.PasswordCanNotLessThan10)
                .MaximumLength(50).WithMessage(RegistroErrorMessages.PasswordCanNotGreaterThan50)
                .Matches("[A-Z]").WithMessage(RegistroErrorMessages.PasswordUppercaseLetter)
                .Matches("[a-z]").WithMessage(RegistroErrorMessages.PasswordLowercaseLetter)
                .Matches("[0-9]").WithMessage(RegistroErrorMessages.PasswordDigit)
                .Matches("[^a-zA-Z0-9]").WithMessage(RegistroErrorMessages.PasswordSpecialCharacter);
            RuleFor(registroDto => registroDto).MustAsync(ComparePassword).WithMessage(RegistroErrorMessages.PasswordDoesNotMatch);
            RuleFor(registroDto => registroDto.UserName)
                .NotNull().WithMessage(RegistroErrorMessages.UserNameCanNotNull)                
                .MinimumLength(10).WithMessage(RegistroErrorMessages.UserNameCanNotLessThan10)
                .MaximumLength(50).WithMessage(RegistroErrorMessages.UserNameCanNotGreaterThan50)
                .MustAsync(UserNotExistValidation).WithMessage(RegistroErrorMessages.UserNameExist); ;
        }

        private async Task<bool> UserNotExistValidation(string userName, CancellationToken cancellationToken)
        {
            var exist = await _userService.UserNameNoExiste(userName);
            return exist;
        }

        private async Task<bool> ComparePassword(RegistroDto registroDto, CancellationToken cancellationToken)
        {
            return registroDto.Password == registroDto.PasswordComprobacion;
        }
    }
}