namespace PolylinerNet
{
    public struct Position
    {
        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }
    }
}
