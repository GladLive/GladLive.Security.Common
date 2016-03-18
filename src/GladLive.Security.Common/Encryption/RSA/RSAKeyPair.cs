using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GladLive.Security.Common
{
	public class RSAKeyPair : IRSAKeyPair
	{
		public RSAParameters RSAParameters { get; }

		public bool isOnlyPublic { get; }

		public RSAKeyPair(RSAParameters parameters)
		{
			RSAParameters = parameters;

			isOnlyPublic = RSAParameters.D == null && RSAParameters.P == null && RSAParameters.Q == null;
		}
	}
}
