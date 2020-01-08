using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ExcelDataReader;
using System.Data;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_ADO_DotNet.Models.BO;

namespace WebApi_ADO_DotNet.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : Controller
    {

        #region Declaration
        Customer _oCustomer = new Customer();
        List<Customer> _oCustomers = new List<Customer>();
        #endregion


        //[Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Gets()
        {
            List<Customer> _oCustomers = new List<Customer>();
            Customer oCustomer = new Customer();
            _oCustomers = Customer.Gets();
            return _oCustomers.ToList();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            _oCustomer = new Customer();
            _oCustomer = Customer.Get(id);
            return _oCustomer;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Save(Customer oCustomer)
        {
            _oCustomer = new Customer();
            _oCustomer = oCustomer.Save(oCustomer);
            return _oCustomer;
        }

        [HttpDelete("{id}")]
        public  ActionResult<Customer> Delete(int id)
        {
            
            _oCustomer = new Customer();
            _oCustomer.Message = _oCustomer.Delete(id);
            return _oCustomer;
        }


        [HttpGet]
        public async Task<ActionResult<Customer>> GetCustomerIncomeInfo()
        {
            _oCustomer = new Customer();
            _oCustomer.SectionWiseInComeList = Customer.Gets("SELECT 0 AS CustomerID, '' AS Name, '' AS Profession, 0 As Latitude, 0 As Longitude,  0 As EducatonLevel, Section,  SUM(MonthlyIncome) As MonthlyIncome FROM Customer  Group by Section  Order by Section");
            _oCustomer.EducationLavelWiseInComeList = Customer.Gets("SELECT 0 AS CustomerID, '' AS Name, '' AS Profession, 0 As Latitude, 0 As Longitude, 0 As Section, EducatonLevel,  SUM(MonthlyIncome) As MonthlyIncome FROM Customer A Group by EducatonLevel  Order by EducatonLevel");
            return _oCustomer;
        }

        #region Excel Upload
        [HttpPost, DisableRequestSizeLimit]
        public ActionResult ExcelUpload()
        {
            var sMessage = "Success";
            var file = Request.Form.Files[0];
              if (Request.Form.Files.Count > 0)
                {


                var folderName = Path.Combine("Resources", "TempFile");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    IExcelDataReader reader = null;

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);

                        if (file.FileName.EndsWith(".xls"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else if (file.FileName.EndsWith(".xlsx"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }

                        DataSet excelRecords = reader.AsDataSet();
                        reader.Close();

                        var finalRecords = excelRecords.Tables[0];
                        for (int i = 0; i < finalRecords.Rows.Count; i++)
                        {
                            Customer oCustomer = new Customer();
                            oCustomer.Name = finalRecords.Rows[i][0].ToString();
                            oCustomer.Profession = finalRecords.Rows[i][1].ToString();
                            oCustomer.MonthlyIncome = Convert.ToDouble(finalRecords.Rows[i][2].ToString());
                            oCustomer.EducatonLevel = EducationLavel(finalRecords.Rows[i][3].ToString());
                            EnumSection myStatus;
                            Enum.TryParse(finalRecords.Rows[i][4].ToString(), out myStatus); //Convert string to  enum 
                            oCustomer.Section = Convert.ToInt16(myStatus);//Convert enum to int
                            oCustomer.Latitude = Convert.ToDouble(finalRecords.Rows[i][5]);
                            oCustomer.Longitude = Convert.ToDouble(finalRecords.Rows[i][6]);
                            _oCustomer = oCustomer.Save(oCustomer);
                        }


                    }
                    return Ok(new { sMessage });
                }
                else
                {
                    return BadRequest();
                }
            }
            return Ok(new { sMessage });
        }

        public int EducationLavel(string sEducationLavel)
        {

            if (sEducationLavel == "illiterate") { return 1; }
            else if (sEducationLavel == "Under SSC") { return 2; }
            else if (sEducationLavel == "HSC") { return 3; }
            else if (sEducationLavel == "BA") { return 4; }
            else if (sEducationLavel == "MA") { return 5; }
            else if (sEducationLavel == "MA+") { return 6; }
            else { return 0; }
        }
        #endregion

    }
}