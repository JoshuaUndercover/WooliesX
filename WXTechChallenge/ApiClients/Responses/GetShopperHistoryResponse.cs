﻿using System.Collections.Generic;

namespace WXTechChallenge.ApiClients.Responses
{
    public class GetShopperHistoryResponse
    {
        public long CustomerId { get; set; }
        public List<GetProductListResponse> Products { get; set; }
    }
}
