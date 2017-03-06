using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BrumWithMe.Services.Providers.FileUpload
{
    public class FileUploadProvider : IFileUploadProvider
    {
        public void UploadCarImage(HttpPostedFileBase carAvatar, string path)
        {
            Guard.WhenArgument(carAvatar, nameof(carAvatar)).IsNull().Throw();
            Guard.WhenArgument(path, nameof(path)).IsNullOrEmpty().Throw();

            carAvatar.SaveAs(path);
        }
    }
}
