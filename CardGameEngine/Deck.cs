using System;
using System.Collections.Generic;

namespace CardGameEngine
{
    public class Deck
    {
        private List<Card> cards;
        private Random random;

        public Deck(List<Card> initialCards)
        {
            // Copia le carte iniziali per creare il mazzo
            cards = new List<Card>(initialCards);
            random = new Random();
        }

        // Metodo per mischiare il mazzo (Algoritmo Fisher-Yates)
        public void Shuffle()
        {
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        // Metodo per pescare una carta dalla cima del mazzo
        public Card? DrawCard()
        {
            if (cards.Count == 0)
            {
                return null; // Mazzo vuoto
            }

            Card drawnCard = cards[0];
            cards.RemoveAt(0);
            return drawnCard;
        }

        public int RemainingCards => cards.Count;
    }
}