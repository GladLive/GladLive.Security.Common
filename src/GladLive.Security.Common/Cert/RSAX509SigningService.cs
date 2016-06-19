using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GladLive.Security.Common
{
	public class RSAX509SigningService : ISigningService
	{
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
				//If this is core clr or dnx46 then the API differs from lower dnx or .net
#if DNX46
				return provider.VerifyData(message, CryptoConfig.MapNameToOID("SHA256"), signedMessage);
#elif DNXCORE50
				//core doesn't have cryptoconfig so we must use hash name (maybe)
				return provider.VerifyData(message, HashAlgorithmName.SHA256, signedMessage);
#endif

#if DNX451 || NET45 || NET451
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


#if DNXCORE50 || DNX46
				return cert.GetRSAPrivateKey().SignData(message, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
#elif DNX451 || NET45 || NET451
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