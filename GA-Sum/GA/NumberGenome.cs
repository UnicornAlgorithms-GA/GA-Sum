using System;
using System.Linq;
using GeneticLib.Genome;

namespace GASum.GA
{
	public class NumberGenome : GenomeBase
	{
		public override object Clone()
		{
			var result = new NumberGenome
            {
                Fitness = this.Fitness,
                Genes = this.Genes
                            .Select(g => new Gene(g))
                            .ToArray()
            };
            return result;
		}

		public override IGenome CreateNew(Gene[] genes)
		{
			var result = new NumberGenome
			{
				Genes = genes.Select(g => new Gene(g))
				             .ToArray()
			};
			return result;
		}
	}
}
