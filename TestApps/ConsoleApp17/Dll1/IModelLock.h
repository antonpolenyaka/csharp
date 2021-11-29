#pragma once

namespace TMW
{
    namespace Common
    {
        /// <summary>
        /// Model Lock
        /// </summary>
        public interface class IModelLock
        {
            /// <summary>
            /// Lock Model
            /// </summary>
            void Lock();
            /// <summary>
            /// UnLock Model
            /// </summary>
            void UnLock();
        };
    }
}
