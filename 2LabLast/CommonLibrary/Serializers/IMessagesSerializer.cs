using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{

    public interface IMessagesSerializer
    {
        byte[] Serialize(Messages message);

        Messages Deserialize(byte[] data);
    }
}
