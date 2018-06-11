using System;
using System.Collections.Generic;
using System.Linq;
using GASum;
using GASum.GA;
using GeneticLib.Generations;
using GeneticLib.GeneticManager;
using GeneticLib.GenomeFactory;
using GeneticLib.GenomeFactory.GenomeProducer;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding;
using GeneticLib.GenomeFactory.GenomeProducer.Breeding.Crossover;
using GeneticLib.GenomeFactory.GenomeProducer.Reinsertion;
using GeneticLib.GenomeFactory.GenomeProducer.Selection;
using GeneticLib.GenomeFactory.Mutation;
using GeneticLib.Randomness;

namespace GA_Sum
{
	class Program
    {
		int targetSum = 50000;
        
		int genomesCount = 50;
		int genesCount = 500;

		int minGeneValue = -1000;
		int maxGeneValue = 1000;

		float geneDeltaMutationPart = 0.1f;
		float geneNbMutationChance = 0.1f;

		float crossoverPart = 0.80f;
		float reinsertionPart = 0.2f;

		GeneticManagerClassic geneticManager;
		FitnessEvaluation fitnessEvaluation;

		public bool targetReached = false;
		public int maxIterations = 10000;

		static void Main(string[] args)
		{
			var program = new Program();

			for (int i = 0; i < program.maxIterations; i++)
			{
				Console.WriteLine(String.Format(
					"{0}) sum = {1}",
					i,
					program.CurrentBestSum()));
				
				if (program.targetReached)
					break;

				program.Evolve();
			}
		}

		public Program()
        {
			GARandomManager.Random = new RandomClassic((int)DateTime.Now.Ticks);
            
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
				crossoverPart,
				minProduction: 1,
				selection: selection,
				crossover: crossover,
				mutationManager: InitMutations());

			var reinsertion = new ReinsertionFromSelection(
				reinsertionPart,
				minProduction: 1,
				selection: new EliteSelection());

			var producers = new List<IGenomeProducer>
			{
				reinsertion,
				breeding
			};

			var genomeForge = new GenomeForge(producers);
            
			geneticManager = new GeneticManagerClassic(
				generationManager,
				initialGenerationGenerator,
				genomeForge,
				genomesCount
			);
			geneticManager.Init();

			fitnessEvaluation = new FitnessEvaluation();         
        }

		public void Evolve()
		{
			var genomes = geneticManager.GenerationManager.CurrentGeneration.Genomes;

			foreach (var genome in genomes)
				genome.Fitness = fitnessEvaluation.Evaluate(targetSum, genome);

			var orderedGenomes = genomes.OrderByDescending(g => g.Fitness)
										.ToArray();

			geneticManager.GenerationManager
			              .CurrentGeneration
			              .Genomes = orderedGenomes;

			if (Math.Abs(orderedGenomes.First().Fitness - targetSum) < 0.1)
			{
				targetReached = true;
			}

			geneticManager.Evolve();
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

		public int CurrentBestSum()
		{
			return geneticManager.GenerationManager
				                 .CurrentGeneration
				                 .Genomes
				                 .First()
				                 .Genes
				                 .Sum(g => (g.Value as ClonableInt).Value);
		}
    }
}
