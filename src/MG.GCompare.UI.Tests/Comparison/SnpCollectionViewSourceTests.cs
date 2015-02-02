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
using System.Collections;
using System.Linq;
using MG.GCompare.UI.Support;
using Xunit;

namespace MG.GCompare.UI.Comparison
{
    public class SnpCollectionViewSourceTests
    {
        [Fact]
        public void Ctor_CorrectlyInitializesMembers_Test()
        {
            var actual = new SnpCollectionViewSource();

            Assert.Null(actual.FilterText);
        }

        [Fact]
        public void OnFilterTextChanged_WhenWhiteSpace_DoesNotApplyAnyFilter_Test()
        {
            var items = new[]
            {
                new SnpViewModel(TestSnpModel.Create("rs001"), null),
                new SnpViewModel(TestSnpModel.Create("rs002"), null),
                new SnpViewModel(TestSnpModel.Create("rs003"), null)
            };
            var source = new SnpCollectionViewSource { Source = items };

            source.FilterText = String.Empty;

            AssertSame(source.View, items);
        }

        [Fact]
        public void OnFilterTextChanged_WhenNull_DoesNotApplyAnyFilter_Test()
        {
            var items = new[]
            {
                new SnpViewModel(TestSnpModel.Create("rs001"), null),
                new SnpViewModel(TestSnpModel.Create("rs002"), null),
                new SnpViewModel(TestSnpModel.Create("rs003"), null)
            };
            var source = new SnpCollectionViewSource { Source = items };

            source.FilterText = String.Empty;

            AssertSame(source.View, items);
        }

        [Fact]
        public void OnFilterTextChanged_AppliesFilterToIdentifiers_Test()
        {
            var items = new[]
            {
                new SnpViewModel(TestSnpModel.Create("rs001"), null),
                new SnpViewModel(TestSnpModel.Create("rs002"), null)
            };
            var source = new SnpCollectionViewSource {Source = items};

            source.FilterText = "rs001";

            AssertSame(source.View, items[0]);
        }

        [Fact]
        public void OnFilterTextChanged_AppliesFilterToPartialMatches_Test()
        {
            var items = new[]
            {
                new SnpViewModel(TestSnpModel.Create("rs001"), null),
                new SnpViewModel(TestSnpModel.Create("rs002"), null),
                new SnpViewModel(TestSnpModel.Create("rs003"), null)
            };
            var source = new SnpCollectionViewSource { Source = items };

            source.FilterText = "rs";

            AssertSame(source.View, items);
        }

        [Fact]
        public void OnFilterTextChanged_AppliesFiltersIgnoringCase_Test()
        {
            var items = new[]
            {
                new SnpViewModel(TestSnpModel.Create("rs001"), null),
                new SnpViewModel(TestSnpModel.Create("rs002"), null),
            };
            var source = new SnpCollectionViewSource { Source = items };

            source.FilterText = "RS001";

            AssertSame(source.View, items[0]);
        }

        [Fact]
        public void OnShowFavouritesChanged_ApplyFilterToFavourites_Test()
        {
            var items = new[]
            {
                new SnpViewModel(TestSnpModel.Create("rs001"), null),
                new SnpViewModel(TestSnpModel.Create("rs002"), null) { IsFavourite = true }
            };
            var source = new SnpCollectionViewSource { Source = items };

            source.ShowFavourites = true;

            AssertSame(source.View, items[1]);
        }

        [Fact]
        public void OnShowFavouritesChanged_ApplyFilterInConjunction_Test()
        {
            var items = new[]
            {
                new SnpViewModel(TestSnpModel.Create("rs10"), null),
                new SnpViewModel(TestSnpModel.Create("rs100"), null) { IsFavourite = true }
            };
            var source = new SnpCollectionViewSource { Source = items };

            source.FilterText = "rs10";
            source.ShowFavourites = true;

            AssertSame(source.View, items[1]);
        }

        private static void AssertSame(IEnumerable view, params SnpViewModel[] expected)
        {
            var actual = view.Cast<SnpViewModel>().ToArray();
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; ++i)
            {
                Assert.Same(expected[i], actual[i]);
            }
        }
    }
}