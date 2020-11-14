using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanko.Models
{
    public class Measurement
    {
        int Number;
        float Value;

        public Measurement(float Value, int Number)
        {
            this.Value = Value;
            this.Number = Number;
        }

        public float GetValue()
        {
            return Value;
        }

        public int GetNumber()
        {
            return Number;
        }
    }
}
