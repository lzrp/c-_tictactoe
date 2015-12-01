using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tictactoe.Classes;
using tictactoeTests.Properties;

namespace tictactoeTests.Classes
{
    [TestClass()]
    public class TictactoeTests
    {
        private readonly List<Button> _buttons = new List<Button>()
        { new Button() {Tag = 00},
          new Button() {Tag = 01},
          new Button() {Tag = 02},
          new Button() {Tag = 10},
          new Button() {Tag = 11},
          new Button() {Tag = 12},
          new Button() {Tag = 20, Content = Resources.BoardCircleMark},
          new Button() {Tag = 21, Content = Resources.BoardCrossMark},
          new Button() {Tag = 22, Content = Resources.BoardEmptyField}
        };

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TicTacToe_ButtonListParameterIsNull_ThrowsException()
        {
            // Assign
            IEnumerable<Button> nullButtonList = null;

            // Act
            var ticTacToe = new Tictactoe(nullButtonList);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void TicTacToe_InvalidButtonCountInButtonList_ThrowsException()
        {
            // Assign
            IEnumerable<Button> filteredButtonList =
                _buttons.Where(x => x.Tag.ToString().StartsWith("0"));

            // Act
            var ticTacToe = new Tictactoe(filteredButtonList);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        public void GetBoardHorizontalSize_ValidUse_Success()
        {
            // Assign
            int expectedBoardHorizontalSize = 3;

            // Act
            int actualBoardHorizontalSize = Tictactoe.GetBoardHorizontalSize();

            // Assert
            Assert.AreEqual(expectedBoardHorizontalSize, actualBoardHorizontalSize);
        }

        [TestMethod()]
        public void GetBoardVerticalSize_ValidUse_Success()
        {
            // Assign
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
            // Assign
            bool validButtonActualResult = Tictactoe.IsBoardFieldEmpty(_buttons.First(x => x.Content.Equals(Resources.BoardEmptyField)));
            Button nullButton = new Button();
            nullButton = null;

            // Act
            bool invalidButtonResult = Tictactoe.IsBoardFieldEmpty(nullButton);

            // Assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod()]
        public void IsBoardFieldEmpty_ValidUse_Success()
        {
            // Assign
            bool emptyFieldButtonExpectedResult = true;
            bool crossMarkButtonExpectedResult = false;
            bool circleMarkButtonExpectedResult = false;

            var emptyFieldButton = _buttons.First(x => x.Content.ToString().StartsWith(Resources.BoardEmptyField));
            var crossMarkButton = _buttons.First(x => x.Content.Equals(Resources.BoardCrossMark));
            var circleMarkButton = _buttons.First(x => x.Content.Equals(Resources.BoardCircleMark));

            // Act
            bool emptyFieldButtonActualResult = Tictactoe.IsBoardFieldEmpty(emptyFieldButton);
            bool crossMarkButtonActualResult = Tictactoe.IsBoardFieldEmpty(crossMarkButton);
            bool circleMarkButtonActualResult = Tictactoe.IsBoardFieldEmpty(circleMarkButton);

            // Assert
            Assert.AreEqual(emptyFieldButtonExpectedResult, emptyFieldButtonActualResult);
            Assert.AreEqual(crossMarkButtonExpectedResult, crossMarkButtonActualResult);
            Assert.AreEqual(circleMarkButtonExpectedResult, circleMarkButtonActualResult);
        }

        [TestMethod()]
        public void IsBoardFieldEmptyTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCurrentTurnPlayerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void NewGameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RestartGameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateUiTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void NextTurnTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PlaceMarkerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PlaceMarkerTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CheckWinnerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CheckForDrawTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CheckForDrawTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GameStateChangedTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AnnounceWinnerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AnnounceDrawTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StopGameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetButtonHorizontalCoordinateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetButtonVerticalCoordinateTest()
        {
            Assert.Fail();
        }
    }
}