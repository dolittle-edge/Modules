/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace RaaLabs.TimeSeries.Modules
{
    /// <summary>
    /// Defines a system that can handle all inputs
    /// </summary>
    public interface IInputHandlers
    {
        /// <summary>
        /// Initialize all input handlers
        /// </summary>
        void Initialize();
    }
}