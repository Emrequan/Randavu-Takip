using RandevuApp.View;

namespace RandevuApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		// NavigationPage içine GirisSayfasi'yı sarıyoruz
		var navigationPage = new NavigationPage(new GirisSayfasi());

		// Geri butonu ve geçişler bu sayede aktif olur
		return new Window(navigationPage);
	}
}
