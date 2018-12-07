namespace AwesomeChatBot.ApiWrapper
{
    /// <summary>
    /// Value object used to send attachements
    /// </summary>
    public class SendAttachement : Attachement
    {
        public SendAttachement() : base(null)
        {
            
        }

        /// <summary>
        /// The filename of the attachement
        /// </summary>
        /// <value></value>
        public override string Name { get; set; }
        
        /// <summary>
        /// The byte[] Content of the fileattachement
        /// </summary>
        /// <value></value>
        public override byte[] Content { get; set; }
    }
}