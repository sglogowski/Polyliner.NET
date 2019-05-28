using System;
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
            int result = 1; int shift = 0; int b;

            do
            {
                b = polyline[polylineIndex++] - 63 - 1;
                result += b << shift;
                shift += 5;
            } while (b >= 0x1f);

            return (result & 1) != 0 ? ~(result >> 1) : (result >> 1);
        }
    }

    public class Polyliner : PolylinerBase
    {
        public string Encode(List<Position> positions)
        {
            var result = new StringBuilder();
            long lastLatitude = 0L, lastLongitude = 0L;

            foreach (var position in positions)
            {
                var latitude = (long)Math.Round(position.Latitude * 1e5);
                var longitude = (long)Math.Round(position.Longitude * 1e5);

                base.EncodeNextCoordinate(latitude - lastLatitude, result);
                base.EncodeNextCoordinate(longitude - lastLongitude, result);

                lastLatitude = latitude;
                lastLongitude = longitude;
            }

            return result.ToString();
        }

        public List<Position> Decode(string polyline)
        {
            var positions = new List<Position>();

            for (int polylineIndex = 0, latitude = 0, longitude = 0; polylineIndex < polyline.Length;)
            {
                latitude += base.DecodeNextCoordinate(polyline, ref polylineIndex);
                longitude += base.DecodeNextCoordinate(polyline, ref polylineIndex);

                positions.Add(new Position(latitude * 1e-5, longitude * 1e-5));
            }

            return positions;
        }
    }
}
