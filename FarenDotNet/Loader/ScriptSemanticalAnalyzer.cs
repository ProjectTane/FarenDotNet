
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Paraiba.Core;
using FarenDotNet.BasicData;
using Tree = FarenDotNet.Loader.ScriptSyntaxAnalyzer.Tree;

namespace FarenDotNet.Loader
{
	public class ScriptSemanticalAnalyzer
	{
		ScriptLexicalAnalyzer _lex;
		ScriptSyntaxAnalyzer _syn;
		IList<UnitData> _units;

		IEnumerator<Tree> _e;
		StringBuilder _b;
		bool[] _varSet;
		int _ifCounter;
		int _gotoCounter;
		int _line;

		public ScriptSemanticalAnalyzer(
			ScriptLexicalAnalyzer lex,
			ScriptSyntaxAnalyzer syn,
			IList<UnitData> units)
		{
			Contract.Requires(lex != null, "lex");
			Contract.Requires(syn != null, "syn");
			Contract.Requires(units != null, "units");

			_lex = lex;
			_syn = syn;
			_units = units;
		}

		public string ToSource()
		{
			_e = _syn.GetTrees().GetEnumerator();
			_b = new StringBuilder();
			_varSet = new bool['z' - 'a' + 1];
			_ifCounter = 0;
			_gotoCounter = 0;
			_line = 0;

			if (_e.MoveNext())
				WriteLines();

			if (_e.MoveNext())
				Throw();

			return HEADER
				+ SetToString()
				+ HEADER2
				+ _b.ToString()
				+ FOOTER;
		}

		private string SetToString()
		{
			var b = new StringBuilder();
			for (int i = 0; i < _varSet.Length; i++)
				if (_varSet[i])
					b.AppendLine(T3 + "int " + (char)('a' + i) + " = 0;");
			return b.ToString();
		}

		private void WriteLines()
		{
			bool skip;
			do
			{
				_line++;
				if (_line != _lex.LineNumber)
				{
					_line = _lex.LineNumber;
					//_b.AppendLine(String.Format(
					//    @"#line {0} ""{1}""", _line, _lex.FilePath));
				}
				skip = false;
				Tree t = _e.Current;
				switch (t.Value)
				{
				case "if":
					int counter = _ifCounter;
					_ifCounter++;

					Tree tExp = t.Args[0];
					string exp;
					if (tExp.Args.Length > 1)
						exp = "!(" + GetString(tExp) + ")";
					else
						exp = GetString(tExp) + " == 0";

					_b.AppendLine(T4 + "if (" + exp + ")");
					_b.AppendLine(T5 + "goto ENDIF" + counter + ";");
					_line++;
					if (_e.MoveNext() && _e.Current.Value == "{" && _e.MoveNext())
					{
						WriteLines();
						Debug.Assert(_e.Current.Value == "}");
					}
					else
						skip = true;
					_b.AppendLine(T3 + "ENDIF" + counter + ":");
					break;
				case "}":
					return;
				case "Start":
					_gotoCounter++;
					int gCounter = _gotoCounter;
					_b.AppendLine(T4 + "start = " + gCounter + ";");
					_b.AppendLine(T4 + "goto case " + gCounter + ";");
					_b.AppendLine(T3 + "case " + gCounter + ":");
					break;

				case "GoTo":
					_b.AppendLine(T4 + "goto START;");
					break;

				case "WindowOn":
					_b.AppendLine(T4 + "yield return " + GetString(t.Args[0]) + ";");
					break;

				case "End":
					_b.AppendLine(T4 + "yield break;");
					break;

				case "Set":
					_b.AppendLine(T4 + GetString(t.Args[0]) + " = " + GetString(t.Args[1]) + ";");
					break;

				case "Plus":
					_b.AppendLine(T4 + GetString(t.Args[0]) + " += " + GetString(t.Args[1]) + ";");
					break;

				default:
					_b.Append(T4 + "sc." + t.Value + "(");
					for (int i = 0; i < t.Args.Length; i++)
					{
						if (i != 0)
							_b.Append(", ");
						_b.Append(GetString(t.Args[i]));
					}
					_b.AppendLine(");");
					break;
				}
			} while (skip || _e.MoveNext());
		}

		private string GetString(Tree t) { return GetString(t, t.Type); }

		private string GetString(Tree t, Type type)
		{
			if (t.Type == typeof(bool))
			{
				string result = "";
				switch (t.Value)
				{
				case "&&":
				case "||":
					result = GetString(t.Args[0], typeof(bool))
						+ " " + t.Value + " "
						+ GetString(t.Args[1], typeof(bool));
					break;
				case "==":
				case "!=":
				case ">=":
				case "<=":
				case "<":
				case ">":
					result = GetString(t.Args[0], typeof(int))
						+ " " + t.Value + " "
						+ GetString(t.Args[1], typeof(int));
					break;
				default:
					Debug.Assert(false);
					break;
				}
				if (type == typeof(bool))
					return result;
				else
				{
					Debug.Assert(type == typeof(int));
					return result + " ? 1 : 0";
				}
			}
			char c = t.Value[0];
			if (t.Type == typeof(int))
			{
				if (c == '-' || '0' <= c && c <= '9')
				{
					if (type == typeof(int))
						return t.Value;
					else
						return t.Value + " != 0";
				}
				if ('a' <= c && c <= 'z')
				{
					if (t.Value.Length == 1)
					{
						_varSet[c - 'a'] = true;
						if (type == typeof(int))
							return t.Value;
						else
							return t.Value + " != 0";
					}
					Debug.Assert(t.Value == "true" || t.Value == "false");
					if (type == typeof(int))
						return t.Value == "true" ? "1" : "0";
					else
						return t.Value;
				}
				if (c == '%')
				{
					int result = 0;
					char[] array = t.Value.ToCharArray();
					string id = new String(array, 1, array.Length - 1);
					for (int i = 0; i < _units.Count; i++)
					{
						if (_units[i].ID == id)
						{
							result = i + 1;
							break;
						}
					}
					if (type == typeof(int))
						return result.ToString();
					else
						return result > 0 ? "true" : "false";
				}
				else
				{
					string result;
					if (t.Args.Length == 0)
						result = "sc." + t.Value;
					else
						result = "sc." + t.Value + "[" + t.Args.Select(n => GetString(n)).JoinString(", ") + "]";
					if (type == typeof(int))
						return result;
					else
						return result + " != 0";
				}
			}
			if (t.Type == typeof(string))
				return '"' + t.Value + '"';
			else
				return null;
		}

		[DebuggerStepThrough]
		private void Throw()
		{
			throw new Exception();
		}

		#region ソース

		const string T2 = "\t\t";
		const string T3 = "\t\t\t";
		const string T4 = "\t\t\t\t";
		const string T5 = "\t\t\t\t\t";

		const string HEADER = @"
using FarenDotNet.Reign.UI;
using System.Collections.Generic;

namespace FarenDotNet.Reign
{
	public class EventScript
	{
		public static IEnumerable<int> Run(Scripter sc)
		{
";

		const string HEADER2 = @"
			int start = 0;
			int loopCount = 0;
		START:
			loopCount++;
			if (loopCount >= sc.LOOP_LIMIT)
			{
				sc.ShowMessage(
					""GoToを行った回数が"" + loopCount + ""に達したため、"" + 
					""スクリプトの実行を終了します。"");
			}
			switch (start)
			{
			case 0:
";

		const string FOOTER = @"break;
			}
		}
	}
}
";
		#endregion

		public class EnuWrap<T> : IEnumerable<T>
		{
			IEnumerable<T> _inner;
			public readonly List<T> Cache;

			public EnuWrap(IEnumerable<T> inner)
			{
				_inner = inner;
				Cache = new List<T>();
			}

			public IEnumerator<T> GetEnumerator()
			{
				foreach (var t in _inner)
				{
					Cache.Add(t);
					yield return t;
				}
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}
		}

	}
}
