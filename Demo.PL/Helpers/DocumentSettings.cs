using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file,string FolderName)
        {
            //1. get located folder path
            //Directory.GetCurrentDirectory()+ "\\wwwroot\\Files\\"+ FolderName;
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            //2. get filename and make it unique
            string FileName=$"{Guid.NewGuid()}{file.FileName}";
            //3.get file path[folder path + filename]
            string FilePath = Path.Combine(FolderPath, FileName);
            //4. save file as streams
            using var Fs = new FileStream(FilePath, FileMode.Create); //lw create wmn8er Guid hyms7 elsora wybdlha wqt el edit
            file.CopyTo(Fs);

            //5.return file name
            return FileName;
        }
        public static void DeleteFile(string FileName,string FolderName)
        {
            //1.get file path
            string FilePath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",FolderName, FileName);
            //2.check if file exists
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }

    }
}
