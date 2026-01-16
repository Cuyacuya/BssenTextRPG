using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BssenTextRPG.Models;

namespace BssenTextRPG.System;

internal class BattleSystem
{
#region 던전 입장 - 전투 실행
//전투 시작
//반환값 : 전투 승리 여부
public bool StartBattle(Player player, Enemy enemy)
{
    Console.Clear();
    Console.WriteLine("\n╔════════════════════════════════╗");
    Console.WriteLine("║       전투 시작!               ║");
    Console.WriteLine("╚════════════════════════════════╝\n");

    //등장한 적 캐릭터 정보 출력
    Console.WriteLine("적 등장!");
    enemy.DisplayInfo();

    //턴 변수 정의
    int turn = 1;

    //전투 루프
    while (player.IsAlive && enemy.IsAlive)
    {
        Console.WriteLine($"\n=====턴 {turn}=====");
        //플레이어 턴
        if(!PlayerTurn(player, enemy))
            {
                //플레이어 도망
                Console.WriteLine($"\n전투에서 도망쳤습니다...");
                return false; //전투 패배
            }
            //TODO : 적 사망 여부 판단
            //TODO : 적 턴
            turn++;
    }

    //전투 결과 반환
    //return player.IsAlive;

    if(player.IsAlive)
        {
            int gainGold = enemy.GoldReward;
            Console.WriteLine("\n전투에서 승리했습니다.");
            Console.WriteLine($"\n{gainGold} 골드를 획득했습니다.");

            player.GainGold(gainGold);
            return true;
        }
    else
        {
            Console.WriteLine($"전투에서 패배했습니다...");
            return false;
        }
}
    #region 플레이어 턴
    //플레이어 턴 (1. 공격, 2. 스킬, 3.도망)
    private bool PlayerTurn(Player player, Enemy enemy)
    {
        Console.WriteLine($"\n{player.Name}의 턴");
        Console.WriteLine($"HP : {player.CurHp}/{player.MaxHp} | MP : {player.CurMp}/{player.MaxMp}");
        Console.WriteLine("1. 공격\n2. 스킬\n3. 도망");

        while (true)
        {
            Console.Write($"행동을 선택해주세요. : ");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1": //공격
                    int damage = player.Attack(enemy);
                    Console.WriteLine($"{player.Name}의 공격! {enemy.Name}에게 {damage}의 피해를 입혔습니다.");
                    Console.WriteLine($"{enemy.Name}의 남은 HP : {enemy.CurHp}/{enemy.MaxHp}");
                    return true;
                case "2": //스킬
                    //스킬 사용 전 MP 체크
                    if (player.CurMp < 15) 
                    {
                        Console.WriteLine($"스킬을 사용할 수 없습니다.");
                        continue;
                    }
                    //스킬 발동
                    int skillDamage = player.SkillAttack(enemy);
                    Console.WriteLine($"{player.Name}의 공격! {enemy.Name}에게 {skillDamage}의 피해를 입혔습니다.");
                    Console.WriteLine($"{enemy.Name}의 남은 HP : {enemy.CurHp}/{enemy.MaxHp}");
                    return true;
                case "3": //도망
                    return false;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요. : ");
                    continue;
            }
        }
            
    }
    #endregion
}

#endregion
