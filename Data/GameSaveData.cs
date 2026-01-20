using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BssenTextRPG.Data;
public class GameSaveData
{
    public PlayerData Player { get; set; }
    public List<ItemData> Inventory { get; set; } = new List<ItemData>();
}

public class PlayerData
{
    //기본 정보
    public string Name { get; set; }
    public string Job { get; set; }

    //스텟 정보
    public int Level { get; set; }
    public int CurHp { get; set; }
    public int MaxHp { get; set; }
    public int CurMp { get; set; }
    public int MaxMp { get; set; }
    public int AttackPower { get; set; }
    public int Defense { get; set; }
    public int Gold { get; set; }

    //장착 아이템
    public string? EquipedWeaponName { get; set; }
    public string? EquipedArmorName { get; set; }
}

public class ItemData
{
    public string ItemType { get; set; }
    public string Name { get; set; }
    public string? Slot { get; set; }
}