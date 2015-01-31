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

using MG.GCompare.UI.Comparison.Favourites;
using Moq;

namespace MG.GCompare.UI.Mocks
{
    internal sealed class MockFavouritesManager
    {
        private readonly Mock<IFavouritesManager> _favouriteManager = new Mock<IFavouritesManager>();

        public MockFavouritesManager()
        {
            _favouriteManager.Setup(x => x.Get(It.IsAny<string>())).Returns(false);
        }

        public IFavouritesManager Object => _favouriteManager.Object;

        public void SetupGetToReturn(string id, bool favourite)
        {
            _favouriteManager.Setup(x => x.Get(id)).Returns(favourite);
        }

        public void VerifySetInvoked(string id, bool favourite, Times? times = null)
        {
            _favouriteManager.Verify(x => x.Set(id, favourite), times ?? Times.Once());
        }
    }
}