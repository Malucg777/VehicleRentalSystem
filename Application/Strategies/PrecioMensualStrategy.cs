public class PrecioMensualStrategy : IPrecioStrategy 
{ 
    public decimal CalcularPrecio(Vehiculo vehiculo, int dias) 
    { 
        return vehiculo.CalcularPrecioBase() * dias * 0.9m; // Aplica un descuento del 10% para alquileres mensuales
    } 
}