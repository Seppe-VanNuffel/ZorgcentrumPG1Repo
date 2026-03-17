using ZorgcentrumSln.Domein.Exceptions;
using ZorgcentrumSln.Domein.Models.Persoon;

namespace ZorgcentrumSln.Domein.Tests.Models;

public class PatientTest
{
    [Fact]
    public void TEST_PATIENTCONSTRUCTOR()
    {
        Patient persoon = new("61.08.05-432.76", "ABDUL-LATIF", "KHALID", ["{ORTH}"]);

        Assert.Equal("61.08.05-432.76", persoon.RijksregisterNummer);
        Assert.Equal("ABDUL-LATIF", persoon.Naam);
        Assert.Equal("KHALID", persoon.Voornaam);
        Assert.Equal(["ORTH"], persoon.Afdelingen);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("GastG")]
    [InlineData("abc")]
    [InlineData("1237")]
    [InlineData("GastG,ONCO")]
    public void TEST_AFDELING_ALS_FOUT(string line)
    {
        Assert.Throws<PersoonException>(() => new Arts("61.08.05-432.76", "ABDUL-LATIF", "KHALID", "1-79010-52-100", "{ANES}", line.Split(",").ToHashSet()));
    }
}
