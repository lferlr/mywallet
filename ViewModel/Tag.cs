using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.ViewModel;

public enum Tag
{
    [Display(Name= "Casa")]
    Casa = 1,
    [Display(Name= "Carro")]
    Carro,
    [Display(Name= "Alimentação")]
    Alimentacao,
    [Display(Name= "Mercado")]
    Mercado,
    [Display(Name= "Lazer")]
    Lazer,
    [Display(Name= "Cartão")]
    Cartao,
    [Display(Name= "Educação")]
    Educacao,
    [Display(Name= "Farmácia")]
    Farmacia,
    [Display(Name= "Cursos")]
    Cursos,
    [Display(Name= "Vestuario")]
    Vestuario,
    [Display(Name= "Viagem")]
    Viagem,
    [Display(Name= "Saúde")]
    Saude,
    [Display(Name= "Outros")]
    Outros
}