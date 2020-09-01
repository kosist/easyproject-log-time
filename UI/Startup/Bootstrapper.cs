using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using EPProvider;
using EPProvider.Mapping;
using Prism.Events;
using UI.ViewModel;

namespace UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.AddAutoMapper(typeof(ProjectMapperProfile).Assembly);

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<EnvironnmentCredentialsProvider>().As<ICredentialsProvider>().SingleInstance();
            builder.RegisterType<RestEPProvider>().As<IEPProvider>();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<TimeEntryViewModel>().As<ITimeEntryViewModel>();
            builder.RegisterType<LoginViewModel>().As<ILoginViewModel>();
            builder.RegisterType<TabViewModel>().As<ITabViewModel>();
            builder.RegisterType<ViewsAggregatorViewModel>().As<IViewsAggregatorViewModel>();

            return builder.Build();
        }
    }
}