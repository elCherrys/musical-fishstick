using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using App_de_Android.Droid;  // Replace with your actual namespace

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace App_de_Android.Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // Remove the underline for Android Entry
                Control.Background = null;
            }
        }
    }
}
