using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FarenDotNet.Loader
{
	internal class SettingFileReader : IEnumerable<KeyValuePair<string, List<string>>>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		private Dictionary<string, List<string>> _dic;

		public SettingFileReader(string filePath)
		{
			_dic = new Dictionary<string, List<string>>();

			using (var reader = new StreamReader(filePath, Global.Encoding))
			{
				string line;
				List<string> lines = null;

				while ((line = reader.ReadLine()) != null)
				{
					var key = line.Trim();
					if (key.Length == 0)
						continue;

					// [Someting] の読み取り
					if (key[0] == '[' && key[key.Length - 1] == ']')
					{
						key = key.Substring(1, key.Length - 2);
						lines = new List<string>();
						_dic[key] = lines;
					}
						// 通常の行を読み取り
					else
						lines.Add(line);
				}
			} // file close
		} // end ctor

		public IList<string> this[string key]
		{
			get { return _dic[key]; }
		}

		public bool HasKey(string key)
		{
			return _dic.ContainsKey(key);
		}

		#region IEnumerable メンバ

		public IEnumerator<KeyValuePair<string, List<string>>> GetEnumerator()
		{
			return _dic.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _dic.GetEnumerator();
		}

		#endregion
	}

	public static class StringExtensionMethods // TODO: public の除去
	{
		public static string[] SplitSpace(this string str)
		{
			return str.Split(
				new[] { ' ', '\t' },
				StringSplitOptions.RemoveEmptyEntries);
		}

		public static string[] SplitSpace(this string str, int max)
		{
			return str.Split(
				new[] { ' ', '\t' },
				max,
				StringSplitOptions.RemoveEmptyEntries);
		}
	}
}