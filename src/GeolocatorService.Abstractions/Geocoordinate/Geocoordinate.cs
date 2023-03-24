using System;
using System.Runtime.CompilerServices;

namespace GeolocatorService
{
	public class Geocoordinate
	{
		public Geocoordinate(
			double accuracy,
			double? altitude,
			double? altitudeAccuracy,
			double? heading,
			double latitude,
			double longitude,
			double? speed,
			DateTimeOffset timeStamp,
			Geopoint point,
			PositionSource positionSource,
			DateTimeOffset? positionSourceTimestamp)
		{
			Accuracy = accuracy;
			Altitude = altitude;
			AltitudeAccuracy = altitudeAccuracy;
			Heading = heading;
			Latitude = latitude;
			Longitude = longitude;
			Speed = speed;
			TimeStamp = timeStamp;
			Point = point;
			PositionSource = positionSource;
			PositionSourceTimestamp = positionSourceTimestamp;
		}

		/// <summary>
		/// The accuracy of the location in meters.
		/// </summary>
		public double Accuracy { get; }

		/// <summary>
		/// The Altitude of the location in meters.
		/// </summary>
		public double? Altitude { get; }

		/// <summary>
		/// The accuracy of the altitude in meters.
		/// </summary>
		public double? AltitudeAccuracy { get; }

		/// <summary>
		/// The current heading in degrees relative to true north.
		/// </summary>
		public double? Heading { get; }

		/// <summary>
		/// The latitude in degrees.
		/// </summary>
		public double Latitude { get; }

		/// <summary>
		/// The longitude in degrees.
		/// </summary>
		public double Longitude { get; }

		/// <summary>
		/// The speed in meters per second.
		/// </summary>
		public double? Speed { get; }

		/// <summary>
		/// The system time at which the location was determined.
		/// </summary>
		public DateTimeOffset TimeStamp { get; }

		/// <summary>
		/// The location of the Geocoordinate.
		/// </summary>
		public Geopoint Point { get; }

		/// <summary>
		/// Gets the source used to obtain a Geocoordinate.
		/// </summary>
		public PositionSource PositionSource { get; }

		/// <summary>
		/// Gets the time at which the associated Geocoordinate position was calculated.
		/// </summary>
		public DateTimeOffset? PositionSourceTimestamp { get; }
	}
}
