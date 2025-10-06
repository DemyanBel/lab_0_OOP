using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Model;
using Model.Figures;

namespace Lab4
{
    /// <summary>
    /// Класс, описывающий форму добавления
    /// </summary>
    public partial class AddFigureForm : Form
    {
        /// <summary>
        /// Словарь для установки видимости контролов. 
        /// Все значения по умолчанию false.
        /// </summary>
        private readonly Dictionary<Control, bool> _defaultVisibilityDict;

        /// <summary>
        /// Инициализация формы и словаря
        /// </summary>
        public AddFigureForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            // Инициализация дефолтных значений видимости (все false)
            _defaultVisibilityDict = new Dictionary<Control, bool>
            {
                { LengthTextbox, false },
                { LengthLabel, false },
                { WidthTextbox, false },
                { WidthLabel, false },
                { HeightTextbox, false },
                { HeigthLabel, false },
                { RadiusTextbox, false },
                { RadiusLabel, false }
            };
            
            OkAddFigureButton.Enabled = false;
            LengthTextbox.TextChanged += ShowOKButton;
            WidthTextbox.TextChanged += ShowOKButton;
            HeightTextbox.TextChanged += ShowOKButton;
            RadiusTextbox.TextChanged += ShowOKButton;
        }
        
        /// <summary>
        /// Поле для создания фигуры
        /// </summary>
        private FigureBase _figure;

        /// <summary>
        /// Свойство для вывода данных о фигуре
        /// </summary>
        public FigureBase FigureData
        {
            get
            {
                return _figure;
            }
        }

        /// <summary>
        /// Установка видимых TextBox в зависимости
        /// от выбранной фигуры
        /// </summary>
        /// <param name="figure">Фигура</param>
        private void MakeVisible(FigureBase figure)
        {
            // Все контролы скрыты (false)
            foreach (var control in _defaultVisibilityDict.Keys)
            {
                control.Visible = false;
            }

            switch (figure)
            {
                case Parallelepiped _:
                {
                    LengthTextbox.Visible = true;
                    LengthLabel.Visible = true;
                    WidthTextbox.Visible = true;
                    WidthLabel.Visible = true;
                    HeightTextbox.Visible = true;
                    HeigthLabel.Visible = true;
                    break;
                }
                case Pyramid _:
                {
                    LengthTextbox.Visible = true;
                    LengthLabel.Visible = true;
                    WidthTextbox.Visible = true;
                    WidthLabel.Visible = true;
                    HeightTextbox.Visible = true;
                    HeigthLabel.Visible = true;
                    break;
                }
                case Ball _:
                {
                    RadiusTextbox.Visible = true;
                    RadiusLabel.Visible = true;
                    break;
                }
                default:
                {
                    throw new ArgumentException("Вы не выбрали " +
                        "тип фигуры!");
                }
            }
        }

        /// <summary>
        /// Событие при выборе фигуры в меню добавления
        /// </summary>
        private void FigureChoiceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (FigureChoiceComboBox.SelectedIndex)
            {
                case 0:
                {
                    _figure = new Parallelepiped();
                    MakeVisible(_figure);
                    break;
                }
                case 1:
                {
                    _figure = new Pyramid();
                    MakeVisible(_figure);
                    break;
                }
                case 2:
                {
                    _figure = new Ball();
                    MakeVisible(_figure);
                    break;
                }
            }
        }

        /// <summary>
        /// Установка значения свойствам экземпляра класса 
        /// Параллелепипед/Пирамида/Шар
        /// </summary>
        private void SetValue(Action action)
        {
            action.Invoke();
        }

        /// <summary>
        /// Ввод данных о параллелепипеде
        /// </summary>
        /// <returns>Созданный экземпляр класса Parallelepiped</returns>
        private Parallelepiped GetNewParallelepiped()
        {
            var newParallelepiped = new Parallelepiped();
            var actions = new List<Action>()
            {
                new Action(() =>
                {
                    newParallelepiped.Length = 
                    Convert.ToDouble(LengthTextbox.Text);
                }),
                new Action(() =>
                {
                    newParallelepiped.Width =
                    Convert.ToDouble(WidthTextbox.Text);
                }),
                new Action(() =>
                {
                    newParallelepiped.Height =
                    Convert.ToDouble(HeightTextbox.Text);
                })
            };
            actions.ForEach(SetValue);
            return newParallelepiped;
        }

        /// <summary>
        /// Ввод данных о пирамиде
        /// </summary>
        /// <returns>Созданный экземпляр класса Pyramid</returns>
        private Pyramid GetNewPyramid()
        {
            var newPyramid = new Pyramid();
            var actions = new List<Action>()
            {
                new Action(() =>
                {
                    newPyramid.Length =
                    Convert.ToDouble(LengthTextbox.Text);
                }),
                new Action(() =>
                {
                    newPyramid.Width =
                    Convert.ToDouble(WidthTextbox.Text);
                }),
                new Action(() =>
                {
                    newPyramid.Height =
                    Convert.ToDouble(HeightTextbox.Text);
                })
            };
            actions.ForEach(SetValue);
            return newPyramid;

        }

        /// <summary>
        /// Ввод данных о шаре
        /// </summary>
        /// <returns>Созданный экземпляр класса Ball</returns>
        private Ball GetNewBall()
        {
            var newBall = new Ball();
            var actions = new List<Action>()
            {
                new Action(() =>
                {
                    newBall.Radius =
                    Convert.ToDouble(RadiusTextbox.Text);
                })
            };
            actions.ForEach(SetValue);
            return newBall;
        }

        /// <summary>
        /// Ввод данных о фигурах
        /// </summary>
        private void InsertData()
        {
            switch (_figure)
            {
                case Parallelepiped _:
                {
                    _figure = GetNewParallelepiped();
                    break;
                }
                case Pyramid _:
                {
                    _figure = GetNewPyramid();
                    break;
                }
                case Ball _:
                {
                    _figure = GetNewBall();
                    break;
                }
                default:
                {
                    throw new ArgumentException("Такой фигуры нет в природе.");
                }
            }
        }

        // <summary>
        /// Событие при добавлении новой фигуры
        /// </summary>
        private void OkAddFigureButton_Click(object sender, EventArgs e)
        {
            try
            {
                InsertData();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                MessageBox.Show("Введено некорректное значение," +
                    " проверьте данные!\nВы должны ввести одно положительное " +
                    "десятичное число в каждое текстовое поле." +
                    " В качестве разделителя используйте запятую.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LengthTextbox.Clear();
                WidthTextbox.Clear();
                HeightTextbox.Clear();
                RadiusTextbox.Clear();
            }
        }

        /// <summary>
        /// Обработка чисел на форме
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericTextboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (double.TryParse(((TextBox)sender).Text + e.KeyChar, out _)
                || e.KeyChar == (char)Keys.Back) return;
        }

        /// <summary>
        /// Активация кнопки ОК, если заполнены поля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowOKButton(object sender, EventArgs e)
        {
            switch (FigureChoiceComboBox.SelectedIndex)
            {
                case 0:
                case 1:
                {
                    OkAddFigureButton.Enabled = LengthTextbox.Text.Length > 0
                        && WidthTextbox.Text.Length > 0
                        && HeightTextbox.Text.Length > 0;
                    break;
                }
                case 2:
                {
                    OkAddFigureButton.Enabled = RadiusTextbox.Text.Length > 0;
                    break;
                }
            }
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
