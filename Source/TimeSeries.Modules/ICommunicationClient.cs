/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Threading.Tasks;

namespace RaaLabs.TimeSeries.Modules
{
    /// <summary>
    /// Defines a client for the module
    /// </summary>
    public interface ICommunicationClient
    {
        /// <summary>
        /// Subscribe
        /// </summary>
        /// <param name="input"></param>
        /// <param name="subscriber"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        void SubscribeTo<T>(Input input, Subscriber<T> subscriber);

        /// <summary>
        /// Register handler for Direct Methods
        /// </summary>
        /// <param name="methodHandler"></param>
        /// <returns></returns>
        void RegisterFunctionHandler(Delegate methodHandler);

        /// <summary>
        /// Send a payload as JSON to a specific <see cref="Output"/>
        /// </summary>
        /// <param name="output"><see cref="Output"/> to send to</param>
        /// <param name="payload">Payload in any type to send</param>
        /// <returns>Awaitable <see cref="Task"/></returns>
        Task SendAsJson(Output output, object payload);

        /// <summary>
        /// Send a payload as raw byte array to a specific <see cref="Output"/>
        /// </summary>
        /// <param name="output"><see cref="Output"/> to send to</param>
        /// <param name="payload">Payload in any type to send</param>
        /// <returns>Awaitable <see cref="Task"/></returns>
        Task SendRaw(Output output, byte[] payload);
    }
}