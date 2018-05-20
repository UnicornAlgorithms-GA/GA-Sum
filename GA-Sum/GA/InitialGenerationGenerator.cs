using System;
using System.Collections.Generic;
using System.Linq;
using GeneticLib.Generations.InitialGeneration;
using GeneticLib.Genome;
using GeneticLib.Randomness;

namespace GASum.GA
{
	public class GASumInitialGenerationGenerator : InitialGenerationCreatorBase
    {
		public int GeneCount { get; set; }
		public int MinValue { get; set; }
		public int MaxValue { get; set; }
      
		protected override IGenome NewRandomGenome()
		{
			var genes = Enumerable.Range(0, GeneCount)
								  .Select(i =>
								  {
									  var val = GARandomManager.Random.Next(MinValue, MaxValue);
									  return new Gene(new ClonableInt(val));
								  }).ToArray();

			var genome = new NumberGenome
			{
				Genes = genes
			};

			return genome;
		}
	}
}
