using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Please
{
    public static class Player
    {
        public static int Money { get; set; } = 100;
        public static List<IngredientFactory.Ingredient> Storage { get; set; } = new List<IngredientFactory.Ingredient>();

        public static DrinkFactory.Drink Menu { get; set; } = new DrinkFactory.Drink();
                

        public static void MakeMenu()
        {         
            Menu.Price = Menu.SetPrice(Menu);
        }

        public static void CheckRecipeBook()
        {

        }
    }
}
