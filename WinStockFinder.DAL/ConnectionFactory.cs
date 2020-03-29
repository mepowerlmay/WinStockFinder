using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
namespace WinStockFinder.DAL
{

 internal class ConnectionFactory
    {

        public static DbConnection GetOpenConnection()
        {
            string connstr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString; 
            //string connStr = @"server=192.168.1.200;uid=sa;pwd=321456852;database=DBstock;";
			var connection = new  System.Data.SqlClient.SqlConnection(connstr); 
            connection.Open(); 
            return connection;

        }

    }


   
}
