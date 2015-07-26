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
using System.Collections.Generic;
using System.IO;
using MG.Common;
using MG.Testing;
using Xunit;
using Xunit.Extensions;

namespace MG.Genetics.Model.IO
{
    public class PrimitiveHelperTests
    {
        [Theory]
        [ClassData(typeof(ParseChromosome_ReturnsCorrespondingChromosome_Data))]
        public void ParseChromosome_ReturnsCorrespondingChromosome_Test(Chromosome expected, string s)
        {
            Assert.Equal(expected, PrimitiveHelper.ParseChromosome(s));
        }

        [Fact]
        public void Load_CorrectlyLoadsRawData_Test()
        {
            using (var writer = new RawDataWriter(
                "rs12564807	1	734462	AA",
                "rs3131972	X	752721	GG",
                "i4000690	MT	16518	G"))
            {
                var genome = PrimitiveHelper.Load(writer.FullPath);

                Assert.Equal(3, genome.Snp.Count);
                AssertSnp(genome.Snp[0], "rs12564807", Chromosome.One, 734462, "AA");
                AssertSnp(genome.Snp[1], "rs3131972", Chromosome.X, 752721, "GG");
                AssertSnp(genome.Snp[2], "i4000690", Chromosome.Mt, 16518, "G");
            }
        }

        [Fact]
        public void Load_IgnoresComments_Test()
        {
            using (var writer = new RawDataWriter(
                "#rs12564807	1	734462	AA",
                "#rs3131972	X	752721	GG"))
            {
                var genome = PrimitiveHelper.Load(writer.FullPath);

                Assert.Empty(genome.Snp);
            }
        }

        [Fact]
        public void Load_IgnoresEmptyLines_Test()
        {
            using (var writer = new RawDataWriter(" "))
            {
                var genome = PrimitiveHelper.Load(writer.FullPath);

                Assert.Empty(genome.Snp);
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

        private sealed class ParseChromosome_ReturnsCorrespondingChromosome_Data : ClassDataProviderBase
        {
            public override IEnumerator<object[]> GetEnumerator()
            {
                yield return A(Chromosome.One, "1");
                yield return A(Chromosome.Two, "2");
                yield return A(Chromosome.Three, "3");
                yield return A(Chromosome.Four, "4");
                yield return A(Chromosome.Five, "5");
                yield return A(Chromosome.Six, "6");
                yield return A(Chromosome.Seven, "7");
                yield return A(Chromosome.Eight, "8");
                yield return A(Chromosome.Nine, "9");
                yield return A(Chromosome.Ten, "10");
                yield return A(Chromosome.Eleven, "11");
                yield return A(Chromosome.Twelth, "12");
                yield return A(Chromosome.Thirteen, "13");
                yield return A(Chromosome.Fourteen, "14");
                yield return A(Chromosome.Fifteen, "15");
                yield return A(Chromosome.Sixteen, "16");
                yield return A(Chromosome.Seventeen, "17");
                yield return A(Chromosome.Eighteen, "18");
                yield return A(Chromosome.Nineteen, "19");
                yield return A(Chromosome.Twenty, "20");
                yield return A(Chromosome.TwentyOne, "21");
                yield return A(Chromosome.TwentyTwo, "22");
                yield return A(Chromosome.X, "X");
                yield return A(Chromosome.Y, "Y");
                yield return A(Chromosome.Mt, "MT");
            }
        }
    }
}