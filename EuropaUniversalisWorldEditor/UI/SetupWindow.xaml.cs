using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace EuropaUniversalisWorldEditor.UI
{
    public partial class SetupWindow : Window
    {
        public SetupWindow()
        {
            InitializeComponent();
        }

        public string GamePath
        {
            get => GamePathText.Text;
            set => GamePathText.Text = value;
        }

        public string ModsPath
        {
            get => ModsPathText.Text;
            set => ModsPathText.Text = value;
        }

        private void OkClick(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void GamePathOpenClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                GamePathText.Text = openFileDialog.FileName;
            }
                
        }
        
        private void ModsPathOpenClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ModsPathText.Text = openFileDialog.FileName;
            }
        }
    }
}