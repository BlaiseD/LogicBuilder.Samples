﻿using Enrollment.Forms.Configuration;
using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Linq;
using Xamarin.Forms;

namespace Enrollment.XPlatform.Views
{
    public class ReadOnlyChildFormPageCS : ContentPage
    {
        public ReadOnlyChildFormPageCS(IReadOnly formReadOnly)
        {
            this.formReadOnly = formReadOnly;
            this.formLayout = (DetailFormLayout)
            (
                (
                    this.formReadOnly.GetType()
                    .GetProperty(nameof(FormReadOnlyObject<string>.FormLayout))
                         ?? throw new ArgumentException($"{nameof(FormReadOnlyObject<string>.FormLayout)}: {{16836217-4D19-479F-9281-CBF604C62302}}")
                )
                .GetValue(this.formReadOnly) ?? throw new ArgumentException($"{nameof(formReadOnly)}: {{9C6FA403-A084-462A-9591-0BEFEA3EC831}}")
            );


            Content = new AbsoluteLayout
            {
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupDialogAbsoluteLayoutStyle),
                Children =
                {
                    new ContentView
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupDialogContentViewStyle),
                        Content = new StackLayout
                        {
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.ChildFormPopupViewStyle),
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
                                        }.AddBinding(Label.TextProperty, new Binding(nameof(FormReadOnlyObject<string>.Title)))
                                    }
                                },
                                new ScrollView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.ChildFormPopupScrollViewStyle),
                                    Content = this.formLayout.ControlGroupBoxList.Aggregate
                                    (
                                        new StackLayout(),
                                        (stackLayout, controlBox) =>
                                        {
                                            if (controlBox.IsVisible == false)
                                                return stackLayout;

                                            stackLayout.Children.Add
                                            (
                                                new Label
                                                {
                                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.DetailFormGroupHeaderStyle),
                                                    BindingContext = controlBox
                                                }
                                                .AddBinding
                                                (
                                                    Label.TextProperty,
                                                    GetLabelBinding(controlBox.HeaderBindings, $"{nameof(ReadOnlyControlGroupBox.GroupHeader)}")
                                                )
                                            );
                                            stackLayout.Children.Add
                                            (
                                                new StackLayout
                                                {
                                                    VerticalOptions = LayoutOptions.StartAndExpand,
                                                    BindingContext = controlBox
                                                }
                                                .AddBinding(BindableLayout.ItemsSourceProperty, new Binding("."))
                                                .SetDataTemplateSelector(DetailFormViewHelpers.ReadOnlyControlTemplateSelector)
                                            );

                                            return stackLayout;
                                        }
                                    )
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
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(FormReadOnlyObject<string>.CancelCommand)))
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
            this.BindingContext = this.formReadOnly;

            BindingBase GetLabelBinding(MultiBindingDescriptor multiBindingDescriptor, string bindingName)
            {
                if (multiBindingDescriptor == null)
                    return new Binding(bindingName);

                return new MultiBinding
                {
                    StringFormat = multiBindingDescriptor.StringFormat,
                    Bindings = multiBindingDescriptor.Fields.Select
                    (
                        field => new Binding($"{nameof(ReadOnlyControlGroupBox.BindingPropertiesDictionary)}[{field.ToBindingDictionaryKey()}].{nameof(IReadOnly.Value)}")
                    )
                    .Cast<BindingBase>()
                    .ToList()
                };
            }
        }

        private readonly IReadOnly formReadOnly;
        private readonly DetailFormLayout formLayout;
    }
}