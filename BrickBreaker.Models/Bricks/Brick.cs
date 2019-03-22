namespace BrickBreaker.Models.Bricks
{
    using System;
    using BrickBreaker.Models.Bricks.Contracts;
   
    public class Brick : IBrick
    {
        private string brickSymbol = "$";
        private int rowsLenght = 25;
        private int colsLenght = Console.WindowWidth - 2; 
        private int startPrintingIndex = 2;

        public Brick()
        {
            this.Matrix = new int[Console.WindowWidth+1, Console.WindowHeight+1];
            this.InitiateMatrix();
        }

        public int[,] Matrix { get; }

        public string BrickSymbol => brickSymbol;

        public int BrickCount { get; private set; }

        private void InitiateMatrix()
        {
            for (int i = startPrintingIndex; i < colsLenght; i++)
            {
                for (int j = startPrintingIndex; j < rowsLenght; j++)
                {
                    this.Matrix[i, j] = 1;
                }
            }
        }

        public void IncreaseBricksCount()
        {
            this.BrickCount++;
        }

        public void DecreaseBrickCount()
        {
            this.BrickCount--;
        }
    }
}
