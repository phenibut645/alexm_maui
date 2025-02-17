using alexm_maui.Models;

namespace alexm_maui
{
    public partial class ValgusfoorPage : ContentPage
    {
        public bool IsItActive { get; set; } = true;
        public Dictionary<string, EventHandler> BottomButtons { get; set;}
        public VerticalStackLayout MainContainer { get; set; } = new VerticalStackLayout()
        {
            BackgroundColor = Color.FromArgb("#0b0d17"),
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill
        };
        public HorizontalStackLayout BottomContainer { get; set; } = new HorizontalStackLayout()
        {
            
            VerticalOptions = LayoutOptions.End,
            Spacing = 10,
            BackgroundColor = Color.FromArgb("#121526"),
            HeightRequest= 200,

        };
        public List<TrafficLightState> States { get; set; } = new List<TrafficLightState>()
        {
            new TrafficLightState("#ff0000", "#360000", "Seis!"),
            new TrafficLightState("#fffb00", "#242300", "Oota!"),
            new TrafficLightState("#11ff00", "#055200", "Mine!")
        };
        public VerticalStackLayout TrafficLightContainer { get; set; } = new VerticalStackLayout()
        {
            Spacing = 20
        };
        public Label StateTextLabel { get; set; } = new Label()
        { 
            FontSize = 22,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#121526"),
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center, 
            Margin = new Thickness(0, 0, 0, 40)
        };
        public TrafficLightState ActiveState { get; set;}
        public void InitTrafficLightsButtons()
        {
            List<Button> buttons = new List<Button>();
            foreach(TrafficLightState state in States)
            {
                Button stateButton = new Button() { 
                    BackgroundColor = state.CurrentColor,
                    WidthRequest = 100,
                    HeightRequest = 100,
                    CornerRadius = 50
                    };
                TrafficLightContainer.Children.Add(stateButton);
                state.Button = stateButton;
                state.ActiveChanged += State_ActiveChanged;
                stateButton.Clicked += (object? sender, EventArgs e) =>
                {
                    if (IsItActive)
                    {
                        TrafficLightState tls = state;
                        if(ActiveState != null) ActiveState.Active = false;
                        tls.Active = !tls.Active;
                        ActiveState = tls;
                    }
                };
            }
            
        }
        private void InitButtons()
        {
            foreach(KeyValuePair<string, EventHandler> bottomButtonProps in BottomButtons)
            {
                Button button = new Button
                {
                    Text = bottomButtonProps.Key,
                    WidthRequest = 150,
                    TextColor = Colors.White,
                    FontSize = 18,
                    HeightRequest = 50,
                    BackgroundColor = Color.FromArgb("#0b0d17")
                };
                button.Clicked += bottomButtonProps.Value;
                BottomContainer.Children.Add(button);
            }
           
        }
        private void TurnOff(object? sender, EventArgs e)
        {
            IsItActive = false;
            foreach(TrafficLightState tls in States)
            {
                tls.Active = false;
            }
        }
        private void TurnOn(object? sender, EventArgs e)
        {
            IsItActive = true;
        }
        private void State_ActiveChanged(TrafficLightState trafficLightState)
        {
            if(! trafficLightState.Active) StateTextLabel.Text = "";
            else StateTextLabel.Text = trafficLightState.Text;
        }
        private bool _isCycleRunning = false;
        private CancellationTokenSource _cancellationTokenSource;
        private async void AutoMode(object? sender, EventArgs e)
        {
             if (_isCycleRunning)
            {
                _cancellationTokenSource.Cancel();
                _isCycleRunning = false;
                TurnOff(null, new EventArgs());
            }
            else
            {
                _cancellationTokenSource = new CancellationTokenSource(); // Создаем новый токен для отмены
                _isCycleRunning = true;
                await StartAutoMode(); // Запускаем цикл
            }
        }
        private async Task StartAutoMode()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                foreach(TrafficLightState state in States)
                {
                    state.Active = true;
                    await Task.Delay(1000);
                    state.Active = false;
                    if(_cancellationTokenSource.IsCancellationRequested) return;
                }
            }
        }
        public ValgusfoorPage()
        {
             BottomButtons = new Dictionary<string, EventHandler>()
            {
                { "Lülita välja",  TurnOff },
                { "Lülita sisse", TurnOn},
                 { "Automaatrežiimi sisselülitamine", AutoMode }
            };
            Content = MainContainer;
            
            MainContainer.Children.Add(StateTextLabel);
            MainContainer.Children.Add(TrafficLightContainer);
            MainContainer.Children.Add(BottomContainer);
            InitTrafficLightsButtons();
            InitButtons();
        }
        
    }
}
 
