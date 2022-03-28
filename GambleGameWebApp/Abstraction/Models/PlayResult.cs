using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Models
{
    public class PlayResult
    {
        public int Account { get; set; }
        public string Point { get; set; }
        public string Status { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as PlayResult;
            if (other.Account == Account
                &&
                other.Point == Point
                && 
                other.Status == Status
                ) return true;
            return false;
        }

    }
}