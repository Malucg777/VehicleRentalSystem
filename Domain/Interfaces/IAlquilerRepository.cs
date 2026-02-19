public interface IAlquilerRepository
{
    void Agregar(Alquiler alquiler);
    Alquiler? ObtenerPorId(int id);
    Alquiler? ObtenerActivoPorCliente(int clienteId);
    IEnumerable<Alquiler> ObtenerPorCliente(int clienteId);
    IEnumerable<Alquiler> ObtenerTodos();
    void Actualizar(Alquiler alquiler);
    void Eliminar(int id);
}