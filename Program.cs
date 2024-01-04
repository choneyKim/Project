

using System.ComponentModel;
using System.Threading;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;


Player player = new Player();
ItemList items = new ItemList();
Shop shop = new Shop(player);
Scene startScene = new Scene(player, items);

startScene.MainScene();


public class Scene
{
    private Player player;
    private Shop shop;
    private Scene(Player player, ItemList items)
    {
        this.player = player;
        this.shop = shop;
    }

    public void MainScene()
    {
        bool isInt;
        int selectNum;
        bool isselect = false;
        Console.WriteLine();
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");


        while (!isselect)
        {
            Console.SetCursorPosition(2, 9);
            string Input = Console.ReadLine();
            isInt = int.TryParse(Input, out selectNum);
            if (selectNum == 1)
            {
                Console.Clear();
                isselect = true;
                StatusScene();
            }
            else if (selectNum == 2)
            {
                Console.Clear();
                isselect = true;
                InventoryScene();
            }
            else if (selectNum == 3)
            {
                Console.Clear();
                isselect = true;
                ShopScene();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

    }
    public void StatusScene()
    {
        bool isInt;
        bool isselect = false;
        int selectNum;
        Console.WriteLine();
        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
        Console.WriteLine();
        Console.WriteLine($"\tLv. {player.level:D2}");
        Console.WriteLine($"\tChad ( {player.chad} )");
        Console.WriteLine($"\t공격력 : {player.attackPoint}");
        Console.WriteLine($"\t방어력 : {player.armorPoint}");
        Console.WriteLine($"\t체  력 : {player.health}");
        Console.WriteLine($"\tGold : {player.gold}");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        while (!isselect)
        {
            Console.SetCursorPosition(2, 14);
            string Input = Console.ReadLine();
            isInt = int.TryParse(Input, out selectNum);
            switch (selectNum)
            {
                case 0:
                    Console.Clear();
                    isselect = true;
                    MainScene();
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }


    }
    public void InventoryScene()
    {
        bool isInt;
        bool isselect = false;
        int selectNum;
        Console.WriteLine();
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        Console.WriteLine();
        Console.WriteLine("1. 장착 관리");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        while (!isselect)
        {
            Console.SetCursorPosition(2, 10);
            string Input = Console.ReadLine();
            isInt = int.TryParse(Input, out selectNum);
            switch (selectNum)
            {
                case 0:
                    Console.Clear();
                    isselect = true;
                    MainScene();
                    break;

                case 1:
                    Console.Clear();
                    isselect = true;
                    EquipScene();
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }


    }
    public void EquipScene()
    {
        bool isInt;
        bool isselect = false;
        int selectNum;
        Console.WriteLine();
        Console.WriteLine("인벤토리 - 장착 관리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        Console.WriteLine();
        //for문 이용해서 아님 foreach? 보유 아이템 열거
        Console.WriteLine("1. 장착 관리");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        while (!isselect)
        {
            Console.SetCursorPosition(2, 10);
            string Input = Console.ReadLine();
            isInt = int.TryParse(Input, out selectNum);
            switch (selectNum)
            {
                case 0:
                    Console.Clear();
                    isselect = true;
                    MainScene();
                    break;

                case 1:
                    Console.Clear();
                    isselect = true;
                    MainScene();
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }


    }
    public void ShopScene()
    {
        bool isInt;
        bool isselect = false;
        int selectNum;
        int itemNum;
        Console.WriteLine();
        Console.WriteLine("상점");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.gold} G");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        itemNum = Item.DisplayItems();
        Console.WriteLine();
        Console.WriteLine("1. 아이템구매");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        while (!isselect)
        {
            Console.SetCursorPosition(2, 13 + itemNum);
            string Input = Console.ReadLine();
            isInt = int.TryParse(Input, out selectNum);
            switch (selectNum)
            {
                case 0:
                    Console.Clear();
                    isselect = true;
                    MainScene();
                    break;

                case 1:
                    Console.Clear();
                    isselect = true;
                    TradeScene();
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }


    }
    public void TradeScene()
    {
        bool isInt;
        bool isselect = false;
        int selectNum;
        int itemNum;
        Console.WriteLine();
        Console.WriteLine("상점 - 아이템 구매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.gold} G");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        itemNum = shop.DisplayItems2();
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        shop.BuyItem();
    }


}


public class Player
{
    public int level = 1;
    public string chad = "전사";
    public int attackPoint = 10;
    public int armorPoint = 5;
    public int health = 100;
    public int gold = 1500;


}
public class ItemList
{
    public List<Item> items = new List<Item>
        {
            new Item("무쇠갑옷", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500),
            new Item("수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", 1000),
            new Item("스파르타의 갑옷", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3000),
            new Item("낡은 검", 2, "쉽게 볼 수 있는 낡은 검입니다.", 600),
            new Item("청동 도끼", 5, "어디선가 사용했던 것 같은 도끼입니다.", 1500),
            new Item("스파르타의 창", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000)
        };
    public int DisplayItems()
    {
        int itemnumber = 0;
        foreach (var item in items)
        {
            Console.WriteLine($"- {item.Name}   | 방어력 +{item.Point}   | {item.Discription}     |  {item.Price} G");
            itemnumber++;
        }
        return itemnumber;
    }
}
class Item
{
    public string Name { get; set; }
    public int Point { get; set; }
    public string Discription { get; set; }
    public int Price { get; set; }
    public bool playeritem = false;

    public Item(string name, int point, string dis, int price)
    {
        Name = name;
        Point = point;
        Discription = dis;
        Price = price;
    }
}
public class Shop
{
    private Player player;
    private Scene scene;
    private Shop(Player player)
    {
        this.player = player;
    }
    List<Item> items = new List<Item>
        {
            new Item("무쇠갑옷", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500),
            new Item("수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", 1000),
            new Item("스파르타의 갑옷", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3000),
            new Item("낡은 검", 2, "쉽게 볼 수 있는 낡은 검입니다.", 600),
            new Item("청동 도끼", 5, "어디선가 사용했던 것 같은 도끼입니다.", 1500),
            new Item("스파르타의 창", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000)
        };
    public int DisplayItems()
    {
        int itemnumber = 0;
        foreach (var item in items)
        {
            Console.WriteLine($"- {item.Name}   | 방어력 +{item.Point}   | {item.Discription}     |  {item.Price} G");
            itemnumber++;
        }
        return itemnumber;
    }

public int DisplayItems2()
    {
        int itemnumber = 0;
        foreach (var item in items)
        {
            itemnumber++;
            Console.WriteLine($"- {itemnumber} {item.Name}   | 방어력 +{item.Point}   | {item.Discription}     |  {item.Price} G");
        }
        return itemnumber;
    }
    public void BuyItem()
    {
        bool isInt;
        int selectNum;
        bool isselect = false;
        while (!isselect)
        {
            Console.SetCursorPosition(2, 12 + items.Count);
            string Input = Console.ReadLine();
            isInt = int.TryParse(Input, out selectNum);
            if (selectNum == 0)
            {
                Console.Clear();
                isselect = true;
                scene.MainScene();
            }
            else if (player.gold >= items[selectNum - 1].Price)
            {
                playeritems.Add(items[selectNum - 1]);
                player.gold -= items[selectNum - 1].Price;
                Console.Clear();
                isselect = true;
                scene.TradeScene();
                Console.WriteLine($"'{items[selectNum].Name}'를 구매하였습니다.");

            }
            else
            {
                Console.WriteLine("금액이 부족합니다.");
            }

        }
    }
}