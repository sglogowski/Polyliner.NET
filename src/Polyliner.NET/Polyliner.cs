using PolylinerNet.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace PolylinerNet
{
    public class PolylinerBase
    {
        protected void EncodeNextCoordinate(long coordinate, StringBuilder result)
        {
            coordinate = coordinate < 0 ? ~(coordinate << 1) : coordinate << 1;

            while (coordinate >= 0x20)
            {
                result.Append((char)((int)((0x20 | (coordinate & 0x1f)) + 63)));
                coordinate >>= 5;
            }

            result.Append((char)((int)(coordinate + 63)));
        }

        protected int DecodeNextCoordinate(string polyline, ref int polylineIndex)
        {
            int result = 0; int shift = 0; int b;

            if (polylineIndex < polyline.Length)
            do
            {
                b = polyline[polylineIndex++] - 63;
                result |= (b & 0x1f) << shift;
                shift += 5;
            } 
            while (b >= 0x20);

            return (result & 1) != 0 ? ~(result >> 1) : (result >> 1);
        }
    }

    public class Polyliner : PolylinerBase, IPolyliner
    {
        public string Encode(List<PolylinePoint> polylinePoints)
        {
            var result = new StringBuilder();
            long lastLatitude = 0L, lastLongitude = 0L;

            foreach (var polylinePoint in polylinePoints)
            {
                long latitude = (long)(polylinePoint.Latitude * 1e5);
                long longitude = (long)(polylinePoint.Longitude * 1e5);

                base.EncodeNextCoordinate(latitude - lastLatitude, result);
                base.EncodeNextCoordinate(longitude - lastLongitude, result);

                lastLatitude = latitude;
                lastLongitude = longitude;
            }

            return result.ToString();
        }

        public List<PolylinePoint> Decode(string polyline)
        {
            var polylinePoints = new List<PolylinePoint>(polyline.Length / 2);

            for (int polylineIndex = 0, latitude = 0, longitude = 0; polylineIndex < polyline.Length;)
            {
                latitude += base.DecodeNextCoordinate(polyline, ref polylineIndex);
                longitude += base.DecodeNextCoordinate(polyline, ref polylineIndex);

                polylinePoints.Add(new PolylinePoint(latitude * 1e-5, longitude * 1e-5));
            }

            return polylinePoints;
        }
    }
}
