using Android.Content;
using Android.Webkit;
using CustomWebViewApp.CustomRenderers;
using CustomWebViewApp.Droid.CustomRenderers;
using CustomWebViewApp.Messages;
using CustomWebViewApp.Models;
using Java.Interop;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewAndroid))]
namespace CustomWebViewApp.Droid.CustomRenderers
{
    public class CustomWebViewAndroid : WebViewRenderer
    {
        const string JavascriptFunction = "var navegacao = { Url: 'https://stackoverflow.com/questions/9749900/webview-addjavascriptinterface-function-with-parameters'};" +
                                          " var data = JSON.stringify(navegacao); " +
                                          "function botaoClick() { ClasseParaJavascript.myFunction(data); }";
        Context _context;

        public CustomWebViewAndroid(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Android.Webkit.WebView webview = (Android.Webkit.WebView)Control;
                webview.Settings.JavaScriptEnabled = true;

                Control.SetWebViewClient(new JavascriptWebViewClient(this, $"javascript: {JavascriptFunction}"));
                Control.AddJavascriptInterface(new ClasseParaJavascript(), "ClasseParaJavascript");
                Control.LoadUrl("file:///android_asset/meuSite.html");
            }
        }
    }

    public class ClasseParaJavascript : Java.Lang.Object
    {
        [JavascriptInterface]
        [Export("myFunction")]
        public void MyFunction(string navegacao)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Navegacao>(navegacao);

            MessagingCenter.Send<string>(obj.Url, MessageCenter.ABRIR_NAVEGADOR_PADRAO);
        }
    }

    public class JavascriptWebViewClient : FormsWebViewClient
    {
        string _javascript;

        public JavascriptWebViewClient(CustomWebViewAndroid renderer, string javascript) : base(renderer)
        {
            _javascript = javascript;
        }

        public override void OnPageFinished(Android.Webkit.WebView view, string url)
        {
            base.OnPageFinished(view, url);
            view.EvaluateJavascript(_javascript, null);
        }
    }
}