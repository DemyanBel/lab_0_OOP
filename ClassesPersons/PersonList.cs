namespace ClassPerson
{
    /// <summary>
    /// Класс, представляющий список экземпляров PersonBase.
    /// </summary>
    public class PersonList
    {
        /// <summary>
        /// Список экземпляров PersonBase.
        /// </summary>
        private List<PersonBase> _people = new List<PersonBase>();

        //TODO: encapsulation +
        /// <summary>
        /// Получает список людей, хранящихся в данном объекте.
        /// </summary>
        private List<PersonBase> People => _people;

        //TODO: rename +
        /// <summary>
        /// Метод добавления элементов списка.
        /// </summary>
        public void Add(PersonBase person)
        {
            _people.Add(person);
        }

        //TODO: rename +
        /// <summary>
        /// Метод удаления элементов списка.
        /// </summary>     `
        public void Remove(PersonBase person)
        {
            _people.Remove(person);
        }

        /// <summary>
        /// Метод удаления элемента по индексу.
        /// </summary>
        public void RemoveIndex(int index)
        {
            ValidateIndex(index);
            _people.RemoveAt(index);
        }

        /// <summary>
        /// Метод получения элемента по индексу.
        /// </summary>
        public PersonBase GetElement(int index)
        {
            ValidateIndex(index);
            return _people[index];
        }

        /// <summary>
        /// Вспомогательный метод для проверки индекса на допустимость.
        /// </summary>
        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= _people.Count)
            {
                throw new IndexOutOfRangeException("Индекс" +
                    " вне диапазона.");
            }
        }

        //TODO: rename +
        /// <summary>
        /// Метод возвращает индекс элемента в списке.
        /// </summary>
        public int GetIndex(PersonBase person)
        {
            return _people.IndexOf(person);
        }

        /// <summary>
        /// Метод очистки списка.
        /// </summary>
        public void Clear()
        {
            _people.Clear();
        }

        /// <summary>
        /// Определение количества элементов в списке.
        /// </summary>
        public int Count => _people.Count;
    }
}
