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
using System.Threading.Tasks;
using MG.GCompare.UI.Support;
using MG.Genetics.Model;
using Moq;

namespace MG.GCompare.UI.Mocks
{
    internal sealed class MockGenomeModelLoader
    {
        private readonly Mock<IGenomeModelLoader> _mock = new Mock<IGenomeModelLoader>();

        public IGenomeModelLoader Object => _mock.Object;

        public void SetupLoadAsyncToReturn(Expression<Func<string, bool>> match, GenomeModel returnValue)
        {
            _mock.Setup(x => x.LoadAsync(It.Is(match))).Returns(Task.FromResult(returnValue));
        }

        public void SetupLoadAsyncToThrow<TException>() where TException : Exception, new()
        {
            _mock.Setup(x => x.LoadAsync(It.IsAny<string>())).Throws<TException>();
        }

        public void AssertLoadAsyncInvoked(Expression<Func<string, bool>> match, Times? times = null)
        {
            _mock.Verify(x => x.LoadAsync(It.Is(match)), times ?? Times.Once());
        }

        public void AssertLoadAsyncInvoked(Times? times = null)
        {
            _mock.Verify(x => x.LoadAsync(It.IsAny<string>()), times ?? Times.Once());
        }
    }
}