using GladLive.Security.Common.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GladLive.Security.Common
{
	public class CertToRSAProviderConverterTests
	{
		[Fact]
		public void Test_Ensure_Ctor_Doesnt_Throw_On_Valid_Cert()
		{
			//arrange
			var cert = RSAX509SigningServiceTests.LoadTestCert();

			//act
			CertToRSAProviderConverter converter = new CertToRSAProviderConverter(cert, true);
		}

		[Fact]
		public void Test_Ensure_Includes_Private_Params_If_Requests()
		{
			//arrange
			var cert = RSAX509SigningServiceTests.LoadTestCert();

			//act
			var rsaCrypto = new CertToRSAProviderConverter(cert, true).Provider;

			//assert:next line shouldn't throw if private was included
			var rsaparams = rsaCrypto.ExportParameters(true);
		}
	}
}
