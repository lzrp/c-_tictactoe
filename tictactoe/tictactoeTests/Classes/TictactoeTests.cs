using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tictactoe.Classes;
using tictactoeTests.Properties;

namespace tictactoeTests.Classes
{
    [TestClass()]
    public class TictactoeTests : BaseTicTacToeTest
    {

    [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TicTacToe_ButtonListParameterIsNull_ThrowsException()
        {
            // Arrange
            IEnumerable<Button> nullButtonList = null;

            // Act
            var ticTacToe = new Tictactoe(nullButtonList);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TicTacToe_InvalidButtonCountInButtonList_ThrowsException()
        {
            // Arrange
            IEnumerable<Button> filteredButtonList =
                GetBoardButtons().Where(x => x.Tag.ToString().StartsWith("0"));

            // Act
            var ticTacToe = new Tictactoe(filteredButtonList);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        public void GetBoardHorizontalSize_ValidUse_Success()
        {
            // Arrange
            int expectedBoardHorizontalSize = 3;

            // Act
            int actualBoardHorizontalSize = Tictactoe.GetBoardHorizontalSize();

            // Assert
            Assert.AreEqual(expectedBoardHorizontalSize, actualBoardHorizontalSize);
        }

        [TestMethod()]
        public void GetBoardVerticalSize_ValidUse_Success()
        {
            // Arrange
            int expectedBoardVerticalSize = 3;

            // Act
            int actualBoardVerticalSize = Tictactoe.GetBoardVerticalSize();

            // Assert
            Assert.AreEqual(expectedBoardVerticalSize, actualBoardVerticalSize);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void IsBoardFieldEmpty_ButtonParameterIsNull_ThrowsException()
        {
            // Arrange
            Button nullButton;
            nullButton = null;

            // Act
            bool invalidButtonResult = Tictactoe.IsBoardFieldEmpty(nullButton);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        public void IsBoardFieldEmpty_ValidUse_Success()
        {
            // Arrange
            var emptyFieldButton = new Button() { Content = Resources.BoardEmptyField };
            var crossMarkButton = new Button() { Content = Resources.BoardCrossMark };
            var circleMarkButton = new Button() { Content = Resources.BoardCircleMark };

            // Act
            bool emptyFieldButtonActualResult = Tictactoe.IsBoardFieldEmpty(emptyFieldButton);
            bool crossMarkButtonActualResult = Tictactoe.IsBoardFieldEmpty(crossMarkButton);
            bool circleMarkButtonActualResult = Tictactoe.IsBoardFieldEmpty(circleMarkButton);

            // Assert
            Assert.AreEqual(true, emptyFieldButtonActualResult);
            Assert.AreEqual(false, crossMarkButtonActualResult);
            Assert.AreEqual(false, circleMarkButtonActualResult);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsBoardFieldEmpty2_HorizontalCoordinateOutOfRange_ThrowsException()
        {
        // Arrange
        var ticTacToe = new Tictactoe(GetBoardButtons());

            // Act
            ticTacToe.IsBoardFieldEmpty(-1, 1);
            ticTacToe.IsBoardFieldEmpty(5, 1);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsBoardFieldEmpty2_VerticalCoordinateOutOfRange_ThrowsException()
        {
            // Arrange
            var ticTacToe = new Tictactoe(GetBoardButtons());

            // Act
            ticTacToe.IsBoardFieldEmpty(1, -1);
            ticTacToe.IsBoardFieldEmpty(1, 5);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        public void IsBoardFieldEmpty2_ValidUse_Success()
        {
            // Arrange
            var ticTacToe = new Tictactoe(GetBoardButtons());

            // Act
            ticTacToe.PlaceMarker(1, 0);

                bool fieldShouldBeEmpty = ticTacToe.IsBoardFieldEmpty(0, 0);
            bool fieldShouldNotBeEmpty = ticTacToe.IsBoardFieldEmpty(1, 0);

            // Assert
            Assert.AreEqual(true, fieldShouldBeEmpty);
                Assert.AreEqual(false, fieldShouldNotBeEmpty);
            
        }

        [TestMethod()]
        public void GetCurrentTurnPlayerMark_ValidUse_Success()
        {
            // Arrange
            var ticTacToe = new Tictactoe(GetBoardButtons());
            ticTacToe.StartNewGame(true, false, 0);

            // Act
            string shouldBeCrossMark = ticTacToe.GetCurrentTurnPlayerMark();
            ticTacToe.NextTurn();

            string shouldBeCircleMark = ticTacToe.GetCurrentTurnPlayerMark();
            ticTacToe.NextTurn();

            string shouldBeCrossMarkAgain = ticTacToe.GetCurrentTurnPlayerMark();

            // Assert
            Assert.AreEqual(Resources.BoardCrossMark, shouldBeCrossMark);
            Assert.AreEqual(Resources.BoardCircleMark, shouldBeCircleMark);
            Assert.AreEqual(Resources.BoardCrossMark, shouldBeCrossMarkAgain);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void StartNewGame_AiParameterOutOfRange_ThrowsException()
        {
            // Arrange
            var ticTacToe = new Tictactoe(GetBoardButtons());

            // Act
            ticTacToe.StartNewGame(false, false, 0);
            ticTacToe.StartNewGame(false, false, 1);
            ticTacToe.StartNewGame(false, false, 2);
            ticTacToe.StartNewGame(false, false, 3);
            ticTacToe.StartNewGame(false, false, -1);

            // Assert
            Assert.Fail("No exception was thrown.");
        }
        
        [TestMethod()]
        public void ResetBoard_ValidUse_Success()
        {
            // Arrange
            var ticTacToe = new Tictactoe(GetBoardButtons());
            ticTacToe.StartNewGame(true, false, 0);

            // Act
            // ResetBoard method call is in the Tictactoe() ctor

            // Assert
            for (int i = 0; i < Tictactoe.GetBoardHorizontalSize(); i++)
            {
                for (int j = 0; j < Tictactoe.GetBoardVerticalSize(); j++)
                {
                    bool isBoardFieldReset = ticTacToe.IsBoardFieldEmpty(i, j);

                    Assert.AreEqual(true, isBoardFieldReset);
                }
            }            
        }

        [TestMethod()]
        public void NextTurn_ValidUse_Success()
        {
            // Arrange
            var ticTacToe = new Tictactoe(GetBoardButtons());
            ticTacToe.StartNewGame(true, false, 0);

            // Act
            bool shouldBeTrue = ticTacToe.Turn;
            ticTacToe.NextTurn();

            bool shouldBeFalse = ticTacToe.Turn;
            ticTacToe.NextTurn();

            bool shouldBeTrueAgain = ticTacToe.Turn;

            // Assert
            Assert.AreEqual(true, shouldBeTrue);
            Assert.AreEqual(false, shouldBeFalse);
            Assert.AreEqual(true, shouldBeTrueAgain);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PlaceMarker_HorizontalParameterOutOfRange_ThrowsException()
        {
            // Arrange
            var ticTacToe = new Tictactoe(GetBoardButtons());

            // Act
            ticTacToe.PlaceMarker(5, 1);
            ticTacToe.PlaceMarker(-1, 1);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PlaceMarker_VerticalParameterOutOfRange_ThrowsException()
        {
            // Arrange
            var ticTacToe = new Tictactoe(GetBoardButtons());

            // Act
            ticTacToe.PlaceMarker(1, 5);
            ticTacToe.PlaceMarker(1, -1);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void PlaceMarker_ButtonParameterIsNull_ThrowsException()
        {
            // Arrange
            var ticTacToe = new Tictactoe(GetBoardButtons());
            ticTacToe.StartNewGame(true, false, 0);

            // Act            
            ticTacToe.PlaceMarker(null);

            // Assert
            Assert.Fail("No exception was thrown.");
        }
           
        [TestMethod()]
        public void PlaceMarker_ValidUse_Success()
        {
            // Arrange
            var ticTacToe = new Tictactoe(GetBoardButtons());
            ticTacToe.StartNewGame(true, false, 0);
            var buttonForCrossMarkPlacement = GetBoardButtons().First(x => x.Tag.ToString() == "00");
            var buttonForCircleMarkPlacement = GetBoardButtons().First(x => x.Tag.ToString() == "01");

            // Act
            ticTacToe.PlaceMarker(buttonForCrossMarkPlacement);
            ticTacToe.NextTurn();
            ticTacToe.PlaceMarker(buttonForCircleMarkPlacement);

            var buttonWithCrossMark = buttonForCrossMarkPlacement.Content.ToString();
            var buttonWithCircleMark = buttonForCircleMarkPlacement.Content.ToString();

            // Assert
            Assert.AreEqual(Resources.BoardCrossMark, buttonWithCrossMark);
            Assert.AreEqual(Resources.BoardCircleMark, buttonWithCircleMark);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void HasPlayerWon_BoardParameterIsNull_ThrowsException()
        {
            // Arrange
            string[,] gameBoard = null;

            // Act
            bool result = Tictactoe.HasPlayerWon(gameBoard, Resources.BoardCrossMark);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void HasPlayerWon_PlayerMarkParameterIsInvalid_ThrowsException()
        {
            // Arrange
            const string invalidPlayerMark = "A";

            // Act
            bool result = Tictactoe.HasPlayerWon(BoardAllEmptyField, invalidPlayerMark);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        public void HasPlayerWon_RowsAreCross_Success()
        {
            // Arrange

            // Act
            bool firstRowAllCrossResult = Tictactoe.HasPlayerWon(GetBoardFirstRowAllCross(), Resources.BoardCrossMark);
            bool secondRowAllCrossResult = Tictactoe.HasPlayerWon(GetBoardSecondRowAllCross(), Resources.BoardCrossMark);
            bool thirdRowAllCrossResult = Tictactoe.HasPlayerWon(GetBoardThirdRowAllCross(), Resources.BoardCrossMark);

            // Assert
            Assert.IsTrue(firstRowAllCrossResult);
            Assert.IsTrue(secondRowAllCrossResult);
            Assert.IsTrue(thirdRowAllCrossResult);
        }

        [TestMethod]
        public void HasPlayerWon_RowsAreCircle_Success()
        {
            // Arrange

            // Act
            bool firsRowAllCircleResult = Tictactoe.HasPlayerWon(GetBoardFirstRowAllCircle(), Resources.BoardCircleMark);
            bool secondRowAllCircleResult = Tictactoe.HasPlayerWon(GetBoardSecondRowAllCircle(),Resources.BoardCircleMark);
            bool thirdRowAllCircleResult = Tictactoe.HasPlayerWon(GetBoardThirdRowAllCircle(), Resources.BoardCircleMark);

            // Assert
            Assert.IsTrue(firsRowAllCircleResult);
            Assert.IsTrue(secondRowAllCircleResult);
            Assert.IsTrue(thirdRowAllCircleResult);
        }

        [TestMethod]
        public void HasPlayerWon_ColumnsAreCross_Success()
        {
            // Arrange

            // Act
            bool firstColumnAllCrossResult = Tictactoe.HasPlayerWon(GetBoardFirstColumnAllCross(), Resources.BoardCrossMark);
            bool secondColumnAllCrossResult = Tictactoe.HasPlayerWon(GetBoardSecondColumnAllCross(), Resources.BoardCrossMark);
            bool thirdColumnAllCrossResult = Tictactoe.HasPlayerWon(GetBoardThirdColumnAllCross(), Resources.BoardCrossMark);

            // Assert
            Assert.IsTrue(firstColumnAllCrossResult);
            Assert.IsTrue(secondColumnAllCrossResult);
            Assert.IsTrue(thirdColumnAllCrossResult);
        }

        [TestMethod]
        public void HasPlayerWon_ColumnsAreCircle_Success()
        {
            // Arrange

            // Act
            bool firsColumnAllCircleResult = Tictactoe.HasPlayerWon(GetBoardFirstColumnAllCircle(), Resources.BoardCircleMark);
            bool secondColumnAllCircleResult = Tictactoe.HasPlayerWon(GetBoardSecondColumnAllCircle(), Resources.BoardCircleMark);
            bool thirdColumnAllCircleResult = Tictactoe.HasPlayerWon(GetBoardThirdColumnAllCircle(), Resources.BoardCircleMark);

            // Assert
            Assert.IsTrue(firsColumnAllCircleResult);
            Assert.IsTrue(secondColumnAllCircleResult);
            Assert.IsTrue(thirdColumnAllCircleResult);
        }

        [TestMethod]
        public void HasPlayerWon_DiagonalsAreCross_Success()
        {
            // Arrange

            // Act
            bool topLeftBottomRightDiagonalAllCrossResult =
                Tictactoe.HasPlayerWon(GetBoardTopLeftBottomRightDiagonalAllCross(), Resources.BoardCrossMark);
            bool topRightBottomLeftDiagonalAllCrossResult =
                Tictactoe.HasPlayerWon(GetBoardTopRightBottomLeftDiagonalAllCross(), Resources.BoardCrossMark);

            // Assert
            Assert.IsTrue(topLeftBottomRightDiagonalAllCrossResult);
            Assert.IsTrue(topRightBottomLeftDiagonalAllCrossResult);
        }

        [TestMethod]
        public void HasPlayerWon_DiagonalsAreCircle_Success()
        {
            // Arrange

            // Act
            bool topLeftBottomRightDiagonalAllCircleResult =
                Tictactoe.HasPlayerWon(GetBoardTopLeftBottomRightDiagonalAllCircle(), Resources.BoardCircleMark);
            bool topRightBottomLeftDiagonalAllCircleResult =
                Tictactoe.HasPlayerWon(GetBoardTopRightBottomLeftDiagonalAllCircle(), Resources.BoardCircleMark);

            // Assert
            Assert.IsTrue(topLeftBottomRightDiagonalAllCircleResult);
            Assert.IsTrue(topRightBottomLeftDiagonalAllCircleResult);
        }

        [TestMethod()]
        public void HavePlayersDraw_ValidUse_Success()
        {
            // Arrange
            // Act
            bool everyFieldIsCrossBoardResult = Tictactoe.HavePlayersDraw(GetBoardAllCross());
            bool everyFieldIsCircleBoardResult = Tictactoe.HavePlayersDraw(GetBoardAllCircle());
            bool everyFieldIsEmptyBoardResult = Tictactoe.HavePlayersDraw(GetBoardAllEmptyField());

            // Assert
            Assert.IsTrue(everyFieldIsCrossBoardResult);
            Assert.IsTrue(everyFieldIsCircleBoardResult);
            Assert.IsFalse(everyFieldIsEmptyBoardResult);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void HavePlayersDraw_BoardParameterIsNull_ThrowsException()
        {
            // Arrange
            string[,] nullBoard = null;

            // Act
            bool result = Tictactoe.HavePlayersDraw(nullBoard);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        // TODO Need to implement NUnit testing framework to suppress MessageBox.Show() calls
        //[TestMethod()]
        //public void HasGameStateChanged_GameStateNotChanged_Success()
        //{
        //    // Arrange
        //    var ticTacToe = new Tictactoe(GetBoardButtons());

        //    // Act
        //    ticTacToe.StartNewGame(true, false, 0);
        //    ticTacToe.PlaceMarker(GetBoardButtons().First(x => x.Tag.ToString() == "00" ));
        //    ticTacToe.NextTurn();

        //    bool gameStateResult = ticTacToe.HasGameStateChanged();

        //    // Assert
        //    Assert.IsFalse(gameStateResult);
        //}

        //[TestMethod]
        //public void HasGameStateChanged_PlayerWins_Success()
        //{
        //    // Arrange
        //    var ticTacToe = new Tictactoe(GetBoardButtons());

        //    // Act
        //    ticTacToe.StartNewGame(true, false, 0);
        //    ticTacToe.PlaceMarker(GetBoardButtons().First(x => x.Tag.ToString() == "00"));
        //    ticTacToe.PlaceMarker(GetBoardButtons().First(x => x.Tag.ToString() == "01"));
        //    ticTacToe.PlaceMarker(GetBoardButtons().First(x => x.Tag.ToString() == "02"));

        //    bool gameStateResult = ticTacToe.HasGameStateChanged();

        //    // Assert
        //    Assert.IsTrue(gameStateResult);

        //}
    }
}
