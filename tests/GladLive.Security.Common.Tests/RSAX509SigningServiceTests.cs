using GladLive.Security.Common.Cert;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Runtime;

namespace GladLive.Security.Common.Tests
{
	public static class RSAX509SigningServiceTests
	{
		[Fact(DisplayName = nameof(Test_Sign_Message_Returns_Non_Null_Bytes))]
		public static void Test_Sign_Message_Returns_Non_Null_Bytes()
		{
			//Create the signing service
			RSAX509SigningService signer = new RSAX509SigningService(LoadTestCert());

			//act
			byte[] bytes = signer.SignMessage("hello");

			//assert
			Assert.NotNull(bytes);
			Assert.True(bytes.Count() != 0);
			Assert.NotSame(bytes, Encoding.ASCII.GetBytes("hello"));
		}

		[Fact]
		public static void Test_Sign_Message_Verifies_After_Signing()
		{

			//Create the signing service
			RSAX509SigningService signer = new RSAX509SigningService(LoadTestCert());

			//act
			byte[] bytes = signer.SignMessage("hello");

			Assert.True(signer.isSigned("hello", bytes));
			Assert.False(signer.isSigned("this should fail", bytes));
		}

		private static X509Certificate2 LoadTestCert()
		{
			byte[] certBytes = null;

			try
			{
				using (Stream inputStream = typeof(RSAX509SigningServiceTests).GetTypeInfo().Assembly.GetManifestResourceStream(nameof(GladLive.Security.Common.Tests) + ".TestCert.pfx"))
				{
					certBytes = new byte[inputStream.Length];
					inputStream.Read(certBytes, 0, (int)inputStream.Length);
				}
			}
			catch(NullReferenceException)
			{
				//Try to load from cd
				using (FileStream stream = new FileStream("TestCert.pfx", FileMode.Open))
				{
					certBytes = new byte[stream.Length];
					stream.Read(certBytes, 0, (int)stream.Length);
				}
			}
			

			return new X509Certificate2(certBytes, "", X509KeyStorageFlags.Exportable);
		}
	}

}
