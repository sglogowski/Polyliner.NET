# Polyliner.NET

Simple Google Maps Polyline encoder/decoder.

https://developers.google.com/maps/documentation/utilities/polylinealgorithm

## Usage

Encode Points:

    var polyliner = new Polyliner();
    var points = new List<PolylinePoint> 
    {
        new PolylinePoint(latitude: 52.22778, longitude: 20.98614), 
        new PolylinePoint(latitude: 52.22555, longitude: 20.98725)
    };
    
    var result = polyliner.Encode(points); 
    
    Console.WriteLine(result); // "svw}Hkza_C|L}E"

Decode Polyline:

    var polyliner = new Polyliner();
    var polyline = "svw}Hkza_C|L}E";

    var result = polyliner.Decode(polyline);
    
    Console.WriteLine(result[0].Latitude.ToString("0.00000", CultureInfo.InvariantCulture));  // "52.22778"
    Console.WriteLine(result[0].Longitude.ToString("0.00000", CultureInfo.InvariantCulture)); // "20.98614"
    Console.WriteLine(result[1].Latitude.ToString("0.00000", CultureInfo.InvariantCulture));  // "52.22555"
    Console.WriteLine(result[1].Longitude.ToString("0.00000", CultureInfo.InvariantCulture)); // "20.98725"
