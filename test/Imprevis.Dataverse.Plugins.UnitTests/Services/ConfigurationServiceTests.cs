namespace Imprevis.Dataverse.Plugins.UnitTests.Services
{
    using Imprevis.Dataverse.Plugins.Services;
    using Microsoft.Xrm.Sdk;
    using Xunit;

    public class ConfigurationServiceTests
    {
        private IConfigurationService config;

        [Fact]
        public void GetUnsecure_ShouldDeserializeJson()
        {
            config = new ConfigurationService("\"value\"", string.Empty);

            var actual = config.GetUnsecure<string>();

            Assert.Equal("value", actual);
        }

        [Fact]
        public void GetUnsecure_ShouldDeserializeXml()
        {
            config = new ConfigurationService("<string>value</string>", string.Empty);

            var actual = config.GetUnsecure<string>(SerializationFormat.Xml);

            Assert.Equal("value", actual);
        }

        [Fact]
        public void GetUnsecure_ShouldThrowException_WhenEmptyString()
        {
            config = new ConfigurationService(string.Empty, string.Empty);

            Assert.Throws<InvalidPluginExecutionException>(() =>
            {
                config.GetUnsecure<string>();
            });
        }

        [Fact]
        public void GetSecure_ShouldDeserializeJson()
        {
            config = new ConfigurationService(string.Empty, "\"value\"");

            var actual = config.GetSecure<string>();

            Assert.Equal("value", actual);
        }

        [Fact]
        public void GetSecure_ShouldDeserializeXml()
        {
            config = new ConfigurationService(string.Empty, "<string>value</string>");

            var actual = config.GetSecure<string>(SerializationFormat.Xml);

            Assert.Equal("value", actual);
        }

        [Fact]
        public void GetSecure_ShouldThrowException_WhenEmptyString()
        {
            config = new ConfigurationService(string.Empty, string.Empty);

            Assert.Throws<InvalidPluginExecutionException>(() =>
            {
                config.GetSecure<string>();
            });
        }
    }
}