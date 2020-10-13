using Autofac;
using AutoMapper;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using BaseLayer.Model;
using Xunit;
using EPProvider;
using EPProvider.Mapping;
using MockProvider;

namespace UnitTests
{
    public class EPProviderMapperTest
    {
        [Fact]
        public void TestMapper()
        {
            var builder = new ContainerBuilder();
            builder.AddAutoMapper(typeof(ProjectMapperProfile).Assembly);
            
            var container = builder.Build();
            var mapperCfg = container.Resolve<MapperConfiguration>();
            mapperCfg.AssertConfigurationIsValid();
        }

        [Fact]
        public void TestMockMapper()
        {
            var builder = new ContainerBuilder();
            builder.AddAutoMapper(typeof(ProjectMockMapperProfile).Assembly);

            var container = builder.Build();
            var mapperCfg = container.Resolve<MapperConfiguration>();
            mapperCfg.AssertConfigurationIsValid();
        }

    }
}