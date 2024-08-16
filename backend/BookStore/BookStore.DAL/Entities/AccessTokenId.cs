using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Entities
{
    public class AccessTokenId
    {
        public int Id { get; set; }

        public Guid AccessTokenGUID { get; set; }

        #region Navigation Properties

        public User User { get; set; }

        public Guid UserId { get; set; }

        #endregion


    }
}
