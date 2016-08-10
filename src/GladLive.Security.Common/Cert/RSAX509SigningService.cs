using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
#if !NET35
using System.Threading.Tasks;
#endif

namespace GladLive.Security.Common
{
	public class RSAX509SigningService : ISigningService
	{
		/// <summary>
		/// The loaded <see cref="X509Certificate2"/>.
		/// </summary>
		private X509Certificate2 cert { get; }

		public RSAX509SigningService(string certPath)
		{
			cert = new X509Certificate2(certPath);
		}

		public RSAX509SigningService(X509Certificate2 certificate)
		{
			cert = certificate;
		}

		public bool isSigned(byte[] message, byte[] signedMessage)
		{
			using (RSACryptoServiceProvider provider = new CertToRSAProviderConverter(cert, false).Provider)
			{
#if NET46
				return provider.VerifyData(message, CryptoConfig.MapNameToOID("SHA256"), signedMessage);
#elif NETSTANDARD1_6
				//core doesn't have cryptoconfig so we must use hash name (maybe)
				return provider.VerifyData(message, HashAlgorithmName.SHA256, signedMessage);
#endif

#if !NETSTANDARD1_6
				return provider.VerifyData(message, CryptoConfig.MapNameToOID("SHA256"), signedMessage);
#else
				throw new NotSupportedException("The current runtime/clr does not support " + nameof(isSigned) + " in class " + this.GetType());
#endif
			}
		}

		public bool isSigned(string message, byte[] signedMessage)
		{
			return isSigned(Encoding.ASCII.GetBytes(message), signedMessage);
		}

		public byte[] SignMessage(byte[] message)
		{


#if NETSTANDARD1_6 || NET46
				return cert.GetRSAPrivateKey().SignData(message, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
#elif NET451 || NET45 || NET35 || NET452
			using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
			{
				provider.FromXmlString(cert.PrivateKey.ToXmlString(true));

				return provider.SignData(message, CryptoConfig.MapNameToOID("SHA256"));
			}
#else
			throw new NotSupportedException("The current runtime/clr does not support " + nameof(SignMessage) + " in class " + this.GetType());
#endif
		}

		public byte[] SignMessage(string message)
		{
			return SignMessage(Encoding.ASCII.GetBytes(message));
		}
	}
}