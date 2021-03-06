﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeChatBot.ApiWrapper
{
    public abstract class User : Config.IConfigurationDependency
    {
        /// <summary>
        /// A reference to the ApiWrapper for internal usage
        /// </summary>
        public ApiWrapper ApiWrapper { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wrapper"></param>
        protected User(ApiWrapper wrapper)
        {
            ApiWrapper = wrapper;
        }

        /// <summary>
        /// An ID used by the API to identify the user
        /// </summary>
        public abstract string UserID { get; }

        /// <summary>
        /// The username in the chat program
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Some chat programs have nicknames and unique user names used to
        /// uniquely identify a user.
        /// </summary>
        public abstract string UniqueUserName { get; }

        /// <summary>
        /// Roles of the user
        /// </summary>
        public abstract IReadOnlyList<UserRole> Roles { get; }

        /// <summary>
        /// The Id that is used to identify this object in config files
        /// </summary>
        public string ConfigId => $"User_{this.UserID}";

        /// <summary>
        /// A user setting is usually server dependent, so in this case we want it to be higher than server, but lower than channel
        /// </summary>
        public int ConfigOrder => 100;

        /// <summary>
        /// Get a string that is used to mention this user
        /// </summary>
        public abstract string GetMention();

        /// <summary>
        /// Adds the role with the given name to the user
        /// </summary>
        /// <param name="roleName">the name of the role to add</param>
        public abstract Task AddRoleAsync(string roleName);

        /// <summary>
        /// Removes the role with the given name from the user
        /// </summary>
        /// <param name="roleName">the name of the role to remove</param>
        public abstract Task RemoveRoleAsync(string roleName);
    }
}
