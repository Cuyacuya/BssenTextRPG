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

    #region
    #endregion
}
