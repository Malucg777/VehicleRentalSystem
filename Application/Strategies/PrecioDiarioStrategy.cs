public class PrecioDiarioStrategy : IPrecioStrategy 
{ 
    public decimal CalcularPrecio(Vehiculo vehiculo, int dias) 
    { 
        return vehiculo.CalcularPrecioBase() * dias; 
    } 
} 
