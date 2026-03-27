using ZorgcentrumSln.Domein.Exceptions;
using ZorgcentrumSln.Domein.Models.Afspraak;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZorgcentrumSln.Domein.Tests.Models.Afspraken;

public class ConsultatieTest
{
    [Fact]
    public void TEST_CONSULTATIE_CTOR()
    {
        DateTime date = DateTime.Now;

        var consultatie = new Consultatie(1, date, 20, "63.01.02-359.38", "15.01.23-042.07", "GAST");

        Assert.Equal(1, consultatie.Id);
        Assert.Equal(date, consultatie.DatumTijdStart);
        Assert.Equal(20, consultatie.DuurMin);
        Assert.Equal("63.01.02-359.38", consultatie.RijksregisterNrPatient);
        Assert.Equal("15.01.23-042.07", consultatie.RijksregisterNrArts);
        Assert.Equal("GAST", consultatie.AfdelingCode);
    }

    [Theory]
    [InlineData(-5)]
    [InlineData(0)]
    [InlineData(9)]
    [InlineData(-27)]
    public void TEST_DUURMIN_ALS_FOUT(int duurmin)
    {
        Assert.Throws<AfspraakException>(() => new Consultatie(1, DateTime.Today, duurmin, "63.01.02-359.38", "15.01.23-042.07", "GAST"));
    }

    [Fact]
    public void TEST_RRN_GOOIT_EX_ALS_GELIJK()
    {
        Assert.Throws<AfspraakException>(() => new Consultatie(1, DateTime.Today, 20, "63.01.02-359.38", "63.01.02-359.38", "GAST"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("GastG")]
    [InlineData("abc")]
    [InlineData("1237")]
    public void TEST_AFDELING_ALS_FOUT(string txt)
    {
        Assert.Throws<AfspraakException>(() => new Consultatie(1, DateTime.Today, 20, "63.01.02-359.38", "63.01.02-359.38", txt));
    }
}