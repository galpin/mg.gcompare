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

using System.Windows;
using System.Windows.Controls;

namespace MG.GCompare.UI.Comparison.Support
{
    /// <summary>
    /// Provides attached properties for <see cref="WebBrowser"/> instances. This class is <see langword="static"/>.
    /// </summary>
    public static class WebBrowserAttachedProperties
    {
        /// <summary>
        /// Defines the dependency property for source. This field is <see langword="readonly"/>.
        /// </summary>
        public static readonly DependencyProperty SourceProperty = DependencyProperty.RegisterAttached(
            "Source",
            typeof(string),
            typeof(WebBrowserAttachedProperties),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnSourceChanged));

        /// <summary>
        /// Sets the source URL for the browser.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <param name="url">The source URL.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if <paramref name="browser"/> or <paramref name="url"/> is <see langword="null"/>.
        /// </exception>
        public static void SetSource(WebBrowser browser, string url)
        {
            browser.SetValue(SourceProperty, url);
        }

        /// <summary>
        /// Gets the source URL for the browser.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <returns>The source URL (can be <see langword="null"/>).</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if <paramref name="browser"/> is <see langword="null"/>.
        /// </exception>
        public static string GetSource(UIElement browser)
        {
            return (string)browser.GetValue(SourceProperty);
        }

        private static void OnSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var url = (string)e.NewValue;
            if (url == null)
            {
                return;
            }
            ((WebBrowser)sender).Navigate(url);
        }
    }
}