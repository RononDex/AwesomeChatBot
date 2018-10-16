using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeChatBot.ApiWrapper
{
    public abstract class Attachement
    {
        /// <summary>
        /// A reference to the ApiWrapper for internal usage
        /// </summary>
        protected ApiWrapper ApiWrapper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wrapper"></param>
        public Attachement(ApiWrapper wrapper)
        {
            this.ApiWrapper = wrapper;
        }

        /// <summary>
        /// The name of the attachement
        /// </summary>
        public abstract string Name { get; set; }

        /// <summary>
        /// The byte contents of the attachement
        /// </summary>
        public abstract byte[] Content { get; set; }
    }
}
