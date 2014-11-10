using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MG.Genetics.Model
{
    public class GenomeModel
    {
        private readonly IReadOnlyList<SnpModel> _snp;

        public GenomeModel(IEnumerable<SnpModel> snp)
        {
            _snp = snp.ToList();
        }

        public IReadOnlyList<SnpModel> Snp
        {
            get { return _snp; }
        }

        public static GenomeModel Load(string path)
        {
            // Guard.IsNotNullOrWhiteSpace(path, "path");

            var snp =File.ReadLines(path)
                .Where(x => !x.StartsWith("#"))
                .Select(x =>
                {
                    var fields = x.Split('\t');
                    return new SnpModel(
                        fields[0],
                        Int32.Parse(fields[1]),
                        Int32.Parse(fields[2]),
                        fields[3]);
                });
            return new GenomeModel(snp);
        }
    }
}

