using System;
using System.Collections.Generic;
using System.Linq;

// Представляет отдельный номер в отеле
class Room
{
    public int Number { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public bool IsBooked { get; set; }
}

// Представляет бронирование, сделанное гостем
class Booking
{
    public int BookingId { get; set; }
    public string GuestName { get; set; }
    public int RoomNumber { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
}

// Управляет номерами и бронированиями в отеле
class BookingSystem
{
    private List<Room> rooms;
    private List<Booking> bookings;

    // Конструктор для инициализации списков
    public BookingSystem()
    {
        rooms = new List<Room>();
        bookings = new List<Booking>();
    }

    // Добавляет номер в список доступных номеров
    public void AddRoom(Room room)
    {
        rooms.Add(room);
    }

    // Создает бронирование для гостя
    public void MakeBooking(string guestName, int roomNumber, DateTime checkInDate, DateTime checkOutDate)
    {
        var room = rooms.FirstOrDefault(r => r.Number == roomNumber && !r.IsBooked);
        if (room == null)
        {
            Console.WriteLine("Номер недоступен.");
            return;
        }

        room.IsBooked = true;
        var booking = new Booking
        {
            BookingId = bookings.Count + 1,
            GuestName = guestName,
            RoomNumber = roomNumber,
            CheckInDate = checkInDate,
            CheckOutDate = checkOutDate
        };

        bookings.Add(booking);
        Console.WriteLine($"Бронирование успешно создано: {booking.BookingId}");
    }

    // Отменяет бронирование по его идентификатору
    public void CancelBooking(int bookingId)
    {
        var booking = bookings.FirstOrDefault(b => b.BookingId == bookingId);
        if (booking != null)
        {
            var room = rooms.FirstOrDefault(r => r.Number == booking.RoomNumber);
            if (room != null)
            {
                room.IsBooked = false;
            }

            bookings.Remove(booking);
            Console.WriteLine("Бронирование отменено.");
        }
        else
        {
            Console.WriteLine("Бронирование не найдено.");
        }
    }

    // Выводит информацию обо всех доступных номерах
    public void DisplayRooms()
    {
        foreach (var room in rooms)
        {
            Console.WriteLine($"Номер: {room.Number}, Тип: {room.Type}, Цена: {room.Price}, Забронирован: {room.IsBooked}");
        }
    }

    // Выводит информацию о всех бронированиях
    public void DisplayBookings()
    {
        foreach (var booking in bookings)
        {
            Console.WriteLine($"ID: {booking.BookingId}, Клиент: {booking.GuestName}, Номер: {booking.RoomNumber}, Заезд: {booking.CheckInDate}, Выезд: {booking.CheckOutDate}");
        }
    }
}

// Основная программа
class Program
{
    static void Main()
    {
        // Создание системы бронирования
        BookingSystem bookingSystem = new BookingSystem();

        // Добавление некоторых номеров в систему
        bookingSystem.AddRoom(new Room { Number = 101, Type = "Одноместный", Price = 1000 });
        bookingSystem.AddRoom(new Room { Number = 102, Type = "Двухместный", Price = 1500 });

        // Основной цикл меню
        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Просмотреть все номера");
            Console.WriteLine("2. Бронировать номер");
            Console.WriteLine("3. Отменить бронь");
            Console.WriteLine("4. Просмотреть все бронирования");
            Console.WriteLine("5. Выход");

            Console.Write("Выберите опцию: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    bookingSystem.DisplayRooms();
                    break;
                case "2":
                    TryMakeBooking(bookingSystem);
                    break;
                case "3":
                    TryCancelBooking(bookingSystem);
                    break;
                case "4":
                    bookingSystem.DisplayBookings();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }

    // Вспомогательный метод для попытки создания бронирования
    static void TryMakeBooking(BookingSystem bookingSystem)
    {
        Console.Write("Имя клиента: ");
        string guestName = Console.ReadLine();

        Console.Write("Номер комнаты: ");
        int roomNumber = Convert.ToInt32(Console.ReadLine());

        Console.Write("Дата заезда (гггг-мм-дд): ");
        DateTime checkInDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Дата выезда (гггг-мм-дд): ");
        DateTime checkOutDate = DateTime.Parse(Console.ReadLine());

        bookingSystem.MakeBooking(guestName, roomNumber, checkInDate, checkOutDate);
    }

    // Вспомогательный метод для попытки отмены бронирования
    static void TryCancelBooking(BookingSystem bookingSystem)
    {
        Console.Write("ID бронирования: ");
        int bookingId = Convert.ToInt32(Console.ReadLine());

        bookingSystem.CancelBooking(bookingId);
    }
}
