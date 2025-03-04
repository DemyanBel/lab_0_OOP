namespace ClassPerson
{
    /// <summary>
    /// Класс, представляющий список экземпляров Person.
    /// </summary>
    public class PersonList
    {
        /// <summary>
        /// Список экземпляров Person.
        /// </summary>
        private List<Person> _people = new List<Person>();

        //TODO: encapsulation
        /// <summary>
        /// Получает список людей, хранящихся в данном объекте.
        /// </summary>
        public List<Person> People => _people;

        //TODO: rename
        /// <summary>
        /// Метод добавления элементов списка.
        /// </summary>
        public void AddList(Person person)
        {
            _people.Add(person);
        }

        //TODO: rename
        /// <summary>
        /// Метод удаления элементов списка.
        /// </summary>     `
        public void RemoveList(Person person)
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
        public Person GetIndex(int index)
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

        //TODO: rename
        /// <summary>
        /// Метод возвращает индекс элемента в списке.
        /// </summary>
        public int IndexElementFromList(Person person)
        {
            return _people.IndexOf(person);
        }

        /// <summary>
        /// Метод очистки списка.
        /// </summary>
        public void ClearList()
        {
            _people.Clear();
        }

        /// <summary>
        /// Определение количества элементов в списке.
        /// </summary>
        public int Count => _people.Count;
    }
}
