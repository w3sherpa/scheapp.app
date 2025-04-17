using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace scheapp.app.Helpers
{
    public class ScheAppViewUpdateHub:Hub
    {
        //private readonly IMemoryCache _memoryCache;
        //public ScheAppViewUpdateHub(IMemoryCache memoryCache)
        //{
        //    _memoryCache = memoryCache;
        //}
        // Mapping between user IDs and connection IDs (you might need to expand this logic)
        //private static ConcurrentDictionary<string, string> userConnections = new ConcurrentDictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            //var userId = Context.UserIdentifier; // Or use custom logic
            //userConnections[userId] = Context.ConnectionId;
            //if (!_memoryCache.TryGetValue(userId, out DateTime cacheValue))
            //{
            //    _memoryCache.Set(userId, Context.ConnectionId);
            //}

            //var userName = GetUserName(Context); // get the username of the connected user

            await Groups.AddToGroupAsync(Context.ConnectionId, Context.UserIdentifier);
            await base.OnConnectedAsync();
        }

        //public override Task OnDisconnectedAsync(Exception exception)
        //{
        //    _memoryCache.Remove(Context.UserIdentifier);
        //    return base.OnDisconnectedAsync(exception);
        //}
    }
}
