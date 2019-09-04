using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataObjects
{
    public class SqlDataContext
    {
        public SqlConnection conn;
        public SqlDataContext()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCompare"].ToString());
        }
    }
}
