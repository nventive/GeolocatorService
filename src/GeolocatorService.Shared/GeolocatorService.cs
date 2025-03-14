using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace GeolocatorService
{
	/// <inheritdoc cref="IGeolocatorService"/>
	public class GeolocatorService : IGeolocatorService
	{
		private readonly Geolocator _locator;

		public GeolocatorService()
		{
			_locator = new Geolocator();
		}

		public event LocationChangedEventHandler LocationChanged;
		public event LocationPermissionChangedEventHandler LocationPermissionChanged;

		public async Task<bool> GetIsPermissionGranted(CancellationToken ct)
		{
			return _locator.LocationStatus == PositionStatus.Ready;
		}

		public async Task<Geocoordinate> GetLocation(CancellationToken ct)
		{
			var position = await _locator.GetGeopositionAsync().AsTask(ct);
			var coordinate = GetGeoCoordinates(position.Coordinate);
			return coordinate;
		}

		private Geocoordinate GetGeoCoordinates(Windows.Devices.Geolocation.Geocoordinate coordinate)
		{
			return new Geocoordinate(
				accuracy: coordinate.Accuracy,
				altitude: coordinate.Altitude,
				altitudeAccuracy: coordinate.AltitudeAccuracy,
				heading: coordinate.Heading,
				latitude: coordinate.Latitude,
				longitude: coordinate.Longitude,
				speed: coordinate.Speed,
				timeStamp: coordinate.Timestamp,
				point: GetPoint(coordinate.Point),
				positionSource: GetPositionSource(coordinate.PositionSource),
				positionSourceTimestamp: coordinate.PositionSourceTimestamp);
		}

		private PositionSource GetPositionSource(Windows.Devices.Geolocation.PositionSource positionSource)
		{
			switch(positionSource)
			{
				case Windows.Devices.Geolocation.PositionSource.Cellular:
					return PositionSource.Cellular;
				case Windows.Devices.Geolocation.PositionSource.Satellite:
					return PositionSource.Satellite;
				case Windows.Devices.Geolocation.PositionSource.WiFi:
					return PositionSource.WiFi;
				case Windows.Devices.Geolocation.PositionSource.IPAddress:
					return PositionSource.IPAddress;
				case Windows.Devices.Geolocation.PositionSource.Unknown:
					return PositionSource.Unknown;
				case Windows.Devices.Geolocation.PositionSource.Default:
					return PositionSource.Default;
				case Windows.Devices.Geolocation.PositionSource.Obfuscated:
					return PositionSource.Obfuscated;
				default:
					throw new NotSupportedException("Invalid Position Source.");
			}
		}

		private Geopoint GetPoint(Windows.Devices.Geolocation.Geopoint point)
		{
			var position = GetBasicGeoposition(point.Position);
			var GeoshapeType = GetGeoshapeType(point.GeoshapeType);
			return new Geopoint(position, GeoshapeType);
		}

		private GeoshapeType GetGeoshapeType(Windows.Devices.Geolocation.GeoshapeType geoshapeType)
		{
			switch (geoshapeType)
			{
				case Windows.Devices.Geolocation.GeoshapeType.Geopoint:
					return GeoshapeType.Geopoint;
				case Windows.Devices.Geolocation.GeoshapeType.Geocircle:
					return GeoshapeType.Geocircle;
				case Windows.Devices.Geolocation.GeoshapeType.Geopath:
					return GeoshapeType.Geopath;
				case Windows.Devices.Geolocation.GeoshapeType.GeoboundingBox:
					return GeoshapeType.GeoboundingBox;
				default:
					throw new NotSupportedException("Invalid Geoshape Type.");

			}
		}

		private BasicGeoposition GetBasicGeoposition(Windows.Devices.Geolocation.BasicGeoposition position)
		{
			return new BasicGeoposition(position.Latitude, position.Longitude, position.Altitude);
		}

		public async Task<bool> RequestPermission(CancellationToken ct)
		{
			// Requesting permission repeatedly is not necessary and can, for example,
			// make the value emitted by the LocationPermissionChanged event become false for a short time
			// when it was true, even thought the user never revoked the permissions.
			var isPermissionGranted = await GetIsPermissionGranted(ct);

			if (isPermissionGranted)
			{
				return true;
			}

			// Only subscribe to events here, not in the ctor, because subscribing to these
			// Geolocator events causes the permission to be immediately requested and we want to allow
			// greater flexibility with the moment the permission is requested.

			_locator.StatusChanged -= OnStatusChanged;
			_locator.StatusChanged += OnStatusChanged;

			var status = await Geolocator.RequestAccessAsync().AsTask(ct);

			if (status == GeolocationAccessStatus.Allowed)
			{
				_locator.PositionChanged -= OnPositionChanged;
				_locator.PositionChanged += OnPositionChanged;
			}

			return status == GeolocationAccessStatus.Allowed;
		}

		private void OnPositionChanged(Geolocator sender, PositionChangedEventArgs args)
		{
			var coordinates = GetGeoCoordinates(args.Position.Coordinate);
			LocationChanged?.Invoke(sender, new LocationChangedEventArgs(coordinates));
		}

		private void OnStatusChanged(Geolocator sender, StatusChangedEventArgs args)
		{
			LocationPermissionChanged?.Invoke(sender, new LocationPermissionChangedEventArgs(args.Status == PositionStatus.Ready));
		}
	}
}
