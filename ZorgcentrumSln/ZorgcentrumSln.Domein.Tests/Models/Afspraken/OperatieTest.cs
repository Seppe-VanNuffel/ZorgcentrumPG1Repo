using ZorgcentrumSln.Domein.Exceptions;
using ZorgcentrumSln.Domein.Models.Afspraak;

namespace ZorgcentrumSln.Domein.Tests.Models.Afspraken;

public class OperatieTest
{
    [Fact]
    public void TEST_OPERATIE_CTOR()
    {
        var controle = new Operatie(1, DateTime.Now, 30, "63.01.02-359.38", "15.01.23-042.07", "GAST", "test", 30);

        Assert.Equal(30, controle.DuurMin);
        Assert.Equal("test", controle.Beschrijving);
        Assert.Equal(30, controle.AantalMinutenOpVoorhand);
    }

    [Theory]
    [InlineData(-5)]
    [InlineData(0)]
    [InlineData(9)]
    [InlineData(29)]
    [InlineData(-27)]
    public void TEST_DUURMIN_ALS_FOUT(int duurmin)
    {
        Assert.Throws<AfspraakException>(() => new Operatie(1, DateTime.Now, duurmin, "63.01.02-359.38", "15.01.23-042.07", "GAST", "test", 30));
    }

    [Theory]
    [InlineData(-5)]
    [InlineData(0)]
    [InlineData(9)]
    [InlineData(29)]
    [InlineData(-27)]
    public void TEST_AANTALMINUTENOPVOORHAND_ALS_FOUT(int aantalMinutenOpVoorhand)
    {
        Assert.Throws<AfspraakException>(() => new Operatie(1, DateTime.Now, 30, "63.01.02-359.38", "15.01.23-042.07", "GAST", "test", aantalMinutenOpVoorhand));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void TEST_BESCHRIJVING_ALS_FOUT(string txt)
    {
        Assert.Throws<AfspraakException>(() => new Operatie(1, DateTime.Now, 30, "63.01.02-359.38", "15.01.23-042.07", "GAST", txt, 30));
    }
}
