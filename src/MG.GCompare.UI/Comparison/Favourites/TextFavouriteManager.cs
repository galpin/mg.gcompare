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
using MG.Common;

namespace MG.GCompare.UI.Comparison.Favourites
{
    /// <summary>
    /// A <see cref="IFavouritesManager"/> that stores SNP favourites in a text file. This class cannot be inherited.
    /// </summary>
    public sealed class TextFavouriteManager : IFavouritesManager
    {
        private readonly string _path;
        private readonly HashSet<string> _favourites;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFavouriteManager"/> class.
        /// </summary>
        /// <param name="path">The path to the file containing the favourites.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="path"/> is <see langword="null" />.
        /// </exception>
        public TextFavouriteManager(string path)
        {
            Guard.IsNotNull(path, "path");

            _path = path;
            _favourites = new HashSet<string>(Load(path));
        }

        public bool Get(string id)
        {
            return _favourites.Contains(id.ToUpperInvariant());
        }

        public void Set(string id, bool favourite)
        {
            var idAsUpper = id.ToUpperInvariant();
            if (favourite && !_favourites.Add(idAsUpper))
            {
                return;
            }
            if (!favourite && !_favourites.Remove(idAsUpper))
            {
                return;
            }
            Save();
        }

        private void Save()
        {
            var directory = Path.GetDirectoryName(_path);
            if (!String.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.WriteAllLines(_path, _favourites);
        }

        private static IEnumerable<string> Load(string path)
        {
            return !File.Exists(path) ? Enumerable.Empty<string>() : File.ReadLines(path);
        }
    }
}