using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BssenTextRPG.Models;

public class Player : Character
{
    #region 프로퍼티
    //직업
    public JobType Job {  get; private set; }
    //골드
    public int Gold { get; private set; }
    //장착 무기
    public Equipment? EquippedWeapon { get; private set; }
    //장착 방어구
    public Equipment? EquippedArmor { get; private set; }
    #endregion

    #region 생성자
    public Player(string name, JobType job) : base(
    name: name,
    maxHp: GetInitHp(job),
    maxMp: GetInitMp(job),
    attackPower: GetInitAttackPower(job),
    defense: GetInitDefense(job),
    level: 1)
    {
        Job = job;
        Gold = 1000;
    }
    #endregion

    #region 직업별 초기 스텟
    private static int GetInitHp(JobType job)
    {
        switch (job)
        {
            case JobType.Warrior: return 150;
            case JobType.Archer: return 100;
            case JobType.Wizard:return 80;
            default: return 100;    
        }
    }

    private static int GetInitMp(JobType job)
    {
        switch (job)
        {
            case JobType.Warrior: return 30;
            case JobType.Archer: return 50;
            case JobType.Wizard: return 100;
            default: return 30;
        }
    }

    private static int GetInitAttackPower(JobType job) =>
        job switch
        {
            JobType.Warrior => 20,
            JobType.Archer => 30,
            JobType.Wizard => 40,
            _ => 20
        };

    private static int GetInitDefense(JobType job) =>
        job switch
        {
            JobType.Warrior => 15,
            JobType.Archer => 10,
            JobType.Wizard => 5,
            _ => 15
        };
    #endregion

    #region 매서드
    public override void DisplayInfo()
    {
        //base.DisplayInfo();
        Console.Clear();
        Console.WriteLine($"====={Name} 정보=====");
        Console.WriteLine($"Level : {Level}");
        Console.WriteLine($"HP : {CurHp}/{MaxHp}");
        Console.WriteLine($"MP : {CurMp}/{MaxMp}");

        int attackBonus = EquippedWeapon != null ? EquippedWeapon.AttackBonus : 0;
        int defenseBonus = EquippedArmor != null ? EquippedArmor.DefenseBonus : 0;

        Console.WriteLine($"ATK : {AttackPower} + {attackBonus}");
        Console.WriteLine($"DEF : {Defense} + {defenseBonus}");
        Console.WriteLine($"골드 : {Gold}");
        Console.WriteLine($"=====================");

        //장착 아이템 목록
        if(EquippedWeapon != null && EquippedArmor != null)
        {
            Console.WriteLine($"\n[장착중인 장비 목록]");
            if(EquippedWeapon != null)
            {
                Console.WriteLine($"무기 : {EquippedWeapon.Name}");
            }
            if(EquippedArmor != null)
            {
                Console.WriteLine($"방어구 : {EquippedArmor.Name}");
            }
        }
    }

    //기본 공격 메서드 (override)
    public override int Attack(Character target)
    {
        //장착한 아이템에 따른 공격력 증가 적용
        int attackDamage = AttackPower;

        //null 병합 연산자 : ??
        //int? a = null; : nullable int : 변수가 null일 수 있음
        //int b = a ?? 10; : a가 null이면 10을 대입, 아니면 a의 값을 대입
        attackDamage += EquippedWeapon?.AttackBonus ?? 0;

        //if(EquippedWeapon != null)
        //{
        //    attackDamage += EquippedWeapon.AttackBonus;
        //}


        return target.TakeDamage(attackDamage);
    }

    //스킬 공격 (MP 소모) : Player 전용 메서드
    public int SkillAttack(Character target)
    {
        int mpCost = 15;

        //스킬 공격 : 일반 공격의 1.5배 데미지
        int totalDamage = (int)(AttackPower * 1.5);
        totalDamage +=   EquippedWeapon?.AttackBonus ?? 0; //null 병합 연산자

        //MP소모
        CurMp -= mpCost;

        //데미지 전달
        return target.TakeDamage(totalDamage);
    }

    //골드 획득 메서드
    public void GainGold(int amount)
    {
        Gold += amount;
        Console.WriteLine($"골드 +{amount} 획득! 현재 골드 : {Gold}");
    }

    //골드 차감 메서드
    public void spendGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
        }
    }

    //장비 착용
    public void EquipItem(Equipment newEquipment) 
    {
        Equipment? prevEquipment = null;

        switch (newEquipment.Slot)
        {
            case EquipmentSlot.Weapon:
                prevEquipment = EquippedWeapon;
                EquippedWeapon = newEquipment;
                break;
            case EquipmentSlot.Armor:
                prevEquipment = EquippedArmor;
                EquippedArmor = newEquipment;
                break;
        }

        //이전 장비 해제 메시지
        if(prevEquipment != null)
        {
            Console.WriteLine($"{prevEquipment.Name} 장착 해제");
        }
        Console.WriteLine($"{newEquipment.Name} 장착 완료");
    }

    //장비 착용
    public Equipment? UnequipItem(EquipmentSlot slot)
    {
        Equipment? equipment = null;
        switch(slot)
        {
            case EquipmentSlot.Weapon:
                equipment = EquippedWeapon;
                EquippedWeapon = null;
                break;
            case EquipmentSlot.Armor:
                equipment = EquippedArmor;
                EquippedArmor = null;
                break;
        }
        if(equipment != null)
        {
            Console.WriteLine($"{equipment.Name} 장착 해제");
        }
        return equipment;
    }

    #endregion
}
