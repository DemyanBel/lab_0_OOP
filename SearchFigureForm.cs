using System;
using System.ComponentModel;
using System.Windows.Forms;
using Model;
using Model.Figures;


namespace Lab4
{
    /// <summary>
    /// Класс, описывающий форму для поиска 
    /// </summary>
    public partial class SearchFigureForm : Form
    {
        /// <summary>
        /// Ивент для передачи данных 
        /// </summary>
        public event EventHandler<FigureEventArgs> SendDataFromFormEvent;

        /// <summary>
        /// Лист фильтрованных фигур
        /// </summary>
        private readonly BindingList<FigureBase> _listFigureSearch;

        /// <summary>
        /// Событие при инициализации формы
        /// </summary>
        public SearchFigureForm(BindingList<FigureBase> figures)
        {
            InitializeComponent();
            _listFigureSearch = figures;
            MaximizeBox = false;
            TextBoxVolume.Enabled = false;
        }

        /// <summary>
        /// Обработка чисел на форме
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericTextboxKeyPress(object sender, 
            KeyPressEventArgs e)
        {
            if (double.TryParse(((TextBox)sender).Text + e.KeyChar, out _)
                || e.KeyChar == (char)Keys.Back) return;
        }

        /// <summary>
        /// Обработчик изменения свойства Check объекта VolumeCheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxVolumeCheckedChanged(object sender, EventArgs e)
        {
            TextBoxVolume.Enabled = CheckBoxVolume.Checked;
        }

        /// <summary>
        /// Кнопка Поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonShowFigure_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (!CheckBoxParallelepiped.Checked &&
                !CheckBoxPyramid.Checked &&
                !CheckBoxBall.Checked &&
                !CheckBoxVolume.Checked)
            {
                MessageBox.Show(
                    "Не выбрано ни одного критерия фильтрации!\n" +
                    "Выбор осуществляется постановкой флажка.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckBoxVolume.Checked)
            {
                if (string.IsNullOrWhiteSpace(TextBoxVolume.Text))
                {
                    MessageBox.Show(
                        "Вы выбрали фильтрацию по объёму, " +
                        "но не ввели значение объёма!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Проверка, что введено число с запятой
                decimal volume;
                if (!decimal.TryParse(TextBoxVolume.Text, out volume))
                {
                    MessageBox.Show(
                        "Некорректное значение объёма! Используйте число с запятой.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            foreach (FigureBase figures in _listFigureSearch)
            {
                switch (figures)
                {
                    case Parallelepiped _ when CheckBoxParallelepiped.Checked:
                    case Pyramid _ when CheckBoxPyramid.Checked:
                    case Ball _ when CheckBoxBall.Checked:
                        {
                            count++;
                            SendDataFromFormEvent?.Invoke(this, 
                                new FigureEventArgs(figures));
                            break;
                        }
                }

                if (CheckBoxVolume.Checked && figures.Volume.ToString().
                    StartsWith(TextBoxVolume.Text))
                {
                    count++;
                    SendDataFromFormEvent?.Invoke(this, 
                        new FigureEventArgs(figures));
                }
            }
            if (count == 0)
            {
                MessageBox.Show(
                    "Нет ни одной фигуры, удовлетворяющей" +
                    " выбранным критериям поиска.",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            CheckBoxParallelepiped.Checked = false;
            CheckBoxPyramid.Checked = false;
            CheckBoxBall.Checked = false;
            CheckBoxVolume.Checked = false;
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        private void CloseFormButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
