using NUnit.Framework;
using System;
using System.Collections;

namespace DesignPatternTest
{
    [TestFixture]
    //[Ignore("not yet")]
    public class AdapterTest
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
  
}