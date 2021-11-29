using AntonUtils.Base;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AntonUtils
{
    public class MainWindowViewModel : BaseModelView
    {
        private string _branchNameSource;
        private string _branchNameConverted;

        public string BranchNameSource
        {
            get { return _branchNameSource; }
            set
            {
                if (_branchNameSource != value)
                {
                    _branchNameSource = value;
                    NotifyPropertyChanged(nameof(BranchNameSource));
                }
            }
        }

        public string BranchNameConverted
        {
            get { return _branchNameConverted; }
            set
            {
                if (_branchNameConverted != value)
                {
                    _branchNameConverted = value;
                    NotifyPropertyChanged(nameof(BranchNameConverted));
                }
            }
        }

        public bool CanExecuteNameConvertCommand { get { return !string.IsNullOrWhiteSpace(BranchNameSource); } }
        public ICommand BranchNameConvertCommand { get; set; }
        public bool CanExecuteBranchNameCopyClipboard { get { return !string.IsNullOrWhiteSpace(BranchNameConverted); } }
        public ICommand BranchNameCopyClipboard { get; set; }

        public MainWindowViewModel()
        {
            BranchNameConvertCommand = new RelayCommand(BranchNameConvertCommandExecute, param => CanExecuteNameConvertCommand);
            BranchNameCopyClipboard = new RelayCommand(BranchNameCopyClipboardExecute, param => CanExecuteNameConvertCommand);
        }

        private void BranchNameCopyClipboardExecute(object obj)
        {
            Clipboard.SetText(BranchNameConverted);
        }

        private void BranchNameConvertCommandExecute(object obj)
        {
            string result = BranchNameSource.ToLower().Replace(" ", "-");
            // E
            char[] change = new char[] { 'é', 'è', 'ë', 'ê' };
            change.ToList().ForEach(c => result = result.Replace(c, 'e'));
            // Y
            change = new char[] { 'ý', 'ÿ' };
            change.ToList().ForEach(c => result = result.Replace(c, 'y'));
            // U
            change = new char[] { 'ú', 'ù', 'ü', 'û' };
            change.ToList().ForEach(c => result = result.Replace(c, 'u'));
            // I
            change = new char[] { 'í', 'ì', 'ï', 'î' };
            change.ToList().ForEach(c => result = result.Replace(c, 'i'));
            // O
            change = new char[] { 'ó', 'ò', 'ö', 'ô', 'º' };
            change.ToList().ForEach(c => result = result.Replace(c, 'o'));
            // A
            change = new char[] { 'á', 'à', 'ä', 'â', 'ª', '@' };
            change.ToList().ForEach(c => result = result.Replace(c, 'a'));
            // Ç
            change = new char[] { 'ç' };
            change.ToList().ForEach(c => result = result.Replace(c, 'c'));

            // Special chars
            change = new char[] { '\\', '!', '|', '"', '·', '#', '$', '~', '%', '&', '¬', '/', '(', ')', '=', '\'', '?', '¡', '¿', '`', '^', '[', '+', '*', ']', '´', '¨', '{', '}', '<', '>', ',', ';', '.', ':', '_' };
            change.ToList().ForEach(c => result = result.Replace(c, '-'));

            int lenReplace = 0;
            do
            {
                lenReplace = result.Length;
                result = result.Replace("--", "-");

            } while (lenReplace != result.Length);

            // Add feat
            result = $"feat{DateTime.Today:yyMM}/{result}";
            // Update view
            BranchNameConverted = result;
        }
    }
}
