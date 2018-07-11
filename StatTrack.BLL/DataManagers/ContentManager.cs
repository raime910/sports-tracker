using StatTrack.BLL.DataManagers.Settings;
using StatTrack.BLL.Repositories;
using StatTrack.BLL.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StatTrack.BLL.DataManagers
{
	public class ContentManager : StggManagerBase
	{

		#region Ctor

		public ContentManager(IRepositories repositories, IAppSettings appSettings, AppUserVm currentUser)
			: base(repositories, appSettings, currentUser)
		{
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Read all from the stream.
		/// </summary>
		/// <param name="stream">Input stream to read.</param>
		private static byte[] ReadBytes(Stream stream)
		{
			var buffer = new byte[16 * 1024];
			using (var ms = new MemoryStream())
			{
				int read;
				while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Saves file into the file system and return the save path.
		/// </summary>
		/// <param name="relativePath">Relative path to the file.</param>
		/// <param name="fileName">File name.</param>
		/// <param name="stream">File stream.</param>
		/// <param name="overwrite">Overwrite flag if the file exists.</param>
		public async Task<string> SaveFileAsync(string relativePath, string fileName, Stream stream, bool overwrite = true)
		{
			var fileExists = FileExists(relativePath, fileName);

			if (fileExists && !overwrite)
			{
				throw new Exception("File already exists.");
			}

			if (fileExists)
			{
				// delete the existing file...
				DeleteFile(relativePath, fileName);
			}

			var directory = string.Concat(
				AppSettings.BasePath, 
				AppSettings.ContentFolder.Replace("/", "\\"), 
				relativePath.Replace("/", "\\"));

			// create directory if it does not exist...
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}

			// build full path to file...
			var fullPath = Path.GetFullPath(Path.Combine(directory, fileName));

			// write the file into the disk...
			using (var fileStream = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
			{
				var content = ReadBytes(stream);
				await fileStream.WriteAsync(content, 0, content.Length);
			}

			return string.Concat(AppSettings.ContentFolder, relativePath, fileName);
		}

		/// <summary>
		/// Returns true if the file exists.
		/// </summary>
		/// <param name="relativePath">Relative path to the file.</param>
		/// <param name="fileName">File name.</param>
		public bool FileExists(string relativePath, string fileName)
		{
			if (string.IsNullOrEmpty(relativePath.Trim()))
			{
				throw new ArgumentException("Relative path cannot be null or empty.");
			}

			if (string.IsNullOrEmpty(fileName.Trim()))
			{
				throw new ArgumentException("File name cannot be null or empty.");
			}

			var directory = GetDirectory(relativePath);
			var fullPath = Path.GetFullPath(directory);
			return Directory.Exists(fullPath) && File.Exists(Path.Combine(fullPath, fileName));
		}

		/// <summary>
		/// Delete the file.
		/// </summary>
		/// <param name="relativePath">Relative path to the file.</param>
		/// <param name="fileName">File name.</param>
		public void DeleteFile(string relativePath, string fileName)
		{
			if (FileExists(relativePath, fileName))
			{
				var directory = GetDirectory(relativePath);
				var fullPath = Path.GetFullPath(directory);
				File.Delete(Path.Combine(fullPath, fileName));
			}
		}

		public string GetDirectory(string relativePath)
		{
			var directory = string.Concat(
				AppSettings.BasePath,
				AppSettings.ContentFolder.Replace("/", "\\"),
				relativePath.Replace("/", "\\"));

			return directory;
		}

		#endregion

	}
}
