//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : PDIParserException.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/13/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains an exception class thrown by the parsers
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/21/2004  EFW  Created the code
// 03/17/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System;
using System.Runtime.Serialization;
using System.Security;

namespace EWSoftware.PDI.Parser
{
    /// <summary>
    /// This exception class is thrown by the parser if it encounters an unrecoverable error
    /// </summary>
    [Serializable]
    public class PDIParserException : Exception
    {
        #region Private data members
        //=====================================================================

        private int lineNbr;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This contains the line number in the data stream at which the error occurred
        /// </summary>
        public int LineNumber
        {
            get { return lineNbr; }
        }
        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are seven overloads for the constructor</overloads>
        public PDIParserException()
        {
        }

        /// <summary>
        /// This constructor takes a message string
        /// </summary>
        /// <param name="message">The exception message</param>
        public PDIParserException(string message) : base(message)
        {
        }

        /// <summary>
        /// This constructor takes a message string and an inner exception
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="inner">The inner exception</param>
        public PDIParserException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// This constructor takes a line number
        /// </summary>
        /// <param name="line">The line number in the data stream at which the error occurred</param>
        public PDIParserException(int line)
        {
            lineNbr = line;
        }

        /// <summary>
        /// This constructor takes a line number and a message string
        /// </summary>
        /// <param name="line">The line number in the data stream at which the error occurred</param>
        /// <param name="message">The exception message</param>
        public PDIParserException(int line, string message) : base(message)
        {
            lineNbr = line;
        }

        /// <summary>
        /// This constructor takes a line number, message string, and an inner exception
        /// </summary>
        /// <param name="line">The line number in the data stream at which the error occurred</param>
        /// <param name="message">The exception message</param>
        /// <param name="inner">The inner exception</param>
        public PDIParserException(int line, string message, Exception inner) : base(message, inner)
        {
            lineNbr = line;
        }

        /// <summary>
        /// Deserialization constructor for use with <see cref="System.Runtime.Serialization.ISerializable"/>
        /// </summary>
        /// <param name="info">The serialization info object</param>
        /// <param name="context">The streaming context object</param>
        protected PDIParserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if(info != null)
                lineNbr = (int)info.GetValue("LineNumber", typeof(int));
        }
        #endregion

        #region Method overrides
        //=====================================================================

        /// <summary>
        /// This implements the <see cref="System.Runtime.Serialization.ISerializable"/> interface and adds the
        /// line number to the serialization information.
        /// </summary>
        /// <param name="info">The serialization info object</param>
        /// <param name="context">The streaming context</param>
        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(info != null)
            {
                base.GetObjectData(info, context);
                info.AddValue("LineNumber", lineNbr);
            }
        }
        #endregion
    }
}
