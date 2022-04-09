using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMwithHebb.HopfieldNeuralNetwork
{
    public class HopfieldNeuralNetwork
    {
        List<CustomVector> vectors { get; set; }
        public HopfieldNeuralNetwork(List<CustomVector> vectors)
        {
            this.vectors = vectors;
        }

        public void Train()
        {

        }
        public CustomVector Predict(List<CustomVector> testVector)
        {
            CustomVector customVector = new CustomVector();
            return (customVector);
        }
    }
}
