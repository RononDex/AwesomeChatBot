namespace AwesomeChatBot.ApiWrapper
{
    /// <summary>
    /// Value object used to send attachments
    /// </summary>
    public class SendAttachment : Attachment
    {
        public SendAttachment() : base(null)
        {

        }

        /// <summary>
        /// The filename of the attachment
        /// </summary>
        public override string Name { get; set; }

        /// <summary>
        /// The byte[] Content of the file attachment
        /// </summary>
        public override byte[] Content { get; set; }
    }
}