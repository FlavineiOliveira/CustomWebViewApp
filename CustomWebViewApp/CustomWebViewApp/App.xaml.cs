using CustomWebViewApp.Pages;
using Xamarin.Forms;

namespace CustomWebViewApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new RenderizadorSitePage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
