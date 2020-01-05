using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WebApi_ADO_DotNet.Models
{
    public class DBConnection
    {
        public static string sConnectionString = "Data Source=DESKTOP-7I61NPR;User ID=sa;Password=12774; Initial Catalog=PracticeDB";
       public SqlConnection oConn = new SqlConnection(sConnectionString);
       public void Open()
       {
           
           oConn.Open();
       }
       public void Close()
       {
           oConn.Close();
       }
    }
    
}
