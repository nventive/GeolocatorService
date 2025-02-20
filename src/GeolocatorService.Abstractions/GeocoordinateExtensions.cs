using System;

namespace GeolocatorService
{
	public static class GeocoordinateExtensions
	{
		/// <summary>
		/// Gets a short string indicating the longitude and latitude of the geocoordinate, or [null] if there is not a
		/// valid longitude and latitude
		/// </summary>
		/// <param name="geocoordinate"></param>
		/// <returns></returns>
		public static string ToShortString(this Geocoordinate geocoordinate)
		{
			if (geocoordinate == null
				|| geocoordinate.Point.Position.Latitude == double.NaN
				|| geocoordinate.Point.Position.Longitude == double.NaN)
			{
				return "[null]";
			}

			return $"({geocoordinate.Point.Position.Longitude}, {geocoordinate.Point.Position.Latitude})";
		}

		/// <summary>
		/// Gets a value that represent the distance between two Geocoordinates. This use the Haversine formula.
		/// </summary>
		/// <param name="geocoordinate"></param>
		/// <param name="destination"></param>
		/// <returns>Distance in meters</returns>
		public static double GetDistanceTo(this Geocoordinate geocoordinate, Geocoordinate destination)
		{

			if (geocoordinate == null || destination == null || double.IsNaN(geocoordinate.Point.Position.Latitude) || double.IsNaN(geocoordinate.Point.Position.Longitude) || double.IsNaN(destination.Point.Position.Latitude) || double.IsNaN(destination.Point.Position.Longitude))
			{
				throw new ArgumentException("Latitude or longitude is not a number NaN");
			}

			return geocoordinate.GetDistanceTo(destination.Point.Position.Latitude, destination.Point.Position.Longitude);
		}


		/// <summary>
		/// Gets a value that represent the distance between the Geocoordinate and a latitude and longitude value.  This use the Haversine formula.
		/// </summary>
		/// <param name="geocoordinate"></param>
		/// <param name="latitude"></param>
		/// <param name="longitude"></param>
		/// <returns>Distance in meters</returns>
		public static double GetDistanceTo(this Geocoordinate geocoordinate, double latitude, double longitude)
		{

			if (geocoordinate == null || double.IsNaN(geocoordinate.Point.Position.Latitude) || double.IsNaN(geocoordinate.Point.Position.Longitude) || double.IsNaN(latitude) || double.IsNaN(longitude))
			{
				throw new ArgumentException("Latitude or longitude is not a number NaN");
			}

			// For more information about this algo you can check : http://www.movable-type.co.uk/scripts/latlong.html 	
			const double r = 6376500.0; //Earth radius in meters
			double phi1 = geocoordinate.Point.Position.Latitude * 0.017453292519943295;
			double delta1 = geocoordinate.Point.Position.Longitude * 0.017453292519943295;
			double phi2 = latitude * 0.017453292519943295;
			double delta2 = longitude * 0.017453292519943295;
			double deltaPhi = delta2 - delta1;
			double deltaLambda = phi2 - phi1;
			double a = Math.Pow(Math.Sin(deltaLambda / 2.0), 2.0) + (Math.Cos(phi1) * Math.Cos(phi2) * Math.Pow(Math.Sin(deltaPhi / 2.0), 2.0));
			double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
			return r * c; // Distance in meters
		}
	}
}
