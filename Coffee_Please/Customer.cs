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
        public static IngredientFactory.Ingredient Requirement { get; set; }

        public static List<DrinkFactory.Drink> Orderables { get; set; }
        public static List<IngredientFactory.PlusIngredient> Requirables { get; set; }

        static Customer()
        {
            DrinkToOrder = new DrinkFactory.Drink();
            Orderables = new List<DrinkFactory.Drink>();
            Requirement = null;
        }
        public static void Order()
        {
            MakeDrinkToOrder();
            MakeRequires();            
            Console.WriteLine($"{DrinkToOrder.Name} 하나 주세요.");            
            Console.WriteLine($"{Requirement.Name}도 추가해서요.");

        }


        public static void MakeDrinkToOrder()//주문가능목록에서 하나 랜덤으로 뽑아서 주문할 음료에 할당하는 매서드.
        {
            int a = Random.Next(0, Orderables.Count);
            DrinkToOrder = Orderables[a];
        }

        public static void MakeRequires()//추가재료목록에서 하나 랜덤으로 뽑아서 요구사항에 할당하는 매서드.
        {
            int a = Random.Next(0, IngredientFactory.PlusIngredients.Count);
            Requirement = IngredientFactory.PlusIngredients[a];
            DrinkToOrder.Recipe.Add(Requirement);
        }

        public static void CheckMenu()
        {           
            bool isMatched = true; //틀린거 찾았나 표시할 bool 변수.
            if (Player.Menu.Recipe.Count == DrinkToOrder.Recipe.Count) //메뉴의 재료 수가 주문 레시피의 재료 수와 같다면
            {
                
                for (int i = 0; i < Player.Menu.Recipe.Count; i++) //메뉴 레시피 수만큼 재료를 순회하면서
                {                    
                    if (Player.Menu.Recipe[i].Type != DrinkToOrder.Recipe[i].Type) //두 재료의 타입이 다르면
                    {
                        isMatched = false; //다른거 찾았다고 표시하고 순회를 종료
                        break;
                    }
                }
                if (isMatched == true) //순회 끝난 후 틀린 게 없다면 발동
                {
                    Console.WriteLine("최고에요!");
                    GiveMoney();
                    Player.Menu.Recipe.Clear();
                }
                else
                {
                    Console.WriteLine("이게 뭐야?!");
                    Player.Menu.Recipe.Clear();
                }
            }
            else
            {
                Console.WriteLine("이게 뭐야?!");
                Player.Menu.Recipe.Clear();
            }
        }

        public static void GiveMoney()
        {
            Player.Money += DrinkToOrder.Price;
        }

    }
}
