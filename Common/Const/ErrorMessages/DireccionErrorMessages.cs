using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Const.ErrorMessages
{
    public class DireccionErrorMessages
    {
        public const string AddressDoesNotExist = "Address does not exist";
        public const string AddressUpdateNotAllowed = "Address update not allowed";
        public const string AddressDeleteNotAllowed = "Address delete not allowed";
        public const string AddressCannotBeNull = "Address cannot be null";
        public const string AddressCannotBeEmpty ="Address cannot be empty";
        public const string AddressTypeCannotBeNull = "Address type cannot be null";
        public const string AddressTypeCannotBeLessThanZero = "Address type cannot be less than zero";
    }
}
