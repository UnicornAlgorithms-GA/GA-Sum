using System;

namespace GASum
{
	public class ClonableInt : ICloneable
	{
		public int Value { get; set; }

		public ClonableInt(int val)
		{
			Value = val;
		}

		public object Clone()
		{
			return new ClonableInt(Value);
		}
	}
}
