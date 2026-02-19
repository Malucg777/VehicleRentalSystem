using VehicleRentalSystem.Domain.Entities;
using VehicleRentalSystem.Domain.Enums;

public class VehiculoFactory
{
    public Vehiculo CrearVehiculo(
        TipoVehiculo tipo,
        string matricula,
        string marca,
        EstadoVehiculo estado = EstadoVehiculo.Disponible)
    {
        return tipo switch
        {
            TipoVehiculo.Auto => new Auto(matricula, marca, estado),
            TipoVehiculo.Moto => new Moto(matricula, marca, estado),
            TipoVehiculo.Camion => new Camion(matricula, marca, estado),
            _ => throw new ArgumentException($"Tipo de vehículo no soportado: {tipo}")
        };
    }

    public Auto CrearAuto(string matricula, string marca, int numeroPuertas = 4)
    {
        return new Auto(matricula, marca, EstadoVehiculo.Disponible, numeroPuertas);
    }

    public Moto CrearMoto(string matricula, string marca, int cilindrada = 150)
    {
        return new Moto(matricula, marca, EstadoVehiculo.Disponible, cilindrada);
    }

    public Camion CrearCamion(string matricula, string marca, double capacidadCarga = 1000)
    {
        return new Camion(matricula, marca, EstadoVehiculo.Disponible, capacidadCarga);
    }
}