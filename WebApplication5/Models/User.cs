using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class User
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }



        public bool IsValid(string _username, string _password)
        {
            using (var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rodolfo.rocha\source\repos\WebApplication5\WebApplication5\App_Data\DadosTeste.mdf;Integrated Security=True"))
            {
                string _sql = @"SELECT [Username] FROM [dbo].[System_Users] " +
                       @"WHERE [Username] = @u AND [Password] = @p";
                var cmd = new SqlCommand(_sql, conn);
                cmd.Parameters
                   .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                   .Value = _username;
                cmd.Parameters
                    .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                    .Value = HELPERS.SHA1.Encode(_password);
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return true;
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return false;

                }
            }


        }
        public List<string> Pegar_Arquivos(string UserName)
        {
            List<string> result = new List<string>();
            using (var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rodolfo.rocha\source\repos\WebApplication5\WebApplication5\App_Data\DadosTeste.mdf;Integrated Security=True"))
            {
                conn.Open();
                string _sql2 = @"SELECT [Id] FROM [dbo].[System_Users] WHERE [Username]=UserName";
                var cmd2 = new SqlCommand(_sql2, conn);
                var reader2 = cmd2.ExecuteReader();
                int UserId = (int)reader2["Id"];

                conn.Dispose();
                string _sql = @"SELECT [Name] FROM ( [dbo].[Arquivo] JOIN [dbo].[System_Users] ON [UserID]=[System_Users].[Id])" +
                       @"WHERE [UserID] =userId";

                var cmd = new SqlCommand(_sql, conn);
                

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader[0].ToString());
                }
                conn.Dispose();
                cmd.Dispose();
                cmd2.Dispose();
            }

            return result;
        }

    }

   }
