﻿using Contoso.Forms.Configuration.Navigation;
using System;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<FlyoutMenuItem> _menuItems = new();
        public ObservableCollection<FlyoutMenuItem> MenuItems
        {
            get { return _menuItems; }
            set
            {
                if (_menuItems == value)
                    return;

                _menuItems = value;
                OnPropertyChanged();
            }
        }
    }
}
