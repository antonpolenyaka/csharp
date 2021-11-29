using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace TedisNetDataSimulator
{
    [DataContract]
    public abstract class BaseEntity : INotifyPropertyChanged, IComparable
    {
        #region Properties
        [DataMember]
        public virtual int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }
        private int _id;

        [DataMember]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        private string _name;
        #endregion

        #region Public functions
        public virtual void refreshName()
        {
        }

        public T Clone<T>()
        {
            return Entity.Clone<T>((T)((object)this));
        }

        public T Copy<T>(T origen)
        {
            T result = (T)((object)this);
            Entity.CopyValues<T>(origen, result);
            return result;
        }

        public T CopyDataMembers<T>(T origen)
        {
            T result = (T)((object)this);
            Entity.CopyDataMembers<T>(origen, result);
            return result;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region IComparable
        public int CompareTo(object obj)
        {
            return string.Compare(ToString(), obj.ToString());
        }
        #endregion
    }
}
