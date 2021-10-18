﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleInventory
{
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
}