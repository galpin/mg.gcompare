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
using System.Diagnostics;
using Caliburn.Micro;
using MG.Common;
using MG.Genetics.Model;

namespace MG.GCompare.UI.Comparison
{
    /// <summary>
    /// Provides a view model for comparison of a single SNP. This class cannot be inherited.
    /// </summary>
    public sealed class SnpViewModel : PropertyChangedBase
    {
        private bool _isFavourite;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnpViewModel"/> class.
        /// </summary>
        /// <param name="a">The <see cref="SnpModel"/> that forms the basis of the comparison
        /// (can be <see langword="null"/>).</param>
        /// <param name="b">The <see cref="SnpModel"/> that is compared to <paramref name="b"/> 
        /// (can be <see langword="null"/>).</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="a"/> and <paramref name="b"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown when <paramref name="a"/> and <paramref name="b"/> do not share the same identifier.
        /// </exception>
        public SnpViewModel(SnpModel a, SnpModel b)
        {
            if (a == null && b == null)
            {
                throw new ArgumentNullException("Must specify at least one SnpModel");
            }
            Guard.IsInRange((a == null || b == null) || a.Id == b.Id, nameof(a));

            Id = a?.Id ?? b?.Id;
            Location = (a?.Location ?? b?.Location).Value;
            IsSame = a == null || b == null ? (bool?) null : a.Genotype == b.Genotype;
            Position = (a?.Position ?? b?.Position).Value;
            GenotypeA = a?.Genotype;
            GenotypeB = b?.Genotype;
        }

        /// <summary>
        /// Gets the SNP identifier.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the SNP location in the reference genome.
        /// </summary>
        public Chromosome Location { get; }

        /// <summary>
        /// Gets the SNP position in the reference genome.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Gets the geneotype for the first SNP.
        /// </summary>
        public string GenotypeA { get; }

        /// <summary>
        /// Gets the genotype for the second SNP.
        /// </summary>
        public string GenotypeB { get; }

        /// <summary>
        /// Indicates whether or not the genotype is the same for both <see cref="SnpModel"/> instances.
        /// </summary>
        public bool? IsSame { get; }

        /// <summary>
        /// Indicates whether or not the SNP is a user-favourite.
        /// </summary>
        public bool IsFavourite
        {
            get { return _isFavourite; }
            set
            {
                if (value == _isFavourite)
                {
                    return;
                }
                _isFavourite = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Toggles the <see cref="IsFavourite"/> property.
        /// </summary>
        public void ToggleIsFavourite()
        {
            IsFavourite = !IsFavourite;
        }

        /// <summary>
        /// Opens a browser window at SNPedia for this SNP.
        /// </summary>
        public void OpenBrowser()
        {
            Process.Start("http://www.snpedia.com/index.php/" + Id);
        }
    }
}