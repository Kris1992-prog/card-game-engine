using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CardGameEngine
{
    public static class CardCatalogLoader
    {
        public static List<Card> LoadFromFile(string relativePath = "cards.json")
        {
            string fullPath = Path.Combine(AppContext.BaseDirectory, relativePath);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"File delle carte non trovato in: {fullPath}", fullPath);
            }

            string jsonContent = File.ReadAllText(fullPath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var cards = JsonSerializer.Deserialize<List<Card>>(jsonContent, options);

            if (cards is null || cards.Count == 0)
            {
                throw new InvalidOperationException("Il file JSON non contiene carte valide.");
            }

            return cards;
        }
    }
}