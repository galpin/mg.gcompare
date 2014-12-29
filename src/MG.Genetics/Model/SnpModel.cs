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
using MG.Common;

namespace MG.Genetics.Model
{
    /// <summary>
    /// Represents an SNP. This class cannot be inherited.
    /// </summary>
    public sealed class SnpModel : IEquatable<SnpModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnpModel"/> class.
        /// </summary>
        /// <param name="id">The SNP identifier.</param>
        /// <param name="location">The SNP location in the reference genome.</param>
        /// <param name="position">The SNP position in the reference genome.</param>
        /// <param name="genotype">
        /// The genotype call oriented with respect to the plus strand on the human reference sequence.
        /// </param>
        public SnpModel(
            string id,
            Chromosome location,
            int position,
            string genotype)
        {
            Guard.IsNotNull(id, nameof(id));
            Guard.IsNotNull(genotype, nameof(genotype));

            Id = id;
            Location = location;
            Position = position;
            Genotype = genotype;
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
        /// Gets the genotype call oriented with respect to the plus strand on the human reference sequence.
        /// </summary>
        public string Genotype { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj != null && obj.GetType() != GetType() && Equals((SnpModel) obj);
        }

        /// <inheritdoc/>
        public bool Equals(SnpModel other)
        {
            return other != null &&
                   Id == other.Id &&
                   Location == other.Location &&
                   Position == other.Position &&
                   Genotype == other.Genotype;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCodeBuilder.For<SnpModel>()
                .Add(Id)
                .Add(Location)
                .Add(Position)
                .Add(Genotype);
        }
    }
}