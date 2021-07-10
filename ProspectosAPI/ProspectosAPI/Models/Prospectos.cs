using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProspectosAPI.Models
{
    public class Prospectos
    {
        [Key]
        public int IdProspecto { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string Nombre { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ApellidoPaterno { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ApellidoMaterno { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Calle { get; set; }

        [Column(TypeName = "nvarchar(4)")]
        public string NumeroCalle { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Colonia { get; set; }

        [Column(TypeName = "varchar(5)")]
        public string CP { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Telefono { get; set; }

        [Column(TypeName = "varchar(13)")]
        public string Rfc { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Estatus { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Observaciones { get; set; }
    }
}
