using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using documentation;
using documentation.Models;

namespace documentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentationContext _context;

        public DocumentsController(DocumentationContext context)
        {
            _context = context;
        }
        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            return BadRequest();
        }

        [HttpGet("allDocuments")]
        public async Task<ActionResult<IEnumerable<Document>>> allDocuments()
        {
            return await _context.Documents
                .Select(x => DocumentTO(x))
                .ToListAsync();
        }

        [HttpGet("GetDocumentById")]
        public async Task<ActionResult<Document>> GetDocumentById(long Id)
        {
            var document = await _context.Documents.FindAsync(Id);

            if (document == null)
            {
                return NotFound();
            }

            return DocumentTO(document);
        }

        [HttpPost("addDocument")]
        public async Task<ActionResult<IEnumerable<Document>>> addDocument(Document documentTO)
        {
            var document = new Document
            {
                Id = documentTO.Id,
                Title = documentTO.Title,
                Description = documentTO.Description,
                Number = documentTO.Number,
                Author = documentTO.Author,
                Recipient = documentTO.Recipient
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDocuments),
                new { id = document.Id },
                DocumentTO(document));
        }

        [HttpPut("UpdateDocument")]
        public async Task<ActionResult> UpdateDocument(int id, Document documentTO)
        {
            if (id != documentTO.Id)
            {
                return BadRequest();
            }

            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            document.Id = documentTO.Id;
            document.Title = documentTO.Title;
            document.Description = documentTO.Description;
            document.Number = documentTO.Number;
            document.Author = documentTO.Author;
            document.Recipient = documentTO.Recipient;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!DocumentExist(id))
            {
                return NotFound();
            }

            return Ok("Document updated");
        }

        [HttpDelete("DeleteDocument")]
        public async Task<ActionResult<IEnumerable<Document>>> DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return new JsonResult("Document deleted");
        }

        private bool DocumentExist(long id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }

        private static Document DocumentTO(Document document) =>
        new Document
        {
            Id = document.Id,
            Title = document.Title,
            Description = document.Description,
            Number = document.Number,
            Author = document.Author,
            Recipient = document.Recipient
        };

    }
}