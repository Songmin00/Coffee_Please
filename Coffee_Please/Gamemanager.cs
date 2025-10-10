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
        public static int Daytime { get; set; } = 90; //한 스테이지 영업시간 변경은 여기로!(현재 [90초]).
        public static int Days { get; set; } = 0;
        public static int DailyGain { get; set; } = 0;

        public static int PreMoney { get; set; } = 0;

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

        public static void EndGame()
        {
            Environment.Exit(0);
        }

        public static void StartGame()
        {            
                DayWork();            
        }

        public static void DayWork()
        {
            SceneManager.ClearScreenByFilling();
            PreMoney = Player.Money;
            Days += 1;
            
            dailyWatch.Restart();
            frameWatch.Restart();

            SceneManager.ReadyToShow();
            

            Customer.Order();
            while (dailyWatch.ElapsedMilliseconds <= Daytime * 1000) //Daytime 초만큼 시간제한
            {
                DailyUpdate(); //프레임 처리 및 키 입력 체크                
            }
            if (Player.Money < 0)
            {
                SceneManager.GameOverScene();
            }
            else
            {
                NightWork();
            }
        }

        public static void DailyUpdate()
        {
            
            if (frameWatch.ElapsedMilliseconds >= 100) //0.1초마다 실행
            {
                frameWatch.Restart();
                Console.SetCursorPosition(12, 2);
                Console.Write($"{Days}일차"); //진행중인 날짜 출력
                int second = (int)dailyWatch.ElapsedMilliseconds / 1000; //경과시간 초 단위로 바꿔서
                Console.SetCursorPosition(5, 4);
                Console.Write($"남은 영업 시간: {Daytime - second} 초"); //남은 영업 시간 출력     
                Console.SetCursorPosition(32, 25);
                Console.Write("1번 키 : 현재 음료 레시피,       2번 키 : 재료 커맨드 확인");
                Console.SetCursorPosition(130, 25);
                Console.Write($"소지금: {Player.Money} $"); //보유 자금 출력
                CheckKeyInput(); // 키 입력 처리
            }
        }
        static void CheckKeyInput() //키 입력을 처리하는 매서드
        {
            if (Console.KeyAvailable) //키가 눌렸는지 확인
            {                
                var key = Console.ReadKey(true).Key; //키 입력 읽기, true = 콘솔에 표시 안함

                if (ArrowKeys.Contains(key)) //입력한 키가 화살표 키라면
                {
                    SceneManager.ReadyToShow();
                    Console.WriteLine("                                   ");
                    SceneManager.ReadyToShow();
                    switch (key)
                    {
                        case ConsoleKey.LeftArrow: Console.Write("        ←"); ; break;
                        case ConsoleKey.RightArrow: Console.Write("        →"); ; break;
                        case ConsoleKey.UpArrow: Console.Write("        ↑"); ; break;
                        case ConsoleKey.DownArrow: Console.Write("        ↓"); ; break;
                    }



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
                else if (key == ConsoleKey.D1)
                {
                    SceneManager.RecipeBookScene();
                }
                else if (key == ConsoleKey.D2)
                {
                    SceneManager.CommandBookScene();
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
                SceneManager.ReadyToShow();
                Console.WriteLine("틀린 입력입니다!"); //오입력 메시지 출력.                
                TempKey.Clear();
            }

        }

        public static void NightWork()
        {
            SceneManager.ClearScreenByFilling();
            TakeRent();            
            SceneManager.ReadyToTalk();
            Console.WriteLine($"금일 영업이익 : {Player.Money - PreMoney} $");
            ShowDailyUpgrade();            
            SceneManager.PleasePress();
            Console.Write("다음 날로 넘어가려면 Enter 키를 누르세요");


            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                DayWork();
            }
        }

        public static void TakeRent()
        {
            int dailyRent = Days * 20;
            SceneManager.ReadyToShow();
            Console.WriteLine($"임대료 {dailyRent}$가 지불되었습니다.");            
            Player.Money -= dailyRent;
        }

        public static void ShowDailyUpgrade()
        {
            if (Days < 5)
            {
                SceneManager.ReadyToNarration();
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
}
