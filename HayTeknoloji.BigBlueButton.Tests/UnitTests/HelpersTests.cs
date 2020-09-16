using System.IO;
using System.Text;
using FluentAssertions;
using HayTeknoloji.BigBlueButton.Helpers;
using HayTeknoloji.BigBlueButton.Models;
using Xunit;
using Xunit.Abstractions;

namespace HayTeknoloji.BigBlueButton.Tests.UnitTests
{
    public class HelpersTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public HelpersTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void QueryStringBuilder_Test()
        {
            /* Arrange */
            var qb = new QueryStringBuilder
            {
                {"testkey", "testvalue"},
                {"testkey2", "testvalue2"}
            };

            /* Act */
            var result = qb.ToString();

            /* Assert */
            result.Should().Be("testkey=testvalue&testkey2=testvalue2");
        }

        [Fact]
        public void CheckSum_Test()
        {
            /* Arrange */

            /* Act */
            var result = UrlHelper.CheckSum("test");

            /* Assert */
            result.Should().Be("a94a8fe5ccb19ba61c4c0873d391e987982fbbd3");
        }

        [Fact]
        public void GetBbbUri_Test()
        {
            /* Arrange */
            var param = new QueryStringBuilder {{"key", "value"}};

            /* Act */
            var result = UrlHelper.GetBbbUri("http://test.com", "test", "test", param.ToString());

            /* Assert */
            result.Should().Be("http://test.com/bigbluebutton/api/test?checksum=804eaa9be3d6ec726d944cbbd12b332286d74d94&key=value");
        }

        [Fact]
        public void Xml_Deserialize_Test()
        {
            /* Arrange */
            var xmlString = @"<response>
                                <returncode>SUCCESS</returncode>
                                <version>2.0</version>
                              </response>";
            var xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString));

            /* Act */
            var result = xmlStream.Deserialize<Response>();

            /* Assert */
            result.Version.Should().Be("2.0");
        }
    }
}