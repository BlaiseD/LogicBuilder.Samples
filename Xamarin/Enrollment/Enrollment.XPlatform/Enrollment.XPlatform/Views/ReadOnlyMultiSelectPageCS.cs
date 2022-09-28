﻿using Enrollment.Forms.Configuration;
using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Enrollment.XPlatform.Views
{
    public class ReadOnlyMultiSelectPageCS : ContentPage
    {
        public ReadOnlyMultiSelectPageCS(IReadOnly multiSelectReadOnly)
        {
            this.multiSelectReadOnly = multiSelectReadOnly;
            this.multiSelectTemplateDescriptor = (MultiSelectTemplateDescriptor)GetPropertyValue(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.MultiSelectTemplate));
            IEnumerable? items = (IEnumerable)GetPropertyValue(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.Items));

            object GetPropertyValue(string propertyName)
            {
                return (
                    this.multiSelectReadOnly.GetType()
                    .GetProperty(propertyName)
                        ?? throw new ArgumentException($"{propertyName}: {{12ACDF6B-04A3-4785-807F-B63FA9B82198}}")
                )
                .GetValue(this.multiSelectReadOnly) ?? throw new ArgumentException($"{propertyName}: {{31B363EA-185E-45C0-916E-6BC63F2439F7}}");
            }

            Content = new AbsoluteLayout
            {
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupDialogAbsoluteLayoutStyle),
                Children =
                {
                    new ContentView
                    {
                        Content = new StackLayout
                        {
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectPopupViewStyle),
                            Children =
                            {
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupHeaderStyle),
                                    Children =
                                    {
                                        new Label
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupHeaderLabelStyle),
                                        }.AddBinding(Label.TextProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.Title)))
                                    }
                                },
                                new ScrollView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectParentStyle),
                                    Content = new Grid
                                    {
                                        Children =
                                        {
                                            new CollectionView
                                            {
                                                HeightRequest = GetCollectionViewHeight(items),
                                                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectPopupCollectionViewStyle),
                                                ItemTemplate = EditFormViewHelpers.GetMultiSelectItemTemplateSelector(this.multiSelectTemplateDescriptor)
                                            }
                                            .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.Items)))
                                            .AddBinding(SelectableItemsView.SelectedItemsProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.SelectedItems))),
                                            new BoxView()
                                        }
                                    }
                                },
                                new BoxView { Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupFooterSeparatorStyle) },
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupFooterStyle),
                                    ColumnDefinitions =
                                    {
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) }
                                    },
                                    Children =
                                    {
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupCancelButtonStyle)
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.CancelCommand)))
                                        .SetGridColumn(2)
                                    }
                                }
                            }
                        }
                    }
                }
            };

            this.BackgroundColor = Color.Transparent;
            Visual = VisualMarker.Material;
            this.BindingContext = this.multiSelectReadOnly;
        }

        private double GetCollectionViewHeight(IEnumerable? list)
        {
            const double defaultHeight = 340;
            if (list == null)
                return defaultHeight;

            double height = GetItemHeight() * list.Cast<object>().Count();

            return height < defaultHeight ? defaultHeight : height;
        }

        public static double GetItemHeight()
            => Device.RuntimePlatform switch
            {
                Platforms.Android => 40,
                Platforms.iOS => 45,
                _ => throw new ArgumentOutOfRangeException(nameof(Device.RuntimePlatform)),
            };

        private readonly IReadOnly multiSelectReadOnly;
        private readonly MultiSelectTemplateDescriptor multiSelectTemplateDescriptor;
    }
}