using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Const.ErrorMessages
{
    public class RegistroErrorMessages
    {
        public const string PasswordCanNotNull = "Password cannot be null";
        public const string PasswordUppercaseLetter = "Password does not contain uppercase letters";
        public const string PasswordLowercaseLetter = "Password does not contain lowercase letters";
        public const string PasswordDigit = "Password does not contain digits";
        public const string PasswordSpecialCharacter = "Password does not contain special character";
        public const string PasswordCanNotLessThan10 = "Password length cannot be less than 10";
        public const string PasswordCanNotGreaterThan50 = "Password length cannot be greater than 50";
        public const string PasswordDoesNotMatch = "Password does not match";
        public const string UserNameExist = "Username exists in application";
        public const string UserNameCanNotNull = "Username cannot be null";
        public const string UserNameCanNotLessThan10 = "UserName length cannot be less than 10";
        public const string UserNameCanNotGreaterThan50 = "UserName length cannot be greater than 50";
    }
}
