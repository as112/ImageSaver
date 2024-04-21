﻿using Microsoft.Extensions.DependencyInjection;

namespace TestSystem.Client.ViewModels
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection AddViews(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddTransient<LoadImageViewModel>()
        ;
    }
}