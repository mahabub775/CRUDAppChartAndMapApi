using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi_ADO_DotNet.Models.BO;
using WebApi_ADO_DotNet.Models.DA;
using System.Data.SqlClient;

namespace WebApi_ADO_DotNet.Models.Service
{
   public class CustomerService
   {
        DBConnection Conn = new DBConnection();
       #region Map Objects
       private Customer MapObject(SqlDataReader oReader)
       {
           Customer oCustomer = new Customer();
           oCustomer.CustomerID = Convert.ToInt32(oReader["CustomerID"]);
           oCustomer.Name = Convert.ToString(oReader["Name"]);
           oCustomer.Profession = Convert.ToString(oReader["Profession"]);
           oCustomer.MonthlyIncome = Convert.ToDouble(oReader["MonthlyIncome"]);
            oCustomer.EducatonLevel = Convert.ToInt16(oReader["EducatonLevel"]);
            oCustomer.Section = Convert.ToInt16(oReader["Section"]);
            oCustomer.Latitude = Convert.ToDouble(oReader["Latitude"]);
            oCustomer.Longitude = Convert.ToDouble(oReader["Longitude"]);
            return oCustomer;
       }
       private Customer CreateObject(SqlDataReader oReader)
       {
           Customer oCustomer =new Customer();
           oCustomer = MapObject(oReader);
           return oCustomer;
       }
       private List<Customer> CreateObjects(SqlDataReader oReader)
       {
           List<Customer> oCustomers = new List<Customer>();
           while(oReader.Read())
           {
               Customer oCustomer = new Customer();
               oCustomer = CreateObject(oReader);
               oCustomers.Add(oCustomer);
           }
           return oCustomers;
       }
       #endregion

       #region Functions
       public Customer Save(Customer oCustomer)
       {
           Conn.Open();
           SqlDataReader oReader = null;
           if (oCustomer.CustomerID <= 0)
           {
               oReader = CustomerDA.IUD(Conn, oCustomer, (int)DBOperation.Insert);
           }
           else
           {
               oReader = CustomerDA.IUD(Conn, oCustomer, (int)DBOperation.Update);
           }
           if (oReader.Read())
           {
               oCustomer = CreateObject(oReader);
           }
           Conn.Close();
           return oCustomer;
       }
        public string Delete(int id)
        {
            Customer oCustomer = new Customer();
            oCustomer.CustomerID = id;
            Conn.Open();
            SqlDataReader oReader = null;
             oReader = CustomerDA.IUD(Conn, oCustomer, (int)DBOperation.Delete);
           
            Conn.Close();
            return "Deleted";
        }
        public  List<Customer> Gets()
       {
           List<Customer> oCustomers = new List<Customer>();
           Conn.Open();
           SqlDataReader oReader = null;
           oReader = CustomerDA.Gets(Conn);
           oCustomers = CreateObjects(oReader);
           Conn.Close();
           return oCustomers;
       }
        public List<Customer> Gets(string sSQL)
        {
            List<Customer> oCustomers = new List<Customer>();
            Conn.Open();
            SqlDataReader oReader = null;
            oReader = CustomerDA.Gets(sSQL, Conn);
            oCustomers = CreateObjects(oReader);
            Conn.Close();
            return oCustomers;
        }
        public Customer Get(int CustomerID)
        {
            Customer oCustomer = new Customer();
            Conn.Open();
            SqlDataReader oReader = null;
            oReader = CustomerDA.Get(CustomerID, Conn);
            if (oReader.Read())
            {
                oCustomer = CreateObject(oReader);
            }
            Conn.Close();
            return oCustomer;
        }
        #endregion
    }
}
