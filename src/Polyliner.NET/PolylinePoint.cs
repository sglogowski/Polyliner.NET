namespace PolylinerNet
{
    public struct PolylinePoint
    {
        public PolylinePoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }
    }
}
