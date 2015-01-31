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
using MG.GCompare.UI.Mocks;
using MG.GCompare.UI.Support;
using Moq;
using Xunit;

namespace MG.GCompare.UI.Comparison
{
    public class ComparisonViewModelTests
    {
        [Fact]
        public void Ctor_WhenBIsNull_CorrectlyInitializesMembers_Test()
        {
            var a = TestGenomeModel.Create();

            var actual = new ComparisonViewModelTestConductor(a).View;

            Assert.Equal(a.Snp.Count, actual.Snp.Count);
            foreach (var snp in actual.Snp)
            {
                Assert.NotNull(snp.GenotypeA);
                Assert.Null(snp.GenotypeB);
            }
        }

        [Fact]
        public void Ctor_WhenBIsNotNull_CorrectlyInitializesMembers_Test()
        {
            var a = TestGenomeModel.Create();
            var b = TestGenomeModel.Create();

            var actual = new ComparisonViewModelTestConductor(a, b).View;

            foreach (var snp in actual.Snp)
            {
                Assert.NotNull(snp.GenotypeA);
                Assert.NotNull(snp.GenotypeB);
            }
        }

        [Fact]
        public void Ctor_WhenBIsNotNull_AddsSnpExclusiveToA_Test()
        {
            var a = TestGenomeModel.Create("rs001", "rs002", "rs003");
            var b = TestGenomeModel.Create(new string[0]);

            var actual = new ComparisonViewModelTestConductor(a, b).View;

            Assert.Equal(a.Snp.Count, actual.Snp.Count);
            foreach (var snp in actual.Snp)
            {
                Assert.NotNull(snp.GenotypeA);
                Assert.Null(snp.GenotypeB);
            }
        }

        [Fact]
        public void Ctor_WhenBIsNotNull_AddsSnpExclusiveToB_Test()
        {
            var a = TestGenomeModel.Create(new string[0]);
            var b = TestGenomeModel.Create("rs001", "rs002", "rs003");

            var actual = new ComparisonViewModelTestConductor(a, b).View;

            Assert.Equal(b.Snp.Count, actual.Snp.Count);
            foreach (var snp in actual.Snp)
            {
                Assert.Null(snp.GenotypeA);
                Assert.NotNull(snp.GenotypeB);
            }
        }

        [Fact]
        public void Ctor_CorrectlyMarksSnpAsFavourites_Test()
        {
            var favouriteManager = new MockFavouritesManager();
            favouriteManager.SetupGetToReturn("rs001", true);
            favouriteManager.SetupGetToReturn("rs002", true);
            favouriteManager.SetupGetToReturn("rs003", false);

            var actual = new ComparisonViewModel(favouriteManager.Object, TestGenomeModel.Create());

            Assert.True(actual.Snp[0].IsFavourite);
            Assert.True(actual.Snp[1].IsFavourite);
            Assert.False(actual.Snp[2].IsFavourite);
        }

        [Fact]
        public void Ctor_ThrowsIfFavouritesManagerIsNull_Test()
        {
            var a = TestGenomeModel.Create();

            Assert.Throws<ArgumentNullException>(() => new ComparisonViewModel(null, a));
        }

        [Fact]
        public void Ctor_ThrowsIfAIsNull_Test()
        {
            var favourites = new MockFavouritesManager().Object;
            var b = TestGenomeModel.Create();

            Assert.Throws<ArgumentNullException>(() => new ComparisonViewModel(favourites, null, b));
        }

        [Fact]
        public void WhenSnpIsFavouriteChanged_UpdatesFavouriteManager_Test()
        {
            var conductor = new ComparisonViewModelTestConductor();
            var snp = conductor.View.Snp[0];

            snp.ToggleIsFavourite();

            conductor.Favourites.VerifySetInvoked(snp.Id, snp.IsFavourite, Times.Once());
        }
    }
}