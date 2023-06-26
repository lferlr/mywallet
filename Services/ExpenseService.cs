using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MyWallet.ViewModel;

namespace MyWallet.Services;

public class ExpenseService : IExpenseService
{
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    public ExpenseService(HttpClient http, AuthenticationStateProvider authenticationStateProvider)
    {
        _http = http;
        _authenticationStateProvider = authenticationStateProvider;
        
        GetUserIdAsync().Wait();
    }

    public async Task<IEnumerable<Expense>> GetAll()
    {
        try
        {
            var listExpense = await _http.GetFromJsonAsync<List<Expense>>("Expense/GetAll/");

            if (listExpense != null) return listExpense;

            return new List<Expense>();
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
            return new List<Expense>();
        }
    }
    
    public async Task<IEnumerable<Expense>> GetAllById()
    {
        try
        {
            var listExpense = await _http.GetFromJsonAsync<IEnumerable<Expense>>("Expense/GetAll/" + UserAuthentication.UserId);
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
            expenseDto.UserId = UserAuthentication.UserId;
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

    private async Task GetUserIdAsync()
    {
        if (string.IsNullOrEmpty(UserAuthentication.UserId))
        {
            var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authenticationState.User;
            var userIdClaim = user.FindFirst(c => c.Type == "sub")?.Value;
            UserAuthentication.UserId = ExtractUserId(userIdClaim);
            UserAuthentication.Email = user.FindFirst(c => c.Type == "email")?.Value;
            UserAuthentication.Name = user.FindFirst(c => c.Type == "name")?.Value;
        }
    }
    
    private string ExtractUserId(string userIdClaim)
    {
        if (!string.IsNullOrEmpty(userIdClaim))
        {
            var parts = userIdClaim.Split('|');
            if (parts.Length > 1)
            {
                return parts[1];
            }
        }
        return userIdClaim;
    }
}