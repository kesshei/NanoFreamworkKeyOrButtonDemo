using nanoFramework.Hardware.Esp32;
using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;

namespace NanoFeamworkKeyOrButtonDemo
{
    public class Program
    {
        public static GpioPin led;
        public static GpioPin button;
        public static void Main()
        {
            var gpioController = new GpioController();
            button = gpioController.OpenPin(Gpio.IO00, PinMode.Input);
            led = gpioController.OpenPin(Gpio.IO02, PinMode.Output);
            button.ValueChanged += Button_ValueChanged;

            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        private static void Button_ValueChanged(object sender, PinValueChangedEventArgs e)
        {
            Debug.WriteLine("按键事件 : " + e.ChangeType.ToString());
            Debug.WriteLine("按键当前值: " + button.Read());

            if (e.ChangeType != PinEventTypes.Rising)
            {
                //按下点亮灯
                led.Write(PinValue.High);
            }
            else
            {
                //松开手把灯搞灭
                led.Write(PinValue.Low);
            }
        }
    }
}
