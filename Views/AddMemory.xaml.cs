namespace Memory_App;

public partial class AddMemory : ContentPage
{
	public AddMemory()
	{
		InitializeComponent();
	}

    private async void OnUploadImageClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Select an image"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var imageSource = ImageSource.FromStream(() => stream);
                UploadedImage.Source = imageSource;
                UploadedImage.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Failed to upload image: " + ex.Message, "OK");
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var caption = CaptionEditor.Text;
        var imageSource = UploadedImage.Source;

        if (imageSource != null && !string.IsNullOrEmpty(caption))
        {
            // Save the memory (this could involve saving to a database or another storage mechanism)
            await DisplayAlert("Memory Saved", "Your memory has been saved successfully!", "OK");

            // Clear the fields after saving
            UploadedImage.Source = null;
            UploadedImage.IsVisible = false;
            CaptionEditor.Text = string.Empty;
        }
        else
        {
            await DisplayAlert("Error", "Please upload an image and enter a caption.", "OK");
        }
    }

}