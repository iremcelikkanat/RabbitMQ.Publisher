using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbitmq.MassTransit.Shared.Messages
{
    public interface IMessages
    {
        public string Text { get; set; }
    }
}
