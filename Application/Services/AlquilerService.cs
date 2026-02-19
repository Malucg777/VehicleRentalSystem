public class AlquilerService
{
    private readonly IAlquilerRepository _alquilerRepository;
    private readonly IVehiculoRepository _vehiculoRepository;
    private readonly IClienteRepository _clienteRepository;

    public AlquilerService(
        IAlquilerRepository alquilerRepository,
        IVehiculoRepository vehiculoRepository,
        IClienteRepository clienteRepository)
    {
        _alquilerRepository = alquilerRepository;
        _vehiculoRepository = vehiculoRepository;
        _clienteRepository = clienteRepository;
    }

    public Alquiler CrearAlquiler(string matricula, int clienteId, int horasEstimadas)
    {
        // Validar cliente
        var cliente = _clienteRepository.ObtenerPorId(clienteId);
        if (cliente == null)
            throw new InvalidOperationException("Cliente no encontrado.");

        // Validar vehículo
        var vehiculo = _vehiculoRepository.ObtenerPorMatricula(matricula);
        if (vehiculo == null)
            throw new InvalidOperationException("Vehículo no encontrado.");

        if (!vehiculo.EstaDisponible())
            throw new InvalidOperationException("El vehículo no está disponible.");

        // Verificar que el cliente no tenga alquiler activo
        var alquilerActivo = _alquilerRepository.ObtenerActivoPorCliente(clienteId);
        if (alquilerActivo != null)
            throw new InvalidOperationException("El cliente ya tiene un alquiler activo.");

        // Crear alquiler
        var alquiler = new Alquiler(
            vehiculo: vehiculo,
            cliente: cliente,
            fechaInicio: DateTime.Now,
            horasEstimadas: horasEstimadas
        );

        // Cambiar estado del vehículo a "Alquilado"
        vehiculo.CambiarEstado(EstadoVehiculo.Alquilado);
        _vehiculoRepository.Actualizar(vehiculo);

        _alquilerRepository.Agregar(alquiler);

        return alquiler;
    }

    public Alquiler FinalizarAlquiler(int alquilerId)
    {
        var alquiler = _alquilerRepository.ObtenerPorId(alquilerId);
        if (alquiler == null)
            throw new InvalidOperationException("Alquiler no encontrado.");

        if (alquiler.EstaFinalizado)
            throw new InvalidOperationException("El alquiler ya fue finalizado.");

        // Calcular horas reales y costo
        alquiler.Finalizar(DateTime.Now);

        // Liberar vehículo
        var vehiculo = alquiler.Vehiculo;
        vehiculo.CambiarEstado(EstadoVehiculo.Disponible);
        _vehiculoRepository.Actualizar(vehiculo);

        _alquilerRepository.Actualizar(alquiler);

        return alquiler;
    }

    public Alquiler? ObtenerPorId(int id)
    {
        return _alquilerRepository.ObtenerPorId(id);
    }

    public Alquiler? ObtenerActivoPorCliente(int clienteId)
    {
        return _alquilerRepository.ObtenerActivoPorCliente(clienteId);
    }

    public IEnumerable<Alquiler> ObtenerHistorialPorCliente(int clienteId)
    {
        return _alquilerRepository.ObtenerPorCliente(clienteId);
    }

    public IEnumerable<Alquiler> ObtenerAlquileresActivos()
    {
        return _alquilerRepository.ObtenerTodos()
            .Where(a => !a.EstaFinalizado);
    }

    public double CalcularCostoEstimado(string matricula, int dias)
    {
        var vehiculo = _vehiculoRepository.ObtenerPorMatricula(matricula);
        if (vehiculo == null)
            throw new InvalidOperationException("Vehículo no encontrado.");

        return vehiculo.CalcularCosto(dias);
    }
}