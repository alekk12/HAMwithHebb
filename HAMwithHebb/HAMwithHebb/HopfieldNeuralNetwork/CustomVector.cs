using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMwithHebb.HopfieldNeuralNetwork
{
    public class CustomVector
    {
        public List<int> Inputs { get; set; }

        /* inputs contain -1 -> bipolar (-1)
         * inputs contain 0 -> unipolar (0)
         * inputs contain neither 0 nor 1 -> none (1)
         */
        public int IsUnipolarOrBipolarOrNone { get; set; } 
    }
}
