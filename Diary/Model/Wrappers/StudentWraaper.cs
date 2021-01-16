﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Model.Wrappers
{
    public class StudentWraaper
    {
        public StudentWraaper()
        {
            Group = new GroupWrapper();
        }
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }
        public string Math { get; set; }
        public string Technology { get; set; }
        public string Physics { get; set; }
        public string PolishLang { get; set; }
        public string EnglishLang{ get; set; }
        public bool Activities { get; set; }
        public GroupWrapper Group { get; set; }


    }
}