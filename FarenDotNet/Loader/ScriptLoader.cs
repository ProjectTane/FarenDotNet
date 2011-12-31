using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using Paraiba.Core;
using FarenDotNet.BasicData;
using FarenDotNet.Reign.UI;
using Microsoft.CSharp;
using Paraiba.Reflection;

namespace FarenDotNet.Loader
{
	public class ScriptLoader
	{
		// HACK : 作成するイベントのネームスペースは可変のほうがよい気がする
		public Func<Scripter, IEnumerable<int>> Load(string filepath, IList<UnitData> units)
		{
			Contract.Requires(filepath != null, "filepath");
//			Contract.Requires(File.Exists(filepath), "filepath");
			var lex = new ScriptLexicalAnalyzer(filepath);
			var syn = new ScriptSyntaxAnalyzer(lex);
			var sem = new ScriptSemanticalAnalyzer(lex, syn, units);

			string source = sem.ToSource();

			CodeDomProvider provider = new CSharpCodeProvider();
			CompilerParameters option = new CompilerParameters(new []{
				//"FarenDotNet.exe"
				Assembly.GetEntryAssembly().Location
			});
			option.GenerateInMemory = true;

			CompilerResults result =
				provider.CompileAssemblyFromSource(option, source);

			bool errFlag = false;
			foreach (CompilerError err in result.Errors)
			{
				if (!err.IsWarning)
				{
					errFlag = true;
					MessageBox.Show(err.ErrorText, "コンパイルエラー");
				}
			}
			//using (FileStream s = new FileStream(@"C:\Documents and Settings\Ground\デスクトップ\EventScript.cs", FileMode.Create))
			//using (TextWriter w = new StreamWriter(s))
			//{
			//    w.WriteLine(source);
			//}

			if (errFlag)
				return null;
			try
			{
				Assembly asm = result.CompiledAssembly;
				Type script = asm.GetType("FarenDotNet.Reign.EventScript");
				MethodInfo run = script.GetMethod("Run");

				return run.ToDelegate<Func<Scripter, IEnumerable<int>>>();

			} catch (NullReferenceException) { return null; }
		}
	}
}
