

using System.ComponentModel;
using System.Threading;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using static System.Formats.Asn1.AsnWriter;


Player player = new Player();
Shop shop = new Shop(player);
Scene startScene = new Scene(player, shop);


startScene.MainScene();


public class Scene
{
    private Player player;
    private Shop shop;

    public Scene(Player player, Shop shop)
    {

        this.player = player;
        this.shop = shop;
    }
    public void BuyItem(Player player)
    {

        bool isInt;
        int selectNum;
        bool isselect = false;
        while (!isselect)
        {
            Console.SetCursorPosition(2, 12 + shop.items.Count);
            string Input = Console.ReadLine();
            isInt = int.TryParse(Input, out selectNum);
            if (selectNum == 0)
            {
                Console.Clear();
                isselect = true;
                MainScene();
            }
            else if (selectNum > shop.items.Count||selectNum<1)
            {
                Console.Write("                                                       ");
                Console.WriteLine("\r잘못된 입력입니다.");
            }
            else if (shop.items[selectNum - 1].IsBuy == true)
            {
                Console.Write("                                                       ");
                Console.WriteLine("\r이미 구매한 아이템 입니다");

            }
            else if (player.gold >= shop.items[selectNum - 1].Price && shop.items[selectNum - 1].IsBuy == false)
            {
                shop.items[selectNum - 1].IsBuy = true;
                player.playeritems.Add(shop.items[selectNum - 1]);                
                player.gold -= shop.items[selectNum - 1].Price;
                Console.SetCursorPosition(0, 5);
                Console.WriteLine($"{player.gold} G");
                Console.SetCursorPosition(0, 8);
                shop.DisplayItems2();
                Console.SetCursorPosition(0, 13 + shop.items.Count);
                Console.Write("                                                       ");
                Console.WriteLine($"\r'{shop.items[selectNum-1].Name}'를 구매하였습니다.");
            }
            else
            {
                Console.Write("                                                       ");
                Console.WriteLine("\r금액이 부족합니다.");
            }
        }
    }
    public void SellItem(Player player)
    {

        bool isInt;
        int selectNum;
        float sellPrice;
        bool isselect = false;
        while (!isselect)
        {
            Console.SetCursorPosition(2, 13 + player.playeritems.Count);
            string Input = Console.ReadLine();
            isInt = int.TryParse(Input, out selectNum);
            if (selectNum == 0)
            {
                Console.Clear();
                isselect = true;
                MainScene();
            }
            else if(player.playeritems.Count == 0)
            {
                Console.SetCursorPosition(0, 5);
                Console.WriteLine($"{player.gold} G");
                Console.SetCursorPosition(0, 8);
                Console.WriteLine(" ");
                Console.SetCursorPosition(0, 14 + player.playeritems.Count);
                Console.Write("                                                       ");
                Console.WriteLine("\r인벤토리가 비어 있습니다.");
            }
            else if(player.playeritems.Count != 0) 
            {
                if (player.playeritems[selectNum - 1].IsBuy == true)
                {
                    foreach (var item in shop.items)
                    {
                        if (item.Name == player.playeritems[selectNum - 1].Name)
                        {
                            item.IsBuy = false;
                            sellPrice = item.Price * 0.85f;
                            player.gold += (int)sellPrice;
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 14 + player.playeritems.Count);
                            Console.Write("                                                       ");
                            Console.WriteLine("\r보유하지 않은 아이템 입니다.");
                            Console.SetCursorPosition(0, 15 + player.playeritems.Count);
                            Console.Write("                                                       ");
                        }
                    }
                    player.playeritems.Remove(player.playeritems[selectNum - 1]);
                    Console.SetCursorPosition(0, 5);
                    Console.WriteLine($"{player.gold} G");
                    Console.SetCursorPosition(0, 8);
                    player.DisplayPlayerItems();
                    Console.SetCursorPosition(0, 14 + player.playeritems.Count);
                    Console.Write("                                                       ");
                    Console.WriteLine("\r판매가 완료되었습니다.");
                }
            }
            
        }
    }
    public void EquipItem()
    {
        bool isInt;
        bool isselect = false;
        int selectNum;
        int itemNum = player.playeritems.Count;
        while (!isselect)
        {
            Console.SetCursorPosition(2, 10 + itemNum);
            string Input = Console.ReadLine();
            isInt = int.TryParse(Input, out selectNum);
            if (selectNum == 0)
            {
                Console.Clear();
                isselect = true;
                MainScene();
            }
            else if (player.playeritems[selectNum - 1].IsEquip == false)
            {
                player.playeritems[selectNum - 1].IsEquip = true;
                if (player.playeritems[selectNum - 1].Isweapon == true)
                {
                    player.attackPoint += player.playeritems[selectNum - 1].Point;
                    Console.Clear();
                    isselect = true;
                    EquipScene();
                }
                else
                {
                    player.armorPoint += player.playeritems[selectNum - 1].Point;
                    Console.Clear();
                    isselect = true;
                    EquipScene();
                }
            }
            else if (player.playeritems[selectNum - 1].IsEquip == true)
            {
                player.playeritems[selectNum - 1].IsEquip = false;
                if (player.playeritems[selectNum - 1].Isweapon == true)
                {
                    player.attackPoint -= player.playeritems[selectNum - 1].Point;
                    Console.Clear();
                    isselect = true;
                    EquipScene();
                }
                else
                {
                    player.armorPoint -= player.playeritems[selectNum - 1].Point;
                    Console.Clear();
                    isselect = true;
                    EquipScene();
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
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
        int itemNum;
        Console.WriteLine();
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        Console.WriteLine();
        itemNum = player.DisplayPlayerItems();
        Console.WriteLine();
        Console.WriteLine("1. 장착 관리");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");
        while (!isselect)
        {
            Console.SetCursorPosition(2, 11 + itemNum);
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
        int itemNum;
        Console.WriteLine();
        Console.WriteLine("인벤토리 - 장착 관리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        Console.WriteLine();
        itemNum = player.DisplayPlayerItems();
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");
        EquipItem();

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
        itemNum = shop.DisplayItems();
        Console.WriteLine();
        Console.WriteLine("1. 아이템구매");
        Console.WriteLine("2. 아이템판매");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        while (!isselect)
        {
            Console.SetCursorPosition(2, 14 + itemNum);
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
                    BuyScene(player);
                    break;

                case 2:
                    Console.Clear();
                    isselect = true;
                    SellScene(player);
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }


    }
    public void BuyScene(Player player)
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

        BuyItem(player);
    }
    public void SellScene(Player player)
    {
        bool isInt;
        bool isselect = false;
        int selectNum;
        int itemNum;
        Console.WriteLine();
        Console.WriteLine("상점 - 아이템 판매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.gold} G");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        itemNum = player.DisplayPlayerItems();
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        SellItem(player);
    }

}
public class Player
{
    public List<Item> playeritems = new List<Item>();

    public int level = 1;
    public string chad = "전사";
    public int attackPoint = 10;
    public int armorPoint = 5;
    public int health = 100;
    public int gold = 1500;
    public bool[] isEqup = { false, false, false, false, false, false, false, false, false, false, false };
    public bool[] isBuy = { false, false, false, false, false, false, false, false, false, false, false };

    public int DisplayPlayerItems()
    {
        int itemnumber = 0;
        if(playeritems.Count == 0)
        {
            Console.WriteLine("                                                                                     ");
        }
        else
        {
            foreach (var item in playeritems)
            {
                if (item.IsEquip == true)
                {
                    itemnumber++;
                    Console.Write("                                                                                     ");
                    Console.WriteLine($"\r- {itemnumber} [E]{item.Name}   | 방어력 +{item.Point}   | {item.Discription}    ");
                }
                else
                {
                    itemnumber++;
                    Console.Write("                                                                                     ");
                    Console.WriteLine($"\r- {itemnumber} {item.Name}   | 방어력 +{item.Point}   | {item.Discription}     ");
                }
            }
        }
        
        return itemnumber;
    }
    


}
public class Item
{
    public string Name { get; set; }
    public int Point { get; set; }
    public string Discription { get; set; }
    public int Price { get; set; }
    public bool Isweapon { get; set; }
    public bool IsBuy { get; set; }
    public bool IsEquip { get; set; }
    public bool playeritem = false;

    public Item(string name, int point, string dis, int price, bool isweapon, bool isbuy, bool isequip)
    {
        Name = name;
        Point = point;
        Discription = dis;
        Price = price;
        Isweapon = isweapon;
        IsBuy = isbuy;
        IsEquip = isequip;
    }
}
public class Shop
{
    public Shop(Player player)
    {
        this.player = player;
    }
    private Player player;
    public List<Item> items = new List<Item>
        {
            new Item("무쇠갑옷", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500,false,false,false),
            new Item("수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", 1000, false,false,false),
            new Item("스파르타의 갑옷", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3000, false,false,false),
            new Item("낡은 검", 2, "쉽게 볼 수 있는 낡은 검입니다.", 600,true,false,false),
            new Item("청동 도끼", 5, "어디선가 사용했던 것 같은 도끼입니다.", 1500,true, false, false),
            new Item("스파르타의 창", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000,true,false,false),
            new Item("개발자의후드티", 50, "음산한 기운이 느껴집니다.", 10000,false,false,false),
            new Item("개발자의노트북", 70, "세상을 바꿀 수 있는 전지전능한 힘이 담긴 물건입니다.", 15000,true,false,false)
        };
    public int DisplayItems()
    {
        int itemnumber = 0;
        foreach (var item in items)
        {
            if (item.IsBuy == false)
            {
                Console.WriteLine($"- {item.Name}   | 방어력 +{item.Point}   | {item.Discription}     |  {item.Price} G");
                itemnumber++;
            }
            else
            {
                Console.WriteLine($"- {item.Name}   | 방어력 +{item.Point}   | {item.Discription}     |     구매완료");
                itemnumber++;
            }
        }
        return itemnumber;
    }
    public int DisplayItems2()
    {
        int itemnumber = 0;
        foreach (var item in items)
        {
            if (item.IsBuy == false)
            {
                itemnumber++;
                Console.WriteLine($"- {itemnumber} {item.Name}   | 방어력 +{item.Point}   | {item.Discription}     |  {item.Price} G");
            }
            else
            {
                itemnumber++;
                Console.WriteLine($"- {itemnumber} {item.Name}   | 방어력 +{item.Point}   | {item.Discription}     |     구매완료");
            }
        }
        return itemnumber;
    }

}