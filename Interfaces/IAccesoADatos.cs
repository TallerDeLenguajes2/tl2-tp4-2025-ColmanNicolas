using CadeteriaClass;
using CadeteClass;
public interface IAccesoADatos
{
    List<Cadete> AccesoADatosCadetes();
    void GuardarDatosDeCadetes(List<Cadete> cadetes);
}