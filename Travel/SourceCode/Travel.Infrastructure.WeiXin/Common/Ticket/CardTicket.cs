using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.WeiXin.Common.Ticket
{
    public class CardTicket : Ticket
    {
        protected override string type { get { return "wx_card"; } }

        public CardTicket(Credential credential)
            : base(credential)
        {
        }

        public string CreateSignature(string code, string timestamp, string card_id, string api_ticket, string nonce_str)
        {
            var sha1BeforeString = string.Format("{0}{1}{2}{3}{4}", timestamp, card_id, code);
            return string.Empty;
        }
    }
}
