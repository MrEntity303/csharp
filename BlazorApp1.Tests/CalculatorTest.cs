using Xunit;
using BlazorApp1.Services;
namespace BlazorApp1.Tests;

public class CalculatorTests
{
    [Fact]
    public void Somma_DueNumeri_RitornaSommaCorretta()
    {
        var calc = new Calculator();
        var risultato = calc.Somma(2, 3);
        Assert.Equal(5, risultato);
    }
}
