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
    //추상 메서드(abstract method) : 반드시 자식 클래스에 구현해야하는 메서드
    public abstract int Attack(Character target);

    //데미지 처리 메서드
    //가상 메서드(virtual method) : 자식 클래스에서 재정의(오버라이드) 가능
    public virtual int TakeDamage(int damage)
    {
        //방어력 적용
        int actualDamage = Math.Max(1, damage - Defense);
        CurHp = Math.Max(0, CurHp - actualDamage);

        return actualDamage;
    }

    //캐릭터 스텟 출력
    public virtual void DisplayInfo()
    {
        Console.Clear();
        Console.WriteLine($"====== {Name} 정보 ======");
        Console.WriteLine($"레벨 : {Level}");
        Console.WriteLine($"체력 : {CurHp}/{MaxHp}");
        Console.WriteLine($"마나 : {CurMp}/{MaxMp}");
        Console.WriteLine($"공격력 : {AttackPower}");
        Console.WriteLine($"방어력 : {Defense}");
        Console.WriteLine($"=====================");
    }

    //HP 회복 메서드
    public int HealHp(int amount)
    {
        int beforeHp = CurHp;
        //회복  후 현재 HP가 최대 HP를 넘지 않도록 설정
        CurHp = Math.Min(CurHp + amount, MaxHp);
        return CurHp - beforeHp; //실제 회복된 HP 반환
    }

    //MP 회복 메서드
    public int HealMp(int amount)
    {
        int beforeMp = CurMp;
        //회복 후 현재 MP가 최대 MP를 넘지 않도록 설정
        CurMp = Math.Min(CurMp + amount, MaxMp);
        return CurMp - beforeMp; //실제 회복된 MP 반환
    }
    #endregion


}
