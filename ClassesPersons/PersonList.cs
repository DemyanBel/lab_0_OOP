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
        /// //TODO: RSDN +
        private List<Person> _people = new List<Person>();

        /// <summary>
        /// Получает список людей, хранящихся в данном объекте.
        /// </summary>
        public List<Person> People => _people;

        /// <summary>
        /// Метод добавления элементов списка.
        /// </summary>
        public void AddList(Person person)
        {
            _people.Add(person);
        }

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
            //TODO: duplication +
            ValidateIndex(index);
            _people.RemoveAt(index);
        }

        /// <summary>
        /// Метод получения элемента по индексу.
        /// </summary>
        public Person GetIndex(int index)
        {
            //TODO: duplication +
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

        //TODO: remove +
    }
}
