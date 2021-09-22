using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Services
{
    public interface IStorageService
    {
        Task<string> Upload(IFormFile formFile, string FileName);
    }

}
