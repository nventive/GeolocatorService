using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocatorService
{
	public enum PositionSource
	{   
		/// <summary>
		/// The position was obtained from cellular network data.
		/// </summary>
		Cellular,

		/// <summary>
		/// The position was obtained from satellite data.
		/// </summary>
		Satellite,

		/// <summary>
		/// The position was obtained from Wi-Fi network data.
		/// </summary>
		WiFi,

		/// <summary>
		/// (Starting with Windows 8.1.) The position was obtained from an IP address.
		/// </summary>
		IPAddress,

		/// <summary>
		/// (Starting with Windows 8.1.) The position was obtained from an unknown source.
		/// </summary>
		Unknown,

		/// <summary>
		/// (Starting with Windows 10, version 1607.) The position was obtained from the user's manually-set location.
		/// </summary>
		Default,

		/// <summary>
		///  (Starting with Windows 10, version 1607.) The position was obtained via the coarse
		///  location feature and was therefore intentionally made inaccurate to a degree.
		/// </summary>
		Obfuscated,
	}
}
