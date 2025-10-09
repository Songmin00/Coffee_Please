using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Please
{
    public class DrinkFactory
    {
        public enum DrinkType
        {
            Espresso, Americano, CaffeLatte,//첫날 해금 메뉴
            Cappuccino,//2일차(+우유거품)
            StrawberryLatte, MatchaLatte,//3일차(+딸기,말차)
            ChocolateLatte,//4일차(+초콜릿)            
            CaffeMocha, Einspanner//5일차(+휘핑크림)
        }
        public class Drink
        {
            public DrinkType Type { get; set; }
            public string Name { get; set; }
            public List<IngredientFactory.Ingredient> Recipe { get; set; } = new List<IngredientFactory.Ingredient>();
            public int Price { get; set; }

            public Drink() 
            {
                Recipe  = new List<IngredientFactory.Ingredient>();
            }

            public string SetName(DrinkType type) //음료 레시피에 재료 할당하는 매서드(음료 레시피 설정 및 수정은 여기로!).
            {
                string name = "";
                switch (type)
                {
                    case DrinkType.Espresso:
                        name = "에스프레소";
                        break;
                    case DrinkType.Americano:
                        name = "아메리카노";
                        break;
                    case DrinkType.CaffeLatte:
                        name = "카페라떼";
                        break;
                    case DrinkType.ChocolateLatte:
                        name = "초콜릿라떼";
                        break;
                    case DrinkType.StrawberryLatte:
                        name = "딸기라떼";
                        break;
                    case DrinkType.Cappuccino:
                        name = "카푸치노";
                        break;
                    case DrinkType.CaffeMocha:
                        name = "카페 모카";
                        break;
                    case DrinkType.Einspanner:
                        name = "아인슈페너";
                        break;
                    case DrinkType.MatchaLatte:
                        name = "말차 라떼";
                        break;
                }
                return name;
            }

            public Drink MakeClone()
            {
                Drink drink = new Drink();
                drink.Name = Name;
                drink.Type = Type;
                drink.Recipe = new List<IngredientFactory.Ingredient>(Recipe);
                return drink;
            }

            public List<IngredientFactory.Ingredient> SetRecipe(DrinkType type) //음료 레시피에 재료 할당하는 매서드(음료 레시피 설정 및 수정은 여기로!).
            {
                List<IngredientFactory.Ingredient> recipe = new List<IngredientFactory.Ingredient>();
                switch (type)
                {
                    case DrinkType.Espresso:
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Shot]);
                        break;
                    case DrinkType.Americano:
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Shot]);
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Water]);
                        break;
                    case DrinkType.CaffeLatte:
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Shot]);
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Milk]);
                        break;
                    case DrinkType.Cappuccino:
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Shot]);
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Milk]);
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.MilkFoam]);
                        break;
                    case DrinkType.Einspanner:
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Shot]);
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Water]);
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.WheepingCream]);
                        break;
                    case DrinkType.CaffeMocha:
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Shot]);
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Milk]);
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.WheepingCream]);
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Chocolate]);
                        break;
                    case DrinkType.ChocolateLatte:
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Chocolate]);
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Milk]);
                        break;
                    case DrinkType.StrawberryLatte:
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Strawberry]);
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Milk]);
                        break;
                    case DrinkType.MatchaLatte:
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Matcha]);
                        recipe.Add(IngredientFactory.Ingredients[IngredientType.Milk]);
                        break;
                }
                return recipe;
            }

            public int SetPrice(Drink drink)//음료에 들어간 재료값의 합 * 마진율을 음료값으로 할당하는 매서드(음료 마진율 수정은 여기로!).
            {
                int price = 0;
                for (int i = 0; i < drink.Recipe.Count; i++)
                {
                    price += drink.Recipe[i].Price;
                }
                price = price * 200 / 100; //현재 마진율은 약 [2.0]입니다(소수점 버림).
                return price;
            }
        }
        public void MakeOrdarables()//해금된 메뉴 주문가능목록에 넣기.
        {            
            for (int i = 0; i < Enum.GetValues(typeof(DrinkType)).Length; i++)
            {
                Customer.Orderables.Add(MakeDrink((DrinkType)i));
            }
        }

        public Drink MakeDrink(DrinkType type)
        {
            Drink drink = new Drink();
            drink.Type = type;
            drink.Name = drink.SetName(type);
            drink.Recipe = drink.SetRecipe(type);
            drink.Price = drink.SetPrice(drink);
            return drink;
        }
    }
}
