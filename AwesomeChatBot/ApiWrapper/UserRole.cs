namespace AwesomeChatBot.ApiWrapper
{
    public abstract class UserRole : Config.IConfigurationDependency
    {
        /// <summary>
        /// Protected Wrapper for internal access
        /// </summary>
        /// <value></value>
        protected ApiWrapper Wrapper { get; }

        /// <summary>
        /// ApiWrapper for reference
        /// </summary>
        /// <param name="wrapper"></param>
        public UserRole(ApiWrapper wrapper)
        {
            Wrapper = wrapper;
        }

        /// <summary>
        /// A unique id to identify the role
        /// </summary>
        /// <value></value>
        public abstract string RoleId { get; }

        /// <summary>
        /// The display name of the role
        /// </summary>
        /// <value></value>
        public abstract string Name { get; }

        /// <summary>
        /// True if the role has admin (highest) privileges
        /// </summary>
        public abstract bool IsAdmin { get; }

        /// <summary>
        /// Get a string to mention this role
        /// </summary>
        /// <value></value>
        public abstract string GetMention();

        /// <summary>
        /// Id in config files
        /// </summary>
        /// <value></value>
        public string ConfigId => $"Role_{RoleId}";

        /// <summary>
        /// Higher than server, but lower than users and channels
        /// </summary>
        public int ConfigOrder => 20;
    }
}