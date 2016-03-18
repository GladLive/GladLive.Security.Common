using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GladLive.Security.Common
{
	interface IRSAKeyPair
	{
		bool isOnlyPublic { get; }

		RSAParameters RSAParameters { get; }
	}
}
