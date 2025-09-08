using CadeteriaClass;
using CadeteClass;
public interface IAccesoADatos
{
    Cadeteria AccesoADatosCadeteria();
    List<Cadete> AccesoADatosCadetes();
    void GuardarDatosDeCadetes(List<Cadete> cadetes);
}