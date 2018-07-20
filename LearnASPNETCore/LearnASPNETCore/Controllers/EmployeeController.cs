using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LearnASPNETCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LearnASPNETCore.Controllers
{
    public class EmployeeController : Controller
    {
//        private readonly IConfiguration configuration;
        private readonly string connectionstring;
        private readonly SqlConnection connection;
        public EmployeeController(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            connection = new SqlConnection(connectionstring);
        }
        [HttpGet]
        public ActionResult Index()
        {
            DataTable EmpdataTable = new DataTable();
            try
            {
                connection.Open();
                SqlDataAdapter sqldata = new SqlDataAdapter("select * from Employee", connection);
                sqldata.Fill(EmpdataTable);
                connection.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error in EmployeeController ==> Index - "+e.Message);    
            }
            return View(EmpdataTable);
        }

       

        // GET: Employee/Create
        public ActionResult Create()
        {
            try
            {
                return View(new EmployeeModel());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in EmployeeController ==> create - " + e.Message);
                return View(e.Message);
            }
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeModel employeeModel)
        {
            try
            {
                connection.Open();
                string query = "Insert into Employee values(@fname,@lname,@empemail,@empcontact)";
                SqlCommand sqlcmd = new SqlCommand(query, connection);
                sqlcmd.Parameters.AddWithValue("@fname", employeeModel.EmpFname);
                sqlcmd.Parameters.AddWithValue("@lname", employeeModel.EmpLname);
                sqlcmd.Parameters.AddWithValue("@empemail", employeeModel.EmpEmail);
                sqlcmd.Parameters.AddWithValue("@empcontact", employeeModel.EmpContactno);
                int rows = sqlcmd.ExecuteNonQuery();
                
                connection.Close();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                Console.WriteLine("Error in EmployeeController ==> Create - " + e.Message);
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                DataTable editDataTable = new DataTable();
                connection.Open();
                string query = "select * from Employee where Empid=@Empid";
                SqlDataAdapter sqldata = new SqlDataAdapter(query, connection);
                sqldata.SelectCommand.Parameters.AddWithValue("@Empid", id);
                sqldata.Fill(editDataTable);
                connection.Close();
                if (editDataTable.Rows.Count == 1)
                {
                    employeeModel.EmpFname = editDataTable.Rows[0][1].ToString();
                    employeeModel.EmpLname = editDataTable.Rows[0][2].ToString();
                    employeeModel.EmpEmail = editDataTable.Rows[0][3].ToString();
                    employeeModel.EmpContactno = editDataTable.Rows[0][4].ToString();
                    return View(employeeModel);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in EmployeeController ==> Create - " + e.Message);
                return View();
            }
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,EmployeeModel employeeModel)
        {
            try
            {
                connection.Open();
                string query = "Update Employee set EmpFname = @EmpFname,EmpLname = @EmpLname,EmpEmail = @EmpEmail,EmpContactno = @EmpContactno where Empid=@Empid";
                SqlCommand sqlcmd = new SqlCommand(query, connection);
                sqlcmd.Parameters.AddWithValue("@Empid", id);
                sqlcmd.Parameters.AddWithValue("@EmpFname", employeeModel.EmpFname);
                sqlcmd.Parameters.AddWithValue("@EmpLname", employeeModel.EmpLname);
                sqlcmd.Parameters.AddWithValue("@EmpEmail", employeeModel.EmpEmail);
                sqlcmd.Parameters.AddWithValue("@EmpContactno", employeeModel.EmpContactno);
                sqlcmd.ExecuteNonQuery();
               
                connection.Close();
                
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in EmployeeController ==> Edit - " + e.Message);
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
               connection.Open();
                string query = "Delete from Employee where Empid=@Empid";
                SqlCommand sqlcmd = new SqlCommand(query, connection);
                sqlcmd.Parameters.AddWithValue("@Empid", id);
                sqlcmd.ExecuteNonQuery();
                connection.Close();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in EmployeeController ==> Delete - " + e.Message);
                return View();
            }
        }
    }
}