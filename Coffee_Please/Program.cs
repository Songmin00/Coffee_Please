using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Please
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Gamemanager gm = new Gamemanager();
            IngredientFactory ingredientFactory = new IngredientFactory();// 재료 팩토리 객체 생성.
            DrinkFactory drinkFactory = new DrinkFactory();// 음료 팩토리 객체 생성.
            ingredientFactory.MakeAllIngredients();// 재료 객체 종류별로 만들어서 딕셔너리에 담기.
            drinkFactory.MakeFirstDayDrinks();// 첫날 해금된 기본 음료를 손님 주문 목록에 담기.
            gm.KeySetting();
            Gamemanager.StartDay();
            
            
        }
    }
}
