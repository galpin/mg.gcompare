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

using System.Linq;
using MG.Genetics.Support;
using Xunit;

namespace MG.Genetics.Model
{
    public class SnpModelCollectionTests
    {
        [Fact]
        public void Ctor_CorrectlyInitializesMembers_Test()
        {
            var expected = new[] {TestSnpModel.Create("rsXXX1"), TestSnpModel.Create("rsXXX2")};

            var actual = new SnpModelCollection(expected);

            Assert.True(actual.SequenceEqual(actual));
        }

        [Fact]
        public void GetByIdOrDefault_WhenSnpExists_ReturnsSnpModel_Test()
        {
            var expected = TestSnpModel.Create();

            var collection = new SnpModelCollection(new[] {expected});
            var actual = collection.GetByIdOrDefault(expected.Id);

            Assert.Same(expected, actual);
        }

        [Fact]
        public void GetByIdOrDefault_WhenSnpDoesNotExists_ReturnsNull_Test()
        {
            var collection = new SnpModelCollection(new SnpModel[0]);

            Assert.Null(collection.GetByIdOrDefault("rsXXXX"));
        }
    }
}