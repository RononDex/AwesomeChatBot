using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeChatBot.Context
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
    }
}
