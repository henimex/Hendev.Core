﻿namespace Core.Application.Pipelines.Caching;

public interface ICacheRequest
{
    //TODO:: Check Area Naming Changed
    string CacheKey { get; }
    bool BypassCache { get; }
    string? CacheGroupKey { get; }
    TimeSpan? SlidingExpiration { get; }
}