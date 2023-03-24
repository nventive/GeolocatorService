﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GeolocatorService
{
	public class LocationChangedEventArgs : EventArgs
	{
		public LocationChangedEventArgs(Geocoordinate coordinate)
		{
			Coordinate = coordinate;
		}

		public Geocoordinate Coordinate { get; }
	}
}
