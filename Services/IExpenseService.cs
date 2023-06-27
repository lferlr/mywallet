using MyWallet.ViewModel;

namespace MyWallet.Services;

public interface IExpenseService
{
    Task<IEnumerable<Expense>> GetAll();
    Task<IEnumerable<Expense>> GetAllById();
    Task<ResponseExpense> Create(Expense expenseDto);
    Task<ResponseExpense> Delete(Guid idExpense);
}