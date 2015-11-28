using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tictactoe.Classes;
using tictactoeTests.Classes;

namespace tictactoeTests.Classes
{
    [TestClass()]
    public class AiTests
    {
        readonly string[,] _board = { { " ", "", "X" }, { "O", " ", "X" }, { " ", " ", "" } };
        readonly Random _randomGenerator = new Random();
        readonly MockRandom _mockRandomGenerator = new MockRandom();

        [TestMethod()]
        public void Ai_BoardParameterIsNotNull_Success()
        {
            // Arrange
            // Act
            var computerPlayer = new Ai(_board, _randomGenerator);
            
            // Assert
            Assert.AreEqual(computerPlayer.Board, _board);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ai_BoardParameterIsNull_ThrowsException()
        {
            // Arrange
            // Act
            var computerPlayer = new Ai(null, _randomGenerator);

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
        [ExpectedException(typeof(ArgumentNullException))]
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
        public void GetMove_EasyAiValidMove_Success()
        {
            // Arrange
            var computerPlayer = new Ai(_board, _randomGenerator);
            var expectedMove = new Ai.Move() {X = 2, Y = 0};
            const int aiDifficulty = (int)Ai.AiDifficulty.Easy;

            // Act
            var computersMove = computerPlayer.GetMove("O", aiDifficulty);
            
            // Assert
            Assert.AreEqual(expectedMove.X, computersMove.X);
            Assert.AreEqual(expectedMove.Y, computersMove.Y);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetMove_DifficultyIsOutOfRange_ThrowsException()
        {
            // Arrange
            var computerPlayer = new Ai(_board, _randomGenerator);
            const int aiDifficulty = 5;

            // Act
            computerPlayer.GetMove("X", aiDifficulty);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        public void GetMove_MediumAiValidMove_Success()
        {
            // Arrange
            var computerPlayer = new Ai(_board, _mockRandomGenerator);
            const int aiDifficulty = (int)Ai.AiDifficulty.Medium;
            
            // Possible valid moves
            var validMoveOne = new Ai.Move() { X = 0, Y = 0 };
            var validMoveTwo = new Ai.Move() { X = 1, Y = 1 };
            var validMoveThree = new Ai.Move() { X = 2, Y = 0 };
            var validMoveFour = new Ai.Move() { X = 2, Y = 1 };

            Ai.Move[] validMoves = { validMoveOne, validMoveTwo, validMoveThree, validMoveFour};

            // Act
            var computedMoveOne = computerPlayer.GetMove("O", aiDifficulty);
            var computedMoveTwo = computerPlayer.GetMove("O", aiDifficulty);
            var computedMoveThree = computerPlayer.GetMove("O", aiDifficulty);
            var computedMoveFour = computerPlayer.GetMove("O", aiDifficulty);

            Ai.Move[] computedMoves = {computedMoveOne, computedMoveTwo, computedMoveThree, computedMoveFour};

            // Assert
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(validMoves[i].X, computedMoves[i].X);
                Assert.AreEqual(validMoves[i].Y, computedMoves[i].Y);
            }
        }

        [TestMethod()]
        public void GetMove_ImpossibleAiValidMove_Success()
        {
            // Arrange
            const int aiDifficulty = (int)Ai.AiDifficulty.Impossible;
            
            // Players mark is X
            string[,] boardPlayerStartsFirstOne = { { "X", "O", "X" }, { "X", "O", " " }, { " ", " ", " " } };
            string[,] boardPlayerStartsFirstTwo = { { "X", "O", "X" }, { "O", "O", " " }, { "X", "X", " " } };
            string[,] boardPlayerStartsFirstThree = { { "X", "O", "X" }, { "O", "O", "X" }, { " ", "X", " " } };

            string[][,] boardsPlayerStartsFirst = {boardPlayerStartsFirstOne, boardPlayerStartsFirstTwo,
                boardPlayerStartsFirstThree};

            var expectedMovePlayerStartsFirstOne = new Ai.Move() { X = 2, Y = 1 };
            var expectedMovePlayerStartsFirstTwo = new Ai.Move() { X = 1, Y = 2 };
            var expectedMovePlayerStartsFirstThree = new Ai.Move() { X = 2, Y = 2 };

            List<Ai.Move> expectedPlayerStartsFirstMoves = new List<Ai.Move>
            {
                expectedMovePlayerStartsFirstOne,
                expectedMovePlayerStartsFirstTwo, expectedMovePlayerStartsFirstThree
            };

            // Players mark is O
            string[,] boardAiStartsFirstOne = { { "X", "O", " " }, { "X", "X", " " }, { "O", "O", " " } };
            string[,] boardAiStartsFirstTwo = { { "X", "O", " " }, { "X", "X", " " }, { "O", " ", "O" } };
            string[,] boardAiStartsFirstThree = { { "X", "X", "O" }, { "O", "O", " " }, { "X", " ", " " } };

            string[][,] boardsAiStartsFirst = {boardAiStartsFirstOne, boardAiStartsFirstTwo,
                boardAiStartsFirstThree};

            var expectedMoveAiStartsFirstOne = new Ai.Move() { X = 1, Y = 2 };
            var expectedMoveAiStartsFirstTwo = new Ai.Move() { X = 1, Y = 2 };
            var expectedMoveAiStartsFirstThree = new Ai.Move() { X = 1, Y = 2 };

            List<Ai.Move> expectedAiStartsFirstMoves = new List<Ai.Move>
            {
                expectedMoveAiStartsFirstOne, expectedMoveAiStartsFirstTwo, expectedMoveAiStartsFirstThree
            };

            List<Ai.Move> performedPlayerStartsFirstMoves = new List<Ai.Move>();
            List<Ai.Move> performedAiStartsFirstMoves = new List<Ai.Move>();

            // Act
            for (int i = 0; i < 3; i++)
            {
                var computerPlayerOMark = new Ai(boardsPlayerStartsFirst[i], _randomGenerator);
                var playerStartsFirstMove = computerPlayerOMark.GetMove("O", aiDifficulty);
                performedPlayerStartsFirstMoves.Add(playerStartsFirstMove);

                var computerPlayerXMark = new Ai(boardsAiStartsFirst[i], _randomGenerator);
                var aiStartsFirstMove = computerPlayerXMark.GetMove("X", aiDifficulty);
                
                performedAiStartsFirstMoves.Add(aiStartsFirstMove);
            }

            // Assert
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(expectedPlayerStartsFirstMoves[i].X, performedPlayerStartsFirstMoves[i].X);
                Assert.AreEqual(expectedPlayerStartsFirstMoves[i].Y, performedPlayerStartsFirstMoves[i].Y);

                Assert.AreEqual(expectedAiStartsFirstMoves[i].X, performedAiStartsFirstMoves[i].X);
                Assert.AreEqual(expectedAiStartsFirstMoves[i].Y, performedAiStartsFirstMoves[i].Y);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof (ArgumentException))]
        public void GetMove_InvalidPlayerMarkArgument_ThrowsException()
        {
            // Arrange
            var computerPlayer = new Ai(_board, _randomGenerator);
            const int aiDifficulty = (int)Ai.AiDifficulty.Easy;

            // Act
            computerPlayer.GetMove("invalidMark", aiDifficulty);

            // Assert
            Assert.Fail();
        }
    }
}