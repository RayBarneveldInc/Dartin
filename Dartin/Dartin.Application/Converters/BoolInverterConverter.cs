﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dartin.Converters
{
    public class BoolInverterConverter : MarkupExtension, IValueConverter
    {
        private static BoolInverterConverter _instance;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_instance == null)
                _instance = new BoolInverterConverter();

            return _instance;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
