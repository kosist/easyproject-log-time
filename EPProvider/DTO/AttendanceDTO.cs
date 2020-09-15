using RestSharp.Deserializers;

namespace EPProvider.DTO
{
    [DeserializeAs (Name = "easy_attendance")]
    public class AttendanceDTO
    {
        [DeserializeAs(Name = "user_id")]
        public int UserId { get; set; }
    }
}