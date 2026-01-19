using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BssenTextRPG.Models;

public class InventorySystem
{
    #region 프로퍼티
    //아이템 목록
    private List<Item> Items { get; set; }

    //아이템 갯수(읽기 전용)
    public int Count => Items.Count; 

    //public int count
    //{
    //    get { return Items.Count; }
    //}
    
    #endregion

    #region 생성자
    public InventorySystem()
    {
        Items = new List<Item>();
    }
    #endregion

    #region 아이템 관리
    //아이템 추가
    public  void AddItem (Item item)
    {
        Items.Add(item);
        Console.WriteLine($"{item.Name}을 인벤토리에 추가하였습니다.");
    }

    //아이템 삭제
    public bool RemoveItem(Item item)
    {
        if (Items.Remove(item))
        {
            Console.WriteLine($"{item.Name}을 인벤토리에서 제거하였습니다.");
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region 인벤토리 표시
    public void DisplayInventory()
    {
        Console.Clear();
        Console.WriteLine("\n╔══════════════════════════════════════════╗");
        Console.WriteLine("║            인벤토리                      ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");

        if(Items.Count == 0)
        {
            Console.WriteLine("인벤토리가 비어있습니다.");
            return;
        }
    }

    public void ShowInventoryMenu()
    {
        while (true)
        {
            DisplayInventory();
            Console.WriteLine($"\n 선택하세요.");
            Console.WriteLine("1. 아이템 사용하기");
            Console.WriteLine("2. 아이템 버리기");
            Console.WriteLine("0. 나가기");
            Console.Write("선택 : ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    //아이템 사용 로직
                    break;
                case "2":
                    //아이템 버리기 로직
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    break;
            }
        }
    }

    #endregion
}

