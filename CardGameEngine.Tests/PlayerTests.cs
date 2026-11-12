using Xunit;
using CardGameEngine;
using System.Collections.Generic;

namespace CardGameEngine.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void Player_ShouldInitializeWithCorrectHpAndZeroMana()
        {
            // Arrange (Preparo i dati di test)
            string playerName = "Kris";
            int initialHp = 20;
            var startingDeck = new List<Card>
            {
                new Card(1, "Goblin", 1, 2, 1)
            };

            // Act (Eseguo l'azione da testare)
            var player = new Player(playerName, initialHp, startingDeck);

            // Assert (Verifico che il risultato sia esattamente quello atteso)
            Assert.Equal("Kris", player.Name);
            Assert.Equal(20, player.Hp);
            Assert.Equal(0, player.CurrentMana);
            Assert.Empty(player.Board);
        }
    }
}