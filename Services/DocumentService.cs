using Microsoft.EntityFrameworkCore;
using Premia_API.Data;
using Premia_API.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Premia_API.Services
{
    /// <summary>
    /// Service class for managing documents.
    /// </summary>
    public class DocumentService
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentService"/> class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public DocumentService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Accepts a document by updating its properties and saving changes to the database.
        /// </summary>
        /// <param name="documentId">The ID of the document to accept.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AcceptDocumentAsync(int documentId)
        {
            var document = await _context.Documents.FindAsync(documentId);
            if (document != null)
            {
                document.isNewDocument = false;
                document.PreAccept = true;
                document.SettlementDate = DateTime.Now;

                _context.Documents.Update(document);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves a list of documents for a supervisor.
        /// </summary>
        /// <param name="supervisorId">The ID of the supervisor.</param>
        /// <returns>A task representing the asynchronous operation that returns a list of documents.</returns>
        public async Task<List<Document>> GetDocumentsForSupervisorAsync(int supervisorId)
        {
            return await _context.Documents
                .Where(d => d.OwnerID == supervisorId && !d.isDeleted && d.isNewDocument)
                .ToListAsync();
        }
    }
}
