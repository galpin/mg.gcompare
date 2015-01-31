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
using System.Windows;
using Caliburn.Micro;
using Caliburn.Micro.MG;
using MG.Common;
using MG.GCompare.UI.Comparison;
using MG.GCompare.UI.Comparison.Favourites;
using MG.GCompare.UI.Support;
using Ninject;

namespace MG.GCompare.UI.Shell
{
    /// <summary>
    /// A default <see cref="IShell"/> implementation. This class cannot be inherited.
    /// </summary>
    public sealed class ShellViewModel : Conductor<IComparisonViewModel>.Collection.OneActive, IShell
    {
        private readonly IDialogManager _dialogManager;
        private readonly IGenomeModelLoader _loader;
        private readonly IFavouritesManager _favouritesManager;
        private bool _isBusy;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="kernel"/> is <see langword="null"/>.
        /// </exception>
        public ShellViewModel(IKernel kernel)
        {
            Guard.IsNotNull(kernel, nameof(kernel));

            _dialogManager = kernel.Get<IDialogManager>();
            _loader = kernel.Get<IGenomeModelLoader>();
            _favouritesManager = kernel.Get<IFavouritesManager>();

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
        /// Prompts the user to open a pair of datasets.
        /// </summary>
        public async void Open()
        {
            var a = OpenFile("Open Genome A");
            if (a == null)
            {
                return;
            }
            var b = OpenFile("Open Genome B");
            using (BeginBusy())
            {
                ActivateItem(new ComparisonViewModel(
                    _favouritesManager,
                    await _loader.LoadAsync(a),
                    b == null ? null : await _loader.LoadAsync(b)));
            }
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        public void Exit()
        {
            Application.Current.Shutdown();
        }

        private string OpenFile(string title)
        {
            return _dialogManager.OpenFile(new OpenFileDialogOptions
            {
                Title = title,
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            });
        }

        private IDisposable BeginBusy()
        {
            IsBusy = true;
            return new DisposableAction(() => IsBusy = false);
        }
    }
}