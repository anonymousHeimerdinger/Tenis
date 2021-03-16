using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenis.data.abstractClasses
{
    abstract class Person
    {
        private String name;

        public Person() {
            this.name = "name";
        }

        public Person(String name) {
            this.name = name;
        }

        public String Name{
            get => this.name;
            set => this.name = value;
        }

}
}
