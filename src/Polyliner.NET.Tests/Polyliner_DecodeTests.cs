using Xunit;

namespace PolylinerNet.Tests
{
    public class Polyliner_DecodeTests
    {
        [Theory]
        [InlineData(@"ikm~Fha|uO", 41.85285, -87.63941)]     // Chicago
        [InlineData(@"svw}Hkza_C", 52.22778, 20.98614)]      // Warsaw
        [InlineData(@"yqv~FeqhkA", 41.89997, 12.50083)]      // Rome
        [InlineData(@"_wxxEuzlsY", 35.68512, 139.66267)]     // Tokyo
        [InlineData(@"xuekEl|inL", -33.45773, -70.67095)]    // Sandiego
        public void Polyliner_Decode_ShouldReturnArray_WithOnePositionElement(string polyline, double latitude, double longitude)
        {
            // arrange
            var polyliner = new Polyliner();

            // act
            var result = polyliner.Decode(polyline);

            // assert
            Assert.Equal(latitude, result[0].Latitude, 5);
            Assert.Equal(longitude, result[0].Longitude, 5);
        }

        // Google exaples(https://developers.google.com/maps/documentation/utilities/polylinealgorithm?csw=1):
        [Theory]
        [InlineData(@"_p~iF~ps|U_ulLnnqC_mqNvxq`@")]
        public void Polyliner_DecodeGoogleExamples_ShouldReturnArray_WithExpectedPositionsElement(string polyline)
        {
            // arrange
            var polyliner = new Polyliner();

            // act
            var result = polyliner.Decode(polyline);

            // assert
            Assert.Equal(3, result.Count);

            Assert.Equal(38.5, result[0].Latitude, 5);
            Assert.Equal(-120.2, result[0].Longitude, 5);

            Assert.Equal(40.7, result[1].Latitude, 5);
            Assert.Equal(-120.95, result[1].Longitude, 5);

            Assert.Equal(43.252, result[2].Latitude, 5);
            Assert.Equal(-126.453, result[2].Longitude, 5);
        }

        [Theory]
        [InlineData(@"mfo~Fvx{uOukAoT", 41.86231, -87.63804, 41.87458, -87.63460)]     // Chicago
        public void Polyliner_Decode_ShouldReturnArray_WithTwoPositionsElement(string polyline, double latitude1, double longitude1, double latitude2, double longitude2)
        {
            // arrange
            var polyliner = new Polyliner();

            // act
            var result = polyliner.Decode(polyline);

            // assert
            Assert.Equal(2, result.Count);
            Assert.Equal(latitude1, result[0].Latitude, 5);
            Assert.Equal(longitude1, result[0].Longitude, 5);
            Assert.Equal(latitude2, result[1].Latitude, 5);
            Assert.Equal(longitude2, result[1].Longitude, 5);
        }

        [Theory]
        [InlineData("wvduHmyjU_CbAi@R_@FiAJkCz@cBd@{@T]Bu@Pq@TeAd@q@b@u@\\\\mAt@i@^aCtB[^Wf@w@bCI^Kv@A`AEh@]zBU`CKf@GPQTcA`A")]
        public void Polyliner_Decode_ShouldNotThrowException(string polyline)
        {
            // arrange
            var polyliner = new Polyliner();

            // act
            var result = polyliner.Decode(polyline);

            // assert
            Assert.NotNull(result);
        }
    }
}
