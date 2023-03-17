using System.ComponentModel.DataAnnotations;
using System;

namespace prueba_paula_rondon.Models
{
    public class PacienteModel
    {
        public int idPaciente { get; set; }

        [Required(ErrorMessage = "El campo número de documento es obligatorio")]
        public string numeroDocumento { get; set; }

        [Required(ErrorMessage = "El campo nombres es obligatorio")]
        public string nombres { get; set; }

        [Required(ErrorMessage = "El campo apellidos es obligatorio")]
        public string apellidos { get; set; }

        [Required(ErrorMessage = "El campo correo electronico es obligatorio")]
        public string correoElectronico { get; set; }

        [Required(ErrorMessage = "El campo telefono es obligatorio")]
        public string telefono { get; set; }

        [Required(ErrorMessage = "El campo fecha de nacimiento es obligatorio")]
        public DateTime fechaNacimiento { get; set; }

        [Required(ErrorMessage = "El campo estado afiliacion es obligatorio")]
        public bool estadoAfiliacion { get; set; }

        [Required(ErrorMessage = "El campo tipo de documento es obligatorio")]
        public int idTipoDocumento { get; set; }
    }


    public class TipoDocumentoModel
    {
        public int idTipoDocumento { get; set; }
        public string nombreDocumento { get; set; }
    }

    public class Rta
    {
        public object data { get; set; }
        public bool ok { get; set; }
    }
}
