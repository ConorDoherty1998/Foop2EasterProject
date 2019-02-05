using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foop2EasterProject
{
    class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DrinkType TypeOfDrink { get; set; }
         
        public enum DrinkType { Vodka, Whiskey, Rum };

        public Product() : this("Example", 10)
        {

        }
        public Product(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
        }
        public override string ToString()
        {
            return string.Format(Name);
        }
    }
}
