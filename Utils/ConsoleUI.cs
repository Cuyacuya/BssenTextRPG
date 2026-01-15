using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BssenTextRPG.Utils;

// 콘솔 관련 UI 유틸리티를 담당하는 클래스
public class ConsoleUI
{
    // 타이틀 표시 메서드
    public static void ShowTitle()
    {
        Console.Clear();
        //@ : 뒤에 나오는 문자열을 보이는 그대로 출력하겠다라는 의미.
        Console.WriteLine(@" 
╔═══════════════════════════════════════════════════════════════════════╗
║                                                                       ║
║  ████████╗███████╗██╗  ██╗████████╗    ██████╗ ██████╗  ██████╗       ║
║  ╚══██╔══╝██╔════╝╚██╗██╔╝╚══██╔══╝    ██╔══██╗██╔══██╗██╔════╝       ║
║     ██║   █████╗   ╚███╔╝    ██║       ██████╔╝██████╔╝██║  ███╗      ║
║     ██║   ██╔══╝   ██╔██╗    ██║       ██╔══██╗██╔═══╝ ██║   ██║      ║
║     ██║   ███████╗██╔╝ ██╗   ██║       ██║  ██║██║     ╚██████╔╝      ║
║     ╚═╝   ╚══════╝╚═╝  ╚═╝   ╚═╝       ╚═╝  ╚═╝╚═╝      ╚═════╝       ║
║                                                                       ║
║                    턴제 전투 텍스트 RPG 게임                          ║
║                                                                       ║
╚═══════════════════════════════════════════════════════════════════════╝
");
    }

    //아무키나 누르면 계속 메시지 출력
    public static void PressAnyKey()
    {
        Console.WriteLine("\n아무 키나 누르면 계속 합니다...");
        Console.ReadKey(true); //입력한 키를 화면에 표시하지않는다.
    }

    //게임 오버 메시지 출력
    public static void ShowGameOver()
    {
        Console.WriteLine("\n╔══════════════════════════════════════════╗");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║            GAME OVER                     ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("╚══════════════════════════════════════════╝\n");
        Console.WriteLine("게임을 종료합니다...");
    }
}
