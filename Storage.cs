using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleInventory
{
    public class Storage
    {
        public string Name { get; private set; }
        public int Capacity { get; protected set; }
        public List<Item> Items { get; protected set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Storage(string name, int capacity, List<Item> items, int w, int h)
        {
            Name = name;
            Capacity = capacity;
            Items = new List<Item>(capacity);
            Width = w;
            Height = h;
            AddItems(items);
            UpdateCapacity();
        }

        public void AddItem(Item item)
        {
            if (Items == null)
            {
                Items = new List<Item>(Capacity);
            }
            if (Items.Count <= Capacity)
            {
                Items.Add(item);
                UpdateCapacity();
            }
            else
            {
                Console.WriteLine("Your inventory is full.");
            }
        }

        private void AddItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                AddItem(item);
            }
        }

        private void RemoveItem(Item item)
        {
            if (Items != null && Items.Count > 0 && Items.Contains(item))
            {
                Items.Remove(item);
                UpdateCapacity();
            }
        }

        private void RemoveItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                RemoveItem(item);
            }
        }

        private void UpdateCapacity()
        {
            Capacity = Items.Count;
        }

        public void DrawContent(bool isInteractable)
        {
            string drawString = "";
            for (int i = 0; i < Width; i++)
            {
                drawString += "_";
            }

            for (int j = 0; j < Height; j++)
            {
                drawString += "\n";
                drawString += "|";
                for (int i = 0; i < Width; i++)
                {
                    drawString += " ";
                }
                drawString += "|";
            }
            drawString += "\n";

            for (int k = 0; k < Width; k++)
            {
                drawString += "_";
            }

            drawString = DrawItemsInside(drawString, Items);
            Console.WriteLine(drawString);
            if (isInteractable)
            {
                LookInside();
                Console.WriteLine("0. Back.");
            }
        }

        private string DrawItemsInside(string furnitureString, List<Item> items)
        {
            Random rnd = new Random();
            foreach (Item item in items)
            {
                int rndIndex = rnd.Next(furnitureString.Length - 1);
                while (!furnitureString[rndIndex].Equals(' '))
                {
                    rndIndex = rnd.Next(furnitureString.Length - 1);
                }
                furnitureString = furnitureString.Remove(rndIndex, 1);
                furnitureString = furnitureString.Insert(rndIndex, item.Symbol.ToString());
            }
            return furnitureString;
        }

        public void Interact()
        {
            Console.WriteLine($"You look inside a {Name}.");
            DrawContent(true);

            string input = Console.ReadLine();
            Regex onlyDigitsRegEx = new Regex(@"\D");
            if (onlyDigitsRegEx.IsMatch(input) || input.Equals(""))
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Only digits allowed.\n");
                Interact();
                return;
            }
            else
            {
                int inputInt = Int32.Parse(input);
                if (inputInt - 1 >= Items.Count || inputInt < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Out of range. Try again.\n");
                    Interact();
                    return;
                }
                else if(inputInt == 0)
                {
                    Console.Clear();
                    return;
                }
                Console.Clear();

                Player.inventory.AddItem(Items[inputInt - 1]);
                RemoveItem(Items[inputInt - 1]);

                Interact();
            }
        }

        public void LookInside()
        {
            string itemsString = "";
            string itemsMessage = "";
            if (Items == null || Items.Count == 0)
            {
                itemsMessage = $"You see an empty {Name}.";
            }
            else
            {
                itemsMessage = $"You see several items on the {Name}.";
                foreach (Item item in Items)
                {
                    int listIndex = Items.IndexOf(item) + 1;
                    itemsString += $"{listIndex}. {item.ToString()}.\n";
                }
            }
            Console.WriteLine(itemsMessage);
            Console.WriteLine(itemsString);
        }
    }
}
