using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tictactoe.Classes;
using tictactoeTests.Properties;

namespace tictactoeTests.Classes
{
    [TestClass()]
    public class AiTests : BaseAiTest
    {
        [TestMethod()]
        public void Ai_ValidUse_Success()
        {
            // Arrange
            // Act
            var computerPlayer = new Ai(GetBoard(), GetRandom());
            
            // Assert
            Assert.AreEqual(computerPlayer.Board, GetBoard());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ai_BoardParameterIsNull_ThrowsException()
        {
            // Arrange
            // Act
            var computerPlayer = new Ai(null, GetRandom());

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IsBoardFieldEmpty_BoardCoordinatesOutOfRange_ThrowsException()
        {
            // Arrange
            var board = GetBoard();
            
            // Act
            Ai.IsBoardFieldEmpty(board, 4, 5);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsBoardFieldEmpty_BoardParameterIsNull_ThrowsException()
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
            var board = GetBoard();

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
            var computerPlayer = new Ai(GetBoard(), GetRandom());
            var expectedMove = new Ai.Move() {X = 0, Y = 0};
            const int aiDifficulty = (int)Ai.AiDifficulty.Easy;

            // Act
            var computersMove = computerPlayer.GetMove(Resources.BoardCrossMark, aiDifficulty);
            
            // Assert
            Assert.AreEqual(expectedMove.X, computersMove.X);
            Assert.AreEqual(expectedMove.Y, computersMove.Y);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetMove_DifficultyIsOutOfRange_ThrowsException()
        {
            // Arrange
            var computerPlayer = new Ai(GetBoard(), GetRandom());
            const int aiDifficulty = 5;

            // Act
            computerPlayer.GetMove(Resources.BoardCrossMark, aiDifficulty);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        public void GetMove_MediumAiValidMove_Success()
        {
            // Arrange
            var computerPlayer = new Ai(GetBoard(), GetMockRandom());
            const int aiDifficulty = (int)Ai.AiDifficulty.Medium;
            
            // Possible valid moves
            var validMoveOne = new Ai.Move() { X = 0, Y = 0 };
            var validMoveTwo = new Ai.Move() { X = 1, Y = 1 };
            var validMoveThree = new Ai.Move() { X = 2, Y = 0 };
            var validMoveFour = new Ai.Move() { X = 2, Y = 1 };

            Ai.Move[] validMoves = { validMoveOne, validMoveTwo, validMoveThree, validMoveFour};

            // Act
            var computedMoveOne = computerPlayer.GetMove(Resources.BoardCircleMark, aiDifficulty);
            var computedMoveTwo = computerPlayer.GetMove(Resources.BoardCircleMark, aiDifficulty);
            var computedMoveThree = computerPlayer.GetMove(Resources.BoardCircleMark, aiDifficulty);
            var computedMoveFour = computerPlayer.GetMove(Resources.BoardCircleMark, aiDifficulty);

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
            string[,] boardPlayerStartsFirstOne = GetBoardPlayerStartsFirstOne();
            string[,] boardPlayerStartsFirstTwo = GetBoardPlayerStartsFirstTwo();
            string[,] boardPlayerStartsFirstThree = GetBoardPlayerStartsFirstThree();

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
            string[,] boardAiStartsFirstOne = GetBoardAiStartsFirstOne();
            string[,] boardAiStartsFirstTwo = GetBoardAiStartsFirstTwo();
            string[,] boardAiStartsFirstThree = GetBoardAiStartsFirstThree();

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
                var computerPlayerOMarkAi = new Ai(boardsPlayerStartsFirst[i], GetRandom());
                var playerStartsFirstMove = computerPlayerOMarkAi.GetMove(Resources.BoardCircleMark, aiDifficulty);
                performedPlayerStartsFirstMoves.Add(playerStartsFirstMove);

                var computerPlayerXMarkAi = new Ai(boardsAiStartsFirst[i], GetRandom());
                var aiStartsFirstMove = computerPlayerXMarkAi.GetMove(Resources.BoardCrossMark, aiDifficulty);
                
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
            var computerPlayer = new Ai(GetBoard(), GetRandom());
            const int aiDifficulty = (int)Ai.AiDifficulty.Easy;

            // Act
            computerPlayer.GetMove("invalidMark", aiDifficulty);

            // Assert
            Assert.Fail("No exception was thrown.");
        }
    }
}