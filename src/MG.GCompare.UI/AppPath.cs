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

namespace MG.GCompare.UI
{
    /// <summary>
    /// Provides utility methods for getting application specific information. This class is <see langword="static"/>.
    /// </summary>
    internal static class AppPath
    {
        static AppPath()
        {
            Directory.CreateDirectory(GetApplicationRoot());
        }

        /// <summary>
        /// Gets the full path to <paramref name="filename"/> located in the application directory.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>The full path to <paramref name="filename"/> in the application directory.</returns>
        public static string GetFullPath(string filename)
        {
            return Path.Combine(GetApplicationRoot(), filename);
        }

        private static string GetApplicationRoot()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MG.GCompare");
        }
    }
}