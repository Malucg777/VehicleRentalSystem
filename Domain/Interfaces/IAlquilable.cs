namespace VehicleRentalSystem.Domain.Interfaces;

public interface IAlquilable
{
    bool EstaDisponible();
    void MarcarComoAlquilado();
    void MarcarComoDevuelto();
}