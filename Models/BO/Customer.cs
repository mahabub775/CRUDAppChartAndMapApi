using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi_ADO_DotNet.Models.Service;

namespace WebApi_ADO_DotNet.Models.BO
{
    public class Customer
    {
       public Customer()
        {
            CustomerID = 0;//PK
            Name = "";
            Profession = "";
            MonthlyIncome = 0;
            EducatonLevel = "";
            Section = "";
            Latitude = 0;
            Longitude = 0;
            Message = "";

        }
        #region Variables
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public double MonthlyIncome { get; set; }
        public string EducatonLevel { get; set; }
        public string Section { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Message { get; set; }
        #endregion
        #region Derived Properties

        #endregion

        #region Functions
        public static List<Customer> Gets()
        {
            CustomerService oCustomerService = new CustomerService();
            return oCustomerService.Gets();
        }
        public Customer Save(Customer oCustomer)
        {
            CustomerService oCustomerService = new CustomerService();
            return oCustomerService.Save(oCustomer);
        }
        public static Customer Get(int CustomerID)
        {
            CustomerService oCustomerService = new CustomerService();
            return oCustomerService.Get(CustomerID);
        }
        public string Delete(int ID)
        {
            CustomerService oCustomerService = new CustomerService();
            return oCustomerService.Delete(ID);
            
        }
        #endregion
    }
    enum DBOperation
    {
        None = 0,
        Insert = 1,
        Update = 2,
        Delete = 3
    }
}
