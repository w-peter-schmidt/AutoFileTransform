

using System.Reflection;

namespace WPS.AutoFileTransform;
class Program
{
	private static readonly Dictionary<string, MethodInfo> argMethods = typeof(ArgMethods).GetMethods().ToDictionary(m => m.Name.ToLower(), m => m);
	private static readonly List<TransformationInfo> transformationInfos = new();

	static void Main(string[] args)
	{
		try
		{
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}
	}

	static void ParseArguments(string[] args, ref int index)
	{
		string arg = args[index];
		if (!argMethods.TryGetValue(arg.ToLower(), out MethodInfo? method))
		{
			method = typeof(ArgMethods).GetMethod(nameof(ArgMethods.Default), BindingFlags.Static);
		}

		var parameterInfos = method.GetParameters();
		int parameterCount = parameterInfos.Length;
		var parameters = args.Skip(index).Take(parameterCount)
			.Select((a, i) => parameterInfos[i].ParameterType == typeof(string) ?
			a :
			Convert.ChangeType(a, parameterInfos[i].ParameterType))
			.ToArray();

		method.Invoke(null, parameters);

		index += parameterCount;
	}

	static class ArgMethods
	{
		public static void Default(string sourceDir, string sourcePattern, bool sourceSubdir, string targetDir, string targetPattern, bool targetSubdir)
		{
			replaceCurrentDir(ref sourceDir);
			replaceCurrentDir(ref targetDir);
			replacePattern(ref sourcePattern);
			replacePattern(ref targetPattern);



			static void replacePattern(ref string dir)
			{
				if (String.IsNullOrWhiteSpace(dir))
				{
					dir = ".+";
				}
			}

			static void replaceCurrentDir(ref string dir)
			{
				if (dir.StartsWith("."))
				{
					dir = Environment.CurrentDirectory + dir.TrimStart('.');
				}
				else if (String.IsNullOrWhiteSpace(dir))
				{
					dir = Environment.CurrentDirectory;
				}
			}
		}
	}
}