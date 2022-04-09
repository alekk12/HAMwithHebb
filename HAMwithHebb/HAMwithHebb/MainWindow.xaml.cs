using HAMwithHebb.HopfieldNeuralNetwork;
using System;
using System.Collections.Generic;
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
        public List<int> vectors;
        public HopfieldNeuralNetwork.HopfieldNeuralNetwork hopfieldNeuralNetwork;
        public bool isInputValid = false;
        public
        public MainWindow()
        {
            InitializeComponent();
            vectors = new List<int>();
            /*List<String> ListOfStrings = new List<String> { "0 1","1 -3"};
            //MyData myDataObject = new MyData(DateTime.Now);
            Binding myBinding = new Binding("ListOfInput");
            myBinding.Source = ListOfStrings;
            BindingOperations.SetBinding(inputListbox, TextBlock.TextProperty, myBinding);
            foreach (string item in ListOfStrings)
            {
                ListViewItem itm = new ListViewItem();
                itm.Content = item;
                itm.IsHitTestVisible = false;
                inputListbox.Items.Add(itm);
            }*/
        }

        private void InputVector_KeyDown(object sender, KeyEventArgs e)
        {
            if (isInputValid)
            {
                InputButton.IsEnabled = true;
                if (e.Key == Key.Enter)
                {

                    //textBlock1.Text = "You Entered: " + textBox1.Text;
                    addToInputList(InputVector.Text);
                }
            }
        }

        private bool validateInputVector(string text)
        {
            if(text.Length < 1) { return false; }
            string[] split = { " " };
            string[] temp = text.Split(split, StringSplitOptions.RemoveEmptyEntries);
            List<int> list = new List<int>();
            foreach (string s in temp)
            {
                int numericValue;
                if(int.TryParse(s, out numericValue))
                {

                }
                else
                {
                    InputError.Content = "Wrong Input! The only allowed characters are -1,0,1";
                    return(false);
                }
            }
            //vectors.Add()
            InputError.Content = "";
            return (true);
        }

        private void TestVector_KeyDown(object sender, KeyEventArgs e)
        {
            //add to the classification
        }

        private void addToInputList(string item)
        {
            ListViewItem itm = new ListViewItem();
            itm.Content = item;
            itm.IsHitTestVisible = false;
            inputListbox.Items.Add(itm);
        }

        private void addToOutputList(string item)
        {
            ListViewItem itm = new ListViewItem();
            itm.Content = item;
            itm.IsHitTestVisible = false;
            outputListbox.Items.Add(itm);
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            if (isInputValid)
            {
                addToInputList(InputVector.Text);
            }
        }

        private void InputVector_TextChanged(object sender, TextChangedEventArgs e)
        {
            isInputValid = validateInputVector(InputVector.Text);
            InputButton.IsEnabled = isInputValid;
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            //CustomVector customVector = addToOutputList(TestVector.Text);
            //hopfieldNeuralNetwork.Predict(customVector);

        }

        private void InputFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Train_Click(object sender, RoutedEventArgs e)
        {
            hopfieldNeuralNetwork = new HopfieldNeuralNetwork.HopfieldNeuralNetwork(vectors);
            hopfieldNeuralNetwork.Train();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            hopfieldNeuralNetwork = null;
            vectors = null;
            inputListbox.Items.Clear();
            outputListbox.Items.Clear();
            InputButton.IsEnabled=false;
            InputVector.Text = "";
            TestVector.Text = "";
            TestButton.IsEnabled=false;
            InputError.Content = "";
            TestError.Content = "";
        }



        /*
<ItemsControl x:Name="input" Grid.Row="3" Grid.Column="1" ItemsSource="{Binding ListOfStrings}">
<ItemsControl.ItemTemplate>
<DataTemplate>
<TextBlock Text="{Binding}"/>
</DataTemplate>
</ItemsControl.ItemTemplate>
</ItemsControl>
*/
    }
}
