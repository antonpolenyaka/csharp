using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ReasignAddressesCAP32.IEC104
{
    /// <summary>
    /// Interaction logic for New104DeviceWindow.xaml
    /// </summary>
    public partial class New104DeviceWindow : Window, INotifyPropertyChanged
    {
        #region Attributies
        private string _deviceName = "DeviceX";
        private int _firstTagInfoRow = 4;
        private int _deviceType = 30; // 30 = Others
        private string _columnTagClassName = "I";
        private string _columnTagIOA = "AI";
        private string _columnTagUnits = "R";
        private string _columnTagScale = "T";
        private string _ioaFormat = "00000";
        private string _excelSheetName = "result";
        #endregion

        #region Commands
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        #endregion

        #region Properties
        public string DeviceName
        {
            get => _deviceName;
            set
            {
                _deviceName = value;
                OnPropertyChanged(nameof(DeviceName));
            }
        }
        public int FirstTagInfoRow
        {
            get => _firstTagInfoRow;
            set
            {
                _firstTagInfoRow = value;
                OnPropertyChanged(nameof(FirstTagInfoRow));
            }
        }
        public int DeviceType
        {
            get => _deviceType;
            set
            {
                _deviceType = value;
                OnPropertyChanged(nameof(DeviceType));
            }
        }
        public string ColumnTagClassName
        {
            get => _columnTagClassName;
            set
            {
                _columnTagClassName = value;
                OnPropertyChanged(nameof(ColumnTagClassName));
            }
        }
        public string ColumnTagIOA
        {
            get => _columnTagIOA;
            set
            {
                _columnTagIOA = value;
                OnPropertyChanged(nameof(ColumnTagIOA));
            }
        }
        public string ColumnTagUnits
        {
            get => _columnTagUnits;
            set
            {
                _columnTagUnits = value;
                OnPropertyChanged(nameof(ColumnTagUnits));
            }
        }
        public string ColumnTagScale
        {
            get => _columnTagScale;
            set
            {
                _columnTagScale = value;
                OnPropertyChanged(nameof(ColumnTagScale));
            }
        }
        public string IoaFormat
        {
            get => _ioaFormat;
            set
            {
                _ioaFormat = value;
                OnPropertyChanged(nameof(IoaFormat));
            }
        }
        public string ExcelSheetName
        {
            get => _excelSheetName;
            set
            {
                _excelSheetName = value;
                OnPropertyChanged(nameof(ExcelSheetName));
            }
        }
        #endregion

        #region Constructors
        public New104DeviceWindow()
        {
            InitializeComponent();
            DataContext = this;
            SaveCommand = new RelayCommand((param) =>
            {
                DialogResult = true;
                Close();
            }, param => true);
            CancelCommand = new RelayCommand((param) =>
            {
                DialogResult = false;
                Close();
            }, param => true);
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
