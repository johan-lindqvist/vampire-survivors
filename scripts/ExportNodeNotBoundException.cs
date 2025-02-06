using System;

namespace VampireSurvivors.scripts;

public class ExportNodeNotBoundException : Exception
{
	public ExportNodeNotBoundException()
	{
	}

	public ExportNodeNotBoundException(string message)
		: base(message)
	{
	}

	public ExportNodeNotBoundException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
