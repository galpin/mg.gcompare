// Copyright (c) Martin Galpin 2014.
//  
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//  
// You should have received a copy of the GNU Lesser General Public
// License along with this library. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;
using MG.Common;
using Xunit;

namespace MG.Genetics.Model
{
    public class GenomeModelTests
    {
        [Fact]
        public void Load_CorrectlyLoadsRawData_Test()
        {
            using (var writer = new RawDataWriter(
                "rs12564807	1	734462	AA",
                "rs3131972	X	752721	GG",
                "i4000690	MT	16518	G"))
            {
                var genome = GenomeModel.Load(writer.FullPath);

                Assert.Equal(3, genome.Snp.Count);
                AssertSnp(genome.Snp[0], "rs12564807", Chromosome.One, 734462, "AA");
                AssertSnp(genome.Snp[1], "rs3131972", Chromosome.X, 752721, "GG");
                AssertSnp(genome.Snp[2], "i4000690", Chromosome.Mt, 16518, "G");
            }
        }

        private static void AssertSnp(
            SnpModel snp,
            string expectedId,
            Chromosome expectedLocation,
            int expectedPosition,
            string expectedGenotype)
        {
            Assert.Equal(expectedId, snp.Id);
            Assert.Equal(expectedLocation, snp.Location);
            Assert.Equal(expectedPosition, snp.Position);
            Assert.Equal(expectedGenotype, snp.Genotype);
        }

        private sealed class RawDataWriter : DisposableBase
        {
            public RawDataWriter(params string[] lines)
            {
                FullPath = Path.GetTempFileName();
                File.WriteAllText(FullPath, String.Join(Environment.NewLine, lines));
            }

            public string FullPath { get; }

            protected override void DisposeOfManagedResources()
            {
                File.Delete(FullPath);
            }
        }
    }
}