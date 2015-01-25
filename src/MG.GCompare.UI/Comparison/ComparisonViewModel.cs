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
        /// <param name="b">The optional second genome to compare, can be <see langword="null"/>.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if <paramref name="a"/> is <see langword="null"/>.
        /// </exception>
        public ComparisonViewModel(GenomeModel a, GenomeModel b = null)
        {
            Guard.IsNotNull(a, nameof(a));

            Snp = new SnpViewModelCollection(MakeSnpViewModels(a, b));
        }

        /// <summary>
        /// Gets a collection of <see cref="SnpViewModel"/> instances that are compared.
        /// </summary>
        public SnpViewModelCollection Snp { get; }

        private static IEnumerable<SnpViewModel> MakeSnpViewModels(GenomeModel a, GenomeModel b)
        {
            if (b == null)
            {
                foreach (var aa in a.Snp)
                {
                    yield return new SnpViewModel(aa, null);
                }
                yield break;
            }
            // SNP in genome A and SNP in both genome A and B.
            foreach (var aa in a.Snp)
            {
                SnpModel bb;
                b.Snp.TryGetById(aa.Id, out bb);
                yield return new SnpViewModel(aa, bb);
            }
            // SNP only in genome B.
            foreach (var bb in b.Snp)
            {
                SnpModel _;
                if (a.Snp.TryGetById(bb.Id, out _))
                {
                    continue;
                }
                yield return new SnpViewModel(null, bb);
            }
        }
    }
}