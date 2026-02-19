public class ClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public void RegistrarCliente(Cliente cliente)
    {
        if (cliente == null)
            throw new ArgumentNullException(nameof(cliente));

        // Validar que no exista ya (por documento)
        var existente = _clienteRepository.ObtenerPorDocumento(cliente.Documento);
        if (existente != null)
            throw new InvalidOperationException("Ya existe un cliente con ese documento.");

        _clienteRepository.Agregar(cliente);
    }

    public Cliente? ObtenerPorId(int id)
    {
        return _clienteRepository.ObtenerPorId(id);
    }

    public Cliente? ObtenerPorDocumento(string documento)
    {
        return _clienteRepository.ObtenerPorDocumento(documento);
    }

    public IEnumerable<Cliente> ObtenerTodos()
    {
        return _clienteRepository.ObtenerTodos();
    }

    public void ActualizarCliente(Cliente cliente)
    {
        if (cliente == null)
            throw new ArgumentNullException(nameof(cliente));

        var existente = _clienteRepository.ObtenerPorId(cliente.Id);
        if (existente == null)
            throw new InvalidOperationException("El cliente no existe.");

        _clienteRepository.Actualizar(cliente);
    }

    public void EliminarCliente(int id)
    {
        var existente = _clienteRepository.ObtenerPorId(id);
        if (existente == null)
            throw new InvalidOperationException("El cliente no existe.");

        _clienteRepository.Eliminar(id);
    }
}