using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Premia_API.Data;

namespace Premia_API.Services
{
    /// <summary>
    /// Represents a class that calculates the profit of a user based on their documents.
    /// </summary>
    public class UserProfit
    {
        private readonly DataContext dbContext;
        public double Profit { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfit"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="Id">The user ID.</param>
        public UserProfit(DataContext dataContext, int Id)
        {
            this.dbContext = dataContext;

            double profit = 0;

            // Retrieve all documents owned by the user that are marked as new
            var documents = dataContext.Documents.Where(documents => documents.OwnerID == Id && documents.isNewDocument == true).ToList();

            // Calculate the total profit by summing up the income of each document
            if (documents.Count > 0)
            {
                foreach (var document in documents)
                {
                    profit += document.Income;
                }
            }

            // Apply a 50% profit margin
            profit = profit * 0.5;

            Profit = profit;
        }
    }
}
