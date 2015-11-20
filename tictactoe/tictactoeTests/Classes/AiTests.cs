using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tictactoe.Classes;
using tictactoeTests.Classes;

namespace tictactoeTests.Classes
{
    [TestClass()]
    public class AiTests
    {
        readonly string[,] _board = { { " ", "", "X" }, { "O", " ", "X" }, { " ", " ", "" } };
        readonly Random RandomGenerator = new Random();
        readonly MockRandom MockRandomGenerator = new MockRandom();

        [TestMethod()]
        public void Ai_BoardParameterIsNotNull_Success()
        {
            // Arrange
            // Act
            var computerPlayer = new Ai(_board, RandomGenerator);
            
            // Assert
            Assert.AreEqual(computerPlayer.Board, _board);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void Ai_BoardParameterIsNull_ThrowsException()
        {
            // Arrange
            // Act
            var computerPlayer = new Ai(null, RandomGenerator);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IsBoardFieldEmpty_BoardCoordinatesOutOfRange_ThrowsException()
        {
            // Arrange
            var board = _board;
            
            // Act
            Ai.IsBoardFieldEmpty(board, 4, 5);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void IsBoardFieldEmpty_BoardIsNull_ThrowsException()
        {
            // Arrange
            string[,] board = null;

            // Act
            Ai.IsBoardFieldEmpty(board, 0, 0);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        public void IsBoardFieldEmpty_ValidUse_Success()
        {
            // Arrange
            var board = _board;

            // Act
            var isEmpty = Ai.IsBoardFieldEmpty(board, 0, 0);
            var isInvalid = Ai.IsBoardFieldEmpty(board, 0, 1);
            var isCross = Ai.IsBoardFieldEmpty(board, 0, 2);
            var isCircle = Ai.IsBoardFieldEmpty(board, 1, 0);
            
            // Assert
            Assert.IsTrue(isEmpty);
            Assert.IsFalse(isInvalid);
            Assert.IsFalse(isCross);
            Assert.IsFalse(isCircle);
        }
        
        [TestMethod()]
        public void PerformMove_EasyAiValidMove_Success()
        {
            // Arrange
            var computerPlayer = new Ai(_board, RandomGenerator);
            var expectedMove = new Ai.Move() {X = 2, Y = 0};
            const int aiDifficulty = (int)Ai.AiDifficulty.Easy;

            // Act
            var computersMove = computerPlayer.PerformMove("O", aiDifficulty);
            
            // Assert
            Assert.AreEqual(expectedMove, computersMove);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PerformMove_DifficultyIsOutOfRange_ThrowsException()
        {
            // Arrange
            var computerPlayer = new Ai(_board, RandomGenerator);
            const int aiDifficulty = 5;

            // Act
            computerPlayer.PerformMove("X", aiDifficulty);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        public void PerformMove_MediumAiValidMove_Success()
        {
            // Arrange
            var computerPlayer = new Ai(_board, MockRandomGenerator);
            const int aiDifficulty = (int)Ai.AiDifficulty.Medium;
            
            // Possible valid moves
            var validMoveOne = new Ai.Move() { X = 0, Y = 0 };
            var validMoveTwo = new Ai.Move() { X = 1, Y = 1 };
            var validMoveThree = new Ai.Move() { X = 2, Y = 0 };
            var validMoveFour = new Ai.Move() { X = 2, Y = 1 };

            Ai.Move[] validMoves = { validMoveOne, validMoveTwo, validMoveThree, validMoveFour};

            // Act
            var computedMoveOne = computerPlayer.PerformMove("O", aiDifficulty);
            var computedMoveTwo = computerPlayer.PerformMove("O", aiDifficulty);
            var computedMoveThree = computerPlayer.PerformMove("O", aiDifficulty);
            var computedMoveFour = computerPlayer.PerformMove("O", aiDifficulty);

            Ai.Move[] computedMoves = {computedMoveOne, computedMoveTwo, computedMoveThree, computedMoveFour};

            // Assert
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(validMoves[i], computedMoves[i]);
            }
        }
    }
}