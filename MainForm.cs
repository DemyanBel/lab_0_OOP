using System;
using System.ComponentModel;
using System.Windows.Forms;
using Model;
using System.IO;
using System.Xml.Serialization;
using Model.Figures;


namespace Lab4
{
    /// <summary>
    /// Класс главная форма
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Инициализация формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            DataFigureView.AllowUserToAddRows = false;
            DataFigureView.RowHeadersVisible = false;
            DropFilterButton.Enabled = false;
            DataFigureView.MultiSelect = false;

        }

        /// <summary>
		/// Cписок фигур
		/// </summary>
		private BindingList<FigureBase> _figureList = 
            new BindingList<FigureBase>();

        /// <summary>
        /// Лист фильтрованных фигур
        /// </summary>
        private readonly BindingList<FigureBase> _listForSearch = 
            new BindingList<FigureBase>();


        /// <summary>
        /// Для файлов
        /// </summary>
        private readonly XmlSerializer _serializer = 
            new XmlSerializer(typeof(BindingList<FigureBase>));
        
        /// <summary>
        /// Событие при загрузке формы
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            CreateTable(_figureList, DataFigureView);
        }


        /// <summary>
        /// Событие при добавлении фигуры
        /// </summary>
        private void AddFigureButton_Click(object sender, EventArgs e)
        {
            var figureForm = new AddFigureForm();

            if (figureForm.ShowDialog() == DialogResult.OK)
            {
                var newFigure = figureForm.FigureData;
                _figureList.Add(newFigure);
            }
        }

        /// <summary>
        /// Метод проверки на пустоту списка
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool EnsureFigureListNotEmpty(string message = null)
        {
            if (_figureList.Count == 0)
            {
                MessageBox.Show(
                    message ?? "Список фигур пуст.",
                    "Информация",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return false;
            }
            return true;
        }

        /// <summary>
        /// Событие при удалении фигуры
        /// </summary>
        private void DeleteFigureButton_Click(object sender, EventArgs e)
        {
            if (!EnsureFigureListNotEmpty()) return;

            if (DataFigureView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите фигуру для удаления.",
                    "Информация", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int selectedIndex = DataFigureView.SelectedRows[0].Index;
            if (DataFigureView.DataSource == _listForSearch)
            {
                var figureToRemove = _listForSearch[selectedIndex];
                _listForSearch.RemoveAt(selectedIndex);
                _figureList.Remove(figureToRemove);
            }
            else
            {
                _figureList.RemoveAt(selectedIndex);
            }
            if (_figureList.Count == 0)
            {
                DropFilterButton_Click(this, EventArgs.Empty);
            }
            else
            {
                // Сброс фильтра, когда отфильтрованный список пуст, а основной нет
                if (_listForSearch.Count == 0 && DataFigureView.DataSource == _listForSearch)
                {
                    DropFilterButton_Click(this, EventArgs.Empty);
                }
            }
        }
        
        /// <summary>
        /// Событие при загрузке файла
        /// </summary>
        private void LoadToolStripMenuItemClick(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Файлы (*.di)|*.di|Все файлы (*.*)|*.*",
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            var path = openFileDialog.FileName.ToString();
            try
            {
                using (FileStream fileStream = new FileStream(path,
                    FileMode.OpenOrCreate))
                {
                    _figureList = (BindingList<FigureBase>)_serializer.
                        Deserialize(fileStream);
                }
                DataFigureView.DataSource = _figureList;
                DataFigureView.CurrentCell = null;
                MessageBox.Show("Файл успешно загружен.", 
                    "Загрузка завершена",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Файл повреждён или не соответствует " +
                    $"формату.\n\nОшибка:\n{ex.Message}" +
                    $"\n\nСтек вызовов:\n{ex.StackTrace}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }
        
        /// <summary>
        /// Событие при сохранении файла
        /// </summary>
        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (!EnsureFigureListNotEmpty("Отсутствуют данные для сохранения.")) return;

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Файлы (*.di)|*.di|Все файлы (*.*)|*.*",
                AddExtension = true,
                DefaultExt = ".di"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var path = saveFileDialog.FileName.ToString();
                using (FileStream fileStream = new FileStream(path,
                    FileMode.OpenOrCreate))
                {
                    _serializer.Serialize(fileStream, _figureList);
                }
                MessageBox.Show("Файл успешно сохранён.",
                    "Сохранение завершено", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Событие при генерации случайной фигуры
        /// </summary>
        private void RandomFigureButton_Click(object sender, EventArgs e)
        {
            var newFigure = RandomFigure.GetRandomFigure();
            _figureList.Add(newFigure);
        }
        
        /// <summary>
        /// Событие при поиске фигуры
        /// </summary>
        private void SearchFigureButton_Click(object sender, EventArgs e)
        {
            var figureSearch = new SearchFigureForm(_figureList);
            figureSearch.SendDataFromFormEvent += AddSearchFigureEvent;
            SearchFigureButton.Enabled = false;
            figureSearch.FormClosed += (s, args) =>
            {
                // Активировать кнопку обратно после закрытия окна
                SearchFigureButton.Enabled = true;
            };
            figureSearch.Show();
        }

        /// <summary>
        /// Обработчик события получения данных из формы поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddSearchFigureEvent(object sender, FigureEventArgs e)
        {
            if (e.SendingFigure == null)
            {
                _listForSearch.Clear();
                return;
            }
            if (!_listForSearch.Contains(e.SendingFigure))
            {
                _listForSearch.Add(e.SendingFigure);
            }
            CreateTable(_listForSearch, DataFigureView);
            DropFilterButton.Enabled = true;
            SearchFigureButton.Enabled = false;
            AddFigureButton.Enabled = false;
            RandomFigureButton.Enabled = false;
        }

        // <summary>
        /// Обработчик события при сбросе фильтрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropFilterButton_Click(object sender, EventArgs e)
        {
            DataFigureView.DataSource = null;
            CreateTable(_figureList, DataFigureView);
            DeleteFigureButton.Enabled = true;
            SearchFigureButton.Enabled = true;
            AddFigureButton.Enabled = true;
            RandomFigureButton.Enabled = true;
            DropFilterButton.Enabled = false;
            _listForSearch.Clear();
        }

        /// <summary>
        /// Событие при очистке всего списка
        /// </summary>
        private void DeleteAllFugureButton_Click(object sender, EventArgs e)
        {
            if (!EnsureFigureListNotEmpty()) return;

            var result = MessageBox.Show("Вы уверены, что " +
                "хотите удалить все фигуры из списка?",
                "Подтверждение", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _figureList.Clear();
                _listForSearch.Clear();
                DataFigureView.DataSource = null;
                CreateTable(_figureList, DataFigureView);

                // Сброс фильтра
                DropFilterButton_Click(this, EventArgs.Empty);

                MessageBox.Show("Список фигур успешно очищен.",
                    "Успех", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Метод создания таблицы желаемого формата
        /// </summary>
        /// <param name="figures">Список конденсаторов</param>
        /// <param name="dataGridView">Таблица с фигурами</param>
        /// <summary>
        /// Метод создания таблицы 
        /// </summary>
        private static void CreateTable(BindingList<FigureBase> figures,
            DataGridView dataGridView)
        {
            dataGridView.DataSource = null;
            dataGridView.DataSource = figures;
            dataGridView.Columns[0].HeaderText = "Фигура";
            dataGridView.Columns[1].HeaderText = "Объём (м)";
            dataGridView.Columns[1].DefaultCellStyle.Format = "F3";
            dataGridView.AutoSizeColumnsMode =
               DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dataGridView.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
