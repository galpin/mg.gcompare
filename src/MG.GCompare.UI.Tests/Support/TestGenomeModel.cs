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

using System.Linq;
using MG.Genetics.Model;

namespace MG.GCompare.UI.Support
{
    internal static class TestGenomeModel
    {
        public static GenomeModel Create()
        {
            return new GenomeModel(new[]
            {
                TestSnpModel.Create("rs001"),
                TestSnpModel.Create("rs002"),
                TestSnpModel.Create("rs003"),
            });
        }

        public static GenomeModel Create(params string[] id)
        {
            return new GenomeModel(id.Select(x => TestSnpModel.Create(x)));
        }
    }
}