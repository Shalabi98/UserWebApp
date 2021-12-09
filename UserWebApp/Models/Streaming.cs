using System.ComponentModel.DataAnnotations;

namespace UserWebApp.Models
{
    public enum StreamType
    {
        Online = 1,
        Offline = 2,
        Live = 3,
        Playback = 4
    }
    
    public class Streaming
    {
        [Key]
        public int StreamingId { get; set; }
    
        public int Channel { get; set; }
    
        public string Description { get; set; }
    
        public StreamingDetail Detail { get; set; }
    }
    
    public class StreamingDetail
    {
        public long Duration { get; set; }
    
        public long Size { get; set; }
    
        public StreamType Type { get; set; }
    }
}
