using Morpheus.Common.Messages;
using Morpheus.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Morpheus.Common.Extensions
{
    public static class MessageErroExtensions
    {
        public static string GetMessage(this MessageNotification messageNotification)
        {
            return Messages_ENG.ResourceManager.GetString(messageNotification.ToString());
        }
    }
}
