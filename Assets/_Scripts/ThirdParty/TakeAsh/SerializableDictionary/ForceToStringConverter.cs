using System;
using System.ComponentModel;
using System.Globalization;

namespace TakeAsh {

    /// <summary>
    /// use object's ToString() when converting to string
    /// </summary>
    /// <remarks>
    /// List&lt;T&gt; class returns no meaning when it is used as ComboBoxItem if DisplayMemberPath is not specified.  
    /// Making a delivered class with ToString() and applying this converter, the results of ToString() appear in ComboBox without DisplayMemberPath.
    /// </remarks>
    public class ForceToStringConverter :
        TypeConverter {

        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destinationType
        ) {
            if (destinationType == typeof(string)) {
                return value.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
