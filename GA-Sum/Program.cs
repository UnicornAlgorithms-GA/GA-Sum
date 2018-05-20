using System;
using System.Collections.Generic;
using GASum.GA;
using GeneticLib.Generations;
using GeneticLib.Genome;
using GeneticLib.GenomeFactory;
using GeneticLib.GenomeFactory.GenomeProducer;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding.Selection;
using GeneticLib.GenomeFactory.GenomeProducer.Reinsertion;
using GeneticLib.GenomeFactory.Mutation;
using GeneticLib.Randomness;

namespace GA_Sum
{
    class Program
    {
		int targetSum = 200;

		int genomesCount = 50;
		int genesCount = 5;

		int minGeneValue = 0;
		int maxGeneValue = 400;

		float geneDeltaMutationPart = 0.1f;
		float geneNbMutationChance = 0.1f;

		static void Main(string[] args)
		{
			
		}

		public Program()
        {
			GARandomManager.Random = new Random(1);
            
			var generationManager = new GenerationManagerKeepLast();

			var initialGenerationGenerator = new GASumInitialGenerationGenerator
            {
                GeneCount = genesCount,
                MinValue = minGeneValue,
                MaxValue = maxGeneValue
            };

			var selection = new EliteSelection();
			var crossover = new OnePointCrossover(useBothChildren: true);

			var breeding = new BreedingClassic(
				selection,
				crossover,
				InitMutations());

            //var reinsertion =

			var producers = new List<IGenomeProducer>
			{
				
			};

			var genomeForge = new GenomeForge();


			//var geneticManager = new SumGeneticManager(
			//	generationManager,
			//	initialGenerationGenerator,
			//);
        }

		private MutationManager InitMutations()
		{
			var mutation = new NbMutation((int)(geneDeltaMutationPart * maxGeneValue));
			var mutManager = new MutationManager();
			mutManager.MutationEntries.Add(new MutationEntry(
				mutation,
				geneNbMutationChance,
				EMutationType.Independent));
			
			return mutManager;
		}
    }
}
