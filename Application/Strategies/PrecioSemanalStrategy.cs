public class PrecioSemanalStrategy : IPrecioStrategy 
{ 
    public decimal CalcularPrecio(Vehiculo vehiculo, int dias) 
    { 
        return vehiculo.CalcularPrecioBase() * dias * 0.95m; // Aplica un descuento del 5% para alquileres semanales
    } 
}
