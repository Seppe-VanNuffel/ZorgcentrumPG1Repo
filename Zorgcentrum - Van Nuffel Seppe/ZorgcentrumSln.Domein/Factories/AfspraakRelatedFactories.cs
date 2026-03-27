using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Models.Afspraak;

namespace ZorgcentrumSln.Domein.Factories;

internal class AfspraakRelatedFactories
{
    internal static AfspraakDto CreateAfspraakDtoFromAfspraak(Afspraak afspraak)
    {
        return afspraak switch
        {
            Consultatie c => new ConsultatieAfspraakDto(c.Id, c.DatumTijdStart, c.DuurMin, c.RijksregisterNrPatient, c.RijksregisterNrArts, c.AfdelingCode),
            Controle c => new ControleAfspraakDto(c.Id, c.DatumTijdStart, c.DuurMin, c.RijksregisterNrPatient, c.RijksregisterNrArts, c.AfdelingCode, c.Referentie),
            Operatie o => new OperatieAfspraakDto(o.Id, o.DatumTijdStart, o.DuurMin, o.RijksregisterNrPatient, o.RijksregisterNrArts, o.AfdelingCode, o.Beschrijving, o.AantalMinutenOpVoorhand),
            _ => throw new NotImplementedException()
        };
    }

    internal static Afspraak CreateAfspraakFromAfspraakDto(AfspraakDto afspraakDto)
    {
        return afspraakDto switch
        {
            ConsultatieAfspraakDto cAD => new Consultatie(cAD.Id, cAD.DatumTijdStart, cAD.duurMin, cAD.RijksregisterNrPatient, cAD.RijksregisterNrArts, cAD.afdelingCode),
            ControleAfspraakDto cAD => new Controle(cAD.Id, cAD.DatumTijdStart, cAD.duurMin, cAD.RijksregisterNrPatient, cAD.RijksregisterNrArts, cAD.afdelingCode, cAD.Referentie),
            OperatieAfspraakDto oAD => new Operatie(oAD.Id, oAD.DatumTijdStart, oAD.duurMin, oAD.RijksregisterNrPatient, oAD.RijksregisterNrArts, oAD.afdelingCode, oAD.Beschrijving, oAD.AantalMinutenOpVoorhand),
            _ => throw new NotImplementedException()
        };
    }
}
