using System.Windows.Input;
using TedisNetDataSimulator.Workers;

namespace TedisNetDataSimulator
{
    public class MainViewModel : BaseModelView
    {
        #region Attributies
        private bool _isGeneratingEvents = false;
        private bool _isGeneratingMeasures = false;
        private int _frecuencyEvents = 5000;
        private int _frecuencyMeasures = 5000;
        private EventWorker _eventWorker = null;
        private MeasuresWorker _measuresWorker = null;
        #endregion

        #region Properties
        public int FrecuencyEvents
        {
            get => _frecuencyEvents;
            set
            {
                if(_frecuencyEvents != value)
                {
                    _frecuencyEvents = value;
                    NotifyPropertyChanged(nameof(FrecuencyEvents));
                }
            }
        }
        public bool IsGeneratingEvents
        {
            get => _isGeneratingEvents;
            set
            {
                if (_isGeneratingEvents != value)
                {
                    _isGeneratingEvents = value;
                    NotifyPropertyChanged(nameof(IsGeneratingEvents));
                }
            }
        }
        public bool CanExecuteGenerateEvents { get { return !IsGeneratingEvents; } }
        public bool CanExecuteStopEvents { get { return IsGeneratingEvents; } }

        public int FrecuencyMeasures
        {
            get => _frecuencyMeasures;
            set
            {
                if (_frecuencyMeasures != value)
                {
                    _frecuencyMeasures = value;
                    NotifyPropertyChanged(nameof(FrecuencyMeasures));
                }
            }
        }
        public bool IsGeneratingMeasures
        {
            get => _isGeneratingMeasures;
            set
            {
                if (_isGeneratingMeasures != value)
                {
                    _isGeneratingMeasures = value;
                    NotifyPropertyChanged(nameof(IsGeneratingMeasures));
                }
            }
        }
        public bool CanExecuteGenerateMeasures { get { return !IsGeneratingMeasures; } }
        public bool CanExecuteStopMeasures { get { return IsGeneratingMeasures; } }
        #endregion

        #region Commands
        public ICommand GenerateEventsCommand { get; set; }
        public ICommand StopEventsCommand { get; set; }
        public ICommand GenerateMeasuresCommand { get; set; }
        public ICommand StopMeasuresCommand { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            GenerateEventsCommand = new RelayCommand(GenerateEventsCommandExecute, param => CanExecuteGenerateEvents);
            StopEventsCommand = new RelayCommand(StopEventsCommandExecute, param => CanExecuteStopEvents);
            _eventWorker = new EventWorker();
            GenerateMeasuresCommand = new RelayCommand(GenerateMeasuresCommandExecute, param => CanExecuteGenerateMeasures);
            StopMeasuresCommand = new RelayCommand(StopMeasuresCommandExecute, param => CanExecuteStopMeasures);
            _measuresWorker = new MeasuresWorker();
        }
        #endregion

        #region Commands Executions
        private void GenerateEventsCommandExecute(object parameter)
        {
            if (!IsGeneratingEvents)
            {
                IsGeneratingEvents = true;
                _eventWorker.StartWorker(FrecuencyEvents);
            }
        }

        private void StopEventsCommandExecute(object parameter)
        {
            if (IsGeneratingEvents)
            {
                _eventWorker.StoptWorker();
                IsGeneratingEvents = false;
            }
        }

        private void GenerateMeasuresCommandExecute(object parameter)
        {
            if (!IsGeneratingMeasures)
            {
                IsGeneratingMeasures = true;
                _measuresWorker.StartWorker(FrecuencyMeasures);
            }
        }

        private void StopMeasuresCommandExecute(object parameter)
        {
            if (IsGeneratingMeasures)
            {
                _measuresWorker.StoptWorker();
                IsGeneratingMeasures = false;
            }
        }
        #endregion
    }
}
