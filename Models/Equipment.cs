using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BssenTextRPG.Models;

public class Equipment : Item
{
    #region 프로퍼티
    //장비 슬롯
    public EquipmentSlot Slot { get; private set; }

    //공격력  보너스
    public int AttackBonus { get; private set; }

    //방어력 보너스
    public int DefenseBonus { get; private set; }
    #endregion

    #region 생성자
    public Equipment(
        string name,
        string description,
        int price,
        EquipmentSlot slot,
        int attackBonus = 0,
        int defenseBonus = 0)
        : base(name, description, price, slot == EquipmentSlot.Weapon ? ItemType.Weapon : ItemType.Armor)
    {
        Slot = slot;
        AttackBonus = attackBonus;
        DefenseBonus = defenseBonus;
    }
    #endregion


    public override bool Use(Player player)
    {
        //장비 착용 로직 구현
        player.EquipItem(this);
        return true;
    }

    #region 장착아이템 생성 메서드
    //무기 생성 메서드
    public static Equipment CreateWeapon(string weaponType)
    {
        switch (weaponType)
        {
            case "목검":
                return new Equipment(
                    name: "목검",
                    description: "기본 무기",
                    price: 100,
                    slot: EquipmentSlot.Weapon,
                    attackBonus: 5);
            case "철검":
                return new Equipment(
                    name: "철검",
                    description: "강력한 무기",
                    price: 500,
                    slot: EquipmentSlot.Weapon,
                    attackBonus: 15);
            case "전설검":
                return new Equipment(
                    name: "전설검",
                    description: "전설의 무기",
                    price: 1000,
                    slot: EquipmentSlot.Weapon,
                    attackBonus: 25);
            default:
                return null;

        }
    }

    //방어구 생성 메서드
    public static Equipment CreateArmor(string armorType)
    {
        switch (armorType)
        {
            case "천갑옷":
                return new Equipment(
                    name: "천갑옷",
                    description: "기본 방어구",
                    price: 100,
                    slot: EquipmentSlot.Armor,
                    defenseBonus: 5);
            case "철갑옷":
                return new Equipment(
                    name: "철갑옷",
                    description: "강력한 방어구",
                    price: 500,
                    slot: EquipmentSlot.Armor,
                    defenseBonus: 20);
            case "전설갑옷":
                return new Equipment(
                    name: "전설갑옷",
                    description: "전설의 방어구",
                    price: 1000,
                    slot: EquipmentSlot.Armor,
                    defenseBonus: 30);
            default:
                return null;
        }
    }
    #endregion 
}
