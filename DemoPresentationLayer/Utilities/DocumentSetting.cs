namespace DemoPresentationLayer.Utilities
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
            string FolderPath=Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\Files",folderName);
            string fileName = $"{Guid.NewGuid}-{file.FileName}";
            string FilePath=Path.Combine(FolderPath,fileName);
            using var stream= new FileStream(FilePath, FileMode.Create);
            file.CopyTo(stream);
            return fileName;
        }
        public static void DeleteFile(string folderName,string fileName)
        {
            string filePath=Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName,fileName);
            if (File.Exists(filePath)) 
                File.Delete(filePath);
        }
    }
}
