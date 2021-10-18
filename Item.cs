namespace ConsoleInventory
{
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
