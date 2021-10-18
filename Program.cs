using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleInventory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Item> tableItems = new List<Item>() { new Item("Amulet", 'V'), new Item("Coin", '@', 33), new Item("Book", 'B') };
            List<Item> drawerItems = new List<Item>() { new Item("Item", 'I', 14), new Item("Map", 'M', 88) };

            Storage table = new Storage("Table", 10, tableItems, 20, 10);
            Storage drawer = new Storage("Drawer", 10, drawerItems, 30, 15);

            Room kitchen = new Room("Kitchen", new List<Storage>() { table, drawer });
            kitchen.Show();
        }
    }

    public static class Player
    {
        public static List<Item> inventoryItems = new List<Item>() { new Item("Key", 'K', 2), new Item("Card", 'C', 3) };
        public static Storage inventory = new Storage("Inventory", 10, inventoryItems, 40, 5);
    }

    public class Room
    {
        public string Name { get; private set; }
        public List<Storage> Furniture { get; private set; }
        public Room(string name, List<Storage> furniture)
        {
            Name = name;
            Furniture = furniture;
        }
        public void Show()
        {
            Player.inventory.DrawContent(false);
            Player.inventory.LookInside();

            string output = "";
            Console.WriteLine($"You are in {Name} room. You see the following:");
            for (int i = 0; i < Furniture.Count; i++)
            {
                output += $"{i + 1}. {Furniture[i].Name}\n";
            }
            Console.WriteLine(output);

            string input = Console.ReadLine();
            Regex onlyDigitsRegEx = new Regex(@"\D");
            if (onlyDigitsRegEx.IsMatch(input) || input.Equals(""))
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Only digits allowed.\n");
                Show();
                return;
            }
            else
            {
                int inputInt = Int32.Parse(input);
                if (inputInt - 1 >= Furniture.Count || inputInt <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Out of range. Try again.\n");
                    Show();
                    return;
                }
                Console.Clear();
                Furniture[inputInt - 1].Interact();
                Show();
            }
        }
    }

    public class Item
    {
        public string Name { get; private set; }
        public int Amount { get; private set; }
        public char Symbol { get; private set; }

        public Item(string itemName, char symbol, int itemAmount = 1)
        {
            Name = itemName;
            Symbol = symbol;
            Amount = itemAmount;
        }

        public override string ToString()
        {
            return $"Item: {Name}({Symbol}), Amount: {Amount}";
        }
    }
}
