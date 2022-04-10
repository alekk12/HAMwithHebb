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

        public double[,] TrainingResultMatrix { get; set; }
        public double TrainingResultN { get; set; }

        public void Train()
        {
            int M = Vectors[ 0 ].Inputs.Count;
            int N = Vectors.Count;

            TrainingResultMatrix = new double[ M, M ];

            for (int i = 0; i < M; i++ )
            {
                for(int j = 0; j < M; j++ )
                {
                    if(i == j)
                    {
                        TrainingResultMatrix[ i, j ] = 0;
                    }
                    else
                    {
                        double r = 0;
                        for(int s = 0; s < N; s++ )
                        {
                            if ( IsUnipolarOrBipolarOrNone == 0 )
                                r += ( 2 * Vectors[ s ].Inputs[ i ] - 1 ) * ( 2 * Vectors[ s ].Inputs[ j ] - 1 );
                            else
                                r += Vectors[ s ].Inputs[ i ] * Vectors[ s ].Inputs[ j ];
                        }
                        TrainingResultMatrix[ i, j ] = r;// N;
                    }
                }
            }
            TrainingResultN = N;
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
