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
using System.IO;
using System.Linq;
using MG.Common;
using Xunit;

namespace MG.GCompare.UI.Comparison.Favourites
{
    public class TextFavouritesManagerTests
    {
        [Fact]
        public void Ctor_ThrowsIfPathIsNull_Test()
        {
            Assert.Throws<ArgumentNullException>(() => new TextFavouriteManager(null));
        }

        [Fact]
        public void Ctor_CorrectlyInitializesFavouritesFromFile_Test()
        {
            string path;
            using (WriteText(out path, "RS001", "RS002", "RS003"))
            {
                var favourites = new TextFavouriteManager(path);

                Assert.True(favourites.Get("RS001"));
                Assert.True(favourites.Get("RS002"));
                Assert.True(favourites.Get("RS003"));
            }
        }

        [Fact]
        public void Ctor_WhenPathDoesNotExist_DoesNotThrow_Test()
        {
            string path;
            using (MakeTempPath(out path))
            {
                Assert.DoesNotThrow(() => new TextFavouriteManager(path));
            }
        }

        [Fact]
        public void Get_ReturnsTrueWhenIdGet_Test()
        {
            string path;
            using (WriteText(out path, "RS001"))
            {
                var favourites = new TextFavouriteManager(path);

                Assert.True(favourites.Get("RS001"));
            }
        }

        [Fact]
        public void Get_ReturnsFalseWhenIdGet_Test()
        {
            string path;
            using (WriteText(out path))
            {
                var favourites = new TextFavouriteManager(path);

                Assert.False(favourites.Get("RS001"));
            }
        }

        [Fact]
        public void Get_IsCaseInsensitive_Test()
        {
            string path;
            using (WriteText(out path, "RS001"))
            {
                var favourites = new TextFavouriteManager(path);

                Assert.True(favourites.Get("RS001"));
                Assert.True(favourites.Get("rs001"));
            }
        }

        [Fact]
        public void Set_WhenFavourite_WritesToPathWithIdentifiers_Test()
        {
            string path;
            using (WriteText(out path))
            {
                var favourites = new TextFavouriteManager(path);

                favourites.Set("rs001", true);
                favourites.Set("rs002", true);

                AssertText(path, "RS001", "RS002");
            }
        }

        [Fact]
        public void Set_WhenNotFavourite_WritesToPathWithoutIdentifiers_Test()
        {
            string path;
            using (WriteText(out path, "RS001", "RS002"))
            {
                var favourites = new TextFavouriteManager(path);

                favourites.Set("rs001", false);
                favourites.Set("rs002", false);

                AssertText(path);
            }
        }

        [Fact]
        public void Set_WhenPathDoesNotExist_CreatesDirectoryAndFile_Test()
        {
            string path;
            using (MakeTempPath(out path))
            {
                var favourites = new TextFavouriteManager(path);

                Assert.DoesNotThrow(() => favourites.Set("rs001", true));
                Assert.True(File.Exists(path));
            }
        }

        private static void AssertText(string path, params string[] expected)
        {
            var actual = File.ReadLines(path);
            Assert.True(expected.SequenceEqual(actual));
        }

        private static IDisposable WriteText(out string path, params string[] lines)
        {
            var disposable = MakeTempPath(out path);
            File.WriteAllLines(path, lines);
            return disposable;
        }

        private static IDisposable MakeTempPath(out string path)
        {
            path = Path.Combine(Path.GetTempPath(), "tests", "favourites.txt");
            var pathLocal = path;
            return new DisposableAction(() => File.Delete(pathLocal));
        }
    }
}