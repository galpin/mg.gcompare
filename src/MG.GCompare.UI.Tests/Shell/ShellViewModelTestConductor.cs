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

using MG.GCompare.UI.Mocks;

namespace MG.GCompare.UI.Shell
{
    internal sealed class ShellViewModelTestConductor
    {
        public ShellViewModelTestConductor()
        {
            DialogManager = new MockDialogManager();
            Loader = new MockGenomeModelLoader();
            Shell = new ShellViewModel(DialogManager.Object, Loader.Object);
        }

        public MockGenomeModelLoader Loader { get; }

        public ShellViewModel Shell { get; }

        public MockDialogManager DialogManager { get; }
    }
}