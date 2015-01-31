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

using Caliburn.Micro.MG;
using MG.GCompare.UI.Comparison.Favourites;
using MG.GCompare.UI.Mocks;
using MG.GCompare.UI.Support;
using Ninject;

namespace MG.GCompare.UI.Shell
{
    internal sealed class ShellViewModelTestConductor
    {
        public ShellViewModelTestConductor()
        {
            Shell = new ShellViewModel(MakeKernel());
        }

        public ShellViewModel Shell { get; }

        public MockFavouritesManager Favourites { get; } = new MockFavouritesManager();

        public MockDialogManager DialogManager { get; } = new MockDialogManager();

        public MockGenomeModelLoader Loader { get; } = new MockGenomeModelLoader();

        private StandardKernel MakeKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IDialogManager>().ToConstant(DialogManager.Object);
            kernel.Bind<IGenomeModelLoader>().ToConstant(Loader.Object);
            kernel.Bind<IFavouritesManager>().ToConstant(Favourites.Object);
            return kernel;
        }
    }
}