using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCTTA.Models;
namespace DCTTA.ViewModels
{
    internal class SearchViewModel : INotifyPropertyChanged
    {
        public SearchViewModel()
        {
            if (Search == null)
            {
                Search = new();
            }
        }
        private Search Search { get; set; }
        public string Text
        {
            get { return Search.Text; }
            set
            {
                Search.Text = value;
                OnPropertyChanged("Text");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, e);
            }
        }
    }
}
