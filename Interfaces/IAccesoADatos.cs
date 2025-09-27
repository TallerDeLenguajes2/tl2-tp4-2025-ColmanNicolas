using CadeteriaClass;
using CadeteClass;
public interface IAccesoADatos<T>
{
    (bool, string, List<T>) Obtener();
    (bool,string) Guardar(List<T> t);
}