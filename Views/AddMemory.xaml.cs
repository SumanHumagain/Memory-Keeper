namespace Memory_App.Views;

public partial class AddMemory : ContentPage
{
	public AddMemory()
	{
		InitializeComponent();
	}

    /* Event handler for when the "Upload Image" button is clicked.*/

    /** <param name="sender">The object that triggered the event, typically the button that was clicked.</param>
     <param name="e">Event arguments related to the click event.</param>
    
    
     This method opens a file picker dialog to allow the user to select an image file.
    If an image is selected, it is displayed in the `UploadedImage` view.
    If any error occurs during the image upload, an alert is shown with the error message.
   **/
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

    /* Event handler for when the "Save" button is clicked. */

    /** <param name="sender">The object that triggered the event, typically the button that was clicked.</param>
     <param name="e">Event arguments related to the click event.</param>
    
    
    This method checks if both an image and a caption have been provided by the user.
    If both are present, it simulates saving the memory (e.g., to a database or other storage),
    displays a confirmation alert to the user, and then clears the input fields and image display.
    If either the image or caption is missing, it shows an error alert asking the user to provide both.
    ***/
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