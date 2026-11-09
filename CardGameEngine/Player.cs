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

        public Player(string name, int initialHp, List<Card> startingDeck)
        {
            Name = name;
            Hp = initialHp;
            MaxMana = 0;
            CurrentMana = 0;
            PlayerDeck = new Deck(startingDeck);
            PlayerDeck.Shuffle();
            Hand = new List<Card>();
        }

        // Inizio del turno: aumenta il mana massimo (fino a 10), ricarica il mana e pesca una carta
        public void StartTurn()
        {
            if (MaxMana < 10)
            {
                MaxMana++;
            }
            CurrentMana = MaxMana;

            Console.WriteLine($"\n--- Turno di {Name} ---");
            Console.WriteLine($"Mana disponibile: {CurrentMana}/{MaxMana} | HP: {Hp}");

            // Pescaggio automatico a inizio turno
            Card? drawnCard = PlayerDeck.DrawCard();
            if (drawnCard != null)
            {
                Hand.Add(drawnCard);
                Console.WriteLine($"-> {Name} pesca una carta: {drawnCard.Name}");
            }
            else
            {
                Console.WriteLine($"-> Il mazzo è vuoto! {Name} subisce danno da stanchezza (Fatigue).");
                Hp -= 2;
            }
        }

        // Tentativo di giocare una carta spendendo il mana
        public bool PlayCard(Card card)
        {
            if (CurrentMana >= card.Cost)
            {
                CurrentMana -= card.Cost;
                Hand.Remove(card);
                Console.WriteLine($"[AZIONE] {Name} gioca '{card.Name}' (Costo: {card.Cost}). Mana rimanente: {CurrentMana}");
                return true;
            }
            else
            {
                Console.WriteLine($"[ERRORE] Mana insufficiente per giocare '{card.Name}'! (Richiesto: {card.Cost}, Disponibile: {CurrentMana})");
                return false;
            }
        }
    }
}