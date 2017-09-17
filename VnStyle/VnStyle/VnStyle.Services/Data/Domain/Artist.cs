namespace VnStyle.Services.Data.Domain
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Seq { get; set; }
        public long ImageId { get; set; }
        public bool ShowOnHompage { get; set; }
    }
}