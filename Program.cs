using HotelRealQuito.Factories;
using HotelRealQuito.Interfaces;
using HotelRealQuito.Models;
using HotelRealQuito.Observers;
using HotelRealQuito.Repositories;
using HotelRealQuito.Services;
using HotelRealQuito.Strategies;

Console.WriteLine("=== HOTEL REAL QUITO - EXAMEN ===\n");

Console.WriteLine("--- EJERCICIO 1: STRATEGY + FACTORY ---\n");

var highSeasonStrategy = new RackRateStrategy();
var lowSeasonStrategy = new LowSeasonRateStrategy();
var corporateStrategy = new CorporateRateStrategy();

var res1 = new Reservation { Id = "R001", GuestName = "Juan Pérez", RoomType = "Suite", Nights = 3, RateType = "temporadaAlta" };
var res2 = new Reservation { Id = "R002", GuestName = "María López", RoomType = "Doble", Nights = 5, RateType = "temporadaBaja" };
var res3 = new Reservation { Id = "R003", GuestName = "Carlos Ruiz", RoomType = "Simple", Nights = 2, RateType = "corporativa" };

Console.WriteLine($"Reserva R001 (temporadaAlta, 3 noches): ${highSeasonStrategy.CalculateCost(res1):F2}  ← $80*3*1.2 = $288.00");
Console.WriteLine($"Reserva R002 (temporadaBaja, 5 noches): ${lowSeasonStrategy.CalculateCost(res2):F2}  ← $80*5*0.85 = $340.00");
Console.WriteLine($"Reserva R003 (corporativa, 2 noches): ${corporateStrategy.CalculateCost(res3):F2}  ← $60*2 = $120.00\n");

Console.WriteLine("--- Prueba de ReservationFactory ---\n");

var (factRes, factStrategy) = ReservationFactory.CreateReservation("R004", "Ana Gómez", "Suite", 4, "temporadaAlta");
factRes.TotalCost = factStrategy.CalculateCost(factRes);
Console.WriteLine($"Factory creó: ID={factRes.Id}, Guest={factRes.GuestName}, Nights={factRes.Nights}");
Console.WriteLine($"Estrategia asignada: {factStrategy.GetType().Name}");
Console.WriteLine($"Costo calculado: ${factRes.TotalCost:F2}\n");

try
{
    ReservationFactory.CreateReservation("Rxxx", "Invitado", "Simple", 1, "tarifaInvalida");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Excepción controlada correctamente: {ex.Message}\n");
}

Console.WriteLine("--- EJERCICIO 2: PROCESAMIENTO COMPLETO ---\n");

var repository = new ReservationRepository();
var observers = new List<IReservationObserver>
{
    new EmailReservationNotifier(),
    new SmsReservationNotifier()
};

var processor = new ReservationProcessor(repository, observers);

processor.ProcessReservation("R005", "Pedro Martínez", "Suite Presidencial", 3, "temporadaAlta");

var savedReservation = repository.GetById("R005");
Console.WriteLine($"[VERIFICACIÓN] Reserve R005 en repositorio: Status={savedReservation?.Status}, " +
                  $"TotalCost=${savedReservation?.TotalCost:F2}\n");

Console.WriteLine("--- EJERCICIO 3: HOTELMANAGER CORREGIDO (SOLID) ---\n");

var correctedManager = new CorrectedHotelManager(repository, observers);

correctedManager.HandleReservation(
    "R006", "Laura Jiménez", new CorporateRateStrategy(), 4);

var savedR6 = repository.GetById("R006");
Console.WriteLine($"\n[VERIFICACIÓN] Reserve R006 en repositorio: Guest={savedR6?.GuestName}, " +
                  $"Status={savedR6?.Status}, TotalCost=${savedR6?.TotalCost:F2}\n");

Console.WriteLine("=== FIN ===");
