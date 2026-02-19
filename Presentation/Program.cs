using VehicleRentalSystem.Domain.Entities;
using VehicleRentalSystem.Domain.Enums;
using VehicleRentalSystem.Domain.Interfaces;
using VehicleRentalSystem.Infrastructure.Repositories;
using VehicleRentalSystem.Application.Services;
using VehicleRentalSystem.Application.Factories;

class Program
{
    private static VehiculoService _vehiculoService = null!;
    private static ClienteService _clienteService = null!;
    private static AlquilerService _alquilerService = null!;
    private static VehiculoFactory _vehiculoFactory = null!;

    static void Main(string[] args)
    {
        InicializarServicios();
        CargarDatosIniciales();
        EjecutarMenuPrincipal();
    }

    static void InicializarServicios()
    {
        // Repositorios (en memoria)
        var vehiculoRepository = new VehiculoRepositoryInMemory();
        var clienteRepository = new ClienteRepositoryInMemory();
        var alquilerRepository = new AlquilerRepositoryInMemory();

        // Servicios
        _vehiculoService = new VehiculoService(vehiculoRepository);
        _clienteService = new ClienteService(clienteRepository);
        _alquilerService = new AlquilerService(alquilerRepository, vehiculoRepository, clienteRepository);

        // Factory
        _vehiculoFactory = new VehiculoFactory();
    }

    static void CargarDatosIniciales()
    {
        // Vehículos de ejemplo
        _vehiculoService.AgregarVehiculo(_vehiculoFactory.CrearAuto("ABC-123", "Toyota", 4));
        _vehiculoService.AgregarVehiculo(_vehiculoFactory.CrearAuto("DEF-456", "Ford", 2));
        _vehiculoService.AgregarVehiculo(_vehiculoFactory.CrearMoto("GHI-789", "Honda", 150));
        _vehiculoService.AgregarVehiculo(_vehiculoFactory.CrearCamion("JKL-012", "Mercedes", 5000));

        // Clientes de ejemplo
        _clienteService.RegistrarCliente(new Cliente(1, "Juan Pérez", "12345678"));
        _clienteService.RegistrarCliente(new Cliente(2, "María García", "87654321"));
    }

    static void EjecutarMenuPrincipal()
    {
        bool continuar = true;

        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║     SISTEMA DE ALQUILER DE VEHÍCULOS    ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine("║  1. Gestión de Vehículos                ║");
            Console.WriteLine("║  2. Gestión de Clientes                 ║");
            Console.WriteLine("║  3. Gestión de Alquileres               ║");
            Console.WriteLine("║  4. Salir                               ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.Write("\nSeleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MenuVehiculos();
                    break;
                case "2":
                    MenuClientes();
                    break;
                case "3":
                    MenuAlquileres();
                    break;
                case "4":
                    continuar = false;
                    Console.WriteLine("¡Hasta luego!");
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    Pausar();
                    break;
            }
        }
    }

    static void MenuVehiculos()
    {
        bool continuar = true;

        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("=== GESTIÓN DE VEHÍCULOS ===\n");
            Console.WriteLine("1. Ver todos los vehículos disponibles");
            Console.WriteLine("2. Agregar vehículo");
            Console.WriteLine("3. Buscar vehículo por matrícula");
            Console.WriteLine("4. Eliminar vehículo");
            Console.WriteLine("5. Volver al menú principal");
            Console.Write("\nSeleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MostrarVehiculosDisponibles();
                    break;
                case "2":
                    AgregarVehiculo();
                    break;
                case "3":
                    BuscarVehiculo();
                    break;
                case "4":
                    EliminarVehiculo();
                    break;
                case "5":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    Pausar();
                    break;
            }
        }
    }

    static void MostrarVehiculosDisponibles()
    {
        Console.Clear();
        Console.WriteLine("=== VEHÍCULOS DISPONIBLES ===\n");

        var vehiculos = _vehiculoService.ObtenerDisponibles();

        if (!vehiculos.Any())
        {
            Console.WriteLine("No hay vehículos disponibles.");
        }
        else
        {
            foreach (var v in vehiculos)
            {
                Console.WriteLine($"- {v.Matricula} | {v.Marca} | Tipo: {v.GetType().Name}");
            }
        }

        Pausar();
    }

    static void AgregarVehiculo()
    {
        Console.Clear();
        Console.WriteLine("=== AGREGAR VEHÍCULO ===\n");

        Console.WriteLine("Tipo de vehículo:");
        Console.WriteLine("1. Auto");
        Console.WriteLine("2. Moto");
        Console.WriteLine("3. Camión");
        Console.Write("Seleccione: ");
        var tipoStr = Console.ReadLine();

        Console.Write("Matrícula: ");
        var matricula = Console.ReadLine() ?? "";

        Console.Write("Marca: ");
        var marca = Console.ReadLine() ?? "";

        try
        {
            Vehiculo vehiculo = tipoStr switch
            {
                "1" => _vehiculoFactory.CrearAuto(matricula, marca),
                "2" => _vehiculoFactory.CrearMoto(matricula, marca),
                "3" => _vehiculoFactory.CrearCamion(matricula, marca),
                _ => throw new ArgumentException("Tipo no válido")
            };

            _vehiculoService.AgregarVehiculo(vehiculo);
            Console.WriteLine("\n¡Vehículo agregado exitosamente!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        Pausar();
    }

    static void BuscarVehiculo()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR VEHÍCULO ===\n");

        Console.Write("Ingrese la matrícula: ");
        var matricula = Console.ReadLine() ?? "";

        var vehiculo = _vehiculoService.ObtenerPorMatricula(matricula);

        if (vehiculo == null)
        {
            Console.WriteLine("Vehículo no encontrado.");
        }
        else
        {
            Console.WriteLine($"\nMatrícula: {vehiculo.Matricula}");
            Console.WriteLine($"Marca: {vehiculo.Marca}");
            Console.WriteLine($"Tipo: {vehiculo.GetType().Name}");
            Console.WriteLine($"Estado: {vehiculo.EstadoVehiculo}");
        }

        Pausar();
    }

    static void EliminarVehiculo()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR VEHÍCULO ===\n");

        Console.Write("Ingrese la matrícula del vehículo a eliminar: ");
        var matricula = Console.ReadLine() ?? "";

        try
        {
            _vehiculoService.EliminarVehiculo(matricula);
            Console.WriteLine("\n¡Vehículo eliminado exitosamente!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        Pausar();
    }

    static void MenuClientes()
    {
        bool continuar = true;

        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("=== GESTIÓN DE CLIENTES ===\n");
            Console.WriteLine("1. Ver todos los clientes");
            Console.WriteLine("2. Registrar cliente");
            Console.WriteLine("3. Buscar cliente");
            Console.WriteLine("4. Volver al menú principal");
            Console.Write("\nSeleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MostrarClientes();
                    break;
                case "2":
                    RegistrarCliente();
                    break;
                case "3":
                    BuscarCliente();
                    break;
                case "4":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    Pausar();
                    break;
            }
        }
    }

    static void MostrarClientes()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE CLIENTES ===\n");

        var clientes = _clienteService.ObtenerTodos();

        if (!clientes.Any())
        {
            Console.WriteLine("No hay clientes registrados.");
        }
        else
        {
            foreach (var c in clientes)
            {
                Console.WriteLine($"- ID: {c.Id} | {c.Nombre} | Doc: {c.Documento}");
            }
        }

        Pausar();
    }

    static void RegistrarCliente()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR CLIENTE ===\n");

        Console.Write("ID: ");
        var idStr = Console.ReadLine();
        int.TryParse(idStr, out int id);

        Console.Write("Nombre: ");
        var nombre = Console.ReadLine() ?? "";

        Console.Write("Documento: ");
        var documento = Console.ReadLine() ?? "";

        try
        {
            var cliente = new Cliente(id, nombre, documento);
            _clienteService.RegistrarCliente(cliente);
            Console.WriteLine("\n¡Cliente registrado exitosamente!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        Pausar();
    }

    static void BuscarCliente()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR CLIENTE ===\n");

        Console.Write("Ingrese el documento: ");
        var documento = Console.ReadLine() ?? "";

        var cliente = _clienteService.ObtenerPorDocumento(documento);

        if (cliente == null)
        {
            Console.WriteLine("Cliente no encontrado.");
        }
        else
        {
            Console.WriteLine($"\nID: {cliente.Id}");
            Console.WriteLine($"Nombre: {cliente.Nombre}");
            Console.WriteLine($"Documento: {cliente.Documento}");
        }

        Pausar();
    }

    static void MenuAlquileres()
    {
        bool continuar = true;

        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("=== GESTIÓN DE ALQUILERES ===\n");
            Console.WriteLine("1. Crear nuevo alquiler");
            Console.WriteLine("2. Finalizar alquiler");
            Console.WriteLine("3. Ver alquileres activos");
            Console.WriteLine("4. Calcular costo estimado");
            Console.WriteLine("5. Volver al menú principal");
            Console.Write("\nSeleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    CrearAlquiler();
                    break;
                case "2":
                    FinalizarAlquiler();
                    break;
                case "3":
                    MostrarAlquileresActivos();
                    break;
                case "4":
                    CalcularCostoEstimado();
                    break;
                case "5":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    Pausar();
                    break;
            }
        }
    }

    static void CrearAlquiler()
    {
        Console.Clear();
        Console.WriteLine("=== CREAR ALQUILER ===\n");

        Console.Write("Matrícula del vehículo: ");
        var matricula = Console.ReadLine() ?? "";

        Console.Write("ID del cliente: ");
        var clienteIdStr = Console.ReadLine();
        int.TryParse(clienteIdStr, out int clienteId);

        Console.Write("Horas estimadas de alquiler: ");
        var horasStr = Console.ReadLine();
        int.TryParse(horasStr, out int horas);

        try
        {
            var alquiler = _alquilerService.CrearAlquiler(matricula, clienteId, horas);
            Console.WriteLine($"\n¡Alquiler creado exitosamente!");
            Console.WriteLine($"ID Alquiler: {alquiler.Id}");
            Console.WriteLine($"Costo estimado: ${alquiler.Vehiculo.CalcularCosto(horas):F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        Pausar();
    }

    static void FinalizarAlquiler()
    {
        Console.Clear();
        Console.WriteLine("=== FINALIZAR ALQUILER ===\n");

        Console.Write("ID del alquiler: ");
        var idStr = Console.ReadLine();
        int.TryParse(idStr, out int id);

        try
        {
            var alquiler = _alquilerService.FinalizarAlquiler(id);
            Console.WriteLine($"\n¡Alquiler finalizado!");
            Console.WriteLine($"Costo total: ${alquiler.CostoTotal:F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        Pausar();
    }

    static void MostrarAlquileresActivos()
    {
        Console.Clear();
        Console.WriteLine("=== ALQUILERES ACTIVOS ===\n");

        var alquileres = _alquilerService.ObtenerAlquileresActivos();

        if (!alquileres.Any())
        {
            Console.WriteLine("No hay alquileres activos.");
        }
        else
        {
            foreach (var a in alquileres)
            {
                Console.WriteLine($"- ID: {a.Id} | Vehículo: {a.Vehiculo.Matricula} | Cliente: {a.Cliente.Nombre}");
            }
        }

        Pausar();
    }

    static void CalcularCostoEstimado()
    {
        Console.Clear();
        Console.WriteLine("=== CALCULAR COSTO ESTIMADO ===\n");

        Console.Write("Matrícula del vehículo: ");
        var matricula = Console.ReadLine() ?? "";

        Console.Write("Horas de alquiler: ");
        var horasStr = Console.ReadLine();
        int.TryParse(horasStr, out int horas);

        try
        {
            var costo = _alquilerService.CalcularCostoEstimado(matricula, horas);
            Console.WriteLine($"\nCosto estimado para {horas} horas: ${costo:F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }

        Pausar();
    }

    static void Pausar()
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
}
