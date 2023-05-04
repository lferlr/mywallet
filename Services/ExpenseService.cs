using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MyWallet.ViewModel;

namespace MyWallet.Services;

public class ExpenseService : IExpenseService
{
    private readonly HttpClient _http;

    public ExpenseService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<Expense>> GetAll()
    {
        try
        {
            var listExpense = await _http.GetFromJsonAsync<List<Expense>>("Expense/GetAll/");

            if (listExpense != null) return listExpense;

            return new List<Expense>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<Expense>();
        }
    }
    
    public async Task<IEnumerable<Expense>> GetAllById()
    {
        try
        {
            var idUser = new Guid("128e916d-9f7e-439f-8844-061ae658dbf1");

            var listExpense = await _http.GetFromJsonAsync<IEnumerable<Expense>>("Expense/GetAll/" + idUser);

            if (listExpense != null) return listExpense;

            return new List<Expense>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<Expense>();
        }
    }

    public async Task<ResponseExpense> Create(Expense expenseDto)
    {
        try
        {
            // TODO: Realizar o ajuste para receber o retorno da API
            // Realizando o id do usuário
            expenseDto.UserId = new Guid().ToString();
            
            var responseExpense = await _http.PostAsJsonAsync("Expense/Create/", expenseDto);

            if (!responseExpense.IsSuccessStatusCode)
                return new ResponseExpense();
            
            var content = await responseExpense.Content.ReadFromJsonAsync<ResponseExpense>();

            return content ?? new ResponseExpense();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}