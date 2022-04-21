using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuOOP
{
    public partial class Form1 : Form
    {
        static Map mapGame = new Map();
        static GameSudoku gameSudoku = new GameSudoku(mapGame.GetCells());
        public Form1()
        {
            InitializeComponent();
            mapGame.createCells(panel1);
            startNewGame();
        }

        
        SudokuCell[,] cells = mapGame.GetCells();
        

        private void startNewGame()
        {
            gameSudoku.loadValues();

            int digitCount = 45;


            gameSudoku.showRandomValuesCells(digitCount);
        }


        private void newGameButton_Click(object sender, EventArgs e)
        {
            startNewGame();
        }

        private void checkAnswerButton_Click(object sender, EventArgs e)
        {
            var wrongCells = new List<SudokuCell>();

            // Знайдіть усі неправильні дані
            foreach (var cell in cells)
            {
                if (!string.Equals(cell.Value.ToString(), cell.Text))
                {
                    wrongCells.Add(cell);
                }
            }

            // Перевірте, чи введені дані неправильні або гравець виграє
            if (wrongCells.Any())
            {
                // Виділіть неправильні введення червоним кольором
                wrongCells.ForEach(x => x.ForeColor = Color.Red);
                MessageBox.Show("Ойй... Але ти не вмієш грати в Судоку...");
            }
            else
            {
                MessageBox.Show("УРА!!! Перемога...");
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            foreach (var cell in cells)
            {
                // Очистіть клітинку, лише якщо вона не заблокована
                if (cell.IsLocked == false)
                    cell.Clear();
            }
        }
    }
}
