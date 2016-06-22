using System;
using System.Collections.Generic;
using System.Linq;

#if !NET35
using System.Threading.Tasks;
#endif

namespace GladLive.Security.Common
{
	/// <summary>
	/// Service for shared encryption and decryption algorithm for a given scheme
	/// </summary>
	public interface ICryptoService
	{
		/// <summary>
		/// Decrypts a <see cref="byte"/> array.
		/// </summary>
		/// <param name="decrypt"><see cref="byte"/> array to decrypt.</param>
		/// <returns>A decrypted <see cref="byte"/> array or null.</returns>
		byte[] Decrypt(byte[] decrypt);

		/// <summary>
		/// Decrypts a <see cref="byte"/> array to a <see cref="string"/>.
		/// </summary>
		/// <param name="decrypt"><see cref="byte"/> array to decrypt.</param>
		/// <returns>A decrypted <see cref="string"/> or null.</returns>
		string DecryptToString(byte[] decrypt);

		/// <summary>
		/// Encrypts a <see cref="byte"/> array.
		/// </summary>
		/// <param name="encrypt">Bytes to encrypt.</param>
		/// <returns>Encrypted <see cref="byte"/> array in the form of a <see cref="byte"/> array.</returns>
		byte[] Encrypt(byte[] encrypt);

		/// <summary>
		/// Encrypts a <see cref="string"/> to <see cref="byte"/>s.
		/// </summary>
		/// <param name="encrypt">String to encrypt</param>
		/// <returns>Encrypted <see cref="string"/> in the form of a <see cref="byte"/> array.</returns>
		byte[] Encrypt(string encrypt);
	}

#if !NET35
	public interface ICryptoServiceAsync
	{
		/// <summary>
		/// Asyncronously decrypts a <see cref="byte"/> array.
		/// </summary>
		/// <param name="decrypt"><see cref="byte"/> array to decrypt.</param>
		/// <returns>A decrypted <see cref="byte"/> array or null.</returns>
		Task<byte[]> DecryptAsync(byte[] decrypt);

		/// <summary>
		/// Asyncronously decrypts a <see cref="byte"/> array to a <see cref="string"/>.
		/// </summary>
		/// <param name="decrypt"><see cref="byte"/> array to decrypt.</param>
		/// <returns>A decrypted <see cref="string"/> or null.</returns>
		Task<string> DecryptToStringAsync(byte[] decrypt);

		/// <summary>
		/// Asyncronously encrypts a <see cref="byte"/> array.
		/// </summary>
		/// <param name="encrypt">Bytes to encrypt.</param>
		/// <returns>Encrypted <see cref="byte"/> array in the form of a <see cref="byte"/> array.</returns>
		Task<byte[]> EncryptAsync(byte[] encrypt);

		/// <summary>
		/// Asyncronously encrypts a <see cref="string"/> to <see cref="byte"/>s.
		/// </summary>
		/// <param name="encrypt">String to encrypt</param>
		/// <returns>Encrypted <see cref="string"/> in the form of a <see cref="byte"/> array.</returns>
		Task<byte[]> EncryptAsync(string encrypt);
	}
#endif
}
