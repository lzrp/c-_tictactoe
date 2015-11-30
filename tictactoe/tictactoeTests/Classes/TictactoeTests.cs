using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tictactoe.Classes;

namespace tictactoeTests.Classes
{
    [TestClass()]
    public class TictactoeTests
    {
        readonly IEnumerable<Button> _buttons = new List<Button>()
        { new Button() {Tag = 00}, new Button() {Tag = 01}, new Button() {Tag = 02},
          new Button() {Tag = 10}, new Button() {Tag = 11}, new Button(){Tag = 12},
          new Button() {Tag = 20}, new Button() {Tag = 21}, new Button() {Tag = 22}
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
            int expectedBoardSizeHorizontal = 3;

            // Act
            int actualBoardSizeHorizontal = Tictactoe.GetBoardHorizontalSize();

            // Assert
            Assert.AreEqual(expectedBoardSizeHorizontal, actualBoardSizeHorizontal);
        }

        [TestMethod()]
        public void GetBoardSizeVerticalTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AskForNewGameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsBoardFieldEmptyTest()
        {
            Assert.Fail();
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