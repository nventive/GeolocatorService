using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GeolocatorService
{
	public class Geopoint
	{
		public Geopoint(BasicGeoposition position)
		{
			Position = position;
		}

		public Geopoint(BasicGeoposition position, GeoshapeType geoshapeType)
		{
			Position = position;
			GeoshapeType = geoshapeType;
		}

		/// <summary>
		/// The position of a geographic point.
		/// </summary>
		public BasicGeoposition Position { get; }

		/// <summary>
		/// The type of geographic shape. (Point, Circle, Path, Box)
		/// </summary>
		public GeoshapeType GeoshapeType { get; }
	}
}
