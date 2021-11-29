using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AntonUtils.Base
{
    public class BaseModelView : INotifyPropertyChanged
    {
        #region Attributes
        private string _title;
        #endregion

        #region Properties
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }
        #endregion

        #region Events & Delegates
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public virtual bool TryClose() => true;
        public virtual void Unload() { }
        #endregion
    }
}
