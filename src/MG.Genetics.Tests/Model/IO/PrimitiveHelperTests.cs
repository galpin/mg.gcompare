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

using System.Collections.Generic;
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