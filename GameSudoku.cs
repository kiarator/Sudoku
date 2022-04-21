using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuOOP
{
    public class GameSudoku
    {
        private SudokuCell[,] _cells;
        public GameSudoku(SudokuCell[,] cells)
        {
            _cells = cells;
        }
        public void showRandomValuesCells(int digitCount)
        {
            // Показати значення у випадкових клітинках

            for (int i = 0; i < digitCount; i++)
            {
                var rX = random.Next(9);
                var rY = random.Next(9);

                //Заблокувати клітинку, щоб гравець не міг редагувати значення
                _cells[rX, rY].Text = _cells[rX, rY].Value.ToString();
                _cells[rX, rY].ForeColor = Color.Black;
                _cells[rX, rY].IsLocked = true;
            }
        }

        public void loadValues()
        {
            //Очистіть значення в кожній клітинці
            foreach (var cell in _cells)
            {
                cell.Value = 0;
                cell.Clear();
            }

            // Цей метод буде викликатися рекурсивно
            // доки не знайде відповідні значення для кожної клітинки
            findValueForNextCell(0, -1);
        }

        Random random = new Random();

        private bool findValueForNextCell(int i, int j)
        {
            // Збільшуємо значення i та j, щоб перейти до наступної клітинки
            // і якщо стовпець закінчується, перейдіть до наступного рядка
            if (++j > 8)
            {
                j = 0;

                // Exit if the line ends
                if (++i > 8)
                    return true;
            }

            var value = 0;
            var numsLeft = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Знайти випадкове та валідне число для комірки та перейти до наступної клітинки
            // і перевіряємо, чи можна призначити йому інше випадкове та валідне число
            do
            {
                // Якщо в списку не залишилося номерів, щоб спробувати наступний,
                // повертаємось до попередньої комірки і приділяємо їй інший номер
                if (numsLeft.Count < 1)
                {
                    _cells[i, j].Value = 0;
                    return false;
                }

                // Беремо випадкове число з чисел, що залишилися у списку
                value = numsLeft[random.Next(0, numsLeft.Count)];
                _cells[i, j].Value = value;

                //Видалити виділене значення зі списку
                numsLeft.Remove(value);
            }
            while (!isValidNumber(value, i, j) || !findValueForNextCell(i, j));

            return true;
        }

        private bool isValidNumber(int value, int x, int y)
        {
            for (int i = 0; i < 9; i++)
            {
                // Перевірте всі клітинки у вертикальному напрямку
                if (i != y && _cells[x, i].Value == value)
                    return false;

                //Перевірте всі клітинки в горизонтальному напрямку
                if (i != x && _cells[i, y].Value == value)
                    return false;
            }

            // Перевірте всі клітинки в певному блоці
            for (int i = x - (x % 3); i < x - (x % 3) + 3; i++)
            {
                for (int j = y - (y % 3); j < y - (y % 3) + 3; j++)
                {
                    if (i != x && j != y && _cells[i, j].Value == value)
                        return false;
                }
            }

            return true;
        }


    }
}
