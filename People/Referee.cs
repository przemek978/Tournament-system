﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public class Referee:Person//Ready
    {
        public Referee(string imie, string nazwisko):base(imie,nazwisko)
        {
            
        }
        public bool Equals(Referee sedzia)
        {
            if (sedzia.GetName() == Name && sedzia.GetSurname() == Surname)
                return true;
            else
                return false;
        }
        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}
