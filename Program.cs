using System.Collections.Generic;

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
}
