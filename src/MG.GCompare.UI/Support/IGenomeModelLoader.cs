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

using System.Threading.Tasks;
using MG.Genetics.Model;

namespace MG.GCompare.UI.Support
{
    /// <summary>
    /// Provides a mechanism for loading <see cref="GenomeModel"/> instances.
    /// </summary>
    public interface IGenomeModelLoader
    {
        /// <summary>
        /// Returns a task that asynchronously loads a <see cref="GenomeModel"/> instance from a file.
        /// </summary>
        /// <param name="path">The path to the file continaing 23andme raw data.</param>
        /// <returns>
        /// An instance of <see cref="GenomeModel"/> containing the genome provided at <paramref name="path"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        Task<GenomeModel> LoadAsync(string path);
    }
}