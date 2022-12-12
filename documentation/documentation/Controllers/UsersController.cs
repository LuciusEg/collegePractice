using documentation.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace documentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<DocumentsController>
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select UserId as ""UserId"",
                    UserFirstName as ""UserFirstName"",
                    UserLastName as ""UserLastName"",
                    UserMiddleName as ""UserMiddleName"",
                    UserRole as ""UserRole"",
                    UserJobTitle as ""UserJobTitle"",
                    UserDepartment as ""UserDepartment"",
                from documents
                
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Users use)
        {
            string query = @"
                insert into Users (UserFirstName,UserLastName,UserMiddleName,UserRole,UserJobTitle,UserDepartment)
                values (@UserFirstName,@UserLastName,@UserMiddleName,@UserRole,@UserJobTitle,@UserDepartment)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@UserFirstName", use.UserFirstName);
                    myCommand.Parameters.AddWithValue("@UserLastName", use.UserLastName);
                    myCommand.Parameters.AddWithValue("@UserMiddleName", use.UserMiddleName);
                    myCommand.Parameters.AddWithValue("@UserRole", use.UserRole);
                    myCommand.Parameters.AddWithValue("@UserJobTitle", use.UserJobTitle);
                    myCommand.Parameters.AddWithValue("@UserDepartment", use.UserDepartment);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Users use)
        {
            string query = @"
                urdate Users set UserFirstName = @UserFirstName,
                UserLastName = @UserLastName,
                UserMiddleName = @UserMiddleName,
                UserRole = @UserRole,
                UserJobTitle = @UserJobTitle,
                UserDepartment = @UserDepartment
                where UserId = UserId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@UserId", use.UserId);
                    myCommand.Parameters.AddWithValue("@UserFirstName", use.UserFirstName);
                    myCommand.Parameters.AddWithValue("@UserLastName", use.UserLastName);
                    myCommand.Parameters.AddWithValue("@UserMiddleName", use.UserMiddleName);
                    myCommand.Parameters.AddWithValue("@UserRole", use.UserRole);
                    myCommand.Parameters.AddWithValue("@UserJobTitle", use.UserJobTitle);
                    myCommand.Parameters.AddWithValue("@UserDepartment", use.UserDepartment);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successesfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                delete from Users
                where UserId = UserId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@UserId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successesfully");
        }
    }
}