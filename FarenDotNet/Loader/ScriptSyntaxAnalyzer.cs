using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;

using Paraiba.Core;
using System.Windows.Forms;

namespace FarenDotNet.Loader
{
	public class ScriptSyntaxAnalyzer
	{
		/* * * * * * * * * * * * * * * * * * * * * * * * * * *
		 * S -> E { So E }
		 * E -> N { Eo N }
		 * N -> TMP | TBL | VAL | UNO | FNC
		 * 
		 * So -> "&&" | "||"
		 * Eo -> "==" | "!=" | "<" | "<=" | ">=" | ">"
		 * 
		 * TMP -> /[a-z][a-zA-Z0-9]{0,}/
		 * TBL -> TableKeyword "[" S "]"
		 * VAL -> ValueKeyword
		 * UNO -> "%" /\w+/
		 * FNC -> Function
		 * 
		 * * * * * * * * * * * * * * * * * * * * * * * * * * */
		private static readonly Command[] COMMANDS =
		{
			new Command("AreaFocus", ArgType.Int),
			new Command("CharDelete", ArgType.Int),
			new Command("CharSet", ArgType.Int, ArgType.Int),
			new Command("Clear"),
			new Command("Conversation", ArgType.Int),
			new Command("DeleteFill", ArgType.Int, ArgType.Int, ArgType.Int, ArgType.Int),
			new Command("End"),
			new Command("FaceOut", ArgType.Int),
			new Command("GoTo"),
			new Command("Music", ArgType.String),
			new Command("OutPrint", ArgType.Int, ArgType.Int, ArgType.String),
			new Command("Picture", ArgType.String, ArgType.Int, ArgType.Int),
			new Command("Plus", ArgType.Int, ArgType.Int),
			new Command("Print", ArgType.String),
			new Command("PrintKz", ArgType.Int),
			new Command("Set", ArgType.Int, ArgType.Int),
			new Command("Size", ArgType.Int, ArgType.Int),
			new Command("Start"),
			new Command("Window", ArgType.Int),
			new Command("WindowCenter"),
			new Command("WindowOff"),
			new Command("WindowOn", ArgType.Int),
			new Command("if", ArgType.Int),
			new Command("{"),
			new Command("}"),
		};
		private static readonly Command[] FUNCTIONS =
		{
			new Command("YesNo", ArgType.String),
			new Command("League", ArgType.Int, ArgType.Int),
			new Command("AreaButaiKz", ArgType.Int),
			new Command("AreaCity", ArgType.Int),
			new Command("AreaKuni", ArgType.Int),
			new Command("AreaKz", ArgType.Int),
			new Command("AreaRoad", ArgType.Int),
			new Command("AreaWall", ArgType.Int),
			new Command("BaseArea", ArgType.Int),
			new Command("Flag", ArgType.Int),
			new Command("HeroKuni", ArgType.Int),
			new Command("HeroFlag", ArgType.Int),
			new Command("KuniFlag", ArgType.Int),
			new Command("KuniPlayer", ArgType.Int),
			new Command("Ley", ArgType.Int),
			new Command("MostWin", ArgType.Int),
			new Command("Random", ArgType.Int),
			new Command("WinKz", ArgType.Int),
			new Command("Scenario"),
			new Command("StartTurn"),
			new Command("Turn"),
			new Command("true"),
			new Command("false"),
		};

		ScriptLexicalAnalyzer _lex;

		public ScriptSyntaxAnalyzer(ScriptLexicalAnalyzer lex)
		{
			_lex = lex;
		}

		public IEnumerable<ScriptSyntaxAnalyzer.Tree> GetTrees()
		{
			foreach (var line in _lex.GetTokens())
			{
				if (line.Count == 0)
					continue;
				Tree result = null;
				try
				{
					try
					{
						Command cm = COMMANDS.Single(c => c.Name == line[0]);
						Tree[] args = new Tree[cm.Args.Length];
						int index = 1;
						for (int i = 0; i < args.Length; i++)
						{
							switch (cm.Args[i])
							{
							case ArgType.String:
								args[i] = ReadString(line, ref index);
								break;
							case ArgType.Int:
								args[i] = ReadValue(line, ref index);
								break;
							}
						}
						result = new Tree(line[0], null, args);
					} catch (InvalidOperationException e)
					{
						ThrowWrongToken(line[0], e);
					}
				} catch (ArgumentException e)
				{
					MessageBox.Show(e.Message, "構文エラー");
				}
				if (result != null)
					yield return result;
			}
		}

		private Tree ReadString(IList<string> line, ref int index)
		{
			return new Tree(line[index++], typeof(string));
		}

		// S
		private Tree ReadValue(IList<string> line, ref int index)
		{
			Tree tree = ReadExp(line, ref index);
			while (index < line.Count)
			{
				string next = line[index];
				if (next == "&&" || next == "||")
				{
					index++;
					tree = new Tree(next, typeof(bool), tree, ReadExp(line, ref index));
				}
				else
					break;
			}
			return tree;
		}

		// E
		private Tree ReadExp(IList<string> line, ref int index)
		{
			string[] table = { "==", "!=", "<", ">", "<=", ">=" };
			Tree tree = ReadNum(line, ref index);
			if (index < line.Count && table.Contains(line[index]))
			{
				string next = line[index++];
				tree = new Tree(next, typeof(bool), tree, ReadNum(line, ref index));
			}
			return tree;
		}

		private Tree ReadNum(IList<string> line, ref int index)
		{
			var word = line[index++];

			// 数値
			if (word[0] == '-' || ('0' <= word[0] && word[0] <= '9'))
			{
				int tmp;
				if (Int32.TryParse(word, out tmp))
					return new Tree(word, typeof(int));
			}

			// 一時変数
			if (word.Length == 1 && 'a' <= word[0] && word[0] <= 'z')
				return new Tree(word, typeof(int));

			// UnitID
			if (word[0] == '%')
				return new Tree(word, typeof(int));

			try
			{
				Command func = FUNCTIONS.Single(f => f.Name == word);
				Tree[] args = new Tree[func.Args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					switch (func.Args[i])
					{
					case ArgType.Int:
						args[i] = ReadNum(line, ref index);
						break;
					case ArgType.String:
						args[i] = ReadString(line, ref index);
						break;
					}
				}
				return new Tree(word, typeof(int), args);
			}
			catch(InvalidOperationException e) {
				ThrowWrongToken(word, e);
				return new Tree("0", typeof(int));
			}
		}

		[DebuggerStepThrough]
		private void ThrowWrongToken(string token, Exception e)
		{
			if (e == null)
				throw new ArgumentException(
					"正しくない単語が出現しました", token);
			else
				throw new ArgumentException(
					"正しくない単語が出現しました", token, e);

			//System.Windows.Forms.MessageBox.Show(
			//    String.Format("{0:N0}行目で正しくない単語「{1}」が出現しました。",
			//    _lex.LineNumber,
			//    token));
		}

		#region Inner

		//[DebuggerDisplay("{ToString()}")]
		public class Tree
		{
			public readonly string Value;
			public readonly Type Type;
			public readonly Tree[] Args;

			public Tree(string value, Type type, params Tree[] args)
			{
				Value = value;
				this.Type = type;
				Args = args;
			}

			public override string ToString()
			{
				if (this.Type == null)
					return Value + "(" + Args.JoinString(", ") + ")";

				if (this.Type == typeof(bool))
					return Args[0].ToString() + " " + Value + " " + Args[1].ToString();

				if (this.Type == typeof(string))
					return '"' + Value + '"';

				if (this.Args.Length > 0)
					return this.Value + "[" + Args.JoinString(", ") + "]";

				else
					return this.Value;
			}
		}

		private class Command
		{
			public readonly string Name;
			public readonly ArgType[] Args;
			public Command(string name, params ArgType[] args)
			{
				this.Name = name;
				this.Args = args;
			}
		}

		private enum ArgType
		{
			String, Int
		}

		#endregion
	}

	
}
