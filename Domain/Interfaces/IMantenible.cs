namespace VehicleRentalSystem.Domain.Interfaces;

public interface IMantenible
{
    bool RequiereMantenimiento();
    void EnviarAMantenimiento(string motivo);
    void MarcarComoDisponibleTrasMantenimiento();
}