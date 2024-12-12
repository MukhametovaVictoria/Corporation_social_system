namespace FrontEnd.Models.Picture
{
    public class UpdatingPictureModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public byte[] Data { get; set; }
        public string ByteAsString { get; set; }
        public Guid NewsId { get; set; }
        public long Size { get; set; }
    }
}
