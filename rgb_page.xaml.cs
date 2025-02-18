using alexm_maui.Models;

namespace alexm_maui;

public partial class rgb_page : ContentPage
{
    public VerticalStackLayout MainContainer { get; set; }
    public VerticalStackLayout SlidersContainer { get; set; }
    public Button RGBColorView { get; set; } = new Button()
    {
        HeightRequest = 200,
        WidthRequest = 200,
    };
    public Label RGBlabel { get; set; }
   
    public RGBSliders RGBSliders { get; set; } = new RGBSliders();

    public rgb_page()
    {
        MainContainer = new VerticalStackLayout()
        {
            BackgroundColor = Color.FromArgb("#0b0d17")

        };
        SlidersContainer = new VerticalStackLayout()
        {
            BackgroundColor = Color.FromArgb("#121526"),
            HeightRequest = 200
        };
        Content = this.MainContainer;
        MainContainer.Children.Add(RGBColorView);
        MainContainer.Children.Add(SlidersContainer);
        foreach (Slider slider in RGBSliders.SlidersList)
        {
            SlidersContainer.Children.Add(slider);
        }
        RGBSliders.FinalColorChanged += FinalColorChanged;
        RGBlabel = new Label()
        {
            FontSize = 22
        };
        MainContainer.Children.Add(RGBlabel);
    }
    private void FinalColorChanged(List<int> rgb)
    {
        RGBColorView.BackgroundColor = Color.FromRgb(rgb[0], rgb[1], rgb[2]);
        RGBlabel.Text = $"{rgb[0]} {rgb[1]} {rgb[2]}";
    }
}