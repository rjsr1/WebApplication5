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

            return false;//
        }


        public List<string> Pegar_Arquivos(string UserName)
        {
            List<string> result = new List<string>();
            int UserID=-1;
            using (var conn1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rodolfo.rocha\source\repos\WebApplication5\WebApplication5\App_Data\DadosTeste.mdf;Integrated Security=True"))
            {
                conn1.Open();
                string _sql1 = @"SELECT [dbo].[System_Users].[Id] FROM [dbo].[System_Users] WHERE [dbo].[System_Users].[Username]=@u";
                var cmd1 = new SqlCommand(_sql1, conn1);
                cmd1.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = UserName;
                var reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    UserID = reader1.GetInt32(0);
                }
                cmd1.Dispose();
                conn1.Dispose();
                reader1.Close();
            }
            using (var conn2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rodolfo.rocha\source\repos\WebApplication5\WebApplication5\App_Data\DadosTeste.mdf;Integrated Security=True"))
            {
                conn2.Open();
                string _sql = @"SELECT [dbo].[Arquivo].[Name] FROM ( [dbo].[Arquivo] JOIN [dbo].[System_Users] ON [dbo].[Arquivo].[UserID]=[dbo].[System_Users].[Id])" +
                       @"WHERE [UserID] =@uid";

                var cmd = new SqlCommand(_sql, conn2);
                cmd.Parameters
                    .Add(new SqlParameter("@uid", SqlDbType.NVarChar))
                    .Value = UserID;

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int i = 0;
                    result.Add(reader.GetString(i));
                    i++;
                }
                reader.Close();
                conn2.Dispose();
                cmd.Dispose();



                return result;
            }

        }

    }
}
