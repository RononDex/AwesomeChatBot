using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeChatBot.ApiWrapper
{
    public abstract class User
    {
        /// <summary>
        /// A reference to the ApiWrapper for internal usage
        /// </summary>
        public ApiWrapper ApiWrapper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wrapper"></param>
        public User(ApiWrapper wrapper)
        {
            this.ApiWrapper = wrapper;
        }

        /// <summary>
        /// An ID used by the API to identify the user
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Get a string that is used to mention this user
        /// </summary>
        /// <returns></returns>
        public abstract string GetMention();
    }
}
