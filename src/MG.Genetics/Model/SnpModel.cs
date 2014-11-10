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

namespace MG.Genetics.Model
{
    /// <summary>
    /// Represents an SNP. This class cannot be inherited.
    /// </summary>
    public sealed class SnpModel
    {
        private readonly string _identifier;
        private readonly int _location;
        private readonly int _position;
        private readonly string _genotype;

        public SnpModel(
            string identifier,
            int location,
            int position,
            string genotype)
        {
            // Guard.IsNotNullOrWhiteSpace(identifier, "identifier");
            // Guard.IsNotNullOrWhiteSpace(genotype, "genotype");

            _identifier = identifier;
            _location = location;
            _position = position;
            _genotype = genotype;
        }

        public string Identifier
        {
            get { return _identifier; }
        }

        public int Location
        {
            get { return _location; }
        }

        public int Position
        {
            get { return _position; }
        }

        public string Genotype
        {
            get { return _genotype; }
        }
    }
}