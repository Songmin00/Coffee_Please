using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Please
{
    public class Gamemanager
    {
        public static Stopwatch frameWatch = new Stopwatch(); //프레임 갱신용 스톱워치.
        public static Stopwatch dailyWatch = new Stopwatch(); //스테이지 제한시간 측정용 스톱워치.
        public static List<ConsoleKey> ArrowKeys { get; set; }= new List<ConsoleKey>();

        public static List<ConsoleKey> TempKey { get; set; } = new List<ConsoleKey>();
        public static int Daytime { get; set; } = 60; //한 스테이지 영업시간 변경은 여기로!(현재 [60초]).

        public Gamemanager()
        {
            KeySetting();
        }

        public void KeySetting()
        {
            ArrowKeys = new List<ConsoleKey>();
            ArrowKeys.Add(ConsoleKey.UpArrow);
            ArrowKeys.Add(ConsoleKey.DownArrow);
            ArrowKeys.Add(ConsoleKey.LeftArrow);
            ArrowKeys.Add(ConsoleKey.RightArrow);
        }
      
        public static void StartDay()
        {
            dailyWatch.Restart();
            frameWatch.Restart();

            Console.WriteLine("하루 시작!");

            while (dailyWatch.ElapsedMilliseconds < Daytime * 1000) //Daytime 초만큼 시간제한
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
                    Console.Write($"\t\t남은 영업 시간: {Daytime - second}초\r"); //남은 영업 시간 출력
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
                    IdentifyCommand();//재료판별 매서드 호출
                }
                else if (key == ConsoleKey.Enter)
                {
                    Customer.CheckMenu();
                }
            }
        }

        static void IdentifyCommand()//키 입력과 재료 커맨드를 대조하는 매서드.
        {            
            bool isMatched = false; //맞는 재료를 찾았나 표시할 bool 변수.
            foreach (var ing in IngredientFactory.Ingredients) //재료목록(딕셔너리)을 순회하면서
            {
                if (ing.Value.Command.SequenceEqual(TempKey)) //입력한 TempKey와 커맨드가 같은 재료가 있다면
                {
                    isMatched = true; //찾았다고 표시하고
                    ing.Value.PutIngredient(); //해당 재료 담는 매서드 호출.                                         
                    TempKey.Clear();
                }
            }
            if (isMatched == false) //못찾았다면
            {                
                Console.WriteLine("\n틀린 입력입니다!"); //오입력 메시지 출력.                
                TempKey.Clear();
            }
        }

    }
}
