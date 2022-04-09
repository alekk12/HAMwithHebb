using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMwithHebb.HopfieldNeuralNetwork
{
    public class HopfieldNeuralNetwork
    {
        public List<CustomVector> Vectors { get; set; }
        //CustomVector testVector { get; set; }

         /* inputs contain -1,1 -> bipolar (-1)
         * inputs contain 0,1 -> unipolar (0)
         * inputs contain only 1s -> none (1)
         */
        public int IsUnipolarOrBipolarOrNone { get; set; }
        public HopfieldNeuralNetwork(List<CustomVector> vectors, int IsUnipolarOrBipolarOrNone)
        {
            this.Vectors = vectors;
            this.IsUnipolarOrBipolarOrNone = IsUnipolarOrBipolarOrNone;
        }

        public void Train()
        {

        }
        public void CalculateWeightMatrix()
        {

        }
        /*returns the predictions as a list of strings, in case we have a cycle of size and more than one output vector*/
        public List<string> Predict(CustomVector testVector)
        {
            //List<string> strings = new List<string>();
            //temporary to see if this works
            List<string> strings = new List<string> { "There is a cycle of size 2.", "0 0", "1 1" };
            return (strings);
        }
    }
}
