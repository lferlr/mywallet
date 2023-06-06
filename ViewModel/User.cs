namespace MyWallet.ViewModel;

public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public decimal Salary { get; set; }
}

public static class UserAuthentication
{
    public static string UserId { get; set; }
    public static string Name { get; set; }
    public static string Email { get; set; }
}