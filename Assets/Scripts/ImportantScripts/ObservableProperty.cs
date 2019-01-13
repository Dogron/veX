using System;

namespace ImportantScripts
{
    public class ObservableProperty<T> where T : class
    {
        private T _value;

        public ObservableProperty(T value)
        {
            this._value = value;
        }

        public void setValue(T newValue)
        {
            var oldValue = _value;
            if (oldValue == newValue) 
                return;
            
            _value = newValue;
            if (PropertyChanged != null) // Someone subscribed
                PropertyChanged(this, new PropertyChangedEventArgs<T>
                {
                    NewValue = newValue,
                    OldValue = oldValue
                });
        }

        public T getValue()
        {
            return _value;
        }

        public event PropertyChangedEventDelegate<T> PropertyChanged;

        /*static void test()
        {
            var prop = new ObservableProperty<String>("sdf");
            var current = prop.getValue();
            prop.PropertyChanged += (sender, args) =>
            {
                var oldv = args.OldValue;
                var newv = args.NewValue;
            };
            prop.setValue("aaa");
        }*/
    }

    public class PropertyChangedEventArgs<T>
    {
        public T OldValue { get; set; }
        public T NewValue { get; set; }
    }

    public delegate void PropertyChangedEventDelegate<T>(object sender, PropertyChangedEventArgs<T> args);
}