using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using FHSDK;

namespace _Heilo
{
    [Activity(Label = "_Heilo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const string Tag = "MainActivity";

        protected override async void OnStart()
        {
            base.OnStart();
            await FHClient.Init();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var button = FindViewById<Button>(Resource.Id.button);
            var result = FindViewById<TextView>(Resource.Id.result);
            var input = FindViewById<EditText>(Resource.Id.helloTo);
            button.Click += async delegate
            {
                result.Text = "Calling Cloud.....";
                var response = await FH.Cloud("hello", "GET", null, new Dictionary<string, string>() { { "hello", input.Text } });
                if (response.Error == null)
                {
                    Log.Debug(Tag, "cloudCall - success");
                    result.Text = (string)response.GetResponseAsDictionary()["msg"];
                }
                else
                {
                    Log.Debug(Tag, "cloudCall - fail");
                    Log.Error(Tag, response.Error.Message, response.Error);
                    result.Text = response.Error.Message;
                }
            };
        }
    }
}

