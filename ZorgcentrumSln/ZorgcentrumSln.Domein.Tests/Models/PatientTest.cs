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
        Assert.Equal(["{ORTH}"], persoon.Afdelingen);
    }
}
