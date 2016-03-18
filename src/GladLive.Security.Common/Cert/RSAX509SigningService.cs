﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GladLive.Security.Common.Cert
{
	public class RSAX509SigningService
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
			using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
			{

//If this is core clr or dnx46 then the API differs from lower dnx or .net
#if DNXCORE50 || DNX46
				provider.ImportParameters(cert.GetRSAPublicKey().ExportParameters(false));
	#if DNX46
				return provider.VerifyData(message, CryptoConfig.MapNameToOID("SHA256"), signedMessage);
	#elif DNXCORE50
				//core doesn't have cryptoconfig so we must use hash name (maybe)
				return provider.VerifyData(message, HashAlgorithmName.SHA256, signedMessage);
	#endif

#elif DNX451 || NET45
				provider.FromXmlString(cert.PrivateKey.ToXmlString(false));
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
			using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
			{

#if DNXCORE50 || DNX46

				provider.ImportParameters(cert.GetRSAPrivateKey().ExportParameters(true));
	#if DNX46
				return provider.SignData(message, CryptoConfig.MapNameToOID("SHA256"));
	#elif DNXCORE50
				return provider.SignData(message, HashAlgorithmName.SHA256);
#endif

#elif DNX451 || NET45
				provider.FromXmlString(cert.PrivateKey.ToXmlString(true));

				return provider.SignData(message, CryptoConfig.MapNameToOID("SHA256"));
#else
				throw new NotSupportedException("The current runtime/clr does not support " + nameof(SignMessage) + " in class " + this.GetType());
#endif
			}
		}

		public byte[] SignMessage(string message)
		{
			return SignMessage(Encoding.ASCII.GetBytes(message));
		}
	}
}