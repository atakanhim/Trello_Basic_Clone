using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trelloClone.Application.Contracts
{
    public class CreateUserResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
