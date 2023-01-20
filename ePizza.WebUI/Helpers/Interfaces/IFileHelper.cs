using Microsoft.AspNetCore.Http;

namespace ePizza.WebUI.Helpers.Interfaces
{
    public interface IFileHelper
    {
        void DeleteFile(string imageUrl);
        string UploadFile(IFormFile file);
    }
}
