using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Please
{
    public enum DrinkType
    {
        Espresso, Americano, CaffeLatte, ChocolateLatte, StrawberryLatte
    }
    public class Drink
    {
        public DrinkType type;
        public List<Ingredient> Recipe {  get; set; }
        public int Price { get; set; }
    }
}
