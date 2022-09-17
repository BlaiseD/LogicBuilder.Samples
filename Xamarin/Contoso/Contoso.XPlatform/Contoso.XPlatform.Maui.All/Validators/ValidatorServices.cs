﻿using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ValidatorServices
    {
        internal static IServiceCollection AddValidatorServices(this IServiceCollection services) 
            => services
                .AddTransient<Func<Type, IEnumerable<IFormField>, object, IClearIfManager>>
                (
                    provider =>
                    (modelType, currentProperties, conditions) =>
                    {
                        return (IClearIfManager)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(ClearIfManager<>).MakeGenericType(modelType),
                            provider.GetRequiredService<IMapper>(),
                            provider.GetRequiredService<UiNotificationService>(),
                            currentProperties,
                            conditions
                        );
                    }
                )
                .AddTransient<Func<Type, ObservableCollection<IValidatable>, IFormGroupSettings, IDirectiveManagers>>
                (
                    provider =>
                    (modelType, properties, formSettings) =>
                    {
                        return (IDirectiveManagers)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(DirectiveManagers<>).MakeGenericType(modelType),
                            provider.GetRequiredService<IDirectiveManagersFactory>(),
                            provider.GetRequiredService<IContextProvider>(),
                            provider.GetRequiredService<IMapper>(),
                            properties,
                            formSettings
                        );
                    }
                )
                .AddTransient<IDirectiveManagersFactory, DirectiveManagersFactory>()
                .AddTransient<Func<Type, IEnumerable<IFormField>, object, IHideIfManager>>
                (
                    provider =>
                    (modelType, currentProperties, conditions) =>
                    {
                        return (IHideIfManager)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(HideIfManager<>).MakeGenericType(modelType),
                            provider.GetRequiredService<IMapper>(),
                            provider.GetRequiredService<UiNotificationService>(),
                            currentProperties,
                            conditions
                        );
                    }
                )
                .AddTransient<Func<Type, ObservableCollection<IReadOnly>, IFormGroupSettings, IReadOnlyDirectiveManagers>>
                (
                    provider =>
                    (modelType, properties, formSettings) =>
                    {
                        return (IReadOnlyDirectiveManagers)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(ReadOnlyDirectiveManagers<>).MakeGenericType(modelType),
                            provider.GetRequiredService<IDirectiveManagersFactory>(),
                            provider.GetRequiredService<IContextProvider>(),
                            provider.GetRequiredService<IMapper>(),
                            properties,
                            formSettings
                        );
                    }
                )
                .AddTransient<Func<Type, IEnumerable<IFormField>, object, IReloadIfManager>>
                (
                    provider =>
                    (modelType, currentProperties, conditions) =>
                    {
                        return (IReloadIfManager)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(ReloadIfManager<>).MakeGenericType(modelType),
                            provider.GetRequiredService<IMapper>(),
                            provider.GetRequiredService<UiNotificationService>(),
                            currentProperties,
                            conditions
                        );
                    }
                )
                .AddTransient<Func<Type, IEnumerable<IFormField>, object, IValidateIfManager>>
                (
                    provider =>
                    (modelType, currentProperties, conditions) =>
                    {
                        return (IValidateIfManager)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(ValidateIfManager<>).MakeGenericType(modelType),
                            provider.GetRequiredService<IMapper>(),
                            provider.GetRequiredService<UiNotificationService>(),
                            currentProperties,
                            conditions
                        );
                    }
                );
    }
}
