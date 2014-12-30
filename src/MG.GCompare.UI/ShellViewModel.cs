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

using System.IO;
using System.Windows;
using Caliburn.Micro;
using MG.GCompare.UI.Comparison;
using MG.GCompare.UI.Support;

namespace MG.GCompare.UI
{
    /// <summary>
    /// A default <see cref="IShell"/> implementation. This class cannot be inherited.
    /// </summary>
    public sealed class ShellViewModel : Conductor<IComparisonViewModel>.Collection.OneActive, IShell
    {
        private readonly IGenomeModelLoader _loader = new StandardGenomeModelLoader();
        private bool _isBusy;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        public ShellViewModel()
        {
            DisplayName = "MG.GCompare";
        }

        /// <summary>
        /// Indicates whether or not the shell is busy.
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            private set
            {
                _isBusy = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Prompts to open a pair of datasets.
        /// </summary>
        public async void Open()
        {
            try
            {
                IsBusy = true;
                var root = @"C:\Users\galpin\Dropbox\Data\genetics\";
                var a = await _loader.LoadAsync(Path.Combine(root, @"genome_Martin_Galpin_Full_20141109003835.txt"));
                var b = await _loader.LoadAsync(Path.Combine(root, @"genome_Carina_Lilley_Full_20141109003848.txt"));
                ActivateItem(new ComparisonViewModel(a, b));
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}