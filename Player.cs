using System.Collections.Generic;

namespace ConsoleInventory
{
    public static class Player
    {
        public static List<Item> inventoryItems = new List<Item>() { new Item("Key", 'K', 2), new Item("Card", 'C', 3) };
        public static Storage inventory = new Storage("Inventory", 10, inventoryItems, 40, 5);
    }
}
