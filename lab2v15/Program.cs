using System;
using System.Runtime.CompilerServices;

namespace Lab2v15
{
    public class House
    {
        private string _address = "N/A";
        private string _type = "Apartment";
        private int _floors = 1;

        public string Address
        {
            get => _address;
            set => _address = string.IsNullOrWhiteSpace(value) ? "N/A" : value;
        }

        public string Type
        {
            get => _type;
            set => _type = string.IsNullOrWhiteSpace(value) ? "Apartment" : value;
        }

        public int Floors
        {
            get => _floors;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(
                        nameof(value), "Кількість поверхів має бути більшою за 0.");
                _floors = value;
            }
        }

        public House(string address, string type, int floors)
        {
            Address = address;
            Type = type;
            Floors = floors;
            Console.WriteLine($"[Constructor] Створено будинок: {_address}");
        }

        public House() : this("N/A", "Apartment", 1)
        {
            Console.WriteLine("[Constructor] Викликано конструктор за замовчуванням");
        }

        public string GetHouseInfo()
        {
            return $"Адреса: {_address}, тип: {_type}, поверхів: {_floors}";
        }

        ~House()
        {
            Console.WriteLine($"[Destructor] Об'єкт знищено: {_address}");
        }
    }

    internal class Program
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void CreateAndUseObjects()
        {
            House house1 = new House();
            House house2 = new House("вул. Соборна, 12", "Private house", 2);
            House house3 = new House("вул. Київська, 5", "Apartment building", 9);

            Console.WriteLine(house1.GetHouseInfo());
            Console.WriteLine(house2.GetHouseInfo());
            Console.WriteLine(house3.GetHouseInfo());

            try
            {
                house1.Floors = 0;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Помилка валідації: {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("--- Creating objects ---");
            CreateAndUseObjects();
            Console.WriteLine("--- Objects created ---");

            Console.WriteLine("--- End of Main, preparing for GC ---");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Console.WriteLine("--- GC finished ---");
         }
    }
}