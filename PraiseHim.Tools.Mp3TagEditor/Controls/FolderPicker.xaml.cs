using System.Collections;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace PraiseHim.Tools.Mp3TagEditor.Controls
{
    /// <summary>
    /// Interaction logic for FolderPicker.xaml
    /// </summary>
    public partial class FolderPicker : System.Windows.Controls.UserControl
    {
        public static readonly DependencyProperty AllFilesProperty =
            DependencyProperty.Register("AllFiles", typeof(IList), typeof(FolderPicker), new PropertyMetadata(null));

        public static readonly DependencyProperty FolderPathProperty =
            DependencyProperty.Register("FolderPath", typeof(string), typeof(FolderPicker), new PropertyMetadata(string.Empty));

        public FolderPicker()
        {
            InitializeComponent();
        }

        public IList AllFiles
        {
            get { return (IList)GetValue(AllFilesProperty); }
            set { SetValue(AllFilesProperty, value); }
        }

        public string FolderPath
        {
            get { return (string)GetValue(FolderPathProperty); }
            set { SetValue(FolderPathProperty, value); }
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            FolderPath = dialog.SelectedPath.Trim();

            AllFiles = Directory.GetFiles(FolderPath).ToList();
        }
    }
}