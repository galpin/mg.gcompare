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
using MG.Common;
using MG.Genetics.Model.IO;

namespace MG.Genetics.Model
{
    /// <summary>
    /// A model of the human genome. This class cannot be inherited.
    /// </summary>
    public class GenomeModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenomeModel"/> class.
        /// </summary>
        /// <param name="snp">The sequence of <see cref="SnpModel"/> instances that make up the genome.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="snp"/> is <see langword="null"/>.
        /// </exception>
        public GenomeModel(IEnumerable<SnpModel> snp)
        {
            Guard.IsNotNull("snp", nameof(snp));

            Snp = new SnpModelCollection(snp);
        }

        /// <summary>
        /// Gets the collection of <see cref="SnpModel"/> instances that make up the genome.
        /// </summary>
        public SnpModelCollection Snp { get; }

        /// <summary>
        /// Loads an instance of <see cref="GenomeModel"/> from a file containing 23andme raw data.
        /// </summary>
        /// <param name="path">The path to the file continaing 23andme raw data.</param>
        /// <returns>
        /// An instance of <see cref="GenomeModel"/> containing the genome provided at <paramref name="path"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        public static GenomeModel Load(string path)
        {
            Guard.IsNotNull(path, nameof(path));

            return PrimitiveHelper.Load(path);
        }
    }
}