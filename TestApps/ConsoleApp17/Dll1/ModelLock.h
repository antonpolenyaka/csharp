/*****************************************************************************/
/* Triangle MicroWorks, Inc.                         Copyright (c) 1994-2014 */
/*****************************************************************************/
/*                                                                           */
/*                                                                           */
/* This file is the property of:                                             */
/*                                                                           */
/*                       Triangle MicroWorks, Inc.                           */
/*                      Raleigh, North Carolina USA                          */
/*                      <www.TriangleMicroWorks.com>                         */
/*                    <support@trianglemicroworks.com>                       */
/*                                                                           */
/* This Source Code and the associated Documentation contain proprietary     */
/* information of Triangle MicroWorks, Inc. and may not be copied or         */
/* distributed in any form without the written permission of Triangle        */
/* MicroWorks, Inc.  Copies of the source code may be made only for backup   */
/* purposes.                                                                 */
/*                                                                           */
/* Your License agreement may limit the installation of this source code to  */
/* specific products.  Before installing this source code on a new           */
/* application, check your license agreement to ensure it allows use on the  */
/* product in question.  Contact Triangle MicroWorks for information about   */
/* extending the number of products that may use this source code library or */
/* obtaining the newest revision.                                            */
/*                                                                           */
/*****************************************************************************/


#pragma once

#include "IModelLock.h"

namespace TMW
{
    namespace Common
    {
        /// <summary>
        /// This is used to Lock the Model
        /// </summary>
        public ref class ModelLock
        {
        public:

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="model">Model to Lock</param>
            ModelLock(IModelLock^ model) : m_modelLock(model)
            {
                model->Lock();
            }

            /// <summary>
            /// Destructor
            /// </summary>
            virtual ~ModelLock() { this->!ModelLock(); }

            /// <summary>
            /// Finalizer
            /// </summary>
            !ModelLock()
            {
                if (m_modelLock != nullptr)
                {
                    m_modelLock->UnLock();
                    m_modelLock = nullptr;
                }
            }
        private:
            ModelLock();
            IModelLock^ m_modelLock;
        };
    }
}
