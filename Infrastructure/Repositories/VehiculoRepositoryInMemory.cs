using VehicleRentalSystem.Domain.Entities;
using VehicleRentalSystem.Domain.Interfaces;

public class VehiculoRepositoryInMemory : IVehiculoRepository
{
    private readonly List<Vehiculo> _vehiculos = new();

    public void Agregar(Vehiculo vehiculo)
    {
        _vehiculos.Add(vehiculo);
    }

    public Vehiculo? ObtenerPorMatricula(string matricula)
    {
        return _vehiculos.FirstOrDefault(v => v.Matricula == matricula);
    }

    public IEnumerable<Vehiculo> ObtenerTodos()
    {
        return _vehiculos.ToList();
    }

    public IEnumerable<Vehiculo> ObtenerDisponibles()
    {
        return _vehiculos.Where(v => v.EstaDisponible()).ToList();
    }

    public void Actualizar(Vehiculo vehiculo)
    {
        var index = _vehiculos.FindIndex(v => v.Matricula == vehiculo.Matricula);
        if (index != -1)
        {
            _vehiculos[index] = vehiculo;
        }
    }

    public void Eliminar(string matricula)
    {
        var vehiculo = ObtenerPorMatricula(matricula);
        if (vehiculo != null)
        {
            _vehiculos.Remove(vehiculo);
        }
    }

    public bool Existe(string matricula)
    {
        return _vehiculos.Any(v => v.Matricula == matricula);
    }
}