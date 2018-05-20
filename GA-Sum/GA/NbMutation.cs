using System;
using GeneticLib.Genome;
using GeneticLib.GenomeFactory.Mutation;
using GeneticLib.Randomness;

namespace GASum.GA
{
	/// <summary>
    /// Randomly choose one gene and change its value a bit.
    /// </summary>
	public class NbMutation : MutationBase
    {
		public int DeltaMutation { get; set; }

		public NbMutation(int deltaMutation)
        {
			DeltaMutation = deltaMutation;
        }

		protected override void DoMutation(IGenome genome)
		{
			var index = GARandomManager.Random.Next(0, genome.Genes.Length);
			var targetGene = genome.Genes[index];
			var clInt = targetGene.Value as ClonableInt;
			var newValue = clInt.Value + GARandomManager.Random
			                                            .Next(-DeltaMutation, DeltaMutation);

			genome.Genes[index] = new Gene(new ClonableInt(newValue));
		}
	}
}
