using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public static int Payment { get; set; }
        static Customer()
        {
            DrinkToOrder = new DrinkFactory.Drink();
            Orderables = new List<DrinkFactory.Drink>();
            Requirement = null;
        }

        public static void Order()
        {
            SceneManager.FarCustomerScene();
            SceneManager.CloseCustomerScene();
            SceneManager.ReadyToTalk();
            DrinkToOrder.Recipe.Clear();
            Player.Menu.Recipe.Clear();                    
            MakeDrinkToOrder();
            MakeRequires();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{DrinkToOrder.Name} ");
            Console.ResetColor();
            Console.Write($"하나 주세요. ");
            if (Requirement != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{Requirement.Name}");
                Console.ResetColor();
                Console.WriteLine("도 추가해서요!");
            }            
        }


        public static void MakeDrinkToOrder()//주문가능목록에서 하나 랜덤으로 뽑아서 주문할 음료에 할당하는 매서드.(전체 음료 종류 수가 바뀔 시 여기를 변경해줘야 합니다!)
        {            
            DrinkToOrder.Recipe.Clear();
            int orderLock = 0;//잠겨있는 메뉴 갯수(타입 기준 맨 뒤부터)
            switch (Gamemanager.Days)
            {
                case 1: orderLock = 6; break;
                case 2: orderLock = 5; break;
                case 3: orderLock = 3; break;
                case 4: orderLock = 2; break;
                default: orderLock = 0; break;
            }
            int i = Random.Next(0, (Orderables.Count - orderLock));            
            DrinkToOrder = Orderables[i].MakeClone();
        }

        public static void MakeRequires()//추가재료목록에서 하나 랜덤으로 뽑아서 요구사항에 할당하는 매서드.(전체 추가 재료 종류 수가 바뀔 시 여기를 변경해줘야 합니다!)
        {
            Requirement = null;
            int j = Random.Next(0,2);
            if (j > 0)
            {

                int orderLock = 0;
                switch (Gamemanager.Days)
                {
                    case 1: orderLock = 2; break;
                    case 2: orderLock = 1; break;
                    case 3: orderLock = 1; break;
                    case 4: orderLock = 1; break;
                    default: orderLock = 0; break;
                }
                int i = Random.Next(0, IngredientFactory.PlusIngredients.Count - orderLock);
                Requirement = IngredientFactory.PlusIngredients[i].MakeClone();
                DrinkToOrder.Recipe.Add(Requirement);
            }
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
                    SceneManager.ReadyToTalk();
                    Console.WriteLine("                                               ");
                    SceneManager.ReadyToTalk();
                    Console.WriteLine("           최고에요!");
                    GiveMoney();
                    SceneManager.ReadyToNarration();
                    Console.WriteLine($"손님이 만족하며 떠납니다. [수입 +{Payment}$]");
                    Thread.Sleep(2000);
                    Order();
                }
                else
                {
                    SceneManager.ReadyToTalk();
                    Console.WriteLine("                                               ");
                    SceneManager.ReadyToTalk();
                    Console.WriteLine("           이게 뭐야?!");
                    SceneManager.ReadyToNarration();
                    Console.WriteLine($"손님이 불평하며 떠납니다. [수입 없음]");
                    Thread.Sleep(2000);
                    Order();
                }
            }
            else
            {
                SceneManager.ReadyToTalk();
                Console.WriteLine("                                               ");
                SceneManager.ReadyToTalk();
                Console.WriteLine("           이게 뭐야?!");
                SceneManager.ReadyToNarration();
                Console.WriteLine($"손님이 불평하며 떠납니다. [수입 없음]");
                Thread.Sleep( 2000 );
                Order();
            }
        }

        public static void GiveMoney()
        {
            Payment = Player.Menu.Price;
            Player.Money += Payment;
        }

    }
}
