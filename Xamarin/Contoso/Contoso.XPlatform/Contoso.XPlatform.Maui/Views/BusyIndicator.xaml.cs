using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Contoso.XPlatform.Views;

public partial class BusyIndicator : ContentPage
{
	public BusyIndicator()
	{
		InitializeComponent();
        Visual = VisualMarker.Default;
        this.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
    }
}