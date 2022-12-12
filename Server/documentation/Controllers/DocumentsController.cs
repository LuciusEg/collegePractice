using documentation.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace documentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DocumentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<DocumentsController>
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select DocumentId as ""DocumentId"",
                    DocumentTitle as ""DocumentTitle"",
                    DocumentDiscription as ""DocumentDiscription"",
                    DocumentNumber as ""DocumentNumber"",
                    DocumentAuthor as ""DocumentAuthor"",
                    DocumentRecipient as ""DocumentRecipient""
                from documents
                
            ";

            DataTable table= new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon=new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader= myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(documents doc)
        {
            string query = @"
                insert into documents (DocumentTitle,DocumentDiscription,DocumentNumber,DocumentAuthor,DocumentRecipient)
                values (@DocumentTitle,@DocumentDiscription,@DocumentNumber,@DocumentAuthor,@DocumentRecipient)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DocumentTitle", doc.DocumentTitle);
                    myCommand.Parameters.AddWithValue("@DocumentDiscription", doc.DocumentDiscription);
                    myCommand.Parameters.AddWithValue("@DocumentNumber", doc.DocumentNumber);
                    myCommand.Parameters.AddWithValue("@DocumentAuthor", doc.DocumentAuthor);
                    myCommand.Parameters.AddWithValue("@DocumentRecipient", doc.DocumentRecipient);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(documents doc)
        {
            string query = @"
                urdate documents set DocumentTitle = @DocumentTitle,
                DocumentDiscription = @DocumentDiscription,
                DocumentNumber = @DocumentNumber,
                DocumentAuthor = @DocumentAuthor,
                DocumentRecipient = @DocumentRecipient
                where DocumentId = DocumentId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DocumentId", doc.DocumentId);
                    myCommand.Parameters.AddWithValue("@DocumentTitle", doc.DocumentTitle);
                    myCommand.Parameters.AddWithValue("@DocumentDiscription", doc.DocumentDiscription);
                    myCommand.Parameters.AddWithValue("@DocumentNumber", doc.DocumentNumber);
                    myCommand.Parameters.AddWithValue("@DocumentAuthor", doc.DocumentAuthor);
                    myCommand.Parameters.AddWithValue("@DocumentRecipient", doc.DocumentRecipient);
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
                delete from documents 
                where DocumentId = DocumentId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DocumentId", id);
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
