namespace ZorgcentrumSln.Domein.Exceptions;


[Serializable]
public class PersoonException : Exception
{
	public PersoonException() { }
	public PersoonException(string message) : base(message) { }
	public PersoonException(string message, Exception inner) : base(message, inner) { }
	protected PersoonException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}