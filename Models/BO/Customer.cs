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
            EducatonLevel = 0;
            Section = 0;
            Latitude = 0;
            Longitude = 0;
            Message = "";
            SectionWiseInComeList = new List<Customer>();
            EducationLavelWiseInComeList = new List<Customer>();

        }
        #region Variables
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public double MonthlyIncome { get; set; }
        public int EducatonLevel { get; set; }
        public int Section { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Message { get; set; }
        #endregion
        #region Derived Properties
        public List<Customer> SectionWiseInComeList { get; set; }
        public List<Customer> EducationLavelWiseInComeList { get; set; }
        public string SectionSt{
            get
            {
                //{id:"Dhaka",value:'Dhaka'},{id:"Chittagong",value:'Chittagong'},{id:"Khulna",value:'Khulna'},{ id: "Barishal",value: 'Barishal'},{ id: "Rajshahi",value: 'Rajshahi'},{ id: "Sylhet",value: 'Sylhet'}
                return ((EnumSection)this.Section).ToString();
                //if (this.Section == 1) { return "Dhaka"; }
                //else if (this.Section == 2) { return "Chittagong"; }
                //else if (this.Section == 3) { return "Khulna"; }
                //else if (this.Section == 4) { return "Barishal"; }
                //else if (this.Section == 5) { return "Rajshahi"; }
                //else if (this.Section == 6) { return "Sylhet"; }
                //else { return ""; }
            }
        }
        public string EducatonLevelSt
        {
            get
            {
                //{id:"illiterate",value:'illiterate'},{id:"Under_SSC",value:'Under SSC'},{id:"HSC",value:'HSC'},{ id: "BA",value: 'BA'},{ id: "MA",value: 'MA'},{ id: "MA+",value: 'MA+'}
                if (this.EducatonLevel == 1) { return "illiterate"; }
                else if (this.EducatonLevel == 2) { return "Under SSC"; }
                else if (this.EducatonLevel == 3) { return "HSC"; }
                else if (this.EducatonLevel == 4) { return "BA"; }
                else if (this.EducatonLevel == 5) { return "MA"; }
                else if (this.EducatonLevel == 6) { return "MA+"; }
                else { return ""; }
            }
        }
        #endregion

        #region Functions
        public static List<Customer> Gets()
        {
            CustomerService oCustomerService = new CustomerService();
            return oCustomerService.Gets();
        }
        public static List<Customer> Gets(string sSQL)
        {
            CustomerService oCustomerService = new CustomerService();
            return oCustomerService.Gets(sSQL);
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

    enum EnumSection
    {
        None = 0,
        Dhaka = 1,
        Chittagong = 2,
        Khulna = 3,
        Barishal = 4,
        Rajshahi = 5,
        Sylhet = 6
    }
}
