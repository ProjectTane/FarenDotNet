using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace FarenDotNet.Loader
{
	public class ScriptLexicalAnalyzer
	{
		private string _filePath;
		private int _count;

		public int LineNumber { get { return _count; } }
		public string FilePath { get { return _filePath; } }

		public ScriptLexicalAnalyzer(string filePath)
		{
			this._filePath = filePath;
		}

		public IEnumerable<List<string>> GetTokens()
		{
			using (var reader = new StreamReader(_filePath, Global.Encoding))
			{
				_count = 0;
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					_count++;
					char[] array = line.ToCharArray();
					yield return SplitToken(array);
				}
			}
		}

		public List<string> SplitToken(char[] line)
		{
			int index = 0;
			var argList = new List<string>();
			while (index < line.Length)
			{
				int start = index;
				char c = line[index++];
				switch (c)
				{
				case ' ':
				case '\t':
				case ',':
				case '[':
				case ']':
				case '(':
				case ')':
					continue;
				case '/':
					if (index < line.Length && line[index] == '/')
						goto END_WHILE;
					goto default;
				case '>':
				case '<':
					// "="があっても無くてもよいタイプ
					if (index < line.Length && line[index] == '=')
						index++;
					break;
				case '=':
				case '!':
					// '='必須
					AcceptChar('=', line, ref index);
					break;
				case '&':
					AcceptChar('&', line, ref index);
					break;
				case '|':
					AcceptChar('|', line, ref index);
					break;
				case '%':
					while (index < line.Length &&
						!IsScriptChar(line[index])) index++;
					break;
				default:
					if (!IsScriptChar(c))
						// 文字列とみなして。
						while (index < line.Length &&
							!IsScriptChar(line[index])) index++;
					else
						ThrowException(line, index);
					break;
					
				}
				argList.Add(new String(line, start, index - start));
				continue;
			} END_WHILE:
			return argList;
		}

		#region Util

		private static void SkipSpace(char[] line, ref int index)
		{
			while (line[index] == ' ' || line[index] == '\t') index++;
		}

		private void AcceptChar(char c, char[] line, ref int index)
		{
			if (index < line.Length && line[index++] != c)
				ThrowException(line, index);
		}

		[DebuggerStepThrough]
		private void ThrowException(char[] line, int index)
		{
			throw new DataFormatException(String.Format(
				"{0}の{1}行目{2}文字目で想定外の記「{3}({4})」が出現しました。",
				_filePath, _count, index, line[index], (int)line[index]
				), _filePath);
		}

		private static bool IsScriptChar(char c)
		{
			switch (c)
			{
			case ' ':
			case '\t':
			case '/':
			case '>':
			case '<':
			case '=':
			case '!':
			case '&':
			case '|':
			case '[':
			case ']':
			case '(':
			case ')':
			case ',':
			case '%':
				return true;
			}
			return Char.IsControl(c);
		}

		#endregion
	}
}
