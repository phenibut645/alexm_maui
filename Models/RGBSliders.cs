using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace alexm_maui.Models
{
    public delegate void FinalColorChangedHandler(List<int> rgb);
    public class RGBSliders
    {
        public event FinalColorChangedHandler FinalColorChanged;
        public int Red
        {
            get
            {
                return (int)RedSlider.Value;
            }
            set
            {
                FinalColor[0] = value;
                FinalColorChanged?.Invoke(FinalColor);
            }
        }
        public int Green
        {
            get
            {
                return (int)GreenSlider.Value;
            }
            set
            {
                FinalColor[1] = value;
                FinalColorChanged?.Invoke(FinalColor);
            }
        }
        public int Blue
        {
            get
            {
                return (int)BlueSlider.Value;
            }
            set
            {
                FinalColor[2] = value;
                FinalColorChanged?.Invoke(FinalColor);
            }
        }
        public Slider RedSlider { get; set; } = null;
        public Slider GreenSlider { get; set; } = null;
        public Slider BlueSlider { get; set; } = null;
        public List<Slider> SlidersList
        {
            get
            {
                return new List<Slider>()
                {
                    RedSlider, GreenSlider, BlueSlider
                };
            }
        }
        private List<int> _finalColor = new List<int>() { 0,0,0};
        public List<int> FinalColor
        {
            get
            {
                return _finalColor;
            }
            set
            {
                _finalColor = value;
            }
        }
        public RGBSliders()
        {
            int index = -1;
            foreach (Slider? slider in SlidersList)
            {
                index++;
                Slider slider2 = new Slider()
                {
                    Minimum = 0,
                    Maximum = 255,
                    Value = 25
                };
                switch (index)
                {
                    case 0:
                        RedSlider = slider2;
                        break;
                    case 1:
                        GreenSlider = slider2;
                        break;
                        case 2: 
                         BlueSlider = slider2;
                        break;
                }
                slider2.ValueChanged += (object? sender, ValueChangedEventArgs e) =>
                {
                    Slider slider = sender as Slider;
                    int ind = index;
                    switch (ind)
                    {
                        case 0:
                            
                            Red = (int)slider2.Value;
                            break;
                        case 1:
                            Green = (int)slider2.Value;
                            break;
                        case 2:
                            Blue = (int)slider2.Value;
                            break;
                    }
                };
            }

        }



    }
}
