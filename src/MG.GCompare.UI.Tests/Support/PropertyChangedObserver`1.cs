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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Xunit.Sdk;

namespace MG.GCompare.UI.Support
{
    internal sealed class PropertyChangeObserver<TModel> where TModel : INotifyPropertyChanged
    {
        private readonly List<string> _actualPropertyChanges = new List<string>();
        private readonly List<string> _expectedPropertyChanges = new List<string>();
        private readonly TModel _model;

        public PropertyChangeObserver(TModel model)
        {
            _model = model;
            _model.PropertyChanged += (_, e) => _actualPropertyChanges.Add(e.PropertyName);
        }

        public PropertyChangeObserver<TModel> ExpectPropertyChanged(Expression<Func<TModel, object>> property)
        {
            _expectedPropertyChanges.Add(GetPropertyName(property));
            return this;
        }

        public void Verify(Action<TModel> action)
        {
            action(_model);
            AssertPropertyChangedExpectations();
        }

        private void AssertPropertyChangedExpectations()
        {
            if (_actualPropertyChanges.Count != _expectedPropertyChanges.Count)
            {
                throw MakeUnexpectedPropertyChangesException();
            }
            for (var i = 0; i < _expectedPropertyChanges.Count; ++i)
            {
                var expected = _expectedPropertyChanges[i];
                var actual = _actualPropertyChanges[i];
                if (!String.Equals(expected, actual, StringComparison.Ordinal))
                {
                    throw new EqualException(expected, actual);
                }
            }
        }

        private EqualException MakeUnexpectedPropertyChangesException()
        {
            return new EqualException(
                String.Join(" -> ", _expectedPropertyChanges),
                String.Join(" -> ", _actualPropertyChanges));
        }

        private static string GetPropertyName<TProperty>(Expression<Func<TProperty, object>> property)
        {
            var body = property.Body as MemberExpression;
            if (body == null)
            {
                body = (MemberExpression)((UnaryExpression)property.Body).Operand;
            }
            return body.Member.Name;
        }
    }
}