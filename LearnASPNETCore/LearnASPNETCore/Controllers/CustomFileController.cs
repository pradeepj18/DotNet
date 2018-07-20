using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnASPNETCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;

namespace LearnASPNETCore.Controllers
{
    public class CustomFileController : Controller
    {
      
        public IActionResult Display(String filename,string filecolumns)
        {
            try
            {

                 List<EmployeeModel> employeeList = new List<EmployeeModel>();
                try
                {
                    var filepath = @".\\Files\\"+filename;
                    FileInfo file = new FileInfo(filepath);
                    ExcelPackage excelPackage = new ExcelPackage(file);
                    StringBuilder stringBuilder = new StringBuilder();
                    ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[1];

                    int excelrows = excelWorksheet.Dimension.Rows;
                    int excelcolumn = excelWorksheet.Dimension.Columns;

                   
                    if (filecolumns == "all")
                    {
                    for (int i = 2; i <= excelrows; i++)
                    {
                        EmployeeModel employeeModel1 = new EmployeeModel();
                        employeeModel1.Empid = Convert.ToInt32(excelWorksheet.Cells[i, 1].Value);
                        employeeModel1.EmpFname = Convert.ToString(excelWorksheet.Cells[i, 2].Value);
                        employeeModel1.EmpLname = Convert.ToString(excelWorksheet.Cells[i, 3].Value);
                        employeeModel1.EmpEmail = Convert.ToString(excelWorksheet.Cells[i, 4].Value);
                        employeeModel1.EmpContactno = Convert.ToString(excelWorksheet.Cells[i, 5].Value);
                        employeeList.Add(employeeModel1);
                    }
                }
                else if (filecolumns == "EmpFname")
                {
                        for (int i = 2; i <= excelrows; i++)
                        {
                            EmployeeModel employeeModel1 = new EmployeeModel();
                            employeeModel1.EmpFname = Convert.ToString(excelWorksheet.Cells[i, 2].Value);
                            employeeList.Add(employeeModel1);
                        }
                    }
                else if (filecolumns == "EmpLname")
                {
                        for (int i = 2; i <= excelrows; i++)
                        {
                            EmployeeModel employeeModel1 = new EmployeeModel();
                            employeeModel1.EmpLname = Convert.ToString(excelWorksheet.Cells[i, 3].Value);
                            employeeList.Add(employeeModel1);
                        }
                    }
                else if (filecolumns == "EmpEmail")
                {
                        for (int i = 2; i <= excelrows; i++)
                        {
                            EmployeeModel employeeModel1 = new EmployeeModel();
                            employeeModel1.EmpEmail = Convert.ToString(excelWorksheet.Cells[i, 4].Value);
                            employeeList.Add(employeeModel1);
                        }
                    }
                else if (filecolumns == "EmpContactno")
                {
                        for (int i = 2; i <= excelrows; i++)
                        {
                            EmployeeModel employeeModel1 = new EmployeeModel();
                            employeeModel1.EmpContactno = Convert.ToString(excelWorksheet.Cells[i, 5].Value);
                            employeeList.Add(employeeModel1);
                        }
                    }
                }
                catch (FileNotFoundException fe)
                {
                    return Content("Error File Not Found - " + fe.Message);
                }

                return View(employeeList);
            }
            catch (Exception e)
            {
                return Content("Error in CustomFileController => index - " + e.Message);
            }
        }

        public IActionResult JsonDisplay(string filename ,string fileheader)
        {
            List<EmployeeModel> employeeList = new List<EmployeeModel>();
            try
            {
                string rawjson = System.IO.File.ReadAllText(@".\\Files\\" + filename);
                var jsonobj = (JsonConvert.DeserializeObject<IEnumerable<EmployeeModel>>(rawjson));

                
                return View(jsonobj);
            }
            catch (Exception e)
            {
                return Content("Error in CustomFileController => JsonDisplay - " + e.Message);
            }
            
        }
        public IActionResult Index()
        {
            try
            {
                IFormFile formfile = Request.Form.Files[0];
                if (formfile == null || formfile.Length == 0)
                {
                    return Content("File Not found");
                }
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Files", formfile.FileName);
                var stream = new FileStream(path, FileMode.Create);
                formfile.CopyToAsync(stream);
                stream.Close();
                string ext = Path.GetExtension(path);
                if (ext.Equals(".json"))
                    return RedirectToAction("JsonDisplay", new { filename = formfile.FileName });
                else if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                    return RedirectToAction("Display", new { filename = formfile.FileName, filecolumns= "all" });
                else
                {
                   /* FileInfo fileinfo = new FileInfo(path);
                    if (fileinfo.Exists)
                    {
                        fileinfo.Delete();
                    }*/
                    
                    return Content("Invalid File");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in CustomFileController => index - " + e.Message);

                return View();
            }
        }
    }
}