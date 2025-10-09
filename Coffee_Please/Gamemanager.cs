using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Please
{
    public static class Gamemanager
    {
        
        public static Stopwatch frameWatch = new Stopwatch(); //프레임 갱신용 스톱워치.
        public static Stopwatch dailyWatch = new Stopwatch(); //스테이지 제한시간 측정용 스톱워치.
        public static List<ConsoleKey> ArrowKeys { get; set; }= new List<ConsoleKey>();

        public static List<ConsoleKey> TempKey { get; set; } = new List<ConsoleKey>();
        public static int Daytime { get; set; } = 60; //한 스테이지 영업시간 변경은 여기로!(현재 [60초]).
        public static int Days { get; set; } = 0;
        public static int DailyGain { get; set; }

        public static int PreMoney { get; set; }

        static Gamemanager()
        {
            KeySetting();
        }

        public static void KeySetting()
        {
            ArrowKeys = new List<ConsoleKey>();
            ArrowKeys.Add(ConsoleKey.UpArrow);
            ArrowKeys.Add(ConsoleKey.DownArrow);
            ArrowKeys.Add(ConsoleKey.LeftArrow);
            ArrowKeys.Add(ConsoleKey.RightArrow);
        }

      
        public static void DayWork()
        {
            PreMoney = Player.Money;
            Days += 1;
            
            dailyWatch.Restart();
            frameWatch.Restart();

            Console.WriteLine("하루 시작!");
            Customer.Order();
            while (dailyWatch.ElapsedMilliseconds <= Daytime * 1000) //Daytime 초만큼 시간제한
            {
                DailyUpdate(); //프레임 처리 및 키 입력 체크                
            }

            Console.WriteLine("하루 끝!");
        }

        public static void DailyUpdate()
        {
            
            if (frameWatch.ElapsedMilliseconds >= 100) //0.1초마다 실행
            {
                frameWatch.Restart();

                CheckKeyInput(); // 키 입력 처리

                if (dailyWatch.ElapsedMilliseconds % 1000 < 100) //1초마다
                {
                    int second = (int)dailyWatch.ElapsedMilliseconds / 1000; //경과시간 초 단위로 바꿔서
                    Console.Write($"\t\t\t\t\t\t남은 영업 시간: {Daytime - second}초\r"); //남은 영업 시간 출력
                    Console.Write($"\t\t\t보유자금: {Player.Money}$\r"); //보유 자금 출력
                    
                }
            }
        }
        static void CheckKeyInput() //키 입력을 처리하는 매서드
        {
            if (Console.KeyAvailable) //키가 눌렸는지 확인
            {                
                var key = Console.ReadKey(true).Key; //키 입력 읽기, true = 콘솔에 표시 안함

                if (ArrowKeys.Contains(key)) //입력한 키가 화살표 키라면
                {
                    TempKey.Add(key); //TempKey에 차례대로 담기

                }
                else if (key == ConsoleKey.Spacebar) //입력한 키가 스페이스바라면
                {
                    IdentifyCommand(); //재료판별 매서드 호출.
                }
                else if (key == ConsoleKey.Enter) //입력한 키가 엔터키라면
                {
                    Player.MakeMenu();
                    Customer.CheckMenu(); //음료 비교 매서드 호출(음료제출).
                }
            }
        }

        static void IdentifyCommand()//키 입력과 재료 커맨드를 대조하는 매서드.
        {
            int ingLock = 0;//잠겨있는 재료 개수(타입 기준 맨 뒤부터)
            switch (Days)
            {
                case 1: ingLock = 6; break;
                case 2: ingLock = 4; break;
                case 3: ingLock = 2; break;
                case 4: ingLock = 1; break;
                default: ingLock = 0; break;
            }
            bool isMatched = false; //맞는 재료를 찾았나 표시할 bool 변수.
        for (int i = 0; i < IngredientFactory.Ingredients.Count - ingLock; i++) //재료목록(딕셔너리)을 순회하면서
              {
                if (IngredientFactory.Ingredients[(IngredientType)i].Command.SequenceEqual(TempKey)) //입력한 TempKey와 커맨드가 같은 재료가 있다면
                {
                    isMatched = true; //찾았다고 표시하고
                    IngredientFactory.Ingredients[(IngredientType)i].PutIngredient(); //해당 재료 담는 매서드 호출.                                         
                    TempKey.Clear();
                }
            }
            if (isMatched == false) //못찾았다면
            {                
                Console.WriteLine("\n틀린 입력입니다!"); //오입력 메시지 출력.                
                TempKey.Clear();
            }

        }

        public static void NightWork()
        {
            Console.Clear();
            TakeRent();
            Console.WriteLine($"금일 영업이익 : {Player.Money - PreMoney}");
            ShowDailyUpgrade();
        }

        public static void TakeRent()
        {
            int dailyRent = Days * 20;
            Console.WriteLine($"임대료 {dailyRent}$가 지불되었습니다.");            
            Player.Money -= dailyRent;
        }

        public static void ShowDailyUpgrade()
        {
            if (Days < 5)
            switch (Days)
            {
                case 1: Console.WriteLine("추가된 재료 : 우유거품, 시럽"); ; break;
                case 2: Console.WriteLine("추가된 재료 : 딸기, 말차"); break;
                case 3: Console.WriteLine("추가된 재료 : 초콜릿"); break;
                case 4: Console.WriteLine("추가된 재료 : 휘핑크림"); break;                
            }
        }

    }
}
