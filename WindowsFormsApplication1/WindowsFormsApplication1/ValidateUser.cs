using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
   public static class ValidateUser
    {
        private static string connString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        //验证登录用户
        public static string validateUser(string name, string psw)
        {


            string sql = "select * from login where account=@name";

            using (SqlConnection sqlCnn = new SqlConnection(connString))
            {

                sqlCnn.Open();
                SqlCommand sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlParameter para = new SqlParameter("@name", SqlDbType.VarChar);
                para.Value = name;
                sqlCmd.Parameters.Add(para);
                SqlDataReader sqlDr = sqlCmd.ExecuteReader();
                string result = null;
                if (sqlDr.Read())
                {
                    if (sqlDr[1].ToString() == psw)
                    {
                        result = "密码正确";
                    }

                    else
                    {
                        result = "密码错误";
                    }

                }
                else
                {
                    result = "用户" + name + "不存在";
                }
                sqlDr.Close();
                return result;
            }
        }
    }
}
