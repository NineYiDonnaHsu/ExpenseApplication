using ExpenseApplication.Models;
using ExpenseApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseApplication.Controllers.Apis
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : Controller
    {
        private readonly ExpenseRepository _expenseRepository;

        public ExpenseController(ApplicationDbContext context)
        {
            _expenseRepository = new ExpenseRepository(context);
        }

        [HttpGet("Search")]
        public ActionResult<IEnumerable<Expense>> Search(string? title, string? category, DateTime? startDate, DateTime? endDate)
        {
            var expenses = _expenseRepository.SearchExpenses(title, category, startDate, endDate);
            return expenses.ToList();
        }

        // GET: api/Expense
        [HttpGet]
        public ActionResult<IEnumerable<Expense>> Get()
        {
            var expenses = _expenseRepository.GetAllExpenses();
            return expenses.ToList();
        }

        // GET: api/Expense/5
        [HttpGet("{id}")]
        public ActionResult<Expense> Get(int id)
        {
            var expense = _expenseRepository.GetExpenseById(id);
            if (expense == null)
            {
                return NotFound();
            }
            return expense;
        }

        // POST: api/Expense
        [HttpPost]
        public ActionResult<Expense> Post(Expense expense)
        {
            try
            {
                _expenseRepository.AddExpense(expense);
                return CreatedAtAction(nameof(Get), new { id = expense.Id }, expense);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Expense/5
        [HttpPut("{id}")]
        public ActionResult<Expense> Put(int id, Expense expenseModel)
        {
            try
            {
                var expense = _expenseRepository.GetExpenseById(id);
                if (expense == null)
                {
                    return NotFound();
                }
                expense.Title = expenseModel.Title;
                expense.Amount = expenseModel.Amount;
                expense.Date = expenseModel.Date.ToUniversalTime();
                expense.Category = expenseModel.Category;
                _expenseRepository.UpdateExpense(expense);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Expense/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var expense = _expenseRepository.GetExpenseById(id);
            if (expense == null)
            {
                return NotFound();
            }
            _expenseRepository.DeleteExpense(expense);
            return NoContent();
        }
    }
}
