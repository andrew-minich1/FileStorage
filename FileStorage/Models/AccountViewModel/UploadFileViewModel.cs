using FileStorage.Infrastructura.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileStorage.Models.AccountViewModel
{
    public class UploadFileViewModel
    {
        [FileSize(102400)]
        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase File { get; set; }
    }
}