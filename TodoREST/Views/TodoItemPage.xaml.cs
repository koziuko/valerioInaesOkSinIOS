using System;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;
using GeoTagger.Services;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using GeoTagger.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace TodoREST
{
	public partial class TodoItemPage : ContentPage
	{
		bool isNewItem;



        public TodoItemPage()
        {
            InitializeComponent();
            
        }

        public TodoItemPage (bool isNew = false)
		{
			InitializeComponent ();
			isNewItem = isNew;
		}

        

        async void OnBuscarActivated(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await App.TodoManager.GetTasksAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnSaveActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			await App.TodoManager.SaveTaskAsync (todoItem, isNewItem);
			await Navigation.PopAsync ();
		}

		async void OnDeleteActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			await App.TodoManager.DeleteTaskAsync (todoItem);
			await Navigation.PopAsync ();
		}

		void OnCancelActivated (object sender, EventArgs e)
		{
			Navigation.PopAsync ();
		}

		void OnSpeakActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			App.Speech.Speak (todoItem.Name  + " " + todoItem.Notes);
		}

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;

            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(todoItem);
            slPerfil.Children.Add(GenerateQR(jsonData));
        }

        ZXingBarcodeImageView GenerateQR(string codeValue)
        {
            var qrCode = new ZXingBarcodeImageView
            {
                BarcodeFormat = BarcodeFormat.QR_CODE,
                BarcodeOptions = new QrCodeEncodingOptions
                {
                    Height = 350,
                    Width = 350
                },
                BarcodeValue = codeValue,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            // Workaround for iOS
            qrCode.WidthRequest = 350;
            qrCode.HeightRequest = 350;
            return qrCode;
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable
                || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No Camera available", "Ok");
                return;
            }

            //var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            //{

            //    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
            //});

            var photo = await CrossMedia.Current.TakePhotoAsync(

            new StoreCameraMediaOptions
            {

                SaveToAlbum = true,

            });


            PathLabel.Text = photo.AlbumPath;

            MainImage.Source = ImageSource.FromStream(() =>
            {
                var stream = photo.GetStream();
                photo.Dispose();
                return stream;


            });

            //if (photo != null)
            //{
            //    Position position = await CrossGeolocator.Current.GetPositionAsync();
            //    EntryService entryService = new EntryService();

            //    var imageName = await entryService.SaveImage(photo.GetStream());
            //    await entryService.SaveEntryAsync(new GeoTagger.Model.GeoTaggerEntry(FotoText.Text, imageName,
            //                                               position.Latitude, position.Longitude));

            //    //PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });


            //}
            //else {
            //    var a = "!s";
            //}
        }



        private async void ShowTags(object sender, EventArgs e)
        {
            Position position = await CrossGeolocator.Current.GetPositionAsync();
            await Navigation.PushAsync(new Tags(position.Latitude, position.Longitude));
        }
    }
}
