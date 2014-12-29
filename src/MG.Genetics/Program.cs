using System;
using AK.CmdLine;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using MG.Genetics.Model;

namespace MG.Genetics
{
    /// <summary>
    /// Provides CLI-utilities for working with 23andme genomes.
    /// </summary>
    public sealed class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            new CmdLineDriver(new Program(), Console.Out).TryProcess(args);
        }

        /// <summary>
        /// Compares to genomes and prints statistics and differences to stdout.
        /// </summary>
        /// <param name="path1">Path1.</param>
        /// <param name="path2">Path2.</param>
        /// <param name="verbose">Indicates whether or not raw data should be included.</param>
        public void Compare(string path1, string path2, bool verbose = false)
        {
            // Guard.IsNotNullOrWhiteSpace(path1, "path1");
            // Guard.IsNotNullOrWhiteSpace(path2, "path2");

            var genome1 = GenomeModel.Load(path1);
            var genome2 = GenomeModel.Load(path2);

            var name1 = Path.GetFileName(path1);
            var name2 = Path.GetFileName(path2);

            Console.WriteLine("Genome '{0}' contains {1} SNP's.", name1, genome1.Snp.Count);
            Console.WriteLine("Genome '{0}' contains {1} SNP's.", name2, genome1.Snp.Count);

            var common = genome1.Snp.Join(genome2.Snp,
                x => x.Id + x.Genotype,
                x => x.Id + x.Genotype,
                (x, y) => x).ToList();

            Console.WriteLine("{0} SNP are common to both genomes.", common.Count);
            if (verbose)
            {
                WriteSnp(common);
            }

            var unique = genome1.Snp.Except(genome2.Snp).ToList();
            Console.WriteLine("{0} SNP exist only in {1}.", unique.Count, name1);
            if (verbose)
            {
                WriteSnp(common);
            }
        }

        private static void WriteSnp(IEnumerable<SnpModel> snps)
        {
            Console.WriteLine();
            foreach (var snp in snps)
            {
                Console.WriteLine(@"{0} ({1})", snp.Id, snp.Genotype);
            }
            Console.WriteLine();
        }
    }
}
