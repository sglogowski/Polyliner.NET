using System.Collections.Generic;

namespace PolylinerNet.Interfaces
{
    public interface IPolyliner
    {
        string Encode(List<PolylinePoint> positions);

        List<PolylinePoint> Decode(string polyline);
    }
}
