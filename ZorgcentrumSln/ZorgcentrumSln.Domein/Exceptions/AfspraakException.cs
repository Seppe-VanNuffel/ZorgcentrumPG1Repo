namespace ZorgcentrumSln.Domein.Exceptions;


[Serializable]
public class AfspraakException : Exception
{
	public AfspraakException() { }
	public AfspraakException(string message) : base(message) { }
	public AfspraakException(string message, Exception inner) : base(message, inner) { }
	protected AfspraakException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) 
	{ }
}