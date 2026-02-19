using VehicleRentalSystem.Domain.Entities;

namespace VehicleRentalSystem.Domain.Interfaces;
public interface IVehiculoRepository
{
    void AgregarVehiculo(Vehiculo vehiculo);
    void ActualizarVehiculo(Vehiculo vehiculo);
    void EliminarVehiculo(string matricula);
    Vehiculo ObtenerVehiculoPorMatricula(string matricula);
    IReadOnlyList<Vehiculo> ObtenerTodosLosVehiculos();
    IReadOnlyList<Vehiculo> ObtenerVehiculosDisponibles();
}