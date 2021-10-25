using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class RegistroDto : UsuarioDto
    {
        public string PasswordComprobacion { get; set; }
    }
}
