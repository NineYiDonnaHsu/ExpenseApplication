using ExpenseApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseApplication.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly HttpClient _httpClient;

        public ExpenseController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyClient");
        }

        public async Task<IActionResult> Index()
        {
            ViewData["SearchErrorMsg"] = string.Empty;
            var response = await _httpClient.GetAsync("/api/Expense");
            if (response.IsSuccessStatusCode)
            {
                var expenses = await response.Content.ReadAsAsync<IEnumerable<Expense>>();
                return View(expenses);
            }
            else
            {
                return View(new List<Expense>());
            }
        }

        public async Task<IActionResult> Search(string title, string category, DateTime? startDate, DateTime? endDate)
        {
            ViewData["SearchErrorMsg"] = string.Empty;
            var formattedStartDate = startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd") : null;
            var formattedEndDate = endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd") : null;

            // Add condition to check if the endDate is greater than 30 days from the startDate
            if (startDate.HasValue && endDate.HasValue && (endDate.Value - startDate.Value).TotalDays > 30)
            {
                ViewData["SearchErrorMsg"] = "日期區間不可超過 30 天";
                return View("Index", new List<Expense>());
            }

            var response = await _httpClient.GetAsync($"/api/Expense/Search?title={title}&category={category}&startDate={formattedStartDate}&endDate={formattedEndDate}");
            if (response.IsSuccessStatusCode)
            {
                var expenses = await response.Content.ReadAsAsync<IEnumerable<Expense>>();
                return View("Index", expenses);
            }
            else
            {
                return View("Index", new List<Expense>());
            }
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Expense expense)
        {
            expense.Date = expense.Date.ToUniversalTime();

            if (!ModelState.IsValid)
            {
                return View(expense);
            }

            var response = await _httpClient.PostAsJsonAsync("/api/Expense", expense);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(expense);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Expense/{id}");
            if (response.IsSuccessStatusCode)
            {
                var expense = await response.Content.ReadAsAsync<Expense>();
                return View(expense);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return View(expense);
            }

            var response = await _httpClient.PutAsJsonAsync($"/api/Expense/{id}", expense);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(expense);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Expense/{id}");
            if (response.IsSuccessStatusCode)
            {
                var expense = await response.Content.ReadAsAsync<Expense>();
                return View(expense);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Expense/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
