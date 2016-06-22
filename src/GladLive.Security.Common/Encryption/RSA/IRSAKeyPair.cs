using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
#if !NET35
using System.Threading.Tasks;
#endif

namespace GladLive.Security.Common
{
	interface IRSAKeyPair
	{
		bool isOnlyPublic { get; }

		RSAParameters RSAParameters { get; }
	}
}
