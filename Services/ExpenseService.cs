using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MyWallet.ViewModel;

namespace MyWallet.Services;

public class ExpenseService : IExpenseService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly HttpClient _http;
    private readonly IAuthorizationHandlerContextFactory _authorizationHandlerContextFactory;
    private readonly IAuthorizationService _authorizationService;
    
    public ExpenseService(HttpClient http, AuthenticationStateProvider authenticationStateProvider, IAuthorizationHandler authorizationHandler, IAuthorizationHandlerContextFactory authorizationHandlerContextFactory, IAuthorizationService authorizationService)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _authorizationHandlerContextFactory = authorizationHandlerContextFactory;
        _authorizationService = authorizationService;
        _http = http;
    }

    public async Task<IEnumerable<Expense>> GetAll()
    {
        try
        {
            var authenticationState = _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authenticationState.Result.User;
            var identity = authenticationState.Result.User.Identity;
            var picture = authenticationState.Result.User.Claims.First(x => x.Type == "picture").Value;
            var identities = authenticationState.Result.User.Identities;
            
            Console.WriteLine(identity.AuthenticationType);
            
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
            var idExpense = "128e916d-9f7e-439f-8844-061ae658dbf1";
            var tt = _http.DefaultRequestHeaders;
            var ttt = _http;

            var factor = _authorizationHandlerContextFactory;
            var service = _authorizationService;

            var listExpense = await _http.GetFromJsonAsync<List<Expense>>("Expense/GetAll/" + idExpense);

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