using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
#if !NET35
using System.Threading.Tasks;
#endif

namespace GladLive.Security.Common
{
	/// <summary>
	/// Adapter for the .Net RSA implementation <see cref="RSACryptoServiceProvider"/>.
	/// </summary>
	public class RSACryptoProviderAdapter : ICryptoService, IDisposable
	{
#if !NET35
		/// <summary>
		/// Internal keypair used for decryption.
		/// </summary>
		private RSAKeyPair keyPair { get; }

		/// <summary>
		/// Service provider for .NET RSA implementation that used the <see cref="keyPair"/>.
		/// </summary>
		private Lazy<RSACryptoServiceProvider> provider;
#else
		/// <summary>
		/// Service provider for .NET RSA implementation that used the <see cref="keyPair"/>.
		/// </summary>
		private RSACryptoServiceProvider provider;
#endif

		/// <summary>
		/// Creates a new <see cref="RSACryptoProviderAdapter"/> service with the provided <see cref="RSAKeyPair"/>
		/// data.
		/// </summary>
		/// <param name="keys">RSA keys to use for the service.</param>
		public RSACryptoProviderAdapter(RSAKeyPair keys)
		{
			if (keys == null)
				throw new ArgumentNullException(nameof(keys), "Key pair cannot be null for " + nameof(RSACryptoProviderAdapter));

#if !NET35
			keyPair = keys;
			provider = new Lazy<RSACryptoServiceProvider>(CreateProvider, true);
#else
			provider = CreateProvider();
#endif
		}

		/// <summary>
		/// Creates a new <see cref="RSACryptoProviderAdapter"/> service with the provided <see cref="RSAKeyPair"/>
		/// data.
		/// </summary>
		/// <param name="keys">RSA keys to use for the service.</param>
		public RSACryptoProviderAdapter(RSACryptoServiceProvider cryptoProvider)
		{
			if (cryptoProvider == null)
				throw new ArgumentNullException(nameof(cryptoProvider), $"{cryptoProvider} cannot be null for " + nameof(RSACryptoProviderAdapter));

#if !NET35
			keyPair = new RSAKeyPair(cryptoProvider.ExportParameters(true));
			provider = new Lazy<RSACryptoServiceProvider>(() => cryptoProvider);
#else
			provider = CreateProvider();
#endif
		}

		private RSACryptoServiceProvider CreateProvider()
		{
			RSACryptoServiceProvider p = new RSACryptoServiceProvider();

#if !NET35
			p.ImportParameters(keyPair.RSAParameters);
#endif

			return p;
		}

		/// <summary>
		/// Decrypts a <see cref="byte"/> array.
		/// </summary>
		/// <param name="decrypt"><see cref="byte"/> array to decrypt.</param>
		/// <returns>A decrypted <see cref="byte"/> array or null.</returns>
		public byte[] Decrypt(byte[] decrypt)
		{
#if !NET35
			if (keyPair.isOnlyPublic)
#else
			if(provider.PublicOnly)
#endif
				throw new InvalidOperationException("Cannot decrypt with only public key.");

#if !NET35
			return provider.Value.Decrypt(decrypt, true);
#else
			return provider.Decrypt(decrypt, true);
#endif
		}

		/// <summary>
		/// Decrypts a <see cref="byte"/> array to a <see cref="string"/>.
		/// </summary>
		/// <param name="decrypt"><see cref="byte"/> array to decrypt.</param>
		/// <returns>A decrypted <see cref="string"/> or null.</returns>
		public string DecryptToString(byte[] decrypt)
		{
#if !NET35
			if (keyPair.isOnlyPublic)
#else
			if (provider.PublicOnly)
#endif
				throw new InvalidOperationException("Cannot decrypt with only public key.");

#if !NET35
			return Encoding.Unicode.GetString(provider.Value.Decrypt(decrypt, true));
#else
			return Encoding.Unicode.GetString(provider.Decrypt(decrypt, true));

#endif
		}

		/// <summary>
		/// Encrypts a <see cref="string"/> to <see cref="byte"/>s.
		/// </summary>
		/// <param name="encrypt">String to encrypt</param>
		/// <returns>Encrypted <see cref="string"/> in the form of a <see cref="byte"/> array.</returns>
		public byte[] Encrypt(string encrypt)
		{
#if !NET35
			return provider.Value.Encrypt(Encoding.Unicode.GetBytes(encrypt), true);
#else
			return provider.Encrypt(Encoding.Unicode.GetBytes(encrypt), true);
#endif
		}

		/// <summary>
		/// Encrypts a <see cref="byte"/> array.
		/// </summary>
		/// <param name="encrypt">Bytes to encrypt.</param>
		/// <returns>Encrypted <see cref="byte"/> array in the form of a <see cref="byte"/> array.</returns>
		public byte[] Encrypt(byte[] encrypt)
		{
#if !NET35
			return provider.Value.Encrypt(encrypt, true);
#else
			return provider.Encrypt(encrypt, true);

#endif
		}

		public void Dispose()
		{
#if !NET35
			if (provider.IsValueCreated)
			{
#if !NETSTANDARD1_6
				provider.Value.Clear();
#else
				provider.Value.Dispose();
#endif
			}
#else
			if (provider != null)
				provider.Clear();
#endif
		}
	}
}
