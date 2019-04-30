using SqsLibraries.Common.Utilities.ResponseObjects;
using SqsLibraries.Common.Utilities.ResponseObjects.Enums;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartData.Common.Exceptions
{
    public class ResponseValidationException : Exception
    {
        private List<ResponseMessage> messages;

        public List<ResponseMessage> Messages
        {
            get
            {
                return messages;
            }
            set
            {
                if (value == null || value.Where(m => m.MessageType == MessageType.ERROR).Count() == 0)
                {
                    this.messages = new List<ResponseMessage>();
                }

                this.messages = value.Where(m => m.MessageType == MessageType.ERROR).ToList();
            }
        }

        public ResponseValidationException(List<ResponseMessage> messages)
        {
            this.Messages = messages;
        }

        public ResponseValidationException(ResponseMessage message)
        {
            this.Messages = new List<ResponseMessage> { message };
        }
    }
}
