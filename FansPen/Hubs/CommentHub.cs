using FansPen.Domain.Models;
using FansPen.Domain.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;

namespace FansPen.Web.Hubs
{
    [HubName("comments")]
    public class CommentHub : Hub {}
}
