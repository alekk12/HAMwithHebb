using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMwithHebb.HopfieldNeuralNetwork
{
    public class CustomVector
    {
        public CustomVector(List<int> inputs, int isUnipolarOrBipolarOrNone)
        {
            Inputs=inputs;
            IsUnipolarOrBipolarOrNone=isUnipolarOrBipolarOrNone;
        }

        public List<int> Inputs { get;set;}

        /* inputs contain -1,1 -> bipolar (-1)
          * inputs contain 0,1 -> unipolar (0)
          * inputs contain only 1s -> none (1)
          */
        public int IsUnipolarOrBipolarOrNone { get; set; }
        public string FormatInputsAsAString()
        {
            return(string.Join(" ", Inputs.Select(x => (x>=0 ? " ":"")+x.ToString()).ToArray()));
        }
    }
}
