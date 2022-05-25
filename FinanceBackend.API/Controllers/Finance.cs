using Microsoft.AspNetCore.Mvc;
using FinanceBackend.Context.Entities;
using FinanceBackend.Context;
using Microsoft.EntityFrameworkCore;

namespace FinanceBackend.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Finance : Controller
    {
        protected ApplicationContext _context = new ApplicationContext();

        [HttpGet("Overview/{ticker}")]
        public JsonResult Overview(string ticker)
        {
            Company searched_company = GetCompanyList().FirstOrDefault(x => x.Symbol == ticker);

            return Json(searched_company);
        }
        

        [HttpGet("MultipleOverview/{offset}/{amount}")]
        public JsonResult MultipleOverview(int offset, int amount)
        {
            List<Company> searched_companies = GetCompanyList().ToList().GetRange(index: offset, count: amount).ToList();

            return Json(searched_companies);
        }

        protected DbSet<Company> GetCompanyList()
        {
            List<BalanceSheet> balanceSheets = _context.BalanceSheets.ToList();
            List<IncomeStatement> incomeStatements = _context.IncomeStatements.ToList();
            List<CashFlow> cashFlows = _context.CashFlows.ToList();
            List<Metrics> metrics = _context.Metrics.ToList();

            return _context.Companies;
        }
    }
}
