using System;
using System.Collections.Generic;

namespace CardGameEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Lab 3: Turn & Battle Engine ===");

            // 1. Catalogo di base delle carte
            List<Card> masterCatalog = new List<Card>
            {
                new Card(1, "Drago di Fuoco", 5, 6, 4),
                new Card(2, "Goblin Esploratore", 1, 1, 1),
                new Card(3, "Cavaliere con Scudo", 3, 2, 5),
                new Card(4, "Arciere Elfo", 2, 3, 2),
                new Card(5, "Pozione di Cura", 2, 0, 0, "Spell")
            };

            // 2. Creiamo il giocatore con 20 HP e il mazzo iniziale
            Player player = new Player("Kris", 20, masterCatalog);

            // Diamo una mano iniziale di 2 carte pescando dal deck
            for (int i = 0; i < 2; i++)
            {
                Card? initialCard = player.PlayerDeck.DrawCard();
                if (initialCard != null)
                {
                    player.Hand.Add(initialCard);
                }
            }

            // 3. Simuliamo l'inizio del turno
            player.StartTurn();

            // Mostriamo le carte in mano
            Console.WriteLine("\nCarte attualmente in mano:");
            foreach (var card in player.Hand)
            {
                Console.WriteLine($" -> {card}");
            }

            // 4. Proviamo a giocare la prima carta disponibile nella mano
            if (player.Hand.Count > 0)
            {
                Console.WriteLine("\nTentativo di giocare la prima carta in mano...");
                Card cardToPlay = player.Hand[0];
                player.PlayCard(cardToPlay);
            }

            // 5. Stato finale del giocatore
            Console.WriteLine($"\nCarte rimaste in mano: {player.Hand.Count}");
            Console.WriteLine($"Stato finale -> HP: {player.Hp} | Mana residuo: {player.CurrentMana}");
        }
    }
}