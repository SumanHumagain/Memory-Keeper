using Plugin.Media.Abstractions;
using Plugin.Media;
using Firebase.Storage;
using Firebase.Database;
using Memory_App.ViewModels;

namespace Memory_App.Views;

public partial class AddMemory : ContentPage
{
    private readonly FirebaseClient _firebaseClient;
    private readonly FirebaseStorage _firebaseStorage;
    public AddMemory()
    {
        InitializeComponent();
        _firebaseStorage = new FirebaseStorage("gs://memorykeeper-8bcf4.appspot.com");
        _firebaseClient = new FirebaseClient("https://memorykeeper-8bcf4-default-rtdb.firebaseio.com/");
    }

    /* Event handler for when the "Upload Image" button is clicked.*/

    /** <param name="sender">The object that triggered the event, typically the button that was clicked.</param>
     <param name="e">Event arguments related to the click event.</param>
    
    
     This method opens a file picker dialog to allow the user to select an image file.
    If an image is selected, it is displayed in the `UploadedImage` view.
    If any error occurs during the image upload, an alert is shown with the error message.
   **/
    //private async void OnUploadImageClicked1(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        var result = await FilePicker.PickAsync(new PickOptions
    //        {
    //            FileTypes = FilePickerFileType.Images,
    //            PickerTitle = "Select an image"
    //        });

    //        if (result != null)
    //        {
    //            var stream = await result.OpenReadAsync();
    //            UploadedImage.Source = ImageSource.FromStream(() => stream);
    //            UploadedImage.IsVisible = true;

    //            var imageUrl = await UploadImageToFirebaseStorage(stream);
    //            //var viewModel = BindingContext as AddMemoryViewModel;
    //            //viewModel.ImageUrl = imageUrl;

    //            //var stream = await result.OpenReadAsync();
    //            //var imageSource = ImageSource.FromStream(() => stream);
    //            //UploadedImage.Source = imageSource;
    //            //UploadedImage.IsVisible = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        await DisplayAlert("Error", "Failed to upload image: " + ex.Message, "OK");
    //    }
    //}
    private async void OnUploadImageClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Please select an image file",
            FileTypes = FilePickerFileType.Images
        });

        if (result != null)
        {
            var stream = await result.OpenReadAsync();
            UploadedImage.Source = ImageSource.FromStream(() => stream);
            UploadedImage.IsVisible = true;

           // var imageName = await SaveImageLocally(result);
            var viewModel = BindingContext as AddMemoryViewModel;
            viewModel.Image = $"{Guid.NewGuid()}.jpg";
            ;
            viewModel.ImageUploaded = await result.OpenReadAsync();
        }
        else
        {
            await DisplayAlert("Error", "No image selected", "OK");
        }
    }

    //private async Task<string> SaveImageLocally(FileResult file)
    //{
    //    var imageName = $"{Guid.NewGuid()}.jpg";
    //    var appDirectory = FileSystem.AppDataDirectory;
    //    var imagesPath = Path.Combine(appDirectory, "Images");

    //    if (!Directory.Exists(imagesPath))
    //    {
    //        Directory.CreateDirectory(imagesPath);
    //    }

    //    var imagePath = Path.Combine(imagesPath, imageName);
    //    var viewModel = BindingContext as AddMemoryViewModel;

    //    viewModel.ImageUploaded = await file.OpenReadAsync();

    //    //using (var stream = await file.OpenReadAsync())
    //    //using (var newStream = File.Create(imagePath))
    //    //{
    //    //    await stream.CopyToAsync(newStream);
    //    //}

    //    return imageName;
    //}


    //private async Task<string> UploadImageToFirebaseStorage(Stream imageStream)
    //{
    //    var imageName = $"{Guid.NewGuid()}.jpg";
    //    var imageUrl = await _firebaseStorage
    //        .Child("images")
    //        .Child(imageName)
    //        .PutAsync(imageStream);

    //    return imageUrl;
    //}

    /* Event handler for when the "Save" button is clicked. */

    /** <param name="sender">The object that triggered the event, typically the button that was clicked.</param>
     <param name="e">Event arguments related to the click event.</param>
    
    
    This method checks if both an image and a caption have been provided by the user.
    If both are present, it simulates saving the memory (e.g., to a database or other storage),
    displays a confirmation alert to the user, and then clears the input fields and image display.
    If either the image or caption is missing, it shows an error alert asking the user to provide both.
    ***/
    //private async void OnSaveClicked(object sender, EventArgs e)
    //{
    //    var caption = CaptionEditor.Text;
    //    var imageSource = UploadedImage.Source;

    //    if (imageSource != null && !string.IsNullOrEmpty(caption))
    //    {
    //        // Save the memory (this could involve saving to a database or another storage mechanism)
    //        await DisplayAlert("Memory Saved", "Your memory has been saved successfully!", "OK");

    //        // Clear the fields after saving
    //        UploadedImage.Source = null;
    //        UploadedImage.IsVisible = false;
    //        CaptionEditor.Text = string.Empty;
    //    }
    //    else
    //    {
    //        await DisplayAlert("Error", "Please upload an image and enter a caption.", "OK");
    //    }
    //}

}