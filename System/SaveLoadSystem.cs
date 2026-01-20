using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using BssenTextRPG.Data;
using BssenTextRPG.System;
using BssenTextRPG.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace BssenTextRPG.System;
internal class SaveLoadSystem
{
    //저장 경로 및 파일명
    private const string SaveFilePath = "savegame.json";

    //Json 직렬화 옵션 
    //직렬화 : 각 데이터가 메모리 여기저기에 흩어져있는 것을 한 곳에 모아 저장하는 것
    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        WriteIndented = true, //가독성 좋게 들여쓰기
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping //한글 지원
    }; 

    #region 저장 기능
    public static bool SaveGame(Player player, InventorySystem inventory)
    {
        try
        {
            //1. 게임 객체 (클래스) -> DTO (Data Transfer Object)  변환
            var saveData = new GameSaveData
            {
                Player = ConvertToPlayerData(player),
                Inventory = ConvertToItemData(inventory)
            };

            //2. DTO 객체 -> JSon 문자열로 반환
            string jsonString = JsonSerializer.Serialize(saveData, jsonOptions);

            //3. Json 문자열 -> 파일로 저장
            File.WriteAllText(SaveFilePath, jsonString); 
            return true;

        }
        catch (Exception e)
        {
            Console.WriteLine($"게임 저장 중 오류 발생: {e.Message}");
            return false;
        }
    }

    //Player -> PlayerData로 변환
    private static PlayerData ConvertToPlayerData(Player player)
    {
        return new PlayerData
        {
            Name =  player.Name,
            Job = player.Job.ToString(),
            Level = player.Level,
            CurHp = player.CurHp,
            MaxHp = player.MaxHp,
            CurMp = player.CurMp,
            MaxMp = player.MaxMp,
            AttackPower = player.AttackPower,
            Defense = player.Defense,
            Gold = player.Gold,
            EquipedWeaponName = player.EquippedWeapon?.Name,
            EquipedArmorName = player.EquippedArmor?.Name
        };
    }
    //Inventory -> ItemData로 변환
    private static List<ItemData> ConvertToItemData(InventorySystem inventory)
    {
        var itemDataList = new List<ItemData>();

        for (int i = 0; i < inventory.Count; i++)
        {
            var item = inventory.GetItem(i);
            if (item == null) continue;

            var itemData = new ItemData
            {
                Name = item.Name,
            };

            if (item is Equipment equipment)
            {
                itemData.ItemType = "Equipment";
                itemData.Slot = equipment.Slot.ToString();
            }
            else if (item is Consumable consumable)
            {
                itemData.ItemType = "Consumable";
                itemData.Slot = null;
            }
            itemDataList.Add(itemData);
        }
        return itemDataList;
    }
    #endregion

    #region 불러오기 기능
    //저장 파일 여부 확인
    public static bool IsSaveFileExists()
    {
        return File.Exists(SaveFilePath);
    }

    public static GameSaveData? LoadGame()
    {
        try
        {
            //1. Json 파일에서 문자열 읽기
            string jsonString = File.ReadAllText(SaveFilePath);

            //2. Json 문자열 -> DTO 객체로 변환 (역직렬화)
            var saveData = JsonSerializer.Deserialize<GameSaveData>(jsonString, jsonOptions);
            Console.WriteLine("게임 데이터가 로드 되었습니다.");
            return saveData;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    //PlayerData DTO를 Player 클래스로 변환 메서드
    public static Player LoadPlayer(PlayerData data)
    {
        //jobType을 문자열 -> enum 변환
        var job = Enum.Parse<JobType>(data.Job);
        //Player 객체 생성
        var player = new Player(data.Name, job);

        //스텟 설정
        player.Level = data.Level;
        player.CurHp = data.CurHp;
        player.MaxHp = data.MaxHp;
        player.CurMp = data.CurMp;
        player.MaxMp = data.MaxMp;
        player.AttackPower = data.AttackPower;
        player.Defense = data.Defense;
        player.Gold = data.Gold;
        return player;
    }

    //ItemData DTO 리스트를 Inventory 클래스로 변환 메서드
    public static InventorySystem LoadInventory(List<ItemData> itemDataList, Player player)
    {
        var inventory = new InventorySystem();

        foreach(var itemData in itemDataList)
        {
            Item? item = null;

            if(itemData.ItemType == "Equipment")
            {
                //장착 슬롯 홧인
                var slot = Enum.Parse<EquipmentSlot>(itemData.Slot);
                if(slot == EquipmentSlot.Weapon)
                {
                    item = Equipment.CreateWeapon(itemData.Name);
                }
                else if(slot == EquipmentSlot.Armor)
                {
                    item = Equipment.CreateArmor(itemData.Name);
                }
            }
            else if(itemData.ItemType == "Consumable")
            {
                item = Consumable.CreatePotion(itemData.Name);
            }

            if(item != null)
            {
                inventory.AddItem(item);
            }
        }
        return inventory;

    }

    //저장된 장착 아이템을 복원 메서드 (무기/방어구)
    public static void LoadEquippedItems(Player player, PlayerData data, InventorySystem inventory)
    {
        //무기 장착 복원
        if(!string.IsNullOrEmpty(data.EquipedWeaponName))
        {
            //인벤토리에서 같은 무기를 찾기
            for(int i=0;i<inventory.Count; i++)
            {
                //인벤토리에서 같은 무기 찾기
                var item = inventory.GetItem(i);
                if(item is Equipment equipment && equipment.Slot == EquipmentSlot.Weapon && equipment.Name == data.EquipedWeaponName)
                {
                    player.EquipItem(equipment);
                    break;
                }
            }
        }

        //방어구 장착 복원
        if(!string.IsNullOrEmpty(data.EquipedArmorName))
        {
            //인벤토리에서 같은 방어구를 찾기
            for(int i=0;i<inventory.Count; i++)
            {
                var item = inventory.GetItem(i);
                if(item is Equipment equipment && equipment.Slot == EquipmentSlot.Armor && equipment.Name == data.EquipedArmorName)
                {
                    player.EquipItem(equipment);
                    break;
                }
            }
        }
    }

    //아이템 생성 -> inventory에 추가
    #endregion
}
