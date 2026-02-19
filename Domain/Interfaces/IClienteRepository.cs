public interface IClienteRepository
{
    void Agregar(Cliente cliente);
    Cliente? ObtenerPorId(int id);
    Cliente? ObtenerPorDocumento(string documento);
    IEnumerable<Cliente> ObtenerTodos();
    void Actualizar(Cliente cliente);
    void Eliminar(int id);
}