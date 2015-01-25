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
using System.Linq;
using Caliburn.Micro.MG;
using MG.GCompare.UI.Comparison;
using MG.GCompare.UI.Support;
using Moq;
using Xunit;

namespace MG.GCompare.UI.Shell
{
    public class ShellViewModelTests
    {
        [Fact]
        public void Ctor_CorrectlyInitializesMembers_Test()
        {
            var dialogManager = new Mock<IDialogManager>().Object;
            var loader = new Mock<IGenomeModelLoader>().Object;

            var actual = new ShellViewModel(dialogManager, loader);

            Assert.Equal("MG.GCompare", actual.DisplayName);
        }

        [Fact]
        public void Open_InvokesOpenFileForFirstDataset_Test()
        {
            var conductor = new ShellViewModelTestConductor();

            conductor.Shell.Open();

            conductor.DialogManager.AssertOpenFileInvoked(x =>
                x.Title == "Open Genome A" &&
                x.DefaultExt == ".txt" &&
                x.Filter == "Text documents (.txt)|*.txt");
        }

        [Fact]
        public void Open_WhenUserCancelsFirstDialog_Returns_Test()
        {
            var conductor = new ShellViewModelTestConductor();
            conductor.DialogManager.SetupOpenFileToReturn(x => x.Title == "Open Genome A", null);

            conductor.Shell.Open();

            conductor.DialogManager.AssertOpenFileInvoked(x => x.Title == "Open Genome B", Times.Never());
        }

        [Fact]
        public void Open_WhenUserSelectsFirstDataset_InvokesOpenFileForSecondDataset_Test()
        {
            var conductor = new ShellViewModelTestConductor();
            conductor.DialogManager.SetupOpenFileToReturn(x => x.Title == "Open Genome A", "a.txt");
            conductor.Loader.SetupLoadAsyncToReturn(x => x == "a.txt", TestGenomeModel.Create());
            conductor.Loader.SetupLoadAsyncToReturn(x => x == "b.txt", TestGenomeModel.Create());

            conductor.Shell.Open();

            conductor.DialogManager.AssertOpenFileInvoked(x =>
                x.Title == "Open Genome B" &&
                x.DefaultExt == ".txt" &&
                x.Filter == "Text documents (.txt)|*.txt");
        }

        [Fact]
        public void Open_WhenUserCancelsSecondDialog_LoadsFirstDataset_Test()
        {
            var conductor = new ShellViewModelTestConductor();
            conductor.DialogManager.SetupOpenFileToReturn(x => x.Title == "Open Genome A", "a.txt");
            conductor.DialogManager.SetupOpenFileToReturn(x => x.Title == "Open Genome B", null);
            conductor.Loader.SetupLoadAsyncToReturn(x => x == "a.txt", TestGenomeModel.Create());

            conductor.Shell.Open();

            conductor.Loader.AssertLoadAsyncInvoked(Times.Once());
            conductor.Loader.AssertLoadAsyncInvoked(x => x == "a.txt", Times.Once());
        }

        [Fact]
        public void Open_WhenUserCancelsSecondDialog_ActivatesComparison_Test()
        {
            var conductor = new ShellViewModelTestConductor();
            conductor.DialogManager.SetupOpenFileToReturn(x => x.Title == "Open Genome A", "a.txt");
            conductor.DialogManager.SetupOpenFileToReturn(x => x.Title == "Open Genome B", null);
            conductor.Loader.SetupLoadAsyncToReturn(x => x == "a.txt", TestGenomeModel.Create());

            conductor.Shell.Open();

            var comparison = Assert.IsType<ComparisonViewModel>(conductor.Shell.ActiveItem);
            Assert.True(comparison.Snp.All(x => x.GenotypeB == null));
        }

        [Fact]
        public void Open_WhenUserSelectsBothDatasets_LoadsFirstAndSecondDatasets_Test()
        {
            var conductor = new ShellViewModelTestConductor();
            conductor.DialogManager.SetupOpenFileToReturn(x => x.Title == "Open Genome A", "a.txt");
            conductor.DialogManager.SetupOpenFileToReturn(x => x.Title == "Open Genome B", "b.txt");
            conductor.Loader.SetupLoadAsyncToReturn(x => x == "a.txt", TestGenomeModel.Create());
            conductor.Loader.SetupLoadAsyncToReturn(x => x == "b.txt", TestGenomeModel.Create());

            conductor.Shell.Open();

            conductor.Loader.AssertLoadAsyncInvoked(Times.Exactly(2));
            conductor.Loader.AssertLoadAsyncInvoked(x => x == "a.txt", Times.Once());
            conductor.Loader.AssertLoadAsyncInvoked(x => x == "b.txt", Times.Once());
        }

        [Fact]
        public void Open_WhenUserSelectsBothDatasets_ActivatesComparison_Test()
        {
            var conductor = new ShellViewModelTestConductor();
            conductor.DialogManager.SetupOpenFileToReturn(x => x.Title == "Open Genome A", "a.txt");
            conductor.DialogManager.SetupOpenFileToReturn(x => x.Title == "Open Genome B", "b.txt");
            conductor.Loader.SetupLoadAsyncToReturn(x => x == "b.txt", TestGenomeModel.Create());
            conductor.Loader.SetupLoadAsyncToReturn(x => x == "a.txt", TestGenomeModel.Create());

            conductor.Shell.Open();

            var comparison = Assert.IsType<ComparisonViewModel>(conductor.Shell.ActiveItem);
            Assert.True(comparison.Snp.All(x => x.GenotypeB != null));
        }

        [Fact]
        public void Open_WhenLoadingDatasets_NotifiesOfPropertyChanged_Test()
        {
            var conductor = new ShellViewModelTestConductor();
            conductor.DialogManager.SetupOpenFileToReturn(x => x.Title == "Open Genome A", "a.txt");
            conductor.Loader.SetupLoadAsyncToReturn(x => x == "a.txt", TestGenomeModel.Create());

            conductor.Shell.CreatePropertyChangedObserver()
                           .ExpectPropertyChanged(x => x.IsBusy)
                           .ExpectPropertyChanged(x => x.ActiveItem)
                           .ExpectPropertyChanged(x => x.IsBusy)
                           .Verify(x => x.Open());

            Assert.False(conductor.Shell.IsBusy);
        }

        [Fact]
        public void Open_WhenLoadingDatasetThrows_UpdatesNotBusy_Test()
        {
            var conductor = new ShellViewModelTestConductor();
            conductor.DialogManager.SetupOpenFileToReturn(x => x.Title == "Open Genome A", "a.txt");
            conductor.Loader.SetupLoadAsyncToThrow<FileNotFoundException>();

            conductor.Shell.Open();

            Assert.False(conductor.Shell.IsBusy);
        }
    }
}