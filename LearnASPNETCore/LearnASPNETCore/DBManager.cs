using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace LearnASPNETCore
{
    public class DBManager 
    {
        private readonly IConfiguration configuration;
        SqlConnection sqlConnection = null;
        public SqlConnection CreateConnection()
        {
            try
            {
                // DefaultConnectionString is coming from appsetting.json file
                //string connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                string connectionstring = "Server=SAURABH-PC\\ARXXUSSQL;Database=milton;User Id=sa;Password=Arxxus@123";
                Console.WriteLine("++++++++++++" + connectionstring);
                sqlConnection = new SqlConnection(connectionstring);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in DBManager ==> CreateConnection - " + e.Message);
            }
            return sqlConnection;
        }

        public SqlDataAdapter GetQuery(string selectquery)
        {
            SqlDataAdapter sqlDataAdapter = null;
            try
            {
                sqlDataAdapter = new SqlDataAdapter(selectquery, sqlConnection);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in DBManager ==> GetQuery - " + e.Message);
            }
            return sqlDataAdapter;
        }

        public SqlCommand DMLQuery(string dmlquery)
        {
            SqlCommand sqlCommand = null;
            try
            {
                sqlCommand = new SqlCommand(dmlquery, sqlConnection);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in DBManager ==> DMLQuery - " + e.Message);
            }
            return sqlCommand;
        }
    }

}
