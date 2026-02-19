public class VehiculoService
{
    private readonly IVehiculoRepository _vehiculoRepository;

    public VehiculoService(IVehiculoRepository vehiculoRepository)
    {
        _vehiculoRepository = vehiculoRepository;
    }

    public void AgregarVehiculo(Vehiculo vehiculo)
    {
        if (vehiculo == null)
            throw new ArgumentNullException(nameof(vehiculo));

        // Validar que no exista ya
        var existente = _vehiculoRepository.ObtenerPorMatricula(vehiculo.Matricula);
        if (existente != null)
            throw new InvalidOperationException("Ya existe un vehículo con esa matrícula.");

        _vehiculoRepository.Agregar(vehiculo);
    }

    public Vehiculo? ObtenerPorMatricula(string matricula)
    {
        return _vehiculoRepository.ObtenerPorMatricula(matricula);
    }

    public IEnumerable<Vehiculo> ObtenerDisponibles()
    {
        return _vehiculoRepository.ObtenerTodos()
            .Where(v => v.EstaDisponible());
    }

    public void EliminarVehiculo(string matricula)
    {
        _vehiculoRepository.Eliminar(matricula);
    }
}