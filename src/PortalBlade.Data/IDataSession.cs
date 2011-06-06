using System;

namespace PortalBlade.Data
{
    public enum SessionType
    {
        ReadOnly,
        AutoFlush
    }

    /// <summary>
    /// Unit of work session to the data store
    /// </summary>
    public interface IDataSession : IDisposable
    {
        /// <summary>
        /// Tells what flushing type this session is using
        /// </summary>
        SessionType SessionType { get; }

        /// <summary>
        /// Flushes the changes to the data store
        /// </summary>
        void Flush( );
    }

}