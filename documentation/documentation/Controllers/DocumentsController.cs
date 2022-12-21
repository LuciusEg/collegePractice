using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using documentation;
using documentation.Models;
//using Microsoft.AspNetCore.Authorization.Infrastructure;

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
        return await _context.Documents.ToListAsync();
    }

    [HttpGet("GetDocumentById")]
    public async Task<ActionResult<IEnumerable<Document>>> GetDocumentById(int id)
    {
        return await _context.Documents.Select(x => DocumentTO(x)).ToListAsync();
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
    public async Task<ActionResult<IEnumerable<Document>>> UpdateDocument(int id, [Bind("Id,Title,Description,Number,Author,Recipient")] Document document)
    {
        if (id != document.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(document);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExist(document.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
            return new JsonResult("Document updated");
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