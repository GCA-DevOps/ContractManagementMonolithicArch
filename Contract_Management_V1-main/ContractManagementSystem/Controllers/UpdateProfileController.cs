using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using ContractManagementSystem.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;


namespace ContractManagementSystem.Controllers
{
    public class UpdateProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UpdateProfileController(UserManager<AppUser> userManager) : base()
        {
            _userManager = userManager;
        }

        public IActionResult AdminProfile()
        {
            return View();
        }

        public IActionResult VendorIndividualProfile()
        {
            return View();
        }

        public IActionResult VendorCompanyProfile()
        {
            return View();
        }

        //Users Profile
        public async Task<IActionResult> UsersProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new AppUser
            {
                ProfilePicturePath = user.ProfilePicturePath,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfilePicture(IFormFile profileImage)
        {
            if (profileImage != null && profileImage.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await profileImage.CopyToAsync(memoryStream);
                    memoryStream.Position = 0; // Reset the stream position

                    // Load the image
                    using (var image = Image.Load(memoryStream))
                    {
                        // Check if the image is square
                        if (image.Width != image.Height)
                        {
                            // If not, crop it to make it square
                            var squareSize = Math.Min(image.Width, image.Height);
                            var x = (image.Width - squareSize) / 2;
                            var y = (image.Height - squareSize) / 2;

                            image.Mutate(ctx => ctx.Crop(new Rectangle(x, y, squareSize, squareSize)));
                        }

                        // Save the cropped image to a MemoryStream
                        using (var croppedStream = new MemoryStream())
                        {
                            image.SaveAsJpeg(croppedStream);
                            var imageBytes = croppedStream.ToArray();

                            var user = await _userManager.GetUserAsync(User);
                            user.ProfilePictureByteArray = imageBytes; // Save the cropped byte array
                            await _userManager.UpdateAsync(user);

                            return RedirectToAction("UsersProfile");
                        }
                    }
                }
            }

            // If no file was uploaded, return to the profile page without changes
            return RedirectToAction("UsersProfile");
        }


        public async Task<IActionResult> GetProfilePicture()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.ProfilePictureByteArray != null)
            {
                return File(user.ProfilePictureByteArray, "image/jpeg");
            }

            //the default image is located in the wwwroot/images folder
            var defaultImagePath = Path.Combine("Project Images", "defaultProfile.jpeg");
            var defaultImage = System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", defaultImagePath));

            // Return a default image if the user doesn't have a profile picture
            return File(defaultImage, "image/jpeg");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProfilePicture()
        {
            var user = await _userManager.GetUserAsync(User);
            user.ProfilePictureByteArray = null; // Set the byte array to null to delete the picture
            await _userManager.UpdateAsync(user);

            return Ok(); // Return a success status
        }
    }
}
