using System;
using GeneticLib.Utils;

namespace GASum
{
	public class ClonableInt : IDeepClonable<ClonableInt>
	{
		public int Value { get; set; }

		public ClonableInt(int val)
		{
			Value = val;
		}

		public ClonableInt Clone()
		{
			return new ClonableInt(Value);
		}
   	}
}
