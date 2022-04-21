using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SudokuOOP
{
    public class Map : SudokuCell
    {

        public SudokuCell[,] GetCells()
        {
            return cells;
        }
        public void createCells(Panel panel1)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // Створіть 81 клітинку зі стилями та розташуваннями на основі індексу
                    cells[i, j] = new SudokuCell();
                    cells[i, j].Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
                    cells[i, j].Size = new Size(50, 50);
                    cells[i, j].ForeColor = SystemColors.ControlDarkDark;
                    cells[i, j].Location = new Point(i * 50, j * 50);
                    cells[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : Color.LightGray; ;
                    cells[i, j].FlatStyle = FlatStyle.Flat;
                    cells[i, j].FlatAppearance.BorderColor = Color.Black;
                    cells[i, j].X = i;
                    cells[i, j].Y = j;

                    // Призначте подію натискання клавіші для кожної клітинки
                    cells[i, j].KeyPress += cell_keyPressed;

                    panel1.Controls.Add(cells[i, j]);
                }
            }
        }
        private void cell_keyPressed(object sender, KeyPressEventArgs e)
        {
            var cell = sender as SudokuCell;

            //Нічого не робіть, якщо комірка заблокована
            if (cell.IsLocked)
                return;

            int value;

            //Додайте значення натиснутої кнопки в клітинку, тільки якщо це число
            if (int.TryParse(e.KeyChar.ToString(), out value))
            {
                // Очистіть значення комірки, якщо натиснута клавіша дорівнює нулю
                if (value == 0)
                    cell.Clear();
                else
                    cell.Text = value.ToString();

                cell.ForeColor = SystemColors.ControlDarkDark;
            }
        }
    }
}
