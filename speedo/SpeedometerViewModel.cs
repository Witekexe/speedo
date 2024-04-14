using System;
using System.Timers;
using Microsoft.Maui.Controls;

namespace speedo
{
    public class SpeedometerViewModel : BindableObject
    {
        private double _speed;
        private System.Timers.Timer _timer;

        public double Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                OnPropertyChanged();
            }
        }

        public SpeedometerViewModel()
        {
            _timer = new System.Timers.Timer(100); // specify the fully qualified name
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var accelerometer = Accelerometer.Default;
            if (accelerometer.IsSupported)
            {
                accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            }
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var reading = e.Reading;
            Speed = Math.Sqrt(Math.Pow(reading.Acceleration.X, 2) + Math.Pow(reading.Acceleration.Y, 2) + Math.Pow(reading.Acceleration.Z, 2)) * 10;
        }
    }
}