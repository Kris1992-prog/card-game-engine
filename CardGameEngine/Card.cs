namespace CardGameEngine
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public string CardType { get; set; }

        public Card(int id, string name, int cost, int attack, int defense, string cardType = "Creature")
        {
            Id = id;
            Name = name;
            Cost = cost;
            Attack = attack;
            Defense = defense;
            CardType = cardType;
        }

        public override string ToString()
        {
            return $"[{Name} | Costo: {Cost} | ATK: {Attack} / DEF: {Defense} | Tipo: {CardType}]";
        }
    }
}