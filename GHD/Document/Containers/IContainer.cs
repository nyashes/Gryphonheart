﻿
namespace GHD.Document.Containers
{
    using System;
    using BlizzardApi.WidgetInterfaces;
    using Buffer;
    using Elements;
    using Flags;

    /// <summary>
    /// Interface for containers.
    /// </summary>
    public interface IContainer : ILinkedElement<IContainer>
    {

        IRegion Region { get; }

        /// <summary>
        /// Provides the cursor to the container.
        /// </summary>
        /// <param name="inEnd">Weather the cursor should be placed in the end or in the beginning.</param>
        /// <param name="cursor">The cursor object</param>
        void SetCursor(bool inEnd, ICursor cursor);

        /// <summary>
        /// Clears the cursor from the container.
        /// </summary>
        void ClearCursor();

        /// <summary>
        /// Navigates the cursor on the container.
        /// </summary>
        /// <param name="type">The type of navigation</param>
        /// <returns>Weather the navigation was successful</returns>
        bool NavigateCursor(NavigationType type);

        /// <summary>
        /// Gets the length of the contained elements. Could be a value calculated every time insert or delete is called.
        /// </summary>
        /// <returns>The length</returns>
        int GetLength();

        /// <summary>
        /// Get the current preferred width of the container.
        /// </summary>
        /// <returns>The width</returns>
        double GetWidth();

        double GetHeight();

        /// <summary>
        /// Activate the highlight within a given interval.
        /// </summary>
        /// <param name="hightLightStart">The local start interval for the highlight.</param>
        /// <param name="highLightEnd">The local end interval for the highlight.</param>
        void SetHightLight(int hightLightStart, int highLightEnd);

        /// <summary>
        /// Clear all highlights.
        /// </summary>
        void ClearHightLight();

        /// <summary>
        /// Get the <see cref="IFlags"/> applied at the cursor position.
        /// </summary>
        /// <returns></returns>
        IFlags GetCurrentFlags();

        /// <summary>
        /// Gets the <see cref="IElement"/> currently at the cursor position.
        /// </summary>
        /// <returns>The element.</returns>
        IElement GetCurrentElement();

        /// <summary>
        /// Inserts the content of the document buffer into itself and sub containers.
        /// </summary>
        /// <param name="documentBuffer">The document buffer.</param>
        /// <param name="dimensionConstraint">The constraining dimensions.</param>
        void Insert(IDocumentBuffer documentBuffer, IDimensionConstraint dimensionConstraint);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentDeleter"></param>
        void Delete(IDocumentDeleter documentDeleter);

        /// <summary>
        /// Goes to the <see cref="IElement"/> that starts at the given position. Recalculate its size and moves all elements after it, using the document buffer. This is relevant after an element changes size.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="documentBuffer"></param>
        [Obsolete("Call the RequestLayoutUpdate method provided in constructor", true)]
        void UpdateLayout(int position , IDocumentBuffer documentBuffer);

    }
}
