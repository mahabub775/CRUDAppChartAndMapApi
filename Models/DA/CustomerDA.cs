using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using WebApi_ADO_DotNet.Models;
using WebApi_ADO_DotNet.Models.BO;
using System.Data;
namespace WebApi_ADO_DotNet.Models.DA
{
    
    public class CustomerDA
    {
        public static SqlDataReader IUD(DBConnection Conn, Customer oCustomer,int nDBOperation)
        {
            SqlCommand oSqlCommand = new SqlCommand();
            oSqlCommand.CommandText = "[SP_IUD_Customer]";
            oSqlCommand.Connection = Conn.oConn;//connection Establish
            oSqlCommand.CommandType = CommandType.StoredProcedure;
            oSqlCommand.Parameters.Add(new SqlParameter("@CustomerID", oCustomer.CustomerID));
            oSqlCommand.Parameters.Add(new SqlParameter("@Name", oCustomer.Name));
            oSqlCommand.Parameters.Add(new SqlParameter("@Profession", oCustomer.Profession));
            oSqlCommand.Parameters.Add(new SqlParameter("@MonthlyIncome", oCustomer.MonthlyIncome));
            oSqlCommand.Parameters.Add(new SqlParameter("@EducatonLevel", oCustomer.EducatonLevel));
            oSqlCommand.Parameters.Add(new SqlParameter("@Section", oCustomer.Section));
            oSqlCommand.Parameters.Add(new SqlParameter("@Longitude", oCustomer.Longitude));
            oSqlCommand.Parameters.Add(new SqlParameter("@Latitude", oCustomer.Latitude));
            oSqlCommand.Parameters.Add(new SqlParameter("@DBOperation", nDBOperation));
            return oSqlCommand.ExecuteReader();
        }

        public static SqlDataReader Gets(DBConnection Conn)
        {
            SqlCommand oSqlCommand = new SqlCommand();
            oSqlCommand.CommandText = "SELECT * FROM Customer";
            oSqlCommand.Connection = Conn.oConn;//connection Establish
            oSqlCommand.CommandType = CommandType.Text;
            return oSqlCommand.ExecuteReader();
        }
        public static SqlDataReader Get(int CustomerID, DBConnection Conn)
        {
            SqlCommand oSqlCommand = new SqlCommand();
            oSqlCommand.CommandText = "SELECT * FROM [Customer] WHERE CustomerID = "+CustomerID;
            oSqlCommand.Connection = Conn.oConn;//connection Establish
            oSqlCommand.CommandType = CommandType.Text;
            return oSqlCommand.ExecuteReader();
        }
    }
}
