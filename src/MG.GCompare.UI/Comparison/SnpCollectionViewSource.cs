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
using System.Windows;
using System.Windows.Data;
using MG.Genetics.Model;

namespace MG.GCompare.UI.Comparison
{
    /// <summary>
    /// Provides a mechanism for sorting/filtering a <see cref="SnpCollectionViewSource"/>. This class cannot be inherited.
    /// </summary>
    public sealed class SnpCollectionViewSource : CollectionViewSource
    {
        /// <summary>
        /// Defines the <see cref="Chromosome"/> dependency property. This field is <see langword="readonly"/>.
        /// </summary>
        public static readonly DependencyProperty ChromosomeProperty =
            DependencyProperty.Register(
                "Chromosome",
                typeof(Chromosome),
                typeof(SnpCollectionViewSource),
                new FrameworkPropertyMetadata(Chromosome.One, OnChromosomeChanged));

        /// <summary>
        /// Initializes a new instance of the <see cref="SnpCollectionViewSource"/> class.
        /// </summary>
        public SnpCollectionViewSource()
        {
            Filter += OnFilter;
        }

        /// <summary>
        /// Gets or sets the chromosome on which to filter SNP.
        /// </summary>
        public Chromosome Chromosome
        {
            get { return (Chromosome) GetValue(ChromosomeProperty); }
            set { SetValue(ChromosomeProperty, value); }
        }

        private void OnFilter(object sender, FilterEventArgs e)
        {
            e.Accepted = ((SnpViewModel) e.Item).Location == Chromosome;
        }

        private static void OnChromosomeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((SnpCollectionViewSource)sender).View.Refresh();
        }
    }
}