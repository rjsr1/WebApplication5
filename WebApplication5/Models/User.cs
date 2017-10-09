using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

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



        public bool IsValid(string username, string password)
        {
            using (var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rodolfo.rocha\source\repos\WebApplication5\WebApplication5\App_Data\DadosTeste.mdf;Integrated Security=True"))
            {
                string _sql = @"SELECT [Username] FROM [dbo].[System_Users] " +
                       @"WHERE [Username] = @u AND [Password] = @p";
                var cmd = new SqlCommand(_sql, conn);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = username;
                cmd.Parameters
                    .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                    .Value = password;
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(0) == username)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    reader.Dispose();
                    cmd.Dispose();

                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return false;

                }
            }

            return false;
        }


        public List<string> Pegar_Arquivos()
        {
            List<string> result = new List<string>();
            string pathValue = ConfigurationManager.AppSettings["Path"];
            DirectoryInfo directory = new DirectoryInfo(pathValue);
            foreach (FileInfo fInfo in directory.GetFiles())
            {
                result.Add(fInfo.Name);
            }
            return result;
           
        }

    }
}
