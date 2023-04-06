using System.Net.Http.Json;
using MyWallet.ViewModel;

namespace MyWallet.Services;

public class ExpenseService : IExpenseService
{
    protected HttpClient Http;
    
    public ExpenseService(HttpClient http)
    {
        Http = http;
    }

    public async Task<IEnumerable<Expense>> GetAll()
    {
        try
        {
            var listExpense = await Http.GetFromJsonAsync<List<Expense>>("Expense/GetAll/"); 
            return listExpense;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<Expense>();
        }
    }

    public Task<object> Create()
    {
        throw new NotImplementedException();
    }
}