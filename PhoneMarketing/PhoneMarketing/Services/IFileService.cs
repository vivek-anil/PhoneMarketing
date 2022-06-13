namespace PhoneMarketing.FileManager
{
    public interface IFileService
    {
        Task<List<string>> ReadFile(string fileName);
    }
}
