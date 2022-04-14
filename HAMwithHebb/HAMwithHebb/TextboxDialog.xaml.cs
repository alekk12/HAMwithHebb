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
using System.Windows.Shapes;

namespace HAMwithHebb
{
    /// <summary>
    /// Logika interakcji dla klasy MatrixDialog.xaml
    /// </summary>
    public partial class TextboxDialog : Window
    {
        public string Text { get; set; }
        
        public TextboxDialog(string text)
        {
            Text = text;
            DataContext = this;
            InitializeComponent();
        }
    }
}
