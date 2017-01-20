using Android.App;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Content;
using System;
using Android.Util;
using Android.Runtime;

namespace Positioning
{
	[Activity(Label = "Positioning", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity, ILocationListener
	{
		Button button;
		//TextView tvLatitude, tvLongtitude, tvProvider;
		TextView tvLatitude;
		TextView tvLongtitude;
		TextView tvProvider;
		string locationProvider;
		LocationManager locationManager;
		EventHandler<DialogClickEventArgs> handler;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main);
			button = FindViewById<Button>(Resource.Id.myButton);
			tvLatitude = FindViewById<TextView>(Resource.Id.tvLatitude);
			tvLongtitude = FindViewById<TextView>(Resource.Id.tvLongitude);
			tvProvider = FindViewById<TextView>(Resource.Id.tvProvider);
			//if (!locationManager.IsProviderEnabled(LocationManager.GpsProvider)){
			//	AlertDialog.Builder builder = new AlertDialog.Builder(this);
			//	builder.SetTitle("定位管理")
			//		   .SetMessage("GPS尚未啟動\n是否啟用GPS..?")
			//	       .SetPositiveButton("啟用", handler)
			//		   .SetNegativeButton("不啟用", null)
			//		   .Create()
			//		   .Show();
			//}

			//handler = new EventHandler<DialogClickEventArgs>((object sender, DialogClickEventArgs e) => 
			//{
			//	Intent intent = new Intent(
			//});
			button.Click += delegate {
				button.Text = "Location Service Running..";
				locationProvider = LocationManager.GpsProvider;
				if (locationManager.IsProviderEnabled(locationProvider))
				{
					// 每一分鐘(6000毫秒)或每一公尺更新一次位置訊息
					//locationManager.RequestLocationUpdates(locationProvider, 6000, 1, this);
					//即時更新
					locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);
				} else {
					Toast.MakeText(this, locationProvider + " available. Does the device have location services enable ?", ToastLength.Short).Show();
					locationProvider = LocationManager.NetworkProvider;
					locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);
				}
			};
		}

		protected override void OnResume()
		{
			base.OnResume();
			//locationManager = GetSystemService(Context.LocationService) as LocationManager;
			locationManager = (LocationManager)GetSystemService(Context.LocationService);
			locationProvider = LocationManager.NetworkProvider; // 在室內時
			//locationProvider = LocationManager.GpsProvider; // 在室外時
		}

		public void OnLocationChanged(Location location)
		{
			//throw new NotImplementedException();
			tvLatitude.Text = "Latitude: " + location.Latitude.ToString();
			tvLongtitude.Text = "Longtitude: " + location.Longitude.ToString();
			tvProvider.Text = "Provider: " + location.Provider.ToString();
		}

		public void OnProviderDisabled(string provider)
		{
			//throw new NotImplementedException();
		}

		public void OnProviderEnabled(string provider)
		{
			//throw new NotImplementedException();
		}

		public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
		{
			//throw new NotImplementedException();
		}

		protected override void OnPause()
		{
			base.OnPause();
			locationManager.RemoveUpdates(this);
		}
	}
}

