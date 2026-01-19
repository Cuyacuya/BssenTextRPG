using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BssenTextRPG.Utils;
using BssenTextRPG.Models;
using BssenTextRPG.System;


namespace BssenTextRPG.Data;

public class GameManager
{
    //싱글톤 패턴 (Singleton Pattern) 구현
    #region 싱글톤 패턴
    //싱글톤 인스턴스 (내부 접근용 변수 : 필드)
    private static GameManager instance;

    //외부에서 인스턴스에 접근할 수 있는 정적 속성(프로퍼티)
    public static GameManager Instance
    {
        get
        {
            //인스턴스가  없으면 새로 생성
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    private GameManager()
    {
        //클래스가 생성될 때 초기화 작업 수행 (생성자)

        //전투 시스템 초기화
        BattleSystem = new BattleSystem();

    }
    #endregion

    #region 프로퍼티
    //플레이어 캐릭텅
    public Player? Player { get; private set; }
    public bool IsRunning { get; private set; } = true;

    //전투 시스템
    public BattleSystem BattleSystem { get; private set; }

    //인벤토리 시스템
    public InventorySystem Inventory { get; private set; }
    #endregion


    #region 게임 시작/종료
    //게임 시작 메서드
    public void StartGame()
    {
        //타이틀 표시
        ConsoleUI.ShowTitle();
        Console.WriteLine("빡센 게임에 오신 것을 환영합니다!\n");

        //캐릭터 생성
        CreateCharacter();

        //인벤토리 초기화
        Inventory = new InventorySystem();

        //초기 아이템 지급
        SetupInitItems();


        //메인 게임 루프
        IsRunning = true;
        while(IsRunning)
        {
            ShowMainMenu();
            
        }

        //게임 종료 처리
        if (!IsRunning)
        {
            ConsoleUI.ShowGameOver();
        }
    }
    #endregion

    #region 캐릭터 생성
    private void CreateCharacter()
    {
        //이름 입력
        Console.Write("플레이어의 이름을 입력하세요 : ");
        string? name = Console.ReadLine(); //nullalbe 허용 (?)

        if (string.IsNullOrWhiteSpace(name))
        {
            name = "무명의 용사";
        }

        Console.WriteLine($"{name}님, 모험을 시작하겠습니다.");

        //직업 선택
        Console.WriteLine("직업을 선택하세요 : ");
        Console.WriteLine("1 : 전사 / 2 : 궁수 / 3 : 마법사");

        JobType job = JobType.Warrior; //기본값 설정

        while (true)
        {
            Console.WriteLine("선택 (1-3) : ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    job = JobType.Warrior;
                    break;
                case "2":
                    job = JobType.Archer;
                    break;
                case "3":
                    job = JobType.Wizard;
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                    continue;
            }
            break;
        }

        //입력한 이름과 선택한 직업으로 플레어 캐릭터 생성
        Player = new Player(name, job);
        Console.WriteLine($"\n{name}님, {job}직업으로 캐릭터가 생성되었습니다.");

        ////적 캐릭터 생성
        //Enemy enemy = Enemy.CreateEnemy(Player.Level);
        //enemy.DisplayInfo();

        ////전투 시스템 테스트
        //BattleSystem battleSystem = new BattleSystem();
        //bool playerWin = battleSystem.StartBattle(Player, enemy);

        //Player.DisplayInfo();
        ConsoleUI.PressAnyKey();
    }

    //초기 아이템 지급
    private void SetupInitItems()
    {
        //기본 장비
        Inventory.AddItem(Equipment.CreateWeapon("목검"));
        Inventory.AddItem(Equipment.CreateArmor("천갑옷"));

        //포션 지급
        Inventory.AddItem(Consumable.CreatePotion("체력포션"));
        Inventory.AddItem(Consumable.CreatePotion("체력포션"));
        Inventory.AddItem(Consumable.CreatePotion("마나포션"));

        Console.WriteLine("\n초기 장비가 지급되었습니다.");
        ConsoleUI.PressAnyKey();
    }
    #endregion


    #region 메인 메뉴
    public void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine(@"
╔═══════════════════════════════════════════════════════════════════════╗
║                            메인메뉴                                ║
╚═══════════════════════════════════════════════════════════════════════╝
");
        Console.WriteLine("\n1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine("4. 던전 입장(전투)");
        Console.WriteLine("5. 휴식(회복)");
        Console.WriteLine("6. 저장");
        Console.WriteLine("0. 게임 종료");
        Console.Write("\n(0-6) : ");
        string? input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Player.DisplayInfo();
                ConsoleUI.PressAnyKey();
                break;
            case "2":
                //인벤토리 구현
                Inventory.ShowInventoryMenu();
                break;
            case "3":
                //TODO : 상점 구현
                break;
            case "4":
                //던전 입장 및 전투 구현
                EnterDungeon();
                break;
            case "5":
                //TODO : 휴식 및 회복 구현
                break;
            case "6":
                //TODO : 저장 구현
                break;
            case "0":
                IsRunning = false;
                Console.WriteLine("\n게임을 종료합니다.");
                break;
            default: 
                Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                ConsoleUI.PressAnyKey();
                break;
        }
    }
    #endregion

    #region 메뉴 기능
    //던전 입장 
    public void EnterDungeon()
    {
        Console.Clear();
        Console.WriteLine("\n던전에 입장합니다...");

        //적 캐릭터 생성 
        Enemy enemy = Enemy.CreateEnemy(Player.Level);
        ConsoleUI.PressAnyKey();

        //전투 시작
        BattleSystem.StartBattle(Player, enemy);

        Console.WriteLine("\n 던전 탐험을 마치고 마을로 돌아갑니다...");
        ConsoleUI.PressAnyKey();
    }
    #endregion
}