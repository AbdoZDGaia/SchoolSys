namespace SchoolsSys.BL.DTOs
{
    public class AttachmentDTO
    {
        public int AttachmentId { get; set; }
        public int StudentId { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
    }
}
