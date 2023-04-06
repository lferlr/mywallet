using MyWallet.ViewModel;

namespace MyWallet.Services;

public interface IExpenseService
{
    Task<IEnumerable<Expense>> GetAll();
    Task<object> Create();
}