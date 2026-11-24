using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGameEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Lab 4 & 5: Protezione Plancia, Combattimento e Abilità Speciali ===");

            // 1. Carichiamo il catalogo esterno da cards.json (come avevi già impostato)
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

            // --- Parte 1: Regola di Attacco Diretto (conservata dal tuo codice) ---
            Console.WriteLine("\n--- Test 1: Tentativo di attacco diretto con difensori attivi ---");
            p1.AttackDirectly(goblin, p2);

            Console.WriteLine("\n--- Test 2: Rimuoviamo il difensore e riproviamo ---");
            p2.Board.Clear();
            Console.WriteLine("L'Orco guardiano è stato eliminato dal campo.");

            p1.AttackDirectly(goblin, p2);

            // --- Parte 2: Combattimento Creatura vs Creatura, Provocazione e Carica ---
            p1.Board.Clear();
            p2.Board.Clear();

            Card cavaliere = new Card(10, "Cavaliere d'Assalto", 4, 4, 5, hasCharge: true);
            Card goblinCombattimento = new Card(1, "Goblin Esploratore", 1, 3, 1) { CanAttack = true };

            Card orcoTaunt = new Card(2, "Orco Guardiano", 2, 2, 3, hasTaunt: true);
            Card arciere = new Card(3, "Arciere Elfico", 2, 2, 2);

            p1.Board.Add(cavaliere);
            p1.Board.Add(goblinCombattimento);
            p2.Board.Add(orcoTaunt);
            p2.Board.Add(arciere);

            // Tentativo di ignorare Provocazione (dovrebbe essere bloccato)
            Console.WriteLine("\n--- Test 3: Tentativo di attaccare l'Arciere ignorando Provocazione ---");
            p1.AttackCreature(cavaliere, arciere, p2);

            // Scontro con sopravvivenza dell'attaccante e distruzione del bersaglio
            Console.WriteLine("\n--- Test 4: Cavaliere attacca Orco con Provocazione (Orco distrutto, Cavaliere sopravvive) ---");
            p1.AttackCreature(cavaliere, orcoTaunt, p2);
            Console.WriteLine($"Stato Cavaliere dopo lo scontro: {cavaliere}");

            // Doppio KO: Goblin (3/1) attacca Arciere (2/2) -> entrambi distrutti
            Console.WriteLine("\n--- Test 5: Doppio KO tra Goblin e Arciere ---");
            p1.AttackCreature(goblinCombattimento, arciere, p2);

            Console.WriteLine($"\nFine simulazione -> Creature su plancia {p1.Name}: {p1.Board.Count} | Creature su plancia {p2.Name}: {p2.Board.Count}");
        }
    }
}