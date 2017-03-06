using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BrumWithMe.Services.Providers.FileUpload
{
    public interface IFileUploadProvider
    {
        void UploadCarImage(HttpPostedFileBase carAvatar, string path);
    }
}
