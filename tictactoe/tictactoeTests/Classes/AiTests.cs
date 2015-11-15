using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tictactoe.Classes;
using tictactoeTests.Classes;

namespace tictactoeTests.Classes
{
    [TestClass()]
    public class AiTests
    {
        readonly string[,] _board = { { " ", "", "X" }, { "O", " ", "X" }, { " ", " ", "" } };

        [TestMethod()]
        public void Ai_BoardAssignmentOnConstructorCallTest()
        {
            // Assign
            // Act
            Ai computerPlayer = new Ai(_board);
            
            // Assert
            Assert.AreEqual(computerPlayer.Board, _board);
        }

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IsBoardFieldEmpty_BoardCoordinatesOutOfRangeTest()
        {
            // Assign
            string[,] board = _board;
            
            // Act
            Ai.IsBoardFieldEmpty(board, 4, 5);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        public void PerformMove_EasyAiDifficultyTest()
        {
            // Assign
            Ai computerPlayer = new Ai(_board);
            Ai.Move expectedMove = new Ai.Move() {X = 2, Y = 0};

            // Act
            Ai.Move computersMove = computerPlayer.PerformMove("X");
            
            // Assert
            Assert.AreEqual(expectedMove, computersMove);
        }
    }
}