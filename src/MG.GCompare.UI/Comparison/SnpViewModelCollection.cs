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
using System.Collections.ObjectModel;

namespace MG.GCompare.UI.Comparison
{
    /// <summary>
    /// Provides a collection of <see cref="SnpViewModel"/> instances. This class cannot be inherited.
    /// </summary>
    public sealed class SnpViewModelCollection : ReadOnlyCollection<SnpViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnpViewModelCollection"/> class.
        /// </summary>
        /// <param name="source">The sequence of <see cref="SnpViewModel"/> instances from which to copy items.</param>
        public SnpViewModelCollection(IEnumerable<SnpViewModel> source)
            : base(new ObservableCollection<SnpViewModel>(source))
        {
        }
    }
}