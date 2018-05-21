using System;
using System.Linq;
using GeneticLib.Genome;

namespace GASum.GA
{
    public class FitnessEvaluation
    {
        public FitnessEvaluation()
        {
        }

		public float Evaluate(
			int targetSum,
			IGenome genome)
		{
			var sum = genome.Genes.Sum(g => (g.Value as ClonableInt).Value);
			var delta = Math.Abs(targetSum - sum);

			return targetSum - delta;
		}
    }
}
