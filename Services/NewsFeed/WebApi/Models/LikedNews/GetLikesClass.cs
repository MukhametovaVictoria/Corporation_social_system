using System;
using System.Collections.Generic;

namespace WebApi.Models.LikedNews
{
    public class GetLikesClass
    {
        public Guid CurrentEmployeeId { get; set; }
        public List<Guid> NewsIds { get; set; }
    }
}
