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
using System.Collections.ObjectModel;
using System.Linq;

namespace MG.Genetics.Model
{
    /// <summary>
    /// Provides a read-only collection of <see cref="SnpModel"/> instances. This class cannot be inherited.
    /// </summary>
    public class SnpModelCollection : ReadOnlyCollection<SnpModel>
    {
        private readonly Dictionary<string, SnpModel> _snpById;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnpModelCollection"/> class.
        /// </summary>
        /// <param name="list">The sequence of <see cref="SnpModel"/> instances from which to copy items.</param>
        public SnpModelCollection(IEnumerable<SnpModel> list)
            : base(new List<SnpModel>(list))
        {
            _snpById = Items.ToDictionary(x => x.Id);
        }

        /// <summary>
        /// Gets an <see cref="SnpModel"/> with the specified identifier or <see langword="null"/> if no SNP exists.
        /// </summary>
        /// <param name="id">The SNP identifier.</param>
        /// <returns>
        /// The <see cref="SnpModel"/> identified by <paramref name="id"/>; otherwise <see langword="null"/>.
        /// </returns>
        public SnpModel GetByIdOrDefault(string id)
        {
            SnpModel model;
            _snpById.TryGetValue(id, out model);
            return model;
        }

        /// <summary>
        /// Attempts to get an <see cref="SnpModel"/> with the specified identifier and returns a value whether or not
        /// it exists.
        /// </summary>
        /// <param name="id">The SNP identifier.</param>
        /// <param name="model">When this method returns; contains the model identified by <paramref name="id"/> or
        /// <see langword="null"/> if no SNP exists.</param>
        /// <returns>
        /// <see langword="true"/> is an SNP with the identifier <paramref name="id"/> exists;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public bool TryGetById(string id, out SnpModel model)
        {
            return _snpById.TryGetValue(id, out model);
        }
    }
}