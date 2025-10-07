using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Please
{
    public static class Customer
    {
        private static Random Random = new Random();
        public static DrinkFactory.Drink DrinkToOrder { get; set; }
        public static List<IngredientFactory.PlusIngredient> Requires { get; set; }
        public static List<DrinkFactory.Drink> Orderables { get; set; }

        static Customer()
        {
            DrinkToOrder = new DrinkFactory.Drink();
            Orderables = new List<DrinkFactory.Drink>();
        }
        


        public static void MakeDrinkToOrder()//주문가능목록에서 하나 랜덤으로 뽑아서 주문할 음료에 할당하는 매서드.
        {
            int a = Random.Next(0, Orderables.Count);
            DrinkToOrder = Orderables[a];
        }

        public static void MakeRequires()//추가재료목록에서 하나 랜덤으로 뽑아서 요구사항에 할당하는 매서드.
        {
            int a = Random.Next(0, IngredientFactory.PlusIngredients.Count);
            Requires.Add(IngredientFactory.PlusIngredients[a]);
        }

        public static void CheckMenu()
        {

        }

        public static void GiveMoney()
        {
            
        }

    }
}
