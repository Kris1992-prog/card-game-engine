using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGameEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Lab 4 (Aggiornato): Regola di Protezione della Plancia ===");

            List<Card> catalog = CardCatalogLoader.LoadFromFile("cards.json");

            Card goblinTemplate = catalog.First(c => c.Id == 1);
            Card orcoTemplate = catalog.First(c => c.Id == 2);

            Player p1 = new Player("Kris", 20, new List<Card> { goblinTemplate });
            Player p2 = new Player("Nemico", 20, new List<Card> { orcoTemplate });

            // Mettiamo una creatura pronta ad attaccare per Kris e un Orco a difesa del Nemico
            Card goblin = new Card(goblinTemplate.Id, goblinTemplate.Name, goblinTemplate.Cost, goblinTemplate.Attack, goblinTemplate.MaxDefense, goblinTemplate.CardType)
            {
                CanAttack = true
            };
            Card orco = new Card(orcoTemplate.Id, orcoTemplate.Name, orcoTemplate.Cost, orcoTemplate.Attack, orcoTemplate.MaxDefense, orcoTemplate.CardType);

            p1.Board.Add(goblin);
            p2.Board.Add(orco);

            Console.WriteLine($"\nPlancia {p1.Name}: {p1.Board[0]}");
            Console.WriteLine($"Plancia {p2.Name}: {p2.Board[0]}");

            // 1. Tentativo di attacco diretto con l'Orco nemico ancora vivo (Dovrebbe fallire)
            Console.WriteLine("\n--- Test 1: Tentativo di attacco diretto con difensori attivi ---");
            p1.AttackDirectly(goblin, p2);

            // 2. Eliminiamo l'Orco dalla plancia (simulando una rimozione o distruzione)
            Console.WriteLine("\n--- Test 2: Rimuoviamo il difensore e riproviamo ---");
            p2.Board.Clear();
            Console.WriteLine("L'Orco guardiano è stato eliminato dal campo.");

            // 3. Ora l'attacco diretto dovrebbe andare a buon fine
            p1.AttackDirectly(goblin, p2);
        }
    }
}