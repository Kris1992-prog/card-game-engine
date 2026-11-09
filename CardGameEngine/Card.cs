namespace CardGameEngine
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Attack { get; set; }
        public int MaxDefense { get; set; }
        public int CurrentDefense { get; set; }
        public string CardType { get; set; }
        public bool CanAttack { get; set; }

        public Card(int id, string name, int cost, int attack, int defense, string cardType = "Creature")
        {
            Id = id;
            Name = name;
            Cost = cost;
            Attack = attack;
            MaxDefense = defense;
            CurrentDefense = defense;
            CardType = cardType;
            CanAttack = false; // Di norma una creatura appena evocata ha la "debolezza da evocazione"
        }

        public override string ToString()
        {
            return $"[{Name} | Costo: {Cost} | ATK: {Attack} / DEF: {CurrentDefense}/{MaxDefense} | Tipo: {CardType}]";
        }
    }
}