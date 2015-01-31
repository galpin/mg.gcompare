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

namespace MG.GCompare.UI.Comparison.Favourites
{
    /// <summary>
    /// Represents a mechanism managing whether or not SNP's are user-favourites.
    /// </summary>
    public interface IFavouritesManager
    {
        /// <summary>
        /// Determines if a SNP with the given identifier is a user-defined favourite.
        /// </summary>
        /// <param name="id">The SNP identifier.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="id"/> is a user-defined favourite;
        /// otherwise <see langword="false"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="id"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when <paramref name="id"/> consists solely of whitespace.
        /// </exception>
        bool Get(string id);

        /// <summary>
        /// Updates whether or not an SNP with the given identifier is a user-defined favourite.
        /// </summary>
        /// <param name="id">The SNP identifier.</param>
        /// <param name="favourite">Whether or not <paramref name="id"/> is a user-defined favourite.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="id"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when <paramref name="id"/> consists solely of whitespace.
        /// </exception>
        void Set(string id, bool favourite);
    }
}