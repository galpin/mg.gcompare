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
using MG.GCompare.UI.Comparison.Support;

namespace MG.GCompare.UI.Comparison
{
    /// <summary>
    /// Provides a mechanism for filtering a <see cref="SnpCollectionViewSource"/>. This class cannot be inherited.
    /// </summary>
    public sealed class SnpCollectionViewSource : CollectionViewSource
    {
        /// <summary>
        /// Defines the <see cref="FilterText"/> dependency property. This field is <see langword="readonly"/>.
        /// </summary>
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register(
                "FilterText",
                typeof(string),
                typeof(SnpCollectionViewSource),
                new FrameworkPropertyMetadata(null, OnChanged));

        /// <summary>
        /// Defines the <see cref="ShowFavourites"/> dependency property. This field is <see langword="readonly"/>.
        /// </summary>
        public static readonly DependencyProperty ShowFavouritesProperty =
            DependencyProperty.Register(
                "ShowFavourites",
                typeof(bool),
                typeof(SnpCollectionViewSource),
                new FrameworkPropertyMetadata(false, OnChanged));

        /// <summary>
        /// Initializes a new instance of the <see cref="SnpCollectionViewSource"/> class.
        /// </summary>
        public SnpCollectionViewSource()
        {
            Filter += OnFilter;
        }

        /// <summary>
        /// Gets or sets the value text with which to filter the identifier.
        /// </summary>
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets whether or not to only show SNP marked as favourites.
        /// </summary>
        public bool ShowFavourites
        {
            get { return (bool)GetValue(ShowFavouritesProperty); }
            set { SetValue(ShowFavouritesProperty, value); }
        }

        private void OnFilter(object sender, FilterEventArgs e)
        {
            var item = (SnpViewModel)e.Item;
            var matches = MatchesIdentifier(FilterText, item);
            e.Accepted = !ShowFavourites ? matches : matches && item.IsFavourite;
        }

        private bool MatchesIdentifier(string filter, SnpViewModel item)
        {
            if (String.IsNullOrWhiteSpace(filter))
            {
                return true;
            }
            return item.Id.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
        }

        private static void OnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((SnpCollectionViewSource)sender).View?.Refresh();
        }
    }
}