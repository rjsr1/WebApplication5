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

        public List<string> Pegar_Arquivos(int userId)
        {
            List<string> result = new List<string>();
            using (var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rodolfo.rocha\source\repos\WebApplication5\WebApplication5\App_Data\DadosTeste.mdf;Integrated Security=True"))
            {
                string _sql = @"SELECT [Name] FROM ( [dbo].[Arquivo] JOIN [dbo].[System_Users] ON [UserID]=[System_Users].[Id])  " +
                       @"WHERE [UserID] =userId";
            
            var cmd = new SqlCommand(_sql, conn);
            conn.Open();
            
            var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader[0].ToString());
                }
                conn.Dispose();
                cmd.Dispose();
            }
            
            return result;
        }
        
    }
}