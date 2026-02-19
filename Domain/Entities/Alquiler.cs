public class Alquiler
{
    public int Id { get; private set; }
    public Cliente Cliente { get; private set; }
    public Vehiculo Vehiculo { get; private set; }
    public DateTime FechaAlquiler { get; private set; }
    public DateTime FechaDevolucion { get; private set; }
    public EstadoAlquiler Estado { get; private set; }
    public decimal PrecioTotal { get; private set; }
    
}