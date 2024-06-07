using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Premia_API.Data;

namespace Premia_API.Services
{
    public class UserProfit
    {
        private readonly DataContext dbContext;
        public double Profit { get; }

        public UserProfit(DataContext dataContext, int Id)
        {

            this.dbContext = dataContext;

            double profit = 0;


            var documents = dataContext.Documents.Where(documents => documents.OwnerID == Id && documents.isNewDocument == true).ToList();

            if (documents.Count > 0)
            {
                foreach (var document in documents)
                {
                    profit += document.Income;
                }
            }

            profit = profit * 0.5;

            Profit = profit;
        }

    }
}
