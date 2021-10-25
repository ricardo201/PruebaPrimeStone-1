using Common.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.DTOs
{
    public class DireccionDto : Dto
    {
        public string Direccion { get; set; }
        public TipoDireccion TipoDireccion { get; set; }
    }
}
