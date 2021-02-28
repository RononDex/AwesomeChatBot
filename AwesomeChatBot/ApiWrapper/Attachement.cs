namespace AwesomeChatBot.ApiWrapper
{
    public abstract class Attachment
    {
        /// <summary>
        /// A reference to the ApiWrapper for internal usage
        /// </summary>
        protected ApiWrapper ApiWrapper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wrapper"></param>
        protected Attachment(ApiWrapper wrapper)
        {
            ApiWrapper = wrapper;
        }

        /// <summary>
        /// The name of the attachment
        /// </summary>
        public abstract string Name { get; set; }

        /// <summary>
        /// The byte contents of the attachment
        /// </summary>
        public abstract byte[] Content { get; set; }
    }
}
