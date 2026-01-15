using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BssenTextRPG.Models;

//캐릭터 기본 추상 클래스
public abstract class Character
{
    #region 프로퍼티
    public string Name { get; protected set; }
    public int CurHp { get; protected set; }
    public int MaxHp { get;  protected set; }
    public int CurMp { get; protected set; }
    public int MaxMp { get; protected set; }

    public int AttackPower { get; protected set; }
    public int Defense { get; protected set; }
    public int Level { get; protected set; }
    public bool IsAlive => CurHp > 0;
    #endregion

    #region 생성자
    protected Character(string name, int maxHp, int maxMp, int attackPower, int defense, int level)
    {
        Name = name;
        MaxHp = maxHp;
        CurHp = maxHp;
        MaxMp = maxMp;
        CurMp = maxMp;
        AttackPower = attackPower;
        Defense = defense;
        Level = level;
    }
    #endregion

    #region 메서드
    //공통 메서드

    //캐릭터 스텟 출력
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"====== {Name} 정보 ======");
        Console.WriteLine($"레벨 : {Level}");
        Console.WriteLine($"체력 : {CurHp}/{MaxHp}");
        Console.WriteLine($"마나 : {CurMp}/{MaxMp}");
        Console.WriteLine($"공격력 : {AttackPower}");
        Console.WriteLine($"방어력 : {Defense}");
        Console.WriteLine($"=====================");
    }
    #endregion

}
