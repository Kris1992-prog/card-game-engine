using System.Collections.Generic;
using Xunit;
using CardGameEngine;

namespace CardGameEngine.Tests
{
    public class CombatTests
    {
        [Fact]
        public void Card_WithCharge_CanAttackImmediately()
        {
            var chargeCard = new Card(10, "Cavaliere d'Assalto", 4, 4, 5, hasCharge: true);
            var normalCard = new Card(1, "Goblin Esploratore", 1, 3, 1);

            Assert.True(chargeCard.CanAttack);
            Assert.False(normalCard.CanAttack);
        }

        [Fact]
        public void AttackCreature_TargetDestroyed_AttackerSurvives()
        {
            var p1 = new Player("Kris", 20, new List<Card>());
            var p2 = new Player("Nemico", 20, new List<Card>());

            var knight = new Card(10, "Cavaliere", 4, 4, 5, hasCharge: true);
            var orc = new Card(2, "Orco", 2, 2, 3);

            p1.Board.Add(knight);
            p2.Board.Add(orc);

            p1.AttackCreature(knight, orc, p2);

            Assert.Contains(knight, p1.Board);
            Assert.Equal(3, knight.CurrentDefense);
            Assert.False(knight.CanAttack);
            Assert.DoesNotContain(orc, p2.Board);
        }

        [Fact]
        public void AttackCreature_DoubleKO_BothCreaturesRemovedFromBoard()
        {
            var p1 = new Player("Kris", 20, new List<Card>());
            var p2 = new Player("Nemico", 20, new List<Card>());

            var goblin = new Card(1, "Goblin", 1, 3, 1) { CanAttack = true };
            var archer = new Card(3, "Arciere", 2, 2, 2);

            p1.Board.Add(goblin);
            p2.Board.Add(archer);

            p1.AttackCreature(goblin, archer, p2);

            Assert.DoesNotContain(goblin, p1.Board);
            Assert.DoesNotContain(archer, p2.Board);
            Assert.Empty(p1.Board);
            Assert.Empty(p2.Board);
        }

        [Fact]
        public void AttackCreature_BlockedByTaunt_WhenTargetingNonTauntCreature()
        {
            var p1 = new Player("Kris", 20, new List<Card>());
            var p2 = new Player("Nemico", 20, new List<Card>());

            var knight = new Card(10, "Cavaliere", 4, 4, 5, hasCharge: true);
            var tauntOrc = new Card(2, "Orco Guardiano", 2, 2, 3, hasTaunt: true);
            var archer = new Card(3, "Arciere", 2, 2, 2);

            p1.Board.Add(knight);
            p2.Board.Add(tauntOrc);
            p2.Board.Add(archer);

            p1.AttackCreature(knight, archer, p2);

            Assert.Equal(2, archer.CurrentDefense);
            Assert.Equal(5, knight.CurrentDefense);
            Assert.True(knight.CanAttack);
            Assert.Equal(2, p2.Board.Count);
        }

        [Fact]
        public void AttackCreature_WhenSummoningSickness_DoesNotDealDamage()
        {
            var p1 = new Player("Kris", 20, new List<Card>());
            var p2 = new Player("Nemico", 20, new List<Card>());

            var goblin = new Card(1, "Goblin", 1, 3, 1); // CanAttack = false di default
            var orc = new Card(2, "Orco", 2, 2, 3);

            p1.Board.Add(goblin);
            p2.Board.Add(orc);

            p1.AttackCreature(goblin, orc, p2);

            Assert.Equal(3, orc.CurrentDefense);
            Assert.Equal(1, goblin.CurrentDefense);
        }

        [Fact]
        public void AttackCreature_WhenAttackerNotOnBoard_DoesNotDealDamage()
        {
            var p1 = new Player("Kris", 20, new List<Card>());
            var p2 = new Player("Nemico", 20, new List<Card>());

            var knightInHand = new Card(10, "Cavaliere", 4, 4, 5, hasCharge: true);
            var orc = new Card(2, "Orco", 2, 2, 3);

            p1.Hand.Add(knightInHand);
            p2.Board.Add(orc);

            p1.AttackCreature(knightInHand, orc, p2);

            Assert.Equal(3, orc.CurrentDefense);
            Assert.True(knightInHand.CanAttack);
        }

        [Fact]
        public void AttackCreature_WhenCardIsNotCreature_DoesNothing()
        {
            var p1 = new Player("Kris", 20, new List<Card>());
            var p2 = new Player("Nemico", 20, new List<Card>());

            var spell = new Card(99, "Palla di Fuoco", 3, 4, 1, cardType: "Spell", hasCharge: true);
            var orc = new Card(2, "Orco", 2, 2, 3);

            p1.Board.Add(spell);
            p2.Board.Add(orc);

            p1.AttackCreature(spell, orc, p2);

            Assert.Equal(3, orc.CurrentDefense);
        }

        [Fact]
        public void AttackDirectly_BlockedByDefender_AndSucceedsWhenBoardEmpty()
        {
            var p1 = new Player("Kris", 20, new List<Card>());
            var p2 = new Player("Nemico", 20, new List<Card>());

            var goblin = new Card(1, "Goblin", 1, 3, 1) { CanAttack = true };
            var orc = new Card(2, "Orco", 2, 2, 3);

            p1.Board.Add(goblin);
            p2.Board.Add(orc);

            // 1. Con un difensore in campo l'attacco diretto è bloccato
            p1.AttackDirectly(goblin, p2);
            Assert.Equal(20, p2.Hp);
            Assert.True(goblin.CanAttack);

            // 2. Rimosso il difensore, l'attacco diretto va a segno
            p2.Board.Clear();
            p1.AttackDirectly(goblin, p2);
            Assert.Equal(17, p2.Hp);
            Assert.False(goblin.CanAttack);
        }
    }
}