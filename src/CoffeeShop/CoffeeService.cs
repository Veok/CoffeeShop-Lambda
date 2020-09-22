using System.Collections.Generic;

namespace CoffeeShop
{
    public interface ICoffeeService
    {
        IEnumerable<Coffee> GetAllAvailableCoffees();
    }

    public class CoffeeService : ICoffeeService
    {
        public IEnumerable<Coffee> GetAllAvailableCoffees()
        {
            return new[]
            {
                new Coffee
                {
                    Name = "Palus Putredinis",
                    Type = "Espresso - Blend",
                    Manufacturer = "NieCzapla"
                },
                new Coffee
                {
                    Name = "Marley One",
                    Type = "Arabica - Beans",
                },
                new Coffee
                {
                    Name = "Lucafe",
                    Type = "Arabica - Beans",
                },
            };
        }
    }
}