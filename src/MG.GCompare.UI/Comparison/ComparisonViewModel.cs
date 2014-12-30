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
using System.Linq;
using MG.Genetics.Model;

namespace MG.GCompare.UI.Comparison
{
    /// <summary>
    /// Provides a comparison of two <see cref="GenomeModel"/> instances. This class cannot be inherited.
    /// </summary>
    public sealed class ComparisonViewModel : IComparisonViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonViewModel"/> class.
        /// </summary>
        /// <param name="a">The first gneome to compare.</param>
        /// <param name="b">The second genome to compare.</param>
        public ComparisonViewModel(GenomeModel a, GenomeModel b)
        {
            Snp = new SnpViewModelCollection(MakeSnpViewModels(a, b));
        }

        /// <summary>
        /// Gets a collection of <see cref="SnpViewModel"/> instances that are compared.
        /// </summary>
        public SnpViewModelCollection Snp { get; }

        private static IEnumerable<SnpViewModel> MakeSnpViewModels(GenomeModel a, GenomeModel b)
        {
            return a.Snp.Select(snp => new SnpViewModel(snp, b.Snp.GetByIdOrDefault(snp.Id)));
        }
    }
}