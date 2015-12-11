using System;

namespace tictactoeTests.Classes
{
    public class MockRandom : Random
    {
        private static int _sequenceNumber = 0;

        /// <summary>
        /// Returns a predefined integer in a sequence
        /// </summary>
        /// <param name="minValue">Lower constraint value.</param>
        /// <param name="maxValue">Upper constraint value.</param>
        /// <returns></returns>
        public override int Next(int minValue, int maxValue)
        {
            // Coordinates of valid moves in x,y,x,y... succession
            int[] validCoordinates = { 0, 0, 1, 1, 2, 0, 2, 1 };
            IncrementSequence();

            return validCoordinates[_sequenceNumber-1];

        }

        /// <summary>
        /// Increments the internal sequence number
        /// </summary>
        private static void IncrementSequence()
        {
            if (_sequenceNumber > 9)
            {
                _sequenceNumber = 0;
            }

            else
            {
                _sequenceNumber++;
            }
        }
    }
}