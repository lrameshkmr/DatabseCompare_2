using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataObjects
{
    public static class DbContextExtensions
    {

        public static DataSet ExecuteStoredProcedure(string spName,Dictionary<string,string> parameters)
        {
            SqlDataContext context = new SqlDataContext();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = context.conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spName;

            SqlParameter param;
            foreach (var item in parameters)
            {
                param = new SqlParameter("@"+item.Key, item.Value);
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);
            }

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sqlDataAdapter.Fill(ds);

            return ds;
        }
    }
}