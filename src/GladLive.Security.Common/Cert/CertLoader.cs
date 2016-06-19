using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GladLive.Security.Common
{
	/// <summary>
	/// Simplifies loading of <see cref="X509Certificate2"/> certificates from
	/// files.
	/// </summary>
	public class CertLoader
	{
		/// <summary>
		/// Loaded <see cref="X509Certificate2"/> cert.
		/// If loading failed it will be null.
		/// </summary>
		public X509Certificate2 Certificate { get; }

		/// <summary>
		/// Creates a certificate loader that searches for the cert file with a given file path.
		/// </summary>
		/// <param name="certPath">File path of the cert including file name and extension.</param>
		public CertLoader(string certPath)
		{
			byte[] certBytes = null;

			//TODO: Create cert loading strategies to look in resources or files and etc.
			try
			{
				using (Stream inputStream = new FileStream(certPath, FileMode.Open))
				{
					certBytes = new byte[inputStream.Length];
					inputStream.Read(certBytes, 0, (int)inputStream.Length);
				}
			}
			catch (Exception e)
			{
				throw new Exception($"Failed to load cert at path: {certPath}.", e);
			}

			//reaching this point should indicate that we've properly loaded the cert.
			if (certBytes != null)
				Certificate = new X509Certificate2(certBytes, "", X509KeyStorageFlags.Exportable);
			else
				throw new Exception($"Failed to parse cert at path: {certPath}.");
		}
	}
}
