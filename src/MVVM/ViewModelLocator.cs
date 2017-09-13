using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM
{
    public static class ViewModelLocator
    {
        public static bool GetAutoWireViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        public static readonly DependencyProperty AutoWireViewModelProperty =
            DependencyProperty.RegisterAttached("AutoWireViewModel",
            typeof(bool), typeof(ViewModelLocator),
            new PropertyMetadata(false, AutoWireViewModelChanged));

        private static void AutoWireViewModelChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d)) return;
            var viewType = d.GetType();
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewTypeName = viewType.FullName;
            var viewModelTypeName = String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}Model, {1}", viewTypeName, viewAssemblyName);
            var viewModelType = Type.GetType(viewModelTypeName);
            if (viewModelType == null)
            {
                viewModelTypeName = viewModelTypeName.Replace(".View.", ".ViewModel.");
                viewModelType = Type.GetType(viewModelTypeName);
            }
            var viewModel = Activator.CreateInstance(viewModelType);
            ((FrameworkElement)d).DataContext = viewModel;
        }
    }
}
