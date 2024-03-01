using System;
using System.Collections.Generic;

namespace BuyIT
{
    class Program
    {
        static void Main(string[] args)
        {
            // Produkty dostawców
            List<Provider> providers = new List<Provider>
            {
                new Provider
                {
                    Name = "Dostawca A",
                    Products = new List<Product>
                    {
                        new Product { Name = "Laptop X", Quantity = 1500, Price = 600 },
                        new Product { Name = "Phone Y", Quantity = 2500, Price = 500 },
                        new Product { Name = "Tablet Z", Quantity = 1000, Price = 500 }
                    },
                    DeliveryPrice = 100 // Dodajemy koszt dostawy dla Dostawcy A
                },
                new Provider
                {
                    Name = "Dostawca B",
                    Products = new List<Product>
                    {
                        new Product { Name = "Laptop Z", Quantity = 1200, Price = 750 },
                        new Product { Name = "Laptop X", Quantity = 1500, Price = 300 },
                        new Product { Name = "Phone Z", Quantity = 1800, Price = 350 },
                        new Product { Name = "Tablet X", Quantity = 800, Price = 450 },
                        new Product { Name = "Tablet Z", Quantity = 800, Price = 450 }
                    },
                    DeliveryPrice = 200 // Dodajemy koszt dostawy dla Dostawcy B
                },
                new Provider
                {
                    Name = "Dostawca C",
                    Products = new List<Product>
                    {
                        new Product { Name = "Laptop Z", Quantity = 1200, Price = 750 },
                        new Product { Name = "Laptop X", Quantity = 1500, Price = 800 },
                        new Product { Name = "Phone Z", Quantity = 1800, Price = 350 },
                        new Product { Name = "Tablet X", Quantity = 800, Price = 450 },
                        new Product { Name = "Tablet Y", Quantity = 0, Price = 450 }
                    },
                    DeliveryPrice = 150 // Dodajemy koszt dostawy dla Dostawcy C
                }
            };

            //Tworzymy listę dostępnych produktów
            List<string> availableProducts = new List<string>();
            foreach (var provider in providers)
            {
                foreach (var product in provider.Products)
                {
                    if (product.Quantity > 0 && !availableProducts.Contains(product.Name))
                    {
                        availableProducts.Add(product.Name);
                    }
                }
            }

            // Dostępne produkty do wyboru
            Console.WriteLine("Dostępne produkty:");
            for (int i = 0; i < availableProducts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableProducts[i]}");
            }

            // Pytamy użytkownika co chce kupić i w jakich ilościach
            Dictionary<string, int> selectedQuantities = new Dictionary<string, int>();

            Console.WriteLine("Podaj numery produktów po spacji (np. , 1 3 dla Laptop X i Tablet Z):");
            string[] selectedProductNumbers = Console.ReadLine().Split(' ');

            foreach (string selectedProductNumber in selectedProductNumbers)
            {
                if (int.TryParse(selectedProductNumber, out int productIndex) && productIndex >= 1 && productIndex <= availableProducts.Count)
                {
                    string selectedProductName = availableProducts[productIndex - 1];

                    Console.WriteLine($"Podaj ilości produktów {selectedProductName}:");
                    int quantity = Convert.ToInt32(Console.ReadLine());
                    selectedQuantities[selectedProductName] = quantity;
                }
                else
                {
                    Console.WriteLine($"Błędny nr produktu '{selectedProductNumber}'. Spróbuj ponownie.");
                    return;
                }
            }

            // Lista dostawców, którzy mają odpowiednią ilość produktów
            List<Provider> eligibleProviders = new List<Provider>();

            foreach (var provider in providers)
            {
                bool allProductsAvailable = true;
                decimal totalPrice = 0;

                foreach (var selectedQuantity in selectedQuantities)
                {
                    var product = provider.Products.Find(p => p.Name == selectedQuantity.Key);
                    if (product != null && product.Quantity >= selectedQuantity.Value)
                    {
                        totalPrice += product.Price * selectedQuantity.Value;
                    }
                    else
                    {
                        allProductsAvailable = false;
                        break;
                    }
                }

                if (allProductsAvailable)
                {
                    Console.WriteLine($"Dostawca '{provider.Name}' może zrealizować zamówienie w cenie {totalPrice:C}, Koszt dostawy: {provider.DeliveryPrice:C}");
                    eligibleProviders.Add(provider);
                }
            }

            if (eligibleProviders.Count == 0)
            {
                Console.WriteLine("Żaden dostawca nie może aktualnie zrealizować zamówienia.");
            }

            // Wyświetlanie dostępnych dostawców
            Console.WriteLine("Wybierz dostawcę z listy:");

            for (int i = 0; i < eligibleProviders.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {eligibleProviders[i].Name} ");
            }

            // Prośba o wybór dostawcy
            Console.WriteLine("Podaj numer dostawcy:");
            if (int.TryParse(Console.ReadLine(), out int providerIndex) && providerIndex >= 1 && providerIndex <= eligibleProviders.Count)
            {
                Provider selectedProvider = eligibleProviders[providerIndex - 1];
                Console.WriteLine($"Wybrano dostawcę: {selectedProvider.Name}");
                // Tutaj będzie złożenie zamówienia przy użyciu wybranego dostawcy
            }
            else
            {
                Console.WriteLine("Błędny numer dostawcy. Spróbuj ponownie.");
            }

            Console.ReadKey();
        }
    }
}
