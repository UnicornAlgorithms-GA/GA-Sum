using System;
using System.Linq;
using GeneticLib.Genome;
using GeneticLib.Genome.Genes;

namespace GASum.GA
{
	public class NumberGenome : GenomeBase
	{         
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
