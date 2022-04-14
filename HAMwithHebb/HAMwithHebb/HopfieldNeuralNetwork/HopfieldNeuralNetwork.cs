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

        /// <summary>
        /// <list type="bullet">
        ///    <item>
        ///        <term>-1</term>
        ///        <description>Bipolar (contains -1 and 1)</description>
        ///    </item>
        ///    <item>
        ///        <term>0</term>
        ///        <description>Unipolar (contains 0 and 1)</description>
        ///    </item>
        ///    <item>
        ///        <term>1</term>
        ///        <description>Contains only 1s, treated as unipolar</description>
        ///    </item>
        ///    <item>
        ///        <term>Other</term>
        ///        <description>Not supported value, will be treated as unipolar</description>
        ///    </item>
        /// </list>
        /// </summary>
        public int IsUnipolarOrBipolarOrNone { get; set; }

        public HopfieldNeuralNetwork(List<CustomVector> vectors, int IsUnipolarOrBipolarOrNone)
        {
            this.Vectors = vectors;
            this.IsUnipolarOrBipolarOrNone = IsUnipolarOrBipolarOrNone;
        }

        /// <summary>
        /// Matrix that is the result of the training
        /// </summary>
        public double[,] TrainingResultMatrix { get; set; }
        /// <summary>
        /// Number of neurons in the network
        /// </summary>
        public int NumberOfNeurons { get; set; }

        /// <summary>
        /// Training the network
        /// </summary>
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
            NumberOfNeurons = M;
        }

        /// <summary>
        /// g function applied to the resulting vector, we went with 3-piece function for >0, =0 and <0
        /// </summary>
        private double[] G(double[] vector, double[] calcVec)
        {
            double[] res = new double[ vector.Length ];
            for(int i = 0; i < vector.Length; i++ )
            {
                if(vector[i] > 0)
                {
                    res[ i ] = 1;
                }
                else if(vector[i] == 0)
                {
                    res[ i ] = calcVec[ i ];
                }
                else //vector[i] < 0
                {
                    res[ i ] = IsUnipolarOrBipolarOrNone == -1 ? -1 : 0;
                }

            }
            return res;
        }

        /// <summary>
        /// Returns result of multiplication of the trained matrix and input
        /// </summary>
        private double[] GetVectorMultiplicationResult(double[] vector)
        {
            double[] res = new double[ vector.Length ];
            for (int row = 0; row < NumberOfNeurons; row++ )
            {
                res[ row ] = 0;
                for(int column = 0; column < NumberOfNeurons; column++ )
                {
                    res[ row ] += ( vector[ column ] <= 0 ? -1 : 1 ) * TrainingResultMatrix[ row, column ];
                }
            }
            return res;
        }

        /// <summary>
        /// Checks if 2 vectors are equal
        /// </summary>
        private bool AreEqual(double[] v1, double[] v2)
        {
            if ( v1.Length != v2.Length ) return false;
            int n = v1.Length;
            for ( int i = 0; i < n; i++ )
                if ( v1[ i ] != v2[ i ] )
                    return false;
            return true;
        }

        /// <summary>
        /// Changes vector to string for convinient printing
        /// </summary>
        private string VecToStr(double[] v)
        {
            string s = "";
            for(int i = 0; i < v.Length-1; i++)
            {
                s += v[ i ].ToString() + ", ";
            }
            s += v[ v.Length-1 ].ToString();
            return s;
        }

        /// <summary>
        /// Returns the result of the test in form of a message informing about stability and resulting vector (or 2 vectors in case of the cycle).
        /// </summary>
        /// <returns>List containing lines of the message</returns>
        public List<string> Predict(CustomVector testVector)
        {
            double[] lastIter = testVector.Inputs.Select( x => (double)x ).ToArray();
            double[] actualIter = lastIter.Select( x => x ).ToArray();

            List<string> strings = new List<string>();
            while(true)
            {
                double[] nextIter = G( GetVectorMultiplicationResult( actualIter ), actualIter );
                if( AreEqual(nextIter, actualIter) )
                {
                    strings.Add( "Vector is stable. The result is: " );
                    strings.Add( VecToStr( nextIter ) );
                    break;
                }
                if(AreEqual(nextIter, lastIter))
                {
                    strings.Add( "Vector finishes with cycle of 2: " );
                    strings.Add( VecToStr( nextIter ) );
                    strings.Add( VecToStr( actualIter ) );
                    break;
                }
                lastIter = actualIter;
                actualIter = nextIter;
            }
            return (strings);
        }

        /// <summary>
        /// Formats the trained matrix for convinient output in the "Show Matrix" button
        /// </summary>
        public string GetMatrixString()
        {
            string s = "";
            for (int i = 0; i < NumberOfNeurons; i++)
            {
                for (int j = 0; j < NumberOfNeurons; j++)
                {
                    s += String.Format("{0,6:0.000}", (TrainingResultMatrix[i, j] / NumberOfNeurons)) + " ";
                }
                s += "\n";
            }
            return s;
        }
    }
}
