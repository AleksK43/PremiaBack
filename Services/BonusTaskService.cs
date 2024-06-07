using Microsoft.EntityFrameworkCore;
using Premia_API.Data;
using Premia_API.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Premia_API.Services
{
    /// <summary>
    /// Service class for managing bonus tasks.
    /// </summary>
    public class BonusTaskService
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BonusTaskService"/> class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public BonusTaskService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves the list of bonus tasks for a supervisor.
        /// </summary>
        /// <param name="supervisorId">The supervisor ID.</param>
        /// <returns>The list of bonus tasks.</returns>
        public async Task<List<BonusTask>> GetTasksForSupervisorAsync(int supervisorId)
        {
            return await _context.BonusTasks
                .Include(t => t.Documents)
                .Where(t => t.SupervisorId == supervisorId && !t.IsCompleted)
                .ToListAsync();
        }

        /// <summary>
        /// Creates a new bonus task.
        /// </summary>
        /// <param name="task">The bonus task to create.</param>
        public async Task CreateTaskAsync(BonusTask task)
        {
            _context.BonusTasks.Add(task);
            await _context.SaveChangesAsync();
        }
    }
}
