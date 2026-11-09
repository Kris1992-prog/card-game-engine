using System;
using System.Collections.Generic;

namespace CardGameEngine
{
    public class Player
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int MaxMana { get; set; }
        public int CurrentMana { get; set; }
        public Deck PlayerDeck { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> Board { get; set; }

        public Player(string name, int initialHp, List<Card> startingDeck)
        {
            Name = name;
            Hp = initialHp;
            MaxMana = 0;
            CurrentMana = 0;
            PlayerDeck = new Deck(startingDeck);
            PlayerDeck.Shuffle();
            Hand = new List<Card>();
            Board = new List<Card>();
        }

        public void StartTurn()
        {
            if (MaxMana < 10)
            {
                MaxMana++;
            }
            CurrentMana = MaxMana;

            // All'inizio del turno, tutte le creature sulla plancia possono attaccare
            foreach (var card in Board)
            {
                card.CanAttack = true;
            }

            Console.WriteLine($"\n--- Turno di {Name} ---");
            Console.WriteLine($"Mana: {CurrentMana}/{MaxMana} | HP: {Hp}");

            Card? drawnCard = PlayerDeck.DrawCard();
            if (drawnCard != null)
            {
                Hand.Add(drawnCard);
                Console.WriteLine($"-> {Name} pesca: {drawnCard.Name}");
            }
            else
            {
                Console.WriteLine($"-> Il mazzo è vuoto! {Name} subisce 2 danni da stanchezza (Fatigue).");
                Hp -= 2;
            }
        }

        // Evocazione: sposta la carta dalla Mano alla Plancia
        public bool PlayCard(Card card)
        {
            if (CurrentMana >= card.Cost)
            {
                CurrentMana -= card.Cost;
                Hand.Remove(card);

                if (card.CardType == "Creature")
                {
                    Board.Add(card);
                    Console.WriteLine($"[EVOCAZIONE] {Name} evoca '{card.Name}' sulla plancia!");
                }
                else
                {
                    Console.WriteLine($"[MAGIA] {Name} gioca la magia '{card.Name}'!");
                }

                return true;
            }

            Console.WriteLine($"[ERRORE] Mana insufficiente per {card.Name}!");
            return false;
        }

        // Combattimento: Creatura vs Creatura
        public void AttackCreature(Card attacker, Card target, Player opponent)
        {
            if (!attacker.CanAttack)
            {
                Console.WriteLine($"[ERRORE] {attacker.Name} non può attaccare in questo turno!");
                return;
            }

            Console.WriteLine($"\n[ATTACCO] {attacker.Name} (ATK {attacker.Attack}) attacca {target.Name} (DEF {target.CurrentDefense}) di {opponent.Name}!");

            // Danno reciproco simultaneo
            target.CurrentDefense -= attacker.Attack;
            attacker.CurrentDefense -= target.Attack;
            attacker.CanAttack = false;

            // Controllo distruzione creatura bersaglio
            if (target.CurrentDefense <= 0)
            {
                Console.WriteLine($"[DISTRUZIONE] {target.Name} di {opponent.Name} è stata distrutta!");
                opponent.Board.Remove(target);
            }

            // Controllo distruzione creatura attaccante
            if (attacker.CurrentDefense <= 0)
            {
                Console.WriteLine($"[DISTRUZIONE] {attacker.Name} di {Name} è stata distrutta nel contrattacco!");
                Board.Remove(attacker);
            }
        }

        // Combattimento: Attacco diretto al giocatore avversario
        public void AttackDirectly(Card attacker, Player opponent)
        {
            if (!attacker.CanAttack)
            {
                Console.WriteLine($"[ERRORE] {attacker.Name} non può attaccare in questo turno!");
                return;
            }

            // REGOLA: Impossibile attaccare direttamente se l'avversario ha creature in difesa sulla plancia
            if (opponent.Board.Count > 0)
            {
                Console.WriteLine($"[ERRORE] Impossibile attaccare direttamente {opponent.Name}: ci sono {opponent.Board.Count} creature sulla sua plancia a fare da scudo!");
                return;
            }

            Console.WriteLine($"\n[ATTACCO DIRETTO] {attacker.Name} attacca direttamente {opponent.Name} per {attacker.Attack} danni!");
            opponent.Hp -= attacker.Attack;
            attacker.CanAttack = false;

            Console.WriteLine($"-> Punti vita di {opponent.Name}: {opponent.Hp} HP");
        }
    }
}