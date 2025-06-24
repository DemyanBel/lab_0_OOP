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
            DataGridFigureTools.CreateTable(_figureList, DataFigureView);
        }

        /// <summary>
        /// Проверка, существует ли фигура в списке
        /// </summary>
        /// <param name="newFigure">Новая фигура для проверки</param>
        /// <returns>True, если фигура уже существует</returns>
        private bool IsFigureDuplicate(FigureBase newFigure)
        {
            foreach (var existingFigure in _figureList)
            {
                if (newFigure.GetType() != existingFigure.GetType())
                    continue;

                switch (newFigure)
                {
                    case Parallelepiped newBox when existingFigure 
                        is Parallelepiped existingBox:
                        if (Math.Abs(newBox.Length - 
                            existingBox.Length) < 0.0001 &&
                            Math.Abs(newBox.Width - 
                            existingBox.Width) < 0.0001 &&
                            Math.Abs(newBox.Height - 
                            existingBox.Height) < 0.0001)
                            return true;
                        break;

                    case Pyramid newPyramid when existingFigure 
                        is Pyramid existingPyramid:
                        if (Math.Abs(newPyramid.Length - 
                                existingPyramid.Length) < 0.0001 &&
                            Math.Abs(newPyramid.Width - 
                                existingPyramid.Width) < 0.0001 &&
                            Math.Abs(newPyramid.Height - 
                                existingPyramid.Height) < 0.0001)
                            return true;
                        break;

                    case Ball newBall when existingFigure 
                        is Ball existingBall:
                        if (Math.Abs(newBall.Radius - 
                                existingBall.Radius) < 0.0001)
                            return true;
                        break;
                }
            }
            return false;
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
                if (IsFigureDuplicate(newFigure))
                {
                    MessageBox.Show("Такая фигура уже " +
                        "существует в списке!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _figureList.Add(newFigure);
            }
        }

        /// <summary>
        /// Событие при удалении фигуры
        /// </summary>
        private void DeleteFigureButton_Click(object sender, EventArgs e)
        {
            if (_figureList.Count == 0)
            {
                MessageBox.Show("Список фигур пуст.",
                    "Информация", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

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
            catch (Exception)
            {
                MessageBox.Show("Файл повреждён или не " +
                    "соответствует формату.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Событие при сохранении файла
        /// </summary>
        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_figureList.Count == 0)
            {
                MessageBox.Show("Отсутствуют данные для сохранения.",
                    "Данные не сохранены",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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
            if (IsFigureDuplicate(newFigure))
            {
                MessageBox.Show("Такая фигура уже существует в списке!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _figureList.Add(newFigure);
        }
        
        /// <summary>
        /// Событие при поиске фигуры
        /// </summary>
        private void SearchFigureButton_Click(object sender, EventArgs e)
        {
            var figureSearch = new SearchFigureForm(_figureList);
            figureSearch.SendDataFromFormEvent += AddSearchFigureEvent;
            figureSearch.Show();
        }

        /// <summary>
        /// Обработчик события получения данных из формы поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddSearchFigureEvent(object sender, FigureEventArgs e)
        {
            if (!_listForSearch.Contains(e.SendingFigure))
            {
                _listForSearch.Add(e.SendingFigure);
            }
            DataGridFigureTools.CreateTable(_listForSearch, DataFigureView);
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
            DataGridFigureTools.CreateTable(_figureList, DataFigureView);
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
            if (_figureList.Count == 0)
            {
                MessageBox.Show("Список фигур пуст.",
                    "Информация", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Вы уверены, что " +
                "хотите удалить все фигуры из списка?",
                "Подтверждение", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _figureList.Clear();
                _listForSearch.Clear();
                DataFigureView.DataSource = null;
                DataGridFigureTools.CreateTable(_figureList, DataFigureView);
                MessageBox.Show("Список фигур успешно очищен.",
                    "Успех", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
