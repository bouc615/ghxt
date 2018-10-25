using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public class sqlHelper
    {
        private static string connString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        /// 执行增删改，返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNoQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery();

                }
            }
        }
      
        /// <summary>
        /// 返回一个数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string sql)
        {
            using (SqlConnection xonn = new SqlConnection(connString))
            {
                xonn.Open();
                using (SqlCommand cmd = xonn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    return dataset;
                }
            }
        }
   
        public static object ExecuteScalar(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteScalar();
                }
            }
        }
       
        /*************END***********************/

    }

}
