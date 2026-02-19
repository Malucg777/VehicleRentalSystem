public abstract class Vehiculo
{
    public string Matricula {get; private set;}
    public string Marca {get; private set;}
    public string EstadoVehiculo {get; private set;}

    public Vehiculo(string Matricula, string Marca, string EstadoVehiculo)
    {
        this.Matricula = Matricula; 
        this.Marca = Marca;
        this.EstadoVehiculo = EstadoVehiculo;
    }

    public bool EstaDisponible() => EstadoVehiculo == "Disponible";

    public abstract double CalcularCosto(int dias); 

}
