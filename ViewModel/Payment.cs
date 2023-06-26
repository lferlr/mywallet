using System.ComponentModel.DataAnnotations;

namespace MyWallet.ViewModel;

public enum Payment
{
    [Display(Name= "Cartão de Crédito")]
    CartaoDeCredito = 1,
    [Display(Name= "Cartão de Débito")]
    CartaoDeDebito,
    [Display(Name= "Boleto")]
    Boleto,
    [Display(Name= "Pix")]
    Pix,
    [Display(Name= "Outros")]
    Outros
}