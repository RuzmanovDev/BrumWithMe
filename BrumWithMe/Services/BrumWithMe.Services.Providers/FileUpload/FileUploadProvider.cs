using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BrumWithMe.Services.Providers.FileUpload
{
    public class FileUploadProvider : IFileUploadProvider
    {
        public void UploadCarImage(HttpPostedFileBase carAvatar, string fileName)
        {
            Guard.WhenArgument(carAvatar, nameof(carAvatar)).IsNull().Throw();
            Guard.WhenArgument(fileName, nameof(fileName)).IsNullOrEmpty().Throw();

            carAvatar.SaveAs(fileName);
        }
    }
}
