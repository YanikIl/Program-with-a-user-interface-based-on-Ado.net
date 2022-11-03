using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratornaya_2
{
    public class Groups
    {
        private int id;
        private string group;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Group
        {
            get => group;
            set => group = value;
        }

        public Groups(
           int id, string group)
        {
            this.id = id;
            this.group = group;
        }
    }
}
