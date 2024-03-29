﻿using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using EPProvider;
using EPProvider.Mapping;
using MockProvider;
using Prism.Events;
using UI.ViewModel;
using ApplicationDataHandler;
using System.Globalization;

namespace UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {



            var builder = new ContainerBuilder();

            builder.AddAutoMapper(typeof(ProjectMapperProfile).Assembly);
            builder.AddAutoMapper(typeof(ProjectMockMapperProfile).Assembly);

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<EnvironnmentCredentialsProvider>().As<ICredentialsProvider>().SingleInstance();
            builder.RegisterType<RestEPProvider>().As<IEPProvider>();
            builder.RegisterType<JsonApplicationDataHandler>().As<IApplicationDataHandler>();
            //builder.RegisterType<MockEpProvider>().As<IEPProvider>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<TimeEntryViewModel>().As<ITimeEntryViewModel>();
            builder.RegisterType<LoginViewModel>().As<ILoginViewModel>();
            builder.RegisterType<TabViewModel>().As<ITabViewModel>();
            builder.RegisterType<SpentTimeViewModel>().As<ISpentTimeViewModel>();
            builder.RegisterType<ViewsAggregatorViewModel>().As<IViewsAggregatorViewModel>();

            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = "."; //Force use . insted of ,
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;

            return builder.Build();
        }
    }
}