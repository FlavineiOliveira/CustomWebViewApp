using CustomWebViewApp.Messages;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomWebViewApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RenderizadorSitePage : ContentPage
    {
        public RenderizadorSitePage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<object>(this, MessageCenter.ABRIR_NAVEGADOR_PADRAO, (sender) =>
            {
                AbrirNavegadorPadrao((string)sender);
            });
        }

        private void AbrirNavegadorPadrao(string url)
        {
            Device.OpenUri(new Uri(url));
        }
    }
}