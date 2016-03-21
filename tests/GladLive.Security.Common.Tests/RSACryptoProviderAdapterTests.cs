using GladLive.Security.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;

namespace GladLive.Security.Common
{
	public class RSACryptoProviderAdapterTests
	{
		[Fact]
		public void Test_Ctor_Doesnt_Throw()
		{
			//arrange
			RSACryptoServiceProvider provider = new RSACryptoServiceProvider();

			//assert
			//Shouldn't throw
			RSACryptoProviderAdapter adapter = new RSACryptoProviderAdapter(new RSAKeyPair(provider.ExportParameters(false)));
		}

		[Fact]
		public void Test_Adapter_Throws_Decrypt_With_Only_Public()
		{
			//arrange
			RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
			RSACryptoProviderAdapter adapter = new RSACryptoProviderAdapter(new RSAKeyPair(provider.ExportParameters(false)));

			//assert
			//should throw
			Assert.Throws(typeof(InvalidOperationException), () => adapter.Decrypt(new byte[5]));
		}

		[Fact]
		public void Test_Adapter_Encrypt_Isnt_Null()
		{
			//arrange
			RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
			RSACryptoProviderAdapter adapter = new RSACryptoProviderAdapter(new RSAKeyPair(provider.ExportParameters(false)));

			//assert
			//should throw
			Assert.NotNull(adapter.Encrypt("Hello"));
		}

		[Fact]
		public void Test_Adapter_Encrypt_And_Then_Decrypt_Produce_Same_String()
		{
			//arrange
			RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
			RSACryptoProviderAdapter adapter = new RSACryptoProviderAdapter(new RSAKeyPair(provider.ExportParameters(true)));

			//act
			byte[] encryptedBytes = adapter.Encrypt("Hello");
			string decryptedString = adapter.DecryptToString(encryptedBytes);

			//assert
			Assert.NotNull(decryptedString);
			Assert.NotEmpty(decryptedString);
			Assert.Equal(decryptedString, "Hello");
			Assert.NotEqual(decryptedString, "Derp");
		}
	}
}
