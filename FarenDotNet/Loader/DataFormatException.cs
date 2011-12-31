using System;

namespace FarenDotNet.Loader
{
	/// <summary>
	/// このクラスはファイルを読んだとき、
	/// ファイル内のデータが適切でないときに投げられる例外です。
	/// </summary>
	public class DataFormatException : Exception
	{
		private readonly string _fileName;

		public DataFormatException(string message, string fileName)
			: this(message, fileName, null)
		{
		}

		public DataFormatException(string message, string fileName, Exception innerException)
			: base(message, innerException)
		{
			_fileName = fileName;
		}

		/// <summary>
		/// 正しくないファイルの名前
		/// </summary>
		public string FileName
		{
			get { return _fileName; }
		}
	}
}