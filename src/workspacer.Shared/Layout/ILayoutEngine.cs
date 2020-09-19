﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workspacer
{
    /// <summary>
    /// ILayoutEngine describes the interface for layout engines
    /// which are used to describe the way windows should be organized inside of a workspace
    /// </summary>
    public interface ILayoutEngine
    {
        /// <summary>
        /// the name of the layout engine
        /// </summary>
        string Name { get; }

        /// <summary>
        /// calculate the desired layout of the workspace
        /// </summary>
        /// <param name="windows">set of windows to be organized</param>
        /// <param name="spaceWidth">width of the available space for layout</param>
        /// <param name="spaceHeight">height of the available space for layout</param>
        /// <returns>desired locations for each of the specified windows</returns>
        IEnumerable<IWindowLocation> CalcLayout(IEnumerable<IWindow> windows, int spaceWidth, int spaceHeight);

        /// <summary>
        /// shrink the primary area of the layout engine.
        /// the behavior of this method is implementation defined
        /// </summary>
        void ShrinkPrimaryArea();

        /// <summary>
        /// expand the primary area of the layout engine.
        /// the behavior of this method is implementation defined
        /// </summary>
        void ExpandPrimaryArea();

        /// <summary>
        /// reset the primary area of the layout engine.
        /// the behavior of this method is implementation defined
        /// </summary>
        void ResetPrimaryArea();

        /// <summary>
        /// increment the number of windows in the layout's primary area
        /// the behavior of this method is implementation defined
        /// </summary>
        void IncrementNumInPrimary();

        /// <summary>
        /// decrement the number of windows in the layout's primary area
        /// the behavior of this method is implementation defined
        /// </summary>
        void DecrementNumInPrimary();

        /// <summary>
        /// flip primary area from left to right side
        /// the behavior of this method could be implementation defined
        /// </summary>
        void FlipPrimaryArea();
    }
}
