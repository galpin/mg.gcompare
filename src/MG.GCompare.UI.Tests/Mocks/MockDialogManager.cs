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
using System.Linq.Expressions;
using Caliburn.Micro.MG;
using Moq;

namespace MG.GCompare.UI.Mocks
{
    internal sealed class MockDialogManager
    {
        private readonly Mock<IDialogManager> _mock = new Mock<IDialogManager>();

        public IDialogManager Object => _mock.Object;

        public void AssertOpenFileInvoked(Expression<Func<OpenFileDialogOptions, bool>> match, Times? times = null)
        {
            _mock.Verify(x => x.OpenFile(It.Is(match)), times ?? Times.Once());
        }

        public void SetupOpenFileToReturn(Expression<Func<OpenFileDialogOptions, bool>> match, string returnValue)
        {
            _mock.Setup(x => x.OpenFile(It.Is(match))).Returns(returnValue);
        }
    }
}