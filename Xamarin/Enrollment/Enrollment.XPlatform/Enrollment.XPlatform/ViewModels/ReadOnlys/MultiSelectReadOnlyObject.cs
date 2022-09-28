﻿using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Forms.Configuration;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.Views.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class MultiSelectReadOnlyObject<T, E> : ReadOnlyObjectBase<T>, IHasItemsSourceReadOnly where T : ObservableCollection<E>
    {
        public MultiSelectReadOnlyObject(
            IHttpService httpService,
            IPopupFormFactory popupFormFactory,
            UiNotificationService uiNotificationService,
            string name,
            List<string> keyFields,
            string title,
            string stringFormat,
            MultiSelectTemplateDescriptor multiSelectTemplate)
            : base(name, multiSelectTemplate.TemplateName, uiNotificationService)
        {
            this._multiSelectTemplate = multiSelectTemplate;
            this._keyFields = keyFields;
            this._stringFormat = stringFormat;
            this.httpService = httpService;
            this.popupFormFactory = popupFormFactory;
            /*MemberNotNull unvailable in 2.1*/
            _title = null!;
            _placeholder = null!;
            _selectedItems = null!;
            /*MemberNotNull unavailable in 2.1*/
            this.Title = title;
            this.Placeholder = this._multiSelectTemplate.LoadingIndicatorText;
            itemComparer = new MultiSelectItemComparer<E>(this._keyFields);
            SelectedItems = new ObservableCollection<object>();
            GetItemSource();
        }

        private readonly IHttpService httpService;
        private readonly IPopupFormFactory popupFormFactory;
        private readonly List<string> _keyFields;
        private readonly string _stringFormat;
        private readonly MultiSelectTemplateDescriptor _multiSelectTemplate;
        private readonly MultiSelectItemComparer<E> itemComparer;

        public MultiSelectTemplateDescriptor MultiSelectTemplate => _multiSelectTemplate;

        public string DisplayText
        {
            get
            {
                if (Value == null)
                    return string.Empty;

                if (string.IsNullOrEmpty(this._stringFormat))
                    return GetText();

                return string.Format
                (
                    CultureInfo.CurrentCulture,
                    this._stringFormat,
                    GetText()
                );

                string GetText()
                    => string.Join
                    (
                        ", ",
                        Value.Select
                        (
                            item => typeof(E).GetProperty(_multiSelectTemplate.TextField)?.GetValue(item) ?? string.Empty
                        )
                    );
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            //[MemberNotNull(nameof(_title))]
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged();
            }
        }

        private string _placeholder;
        public string Placeholder
        {
            get => _placeholder;
            //[MemberNotNull(nameof(_placeholder))]
            set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }

        public override T? Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;

                UpdateSelectedItems();

                OnPropertyChanged(nameof(DisplayText));
                OnPropertyChanged(nameof(SelectedItems));
            }
        }

        ObservableCollection<object> _selectedItems;
        public ObservableCollection<object> SelectedItems
        {
            get
            {
                return _selectedItems;
            }
            //[MemberNotNull(nameof(_selectedItems))]
            set
            {
                if (_selectedItems != value)
                {
                    _selectedItems = value;
                }
            }
        }

        private List<E>? _items;
        public List<E>? Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
                CheckCanExecute((Command)OpenCommand);
            }
        }

        private async void GetItemSource()
        {
            try
            {
                BaseResponse response = await this.httpService.GetObjectDropDown
                (
                    new GetTypedListRequest
                    {
                        DataType = this._multiSelectTemplate.RequestDetails.DataType,
                        ModelType = this._multiSelectTemplate.RequestDetails.ModelType,
                        ModelReturnType = this._multiSelectTemplate.RequestDetails.ModelReturnType,
                        DataReturnType = this._multiSelectTemplate.RequestDetails.DataReturnType,
                        Selector = this.MultiSelectTemplate.TextAndValueSelector
                    },
                    this._multiSelectTemplate.RequestDetails.DataSourceUrl
                );

                if (response.Success != true)
                {
#if DEBUG
                    await App.Current!.MainPage!.DisplayAlert
                    (
                        "Errors",
                        string.Join(Environment.NewLine, response.ErrorMessages),
                        "Ok"
                    );
#endif
                    return;
                }

                Items = ((GetListResponse)response).List.OfType<E>().ToList();
                UpdateSelectedItems();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public void Reload(object entity, Type entityType)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            Items = null;
            Value = default;
        }

        private void UpdateSelectedItems()
        {
            if (Items?.Any() != true)
                return;

            var selected = Value?.Any() != true
                ? Enumerable.Empty<object>()
                : Items.Where(i => Value.Contains(i, itemComparer)).Cast<object>();

            SelectedItems.Clear();
            foreach (var item in selected)
                SelectedItems.Add(item);

            this.Placeholder = this._multiSelectTemplate.PlaceholderText;
        }

        private ICommand? _openCommand;
        public ICommand OpenCommand
        {
            get
            {
                if (_openCommand != null)
                    return _openCommand;

                _openCommand = new Command
                (
                    () =>
                    {
                        MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current!.MainPage!.Navigation.PushModalAsync/*App.Current.MainPage is not null at this point*/
                            (
                                popupFormFactory.CreateReadOnlyMultiSelectPage(this)
                            )
                        );
                    },
                    () => Items?.Any() == true
                );

                return _openCommand;
            }
        }

        private ICommand? _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand != null)
                    return _cancelCommand;

                _cancelCommand = new Command
                (
                    () =>
                    {
                        MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current!.MainPage!.Navigation.PopModalAsync()/*App.Current.MainPage is not null at this point*/
                        );
                    });

                return _cancelCommand;
            }
        }

        private void CheckCanExecute(Command command)
        {
            command.ChangeCanExecute();
        }
    }
}
