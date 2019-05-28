using System.Globalization;
using Xunit;

namespace PolylinerNet.Tests
{
    public class Polyliner_DecodeTests
    {
        [Theory]
        [InlineData("ikm~Fha|uO", "41.85285", "-87.63941")]     // Chicago
        [InlineData("svw}Hkza_C", "52.22778", "20.98614")]      // Warsaw
        [InlineData("yqv~FeqhkA", "41.89997", "12.50083")]      // Rome
        [InlineData("_wxxEuzlsY", "35.68512", "139.66267")]     // Tokyo
        [InlineData("xuekEl|inL", "-33.45773", "-70.67095")]    // Sandiego
        public void Polyliner_Decode_ShouldReturnArray_WithOnePositionElement(string polyline, string latitude, string longitude)
        {
            // arrange
            var polyliner = new Polyliner();

            // act
            var result = polyliner.Decode(polyline);

            // assert
            Assert.Single(result);
            Assert.Equal(latitude, result[0].Latitude.ToString("0.00000", CultureInfo.InvariantCulture));
            Assert.Equal(longitude, result[0].Longitude.ToString("0.00000", CultureInfo.InvariantCulture));
        }

        [Theory]
        [InlineData("mfo~Fvx{uOukAoT", "41.86231", "-87.63804", "41.87458", "-87.63460")]     // Chicago
        public void Polyliner_Decode_ShouldReturnArray_WithTwoPositionsElement(string polyline, string latitude1, string longitude1, string latitude2, string longitude2)
        {
            // arrange
            var polyliner = new Polyliner();

            // act
            var result = polyliner.Decode(polyline);

            // assert
            Assert.Equal(2, result.Count);
            Assert.Equal(latitude1, result[0].Latitude.ToString("0.00000", CultureInfo.InvariantCulture));
            Assert.Equal(longitude1, result[0].Longitude.ToString("0.00000", CultureInfo.InvariantCulture));
            Assert.Equal(latitude2, result[1].Latitude.ToString("0.00000", CultureInfo.InvariantCulture));
            Assert.Equal(longitude2, result[1].Longitude.ToString("0.00000", CultureInfo.InvariantCulture));
        }
    }
}
