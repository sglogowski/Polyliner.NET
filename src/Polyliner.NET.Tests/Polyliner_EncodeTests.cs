using System.Collections.Generic;
using Xunit;

namespace PolylinerNet.Tests
{
    public class Polyliner_EncodeTests
    {
        [Theory]
        [InlineData(41.85285, -87.63941, "gkm~Fha|uO")]     // Chicago
        [InlineData(52.22778, 20.98614, "svw}Hkza_C")]      // Warsaw
        [InlineData(41.89997, 12.50083, "yqv~FeqhkA")]      // Rome
        [InlineData(35.68512, 139.66267, "_wxxEuzlsY")]     // Tokyo
        [InlineData(-33.45773, -70.67095, "xuekEl|inL")]    // Sandiego
        public void Polyliner_EncodeListWithOneElement_ShouldReturnExpectedPolyline(double latitude, double longitude, string expectedPolyline)
        {
            // arrange
            var polyliner = new Polyliner();
            var points = new List<PolylinePoint> {
                new PolylinePoint(latitude, longitude)
            };

            // act
            var result = polyliner.Encode(points);

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedPolyline, result);
        }

        // Google exaples(https://developers.google.com/maps/documentation/utilities/polylinealgorithm?csw=1):
        [Fact]
        public void Polyliner_EncodeGoogleExamples_ShouldReturnExpectedPolyline()
        {
            // arrange
            var polyliner = new Polyliner();
            var points = new List<PolylinePoint> {
                new PolylinePoint(38.5, -120.2),
                new PolylinePoint(40.7, -120.95),
                new PolylinePoint(43.252, -126.453),
            };

            // act
            var result = polyliner.Encode(points);

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(@"_p~iF~ps|U_ulLnnqC_mqNvxq`@", result);
        }

        [Theory]
        [InlineData(41.86231, -87.63804, 41.87458, -87.63460, "mfo~Fvx{uOukAoT")]     // Chicago
        public void Polyliner_EncodeListWithTwoElements_ShouldReturnExpectedPolyline(
            double latitude1, double longitude1, double latitude2, double longitude2, string expectedPolyline)
        {
            // arrange
            var polyliner = new Polyliner();
            var points = new List<PolylinePoint> {
                new PolylinePoint(latitude1, longitude1),
                new PolylinePoint(latitude2, longitude2)
            };

            // act
            var result = polyliner.Encode(points);

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedPolyline, result);
        }
    }
}
