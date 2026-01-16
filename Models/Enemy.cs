using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BssenTextRPG.Models;

internal class Enemy : Character
{
    #region 프로퍼티
    public int GoldReward { get; private set; }
    public Enemy(string name, int maxHp, int maxMp, int attackPower, int defense, int goldReward, int level)
        : base(name, maxHp, maxMp, attackPower, defense, level)
    {
        GoldReward = goldReward;
    }
    #endregion

    #region 메서드

    //적 생성 메서드 (레벨에 따른 난이도 조절)
    public static Enemy CreateEnemy(int playerlevel)
    {
        //난수 생성기
        Random random = new Random();

        //적 캐릭터의 레벨 (플레이어 레벨 +1)
        int enemyLevel = Math.Max(1, playerlevel + random.Next(-1, 2));
        //적 캐릭터의 동료 
        string[] enemyTypes = {"고블린", "오크", "트롤"};
        string enemyName = enemyTypes[random.Next(0, enemyTypes.Length)];

        //적의 스텟 (레벨에 비례)
        int maxHp = 50 + (enemyLevel - 1) * 20;
        int maxMp = 20 + (enemyLevel - 1) * 10;
        int attackPower = 10 + (enemyLevel - 1) * 5;
        int defense =  + (enemyLevel - 1) * 3;
        int goldReward = 20 + (enemyLevel - 1) * 10;

        return new Enemy($"Lv{enemyLevel} {enemyName}", maxHp, maxMp, attackPower, defense, enemyLevel, goldReward);
    }

    //적 캐릭터 정보 출력
    public override void DisplayInfo()
    {
        //base.DisplayInfo();
        Console.WriteLine($"====== {Name} 정보 ======");
        Console.WriteLine($"레벨 : {Level}");
        Console.WriteLine($"HP : {CurHp}/{MaxHp}");
        Console.WriteLine($"공격력 : {AttackPower}");
        Console.WriteLine($"방어력 : {Defense}");
    }
    #endregion
}

