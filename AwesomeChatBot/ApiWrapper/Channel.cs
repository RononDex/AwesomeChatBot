﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeChatBot.ApiWrapper
{
    public abstract class Channel : Config.IConfigurationDependency
    {
        /// <summary>
        /// A reference to the ApiWrapper for internal usage
        /// </summary>
        protected ApiWrapper ApiWrapper { get; set; }

        /// <summary>
        /// A reference to the pranent server (if any)
        /// </summary>
        public Server ParentServer { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wrapper"></param>
        public Channel(ApiWrapper wrapper)
        {
            this.ApiWrapper = wrapper;
        }

        /// <summary>
        /// True is this channel is a direct message (not linked to a server)
        /// </summary>
        public abstract bool IsDirectMessageChannel { get; }

        /// <summary>
        /// The name of the channel
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The id of the channel
        /// </summary>
        public abstract string ChannelId { get; }

        /// <summary>
        /// ID used to identify this channel in the config files
        /// </summary>
        /// <returns></returns>
        public string ConfigId => $"Channel_{this.ChannelId}";

        /// <summary>
        /// The channel is usually at the end of the hirarchy
        /// </summary>
        /// <returns></returns>
        public int ConfigOrder => 1000;

        /// <summary>
        /// Sends a message asynchroniously in the current channel
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public abstract Task SendMessageAsync(SendMessage message);

        /// <summary>
        /// Gets a string to mention this channel in the chat
        /// </summary>
        /// <returns></returns>
        public abstract string GetMention();
    }
}
