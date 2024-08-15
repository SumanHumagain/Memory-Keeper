using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Memory_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Memory_App.Services;
using Firebase.Auth.Providers;
using Firebase.Storage;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Memory_App.Global;

namespace Memory_App.ViewModels
{
    public class AddMemoryViewModel : ObservableObject
    {
        private MemoryDetail _memoryDetail;
        private readonly FirebaseClient _firebaseClient;
        private readonly FirebaseStorage _firebaseStorage;
        public string Location { get; set; }
        public string Caption { get; set; }
        public string Image { get; set; }
        public Stream ImageUploaded { get; set; }

        private ImageSource _uploadedImage; 
        public Stream ims; 

        private MediaFile _mediaFile; 
        public ICommand UploadImageCommand { get; }
        public ImageSource UploadedImage
        {
            get => _uploadedImage;
            set
            {
                _uploadedImage = value;
                OnPropertyChanged();
            }
        }

        public IAsyncRelayCommand AddMemoryCommand { get; }
        public AddMemoryViewModel()
        {
            UploadImageCommand = new Command(async () => await UploadImageAsync());
            AddMemoryCommand = new AsyncRelayCommand(AddMemory);
            _firebaseStorage = new FirebaseStorage("memorykeeper-8bcf4.appspot.com");
            _firebaseClient = new FirebaseClient("https://memorykeeper-8bcf4-default-rtdb.firebaseio.com/");
        }

        private async Task UploadImageAsync()
        {
            //var result = await FilePicker.PickAsync(new PickOptions
            //{
            //    FileTypes = FilePickerFileType.Images,
            //    PickerTitle = "Select an image"
            //});
            await CrossMedia.Current.Initialize();
            _mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium
            });

            //if (result != null)
            //{
            //    ims = await result.OpenReadAsync();
            //    _mediaFile = ImageSource.FromStream(() => ims.GetStream());
            //    ims = ImageSource.FromStream(() => ims);
            //    //UploadedImage.Source = imageSource;
            //    //UploadedImage.IsVisible = true;
            //}


            //await CrossMedia.Current.Initialize();

            //if (!CrossMedia.Current.IsPickPhotoSupported)
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", "Photo picking is not supported", "OK");
            //    return;
            //}


            //_mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            //{
            //    PhotoSize = PhotoSize.Medium
            //});

            //    if (_mediaFile == null)
            //        return;

            //    UploadedImage = ImageSource.FromStream(() => _mediaFile.GetStream());
        }

        private async Task AddMemory()
        {

            var imageUrl = await UploadImageToFirebaseStorage(ImageUploaded);
            var memoryDetail = new MemoryDetail
            {
                Email = GlobalVariable.Email,
                Location = Location,
                Caption = Caption,
                Image = imageUrl,
            };
            await SaveUserToFirebase(memoryDetail);
            //await AlertDialog("Success", "Image uploaded successfully", "OK");
            await Shell.Current.GoToAsync($"///{nameof(Views.HomePage)}");

        }

        private async Task SaveUserToFirebase(MemoryDetail memoryDetail)
        {
            await _firebaseClient
                .Child("MemoryDetail")
                .PostAsync(memoryDetail);
        }

        private async Task<string> UploadImageToFirebaseStorage(Stream imageStream)
        {
            var imageName = $"{Guid.NewGuid()}.jpg";
            var imageUrl = await _firebaseStorage
                .Child("images")
                .Child(imageName)
                .PutAsync(imageStream);

            return imageUrl;
        }
    }
}
