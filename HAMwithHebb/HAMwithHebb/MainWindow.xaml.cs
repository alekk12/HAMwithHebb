using HAMwithHebb.HopfieldNeuralNetwork;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HAMwithHebb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CustomVector currentVector;
        public CustomVector testVector;
        public List<CustomVector> vectors;
        public HopfieldNeuralNetwork.HopfieldNeuralNetwork hopfieldNeuralNetwork;
        public bool isInputValid = false;
        public bool isPredictVectorValid = false;
        public int IsUniBiOrNone = 1; //assume that initial ham is neither unipolar nor bipolar
        public bool canPredict = false;
        public MainWindow()
        {
            InitializeComponent();
            vectors = new List<CustomVector>();
            ShowMatrix.IsEnabled = false;
        }
        private void UpdateInputList()
        {
            AddToInputList(currentVector.FormatInputsAsAString());
            vectors.Add( currentVector );
            if (currentVector.IsUnipolarOrBipolarOrNone!=1 && IsUniBiOrNone != currentVector.IsUnipolarOrBipolarOrNone)
            {
                IsUniBiOrNone = currentVector.IsUnipolarOrBipolarOrNone;
                Train.Content = Train.Content+" "+(IsUniBiOrNone==0 ? "Unipolar" : "Bipolar");
            }
            InputButton.IsEnabled = false;
            currentVector = new CustomVector();
            InputVector.Text = "";
            Train.IsEnabled = true;
        }
        private void UpdateNetworkType(CustomVector vector)
        {
                if (vector.IsUnipolarOrBipolarOrNone!=1 && IsUniBiOrNone != vector.IsUnipolarOrBipolarOrNone)
                {
                    IsUniBiOrNone = vector.IsUnipolarOrBipolarOrNone;
                    Train.Content = Train.Content+" "+(IsUniBiOrNone==0 ? "Unipolar" : "Bipolar");
                }
            currentVector = new CustomVector();
        }
        private void UpdateInputList(List<CustomVector> currentVectors)
        {
            foreach (CustomVector vector in currentVectors)
            {
                AddToInputList(vector.FormatInputsAsAString());
                vectors.Add(vector);
            }
            InputButton.IsEnabled = false;
            currentVector = new CustomVector();
            InputVector.Text = "";
            Train.IsEnabled = true;
        }
        private void UpdateOutputList()
        {
            outputListbox.Items.Clear();
            List<string> items = hopfieldNeuralNetwork.Predict(testVector);
            foreach (string item in items)
            {
                AddToOutputList(item);
            }
            //PredictButton.IsEnabled = false;
        }

        private void InputVector_KeyDown(object sender, KeyEventArgs e)
        {
            if (isInputValid && e.Key == Key.Enter){UpdateInputList();}
        }

        private bool ValidateVector(string text, Label InputError)
        {
            if(text.Length < 1) { return false; }
            string[] split = { " " };
            string[] temp = text.Split(split, StringSplitOptions.RemoveEmptyEntries);
            int[] opts = { -1, 0, 1 };
            List<int> list = new List<int>();
            int netUniBiNone = IsUniBiOrNone;
            int vecUniBiNone = 1;
            /*if(vectors.Count > 0 && vectors[0].Inputs.Count != temp.Length)
            {
                InputError.Content = "Wrong Input Length! ";
                return (false);
            }*/
            foreach (string s in temp)
            {
                if (int.TryParse(s, out int numericValue))
                {
                    if (!opts.Contains(numericValue))
                    {
                        InputError.Content = "Wrong Input! The "+(netUniBiNone==0 ? "Uni" : "Bi")+"polar network allows only "+netUniBiNone+",1 as input";
                        return (false);
                    }
                    if (netUniBiNone!=vecUniBiNone && netUniBiNone == numericValue)
                    {
                        vecUniBiNone = numericValue;
                    }
                    else if (numericValue!=netUniBiNone && numericValue != vecUniBiNone)
                    {
                        if (netUniBiNone==1)
                        {
                            netUniBiNone = numericValue;
                            vecUniBiNone = numericValue;
                        }
                        else if (numericValue!=1)
                        {
                            InputError.Content = "Wrong Input! The "+(netUniBiNone==0 ? "Uni" : "Bi")+"polar network allows only "+netUniBiNone+",1 as input";
                            return (false);
                        }
                    }
                    list.Add(numericValue);

                }
                else if (s!="-" || netUniBiNone == 0)
                {
                    InputError.Content = "Wrong Input! The "+(netUniBiNone==1 ? "network allows only -1,0,1 as input" : ((netUniBiNone==0 ? "Unipolar " : "Bipolar ")+"network allows only "+netUniBiNone+",1 as input"));
                    return (false);
                }
            }
            currentVector = new CustomVector(list, vecUniBiNone);
            InputError.Content = "";
            return (true);
        }

        private void PredictVector_KeyDown(object sender, KeyEventArgs e)
        {
            if (isPredictVectorValid && e.Key == Key.Enter) { UpdateOutputList(); }
            //add to the classification
        }

        private void AddToInputList(string item)
        {
            ListViewItem itm = new ListViewItem
            {
                Content = item,
                IsHitTestVisible = false
            };
            inputListbox.Items.Add(itm);
        }

        private void AddToOutputList(string item)
        {
            ListViewItem itm = new ListViewItem
            {
                Content = item,
                IsHitTestVisible = false
            };
            outputListbox.Items.Add(itm);
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            if (isInputValid)
            {
                UpdateInputList();
            }
        }

        private void InputVector_TextChanged(object sender, TextChangedEventArgs e)
        {
            isInputValid = ValidateVector(InputVector.Text, InputError);
            InputButton.IsEnabled = isInputValid;
        }

        private void PredictButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPredictVectorValid) {
                UpdateOutputList();
            }
        }

        private void InputFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                InputError.Content = "";
                List<CustomVector> vectors = new List<CustomVector>();
                int prevIsUniBiOrNone = IsUniBiOrNone;
                /*if we want to reset the input, this line should be uncomennted*/
                //Reset_Click(sender, e);
                foreach (string line in File.ReadLines(openFileDialog.FileName))
                {
                    bool temp = ValidateVector(line, InputError);
                    if (temp) { vectors.Add(currentVector); UpdateNetworkType(currentVector); }
                    else 
                    { 
                        currentVector = new CustomVector(); 
                        IsUniBiOrNone = prevIsUniBiOrNone; 
                        InputError.Content+=" The input file is wrong!"; 
                        return; 
                    }
                }
                UpdateInputList(vectors);
            }
        }

        private void Train_Click(object sender, RoutedEventArgs e)
        {
            hopfieldNeuralNetwork = new HopfieldNeuralNetwork.HopfieldNeuralNetwork(vectors,IsUniBiOrNone);
            hopfieldNeuralNetwork.Train();
            Train.Background = new SolidColorBrush(Colors.Green);
            Train.Foreground = new SolidColorBrush(Colors.White);
            Train.Content = "Train Again";
            PredictVector.IsEnabled = true;
            ShowMatrix.IsEnabled = true;
            PredictVector.BorderBrush = new SolidColorBrush(Colors.Green);
            //if training was successful, we can add the prediction
            //canPredict = true;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            hopfieldNeuralNetwork = null;
            vectors = new List<CustomVector>();
            currentVector = new CustomVector();
            testVector = new CustomVector();
            inputListbox.Items.Clear();
            outputListbox.Items.Clear();
            InputButton.IsEnabled=false;
            InputVector.Text = "";
            PredictVector.Text = "";
            PredictButton.IsEnabled=false;
            PredictVector.IsEnabled = false;
            ShowMatrix.IsEnabled = false;
            InputError.Content = "";
            PredictError.Content = "";
            IsUniBiOrNone = 1;
            Train.Content = "Train";
            Train.Background = new SolidColorBrush(Colors.LightGray);
            Train.Foreground = new SolidColorBrush(Colors.Black);
            PredictVector.BorderBrush = new SolidColorBrush(Colors.Black);
            canPredict = false;
        }

        private void ShowMatrix_Click(object sender, RoutedEventArgs e)
        {
            TextboxDialog textboxDialog = new TextboxDialog(hopfieldNeuralNetwork.GetMatrixString());
            textboxDialog.ShowDialog();
        }

        private void PredictVector_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (hopfieldNeuralNetwork != null)
            {
                isPredictVectorValid = ValidateVector(PredictVector.Text, PredictError);
                PredictButton.IsEnabled = isPredictVectorValid;
                if (isPredictVectorValid)
                {
                    testVector = currentVector;
                    currentVector = new CustomVector();
                }
                //PredictVector.IsEnabled = isPredictVectorValid;
            }
        }
    }
}
