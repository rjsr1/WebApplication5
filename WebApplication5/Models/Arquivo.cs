using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Arquivo
    {
        [Required]
        [Display(Name = "Arquivo_Nome")]
       public string Arquivo_Nome { get; set; }

        
        
    }
}