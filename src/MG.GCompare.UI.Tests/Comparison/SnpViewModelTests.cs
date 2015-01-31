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
using MG.GCompare.UI.Support;
using Xunit;

namespace MG.GCompare.UI.Comparison
{
    public class SnpViewModelTests
    {
        [Fact]
        public void Ctor_WhenAIsNull_CorrectlyInitializesMembers_Test()
        {
            var b = TestSnpModel.Create();

            var actual = new SnpViewModel(null, b);

            Assert.Equal(b.Id, actual.Id);
            Assert.Equal(b.Location, actual.Location);
            Assert.Equal(b.Position, actual.Position);
            Assert.Equal(b.Genotype, actual.GenotypeB);
            Assert.Null(actual.GenotypeA);
            Assert.Null(actual.IsSame);
        }

        [Fact]
        public void Ctor_WhenBIsNull_CorrectlyInitializesMembers_Test()
        {
            var a = TestSnpModel.Create();

            var actual = new SnpViewModel(a, null);

            Assert.Equal(a.Id, actual.Id);
            Assert.Equal(a.Location, actual.Location);
            Assert.Equal(a.Position, actual.Position);
            Assert.Equal(a.Genotype, actual.GenotypeA);
            Assert.Null(actual.GenotypeB);
            Assert.Null(actual.IsSame);
        }

        [Fact]
        public void Ctor_WhenBIsSameAsA_CorrectlyInitializesMembers_Test()
        {
            var a = TestSnpModel.Create();
            var b = TestSnpModel.Create();

            var actual = new SnpViewModel(a, b);

            Assert.Equal(a.Id, actual.Id);
            Assert.Equal(a.Location, actual.Location);
            Assert.Equal(a.Position, actual.Position);
            Assert.Equal(a.Genotype, actual.GenotypeA);
            Assert.Equal(b.Genotype, actual.GenotypeB);
            Assert.True(actual.IsSame.Value);
        }

        [Fact]
        public void Ctor_WhenBIsDifferentToA_CorrectlyInitializesMembers_Test()
        {
            var a = TestSnpModel.Create(genotype:"AA");
            var b = TestSnpModel.Create(genotype:"AB");

            var actual = new SnpViewModel(a, b);

            Assert.Equal(a.Id, actual.Id);
            Assert.Equal(a.Location, actual.Location);
            Assert.Equal(a.Position, actual.Position);
            Assert.Equal(a.Genotype, actual.GenotypeA);
            Assert.Equal(b.Genotype, actual.GenotypeB);
            Assert.False(actual.IsSame.Value);
        }

        [Fact]
        public void Ctor_ThrowsIfBothIsNull_Test()
        {
            Assert.Throws<ArgumentNullException>(() => new SnpViewModel(null, null));
        }

        [Fact]
        public void Ctor_ThrowsIfAIsNotSameIdAsB_Test()
        {
            var a = TestSnpModel.Create("rs1");
            var b = TestSnpModel.Create("rs2");

            Assert.Throws<ArgumentOutOfRangeException>(() => new SnpViewModel(a, b));
        }

        [Fact]
        public void ToggleFavourite_UpdatesIsFavouriteProperty_Test()
        {
            var model = new SnpViewModel(TestSnpModel.Create(), null);
            var expected = !model.IsFavourite;

            model.ToggleIsFavourite();

            Assert.Equal(expected, model.IsFavourite);
        }

        [Fact]
        public void ToggleFavourite_NotifiesOfPropertyChangedForIsFavourite_Test()
        {
            var model = new SnpViewModel(TestSnpModel.Create(), null);

            model.CreatePropertyChangedObserver()
                 .ExpectPropertyChanged(x => x.IsFavourite)
                 .Verify(x => x.ToggleIsFavourite());
        }
    }
}