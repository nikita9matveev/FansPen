using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;

namespace FansPen.Web.Hubs
{
    [HubName("comments")]
    public class CommentHub : Hub {}
}
