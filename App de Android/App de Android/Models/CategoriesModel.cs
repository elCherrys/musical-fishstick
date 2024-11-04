using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App_de_Android.Models
{
    public class CategoriesModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string IconUrl { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
