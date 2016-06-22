using System;
using System.Collections.Generic;
using System.Linq;
#if !NET35
using System.Threading.Tasks;
#endif

namespace GladLive.Security.Common
{
	/// <summary>
	/// Service contract for hashing.
	/// </summary>
	public interface IHashingService
	{
		/// <summary>
		/// Converts <paramref name="valueToHash"/> to the equivalent hash.
		/// </summary>
		/// <param name="valueToHash">String to hash.</param>
		/// <returns>Hashed form of <paramref name="valueToHash"/>.</returns>
		string Hash(string valueToHash);

		/// <summary>
		/// Compares two hashed values for underlying data equivalence.
		/// </summary>
		/// <param name="hashOne">Hash to compare to.</param>
		/// <param name="hashTwo">Hash to compare.</param>
		/// <returns>True if the underlying data is equal or the hash values are equal; false if otherwise.</returns>
		bool isHashValuesEqual(string hashOne, string hashTwo);
	}
}
