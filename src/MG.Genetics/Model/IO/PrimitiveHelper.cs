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
using System.Linq;

namespace MG.Genetics.Model.IO
{
    internal static class PrimitiveHelper
    {
        public static GenomeModel Load(string path)
        {
            return new GenomeModel(ReadSnp(path));
        }

        public static Chromosome ParseChromosome(string s)
        {
            switch (s)
            {
                case "1":
                    return Chromosome.One;
                case "2":
                    return Chromosome.Two;
                case "3":
                    return Chromosome.Three;
                case "4":
                    return Chromosome.Four;
                case "5":
                    return Chromosome.Five;
                case "6":
                    return Chromosome.Six;
                case "7":
                    return Chromosome.Seven;
                case "8":
                    return Chromosome.Eight;
                case "9":
                    return Chromosome.Nine;
                case "10":
                    return Chromosome.Ten;
                case "11":
                    return Chromosome.Eleven;
                case "12":
                    return Chromosome.Twelth;
                case "13":
                    return Chromosome.Thirteen;
                case "14":
                    return Chromosome.Fourteen;
                case "15":
                    return Chromosome.Fifteen;
                case "16":
                    return Chromosome.Sixteen;
                case "17":
                    return Chromosome.Seventeen;
                case "18":
                    return Chromosome.Eighteen;
                case "19":
                    return Chromosome.Nineteen;
                case "20":
                    return Chromosome.Twenty;
                case "21":
                    return Chromosome.TwentyOne;
                case "22":
                    return Chromosome.TwentyTwo;
                case "X":
                    return Chromosome.X;
                case "Y":
                    return Chromosome.Y;
                case "MT":
                    return Chromosome.Mt;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static IEnumerable<SnpModel> ReadSnp(string path)
        {
            return File.ReadLines(path)
                       .Where(ShouldReadLine)
                       .Select(x =>
                       {
                           var fields = x.Split('\t');
                           return new SnpModel(
                               fields[0],
                               ParseChromosome(fields[1]),
                               Int32.Parse(fields[2]),
                               fields[3]);
                       });
        }

        private static bool ShouldReadLine(string line)
        {
            return !line.StartsWith("#") && !String.IsNullOrWhiteSpace(line);
        }
    }
}