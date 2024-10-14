namespace API_Application.Core.Database.InMemory
{
    public class ImagesMemory
    {
        public Dictionary<string, Image> ImagesInMem { get; set; }
        public ImagesMemory()
        {
            ImagesInMem = new Dictionary<string, Image>();
        }
    }
}
