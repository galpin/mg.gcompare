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

using System.Collections.Generic;
using Caliburn.Micro.MG;
using MG.GCompare.UI.Shell;
using Ninject.Modules;

namespace MG.GCompare.UI
{
    /// <summary>
    /// Provides the application bootstrapper. This class is cannot be inherited.
    /// </summary>
    public sealed class AppBootstrapper : NinjectBootstrapper<IShell>
    {
        /// <inheritdoc/>
        protected override IEnumerable<NinjectModule> GetModules()
        {
            yield return new NamespaceModule();
        }
    }
}