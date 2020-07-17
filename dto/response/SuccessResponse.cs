using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wiki_server.dto.response
{
    public class SuccessResponse
    {
        static SuccessResponse success = new SuccessResponse();
        public static SuccessResponse OK() {
            return success;
        }
    }
}
