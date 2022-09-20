using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.Web;

namespace OrderManager.IntegrationTests.AutoFixture
{
    public delegate TestIntegrationHost IntegrationTestHostBuilder(
        Action<IServiceCollection> serviceCollectionModifier = null,
        IEnumerable<KeyValuePair<string, string>> configuration = null);

    public class IntegrationHostInlineDataAttribute : InlineAutoDataAttribute
    {

        public IntegrationHostInlineDataAttribute(params object[] values) : base(new IntegrationHostDataAttribute(), values)
        {
        }

        private class IntegrationHostDataAttribute : AutoDataAttribute
        {
            public IntegrationHostDataAttribute() : base(FixtureFactory)
            {
            }

            private static IFixture FixtureFactory()
            {
                var fixture = new Fixture();
                fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
                fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => fixture.Behaviors.Remove(b));
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());

                fixture.Register<IntegrationTestHostBuilder>(() => (serviceCollectionModifier, config) => new TestIntegrationHost(serviceCollectionModifier, config));

                return fixture;
            }

        }
    }

    public class TestIntegrationHost : WebApplicationFactory<Program>
    {
        private readonly Action<IServiceCollection> _serviceCollectionModifier;
        private readonly IEnumerable<KeyValuePair<string, string>> _configuration;

        public TestIntegrationHost(
            Action<IServiceCollection> serviceCollectionModifier,
            IEnumerable<KeyValuePair<string, string>> configuration)
        {
            _serviceCollectionModifier = serviceCollectionModifier;
            _configuration = configuration ?? new KeyValuePair<string, string>[0];
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureAppConfiguration(configBuilder => configBuilder.AddInMemoryCollection(_configuration));

            if (_serviceCollectionModifier != null)
            {
                builder.ConfigureServices(_serviceCollectionModifier);
            }
        }
    }
}