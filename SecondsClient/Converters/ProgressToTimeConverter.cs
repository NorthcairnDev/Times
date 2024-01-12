﻿using System.Globalization;

namespace SecondsClient.Converters
{
    public class ProgressToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
        
            var progress = (double)value * 5;
            
           // var  progressRounded = Math.Round((decimal)progress,2);
            
            return "Reserve: "+ progress.ToString("F", CultureInfo.InvariantCulture) + "sec";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}