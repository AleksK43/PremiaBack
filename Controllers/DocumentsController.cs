using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Premia_API.Data;
using Premia_API.Entities;
using Premia_API.Services;

namespace Premia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DataContext _context;

        public DocumentsController(DataContext context)
        {
            _context = context;
        }

        private readonly DocumentService _documentService;

        public DocumentsController(DocumentService documentService)
        {
            _documentService = documentService;
        }



        /// <summary>
        /// Retrieves all documents.
        /// </summary>
        /// <returns>A list of documents.</returns>
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific document by its ID.
        /// </summary>
        /// <param name="id">The ID of the document.</param>
        /// <returns>The document with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <param name="id">The ID of the document to update.</param>
        /// <param name="document">The updated document.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(int id, Document document)
        {
            if (id != document.DocumentID)
            {
                return BadRequest();
            }

            _context.Entry(document).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new document.
        /// </summary>
        /// <param name="document">The document to create.</param>
        /// <returns>The created document.</returns>
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument(Document document)
        {
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocument", new { id = document.DocumentID }, document);
        }

        /// <summary>
        /// Deletes a document.
        /// </summary>
        /// <param name="id">The ID of the document to delete.</param>
        /// <returns>OK if the document is successfully deleted.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            document.DeleteDate = DateTime.Now;
            document.isDeleted = true;

            _context.Documents.Update(document);
            await _context.SaveChangesAsync();

            return Ok("Dokument usunięty");
        }

        /// <summary>
        /// Accepts a document.
        /// </summary>
        /// <param name="id">The ID of the document to accept.</param>
        /// <returns>OK if the document is successfully accepted.</returns>
        [HttpPost]
        [Route("accept/{id}")]
        public async Task<IActionResult> AcceptDocument(int id)
        {
            await _documentService.AcceptDocumentAsync(id);
            return Ok();
        }

        /// <summary>
        /// Retrieves documents for a supervisor.
        /// </summary>
        /// <param name="id">The ID of the supervisor.</param>
        /// <returns>A list of documents for the supervisor.</returns>
        [HttpGet]
        [Route("supervisor/{id}")]
        public async Task<IActionResult> GetDocumentsForSupervisor(int id)
        {
            var documents = await _documentService.GetDocumentsForSupervisorAsync(id);
            return Ok(documents);
        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.DocumentID == id);
        }

    }
}
