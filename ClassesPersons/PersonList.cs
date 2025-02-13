

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
        private List<Person> people = new List<Person>();

        /// <summary>
        /// Метод добавления элементов списка.
        /// </summary>
        public void AddList(Person person)
        {
            people.Add(person);
        }

        /// <summary>
        /// Метод удаления элементов списка.
        /// </summary>     `
        public void RemoveList(Person person)
        {
            people.Remove(person);
        }

        /// <summary>
        /// Метод удаления элемента по индексу.
        /// </summary>
        public void RemoveIndex(int index)
        {
            if (index < 0 || index >= people.Count)
            {
                throw new IndexOutOfRangeException("Индекс вне диапазона.");
            }
            people.RemoveAt(index);
        }

        /// <summary>
        /// Метод получения элемента по индексу.
        /// </summary>
        public Person GetIndex(int index)
        {
            if (index >= people.Count || index < 0)
            {
                throw new IndexOutOfRangeException("Индекс вне диапазона.");
            }
            return people[index];
        }

        /// <summary>
        /// Метод возвращает индекс элемента в списке.
        /// </summary>
        public int IndexElementFromList(Person person)
        {
            return people.IndexOf(person);
        }

        /// <summary>
        /// Метод очистки списка.
        /// </summary>
        public void ClearList()
        {
            people.Clear();
        }

        /// <summary>
        /// Определение количества элементов в списке.
        /// </summary>
        public int Count => people.Count;

        /// <summary>
        /// Метод получения элементов в списке.
        /// </summary>
        public void PrintList()
        {
            if (people.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }
            foreach (Person person in people)
            {
                Console.WriteLine(person.GetInfo());
            }
        }
    }
}
