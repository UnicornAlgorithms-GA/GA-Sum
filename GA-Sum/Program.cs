using System;
using System.Collections.Generic;
using System.Linq;
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
		int targetFitness = 200;

		int genomesCount = 50;
		int genesCount = 5;

		int minGeneValue = 0;
		int maxGeneValue = 400;

		float geneDeltaMutationPart = 0.1f;
		float geneNbMutationChance = 0.1f;

		float crossoverPart = 0.7f;
		float reinsertionPart = 0.3f;

		SumGeneticManager geneticManager;
		FitnessEvaluation fitnessEvaluation;

		public bool targetReached = false;
		public int maxIterations = 100;

		static void Main(string[] args)
		{
			var program = new Program();

			for (int i = 0; i < program.maxIterations; i++)
			{
				if (program.targetReached)
					break;

				program.Evolve();
			}
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
				crossoverPart,
				minProduction: 1,
				selection: selection,
				crossover: crossover,
				mutationManager: InitMutations());

			var reinsertion = new EliteReinsertion(
				reinsertionPart, 
				minProduction:1);

			var producers = new List<IGenomeProducer>
			{
				reinsertion,
				breeding
			};

			var genomeForge = new GenomeForge(producers);
            
			geneticManager = new SumGeneticManager(
				generationManager,
				initialGenerationGenerator,
				genomeForge,
				genomesCount
			);

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

			if (Math.Abs(orderedGenomes.First().Fitness - targetFitness) < 5)
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
    }
}
