using NightlyCode.Discord.Data.Embeds;

namespace NightlyCode.Discord.Data.Channels {
    public class CreateRichMessageBody : CreateMessageBody {
        public Embed Embed { get; set; }
    }
}