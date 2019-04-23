using NUnit.Framework;
using System;
using System.Collections;

namespace DesignPatternTest
{
    [TestFixture]
    //[Ignore("not yet")]
    public class MyEnumerableTest
    {
        [Test]
        public void people_array_foreach_count_correct()
        {
            Person[] peopleArray = new Person[3]
            {
                new Person("John", "Smith"),
                new Person("Jim", "Johnson"),
                new Person("Sue", "Rabon"),
            };

            var newPersonArray = new Person[3];

            MyEnumerable peopleList = new MyEnumerable(peopleArray);
            int idx = 0;
            foreach (var people in peopleList)
            {
                newPersonArray[idx] = (Person)people;
                idx++;
            }

            Assert.AreEqual(peopleArray, newPersonArray);
        }
    }

    public class MyEnumerable : IEnumerable
    {
        private Person[] _peopleArray;

        public MyEnumerable(Person[] peopleArray)
        {
            _peopleArray = peopleArray;
        }

        public IEnumerator GetEnumerator()
        {
            return new MyEnumerator(_peopleArray);
        }
    }

    public class MyEnumerator : IEnumerator
    {
        private Person[] _peopleArray;
        private int position = -1;

        public MyEnumerator(Person[] peopleArray)
        {
            _peopleArray = peopleArray;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _peopleArray.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        public object Current
        {
            get
            {
                try
                {
                    return _peopleArray[position];
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }

    public class Person
    {
        public string FirstName;
        public string LastName;

        public Person(string firstName, string lastName)
        {
            LastName = lastName;
            FirstName = firstName;
        }
    }
}