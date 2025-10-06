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
            // Очищаем результаты предыдущего поиска
            SendDataFromFormEvent?.Invoke(this, new FigureEventArgs(null));

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

            // Значение объёма, введённое пользователем для фильтрации.
            double filterVolume = 0;

            // Флаг, указывающий, используется ли фильтрация по объёму.
            bool hasVolume = false;

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
                if (!double.TryParse(TextBoxVolume.Text,
                    System.Globalization.NumberStyles.Any,
                    new System.Globalization.CultureInfo("ru-RU"),
                    out filterVolume))
                {
                    MessageBox.Show(
                        "Некорректное значение объёма! Используйте число с запятой.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                filterVolume = Math.Round(filterVolume, 3);
                hasVolume = true;
            }

            foreach (FigureBase figures in _listFigureSearch)
            {
                bool matchType =
                    (CheckBoxParallelepiped.Checked
                    && figures is Parallelepiped) || 
                    (CheckBoxPyramid.Checked && figures is Pyramid) ||
                    (CheckBoxBall.Checked && figures is Ball);

                // Если вообще ни один чекбокс типа не выбран — считаем,
                // что matchType = true (разрешить любые типы)
                if (!CheckBoxParallelepiped.Checked &&
                    !CheckBoxPyramid.Checked &&
                    !CheckBoxBall.Checked)
                    matchType = true;

                bool matchVolume = !hasVolume ||
                    Math.Round(figures.Volume, 3) == filterVolume;

                // Добавлять только если выполнены ВСЕ активные фильтры
                if (matchType && matchVolume)
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
                    "Информация", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
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
