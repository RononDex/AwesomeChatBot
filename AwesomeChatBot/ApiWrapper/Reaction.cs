namespace AwesomeChatBot.ApiWrapper
{
    public abstract class Reaction
    {
        /// <summary>
        /// Internal reference to the ApiWrapper
        /// </summary>
        public ApiWrapper ApiWrapper { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wrapper"></param>
        protected Reaction(ApiWrapper wrapper)
        {
            this.ApiWrapper = wrapper;
        }

        /// <summary>
        /// Message that was reacted to
        /// </summary>
        public abstract ChatMessage Message { get; }

        /// <summary>
        /// User that is the author of the reaction
        /// </summary>
        public abstract User User { get; }

        /// <summary>
        /// The reaction content (emoji, text, ...)
        /// </summary>
        public abstract string Content { get; }
    }
}
