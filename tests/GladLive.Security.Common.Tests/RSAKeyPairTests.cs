using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace GladLive.Security.Common.Tests
{
	public class RSAKeyPairTests
	{
		//Suprise, this was a fault at first
		[Fact]
		public void Test_RSA_Parameters_Is_Set()
		{
			//arrange
			RSAParameters parameters = new RSAParameters();
			RSAKeyPair keys = new RSAKeyPair(parameters);

			Assert.Equal(parameters, keys.RSAParameters);
		}
	}
}
