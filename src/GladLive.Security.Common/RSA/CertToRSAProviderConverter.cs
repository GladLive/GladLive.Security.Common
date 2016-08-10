using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
#if !NET35
using System.Threading.Tasks;
#endif

namespace GladLive.Security.Common
{
	/// <summary>
	/// Class converts a <see cref="X509Certificate2"/> into an <see cref="RSACryptoProvider"/> 
	/// </summary>
	public class CertToRSAProviderConverter
	{
		/// <summary>
		/// RSA provider that was built using the provided <see cref="X509Certificate2"/>.
		/// </summary>
		public RSACryptoServiceProvider Provider { get; }

		/// <summary>
		/// Creates a <see cref="X509Certificate2"/> converter that produces an initialized instance of
		/// <see cref="RSACryptoServiceProvider"/>.
		/// </summary>
		/// <param name="cert">Cert to use information from.</param>
		/// <param name="includePrivateKey">Indicates if the private key should be included.</param>
		public CertToRSAProviderConverter(X509Certificate2 cert, bool includePrivateKey)
		{
			//Don't do a using or dispose. Let the consumers of this class deal with that.
			Provider = new RSACryptoServiceProvider();

#if NETSTANDARD1_6
			Provider.ImportParameters(cert.GetRSAPublicKey().ExportParameters(includePrivateKey));
#endif

#if NET451 || NET45 || NET35 || NET452 || NET46
			Provider.FromXmlString(cert.PrivateKey.ToXmlString(includePrivateKey));
#endif
		}
	}
}
