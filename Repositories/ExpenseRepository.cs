using ExpenseApplication.Models;

namespace ExpenseApplication.Repositories
{
    public class ExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create
        public void AddExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();
        }

        // Read
        public Expense GetExpenseById(int id)
        {
            return _context.Expenses.FirstOrDefault(e => e.Id == id);
        }

        public List<Expense> GetAllExpenses()
        {
            return _context.Expenses.ToList();
        }

        // Update
        public void UpdateExpense(Expense expense)
        {
            _context.Expenses.Update(expense);
            _context.SaveChanges();
        }

        // Delete
        public void DeleteExpense(Expense expense)
        {
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
        }

        // Search
        public List<Expense> SearchExpenses(string keyword, string category, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Expenses.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(e => e.Title.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(e => e.Category == category);
            }

            if (startDate != null && endDate != null)
            {
                query = query.Where(e => e.Date >= startDate.Value.ToUniversalTime() && e.Date <= endDate.Value.ToUniversalTime());
            }

            return query.ToList();
        }
    }
}
