namespace MyWallet.ViewModel;

public class Expense
{
    public Guid Id { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public Tag Tag { get; set; }
    public decimal Value { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool Status { get; set; }
    public string UserId { get; set; } 
}