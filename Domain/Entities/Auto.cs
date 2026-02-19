public class Auto : Vehiculo
{
    public String Color {get; private set;}
    public String Categoria {get; private set;}
    public Auto(string Matricula, string Marca, string EstadoVehiculo, string Color, string Categoria) : base( Matricula, Marca, EstadoVehiculo)
    {
        this.Color = Color; 
        this.Categoria = Categoria; 
    }

    public override double CalcularCosto(int dias)
    {
   
    }
}