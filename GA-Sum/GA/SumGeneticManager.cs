using System;
using GeneticLib.Generations;
using GeneticLib.Generations.InitialGeneration;
using GeneticLib.GeneticManager;
using GeneticLib.GenomeFactory;

namespace GASum.GA
{
	public class SumGeneticManager : GeneticManagerClassic
    {
		public SumGeneticManager(
			IGenerationManager generationManager,
            IInitialGenerationCreator initialGenerationCreator,
            GenomeForge genomeForge,
            int populationGenomesCount
		)
			: base(generationManager,
			       initialGenerationCreator,
			       genomeForge,
			       populationGenomesCount)
        {
        }
    }
}
