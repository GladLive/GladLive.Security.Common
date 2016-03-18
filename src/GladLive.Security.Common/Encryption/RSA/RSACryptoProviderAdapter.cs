using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GladLive.Security.Common
{
	/// <summary>
	/// Adapter for the .Net RSA implementation <see cref="RSACryptoServiceProvider"/>.
	/// </summary>
	public class RSACryptoProviderAdapter : ICryptoService, IDisposable
	{
		/// <summary>
		/// Internal keypair used for decryption.
		/// </summary>
		private RSAKeyPair keyPair { get; }

		/// <summary>
		/// Service provider for .NET RSA implementation that used the <see cref="keyPair"/>.
		/// </summary>
		private Lazy<RSACryptoServiceProvider> provider;

		public RSACryptoProviderAdapter(RSAKeyPair keys)
		{
			if (keys == null)
				throw new ArgumentNullException(nameof(keys), "Key pair cannot be null for " + nameof(RSACryptoProviderAdapter));

			keyPair = keys;
			provider = new Lazy<RSACryptoServiceProvider>(CreateProvider, true);
		}

		private RSACryptoServiceProvider CreateProvider()
		{
			RSACryptoServiceProvider p = new RSACryptoServiceProvider();

			p.ImportParameters(keyPair.RSAParameters);

			return p;
		}

		/// <summary>
		/// Decrypts a <see cref="byte"/> array.
		/// </summary>
		/// <param name="decrypt"><see cref="byte"/> array to decrypt.</param>
		/// <returns>A decrypted <see cref="byte"/> array or null.</returns>
		public byte[] Decrypt(byte[] decrypt)
		{
			if (keyPair.isOnlyPublic)
				throw new InvalidOperationException("Cannot decrypt with only public key.");

			return provider.Value.Decrypt(decrypt, true);
		}

		/// <summary>
		/// Decrypts a <see cref="byte"/> array to a <see cref="string"/>.
		/// </summary>
		/// <param name="decrypt"><see cref="byte"/> array to decrypt.</param>
		/// <returns>A decrypted <see cref="string"/> or null.</returns>
		public string DecryptToString(byte[] decrypt)
		{
			if (keyPair.isOnlyPublic)
				throw new InvalidOperationException("Cannot decrypt with only public key.");

			return Encoding.Unicode.GetString(provider.Value.Decrypt(decrypt, true));
		}

		/// <summary>
		/// Encrypts a <see cref="string"/> to <see cref="byte"/>s.
		/// </summary>
		/// <param name="encrypt">String to encrypt</param>
		/// <returns>Encrypted <see cref="string"/> in the form of a <see cref="byte"/> array.</returns>
		public byte[] Encrypt(string encrypt)
		{
			return provider.Value.Encrypt(Encoding.Unicode.GetBytes(encrypt), true);
		}

		/// <summary>
		/// Encrypts a <see cref="byte"/> array.
		/// </summary>
		/// <param name="encrypt">Bytes to encrypt.</param>
		/// <returns>Encrypted <see cref="byte"/> array in the form of a <see cref="byte"/> array.</returns>
		public byte[] Encrypt(byte[] encrypt)
		{
			return provider.Value.Encrypt(encrypt, true);
		}

		public void Dispose()
		{
			if (provider.IsValueCreated)
			{
#if !DNXCORE50
				provider.Value.Clear();
#else
				provider.Value.Dispose();
#endif
			}	
		}
	}
}
