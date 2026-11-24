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
        public bool HasTaunt { get; set; }
        public bool HasCharge { get; set; }

        public Card(
            int id,
            string name,
            int cost,
            int attack,
            int defense,
            string cardType = "Creature",
            bool hasTaunt = false,
            bool hasCharge = false)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Attack = attack;
            MaxDefense = defense;
            CurrentDefense = defense;
            CardType = cardType;
            HasTaunt = hasTaunt;
            HasCharge = hasCharge;
            CanAttack = hasCharge; // Se ha Carica attacca subito, altrimenti ha debolezza da evocazione
        }

        public override string ToString()
        {
            string abilities = "";
            if (HasTaunt) abilities += " | Provocazione";
            if (HasCharge) abilities += " | Carica";

            return $"[{Name} | Costo: {Cost} | ATK: {Attack} / DEF: {CurrentDefense}/{MaxDefense} | Tipo: {CardType}{abilities}]";
        }
    }
}