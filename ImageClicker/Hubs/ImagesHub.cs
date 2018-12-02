using System.Threading.Tasks;
using ImageClicker.Models;
using Microsoft.AspNetCore.SignalR;

namespace ImageClicker.Hubs
{
    public class ImagesHub : Hub
    {
        public async Task SendImage(ImageToClick image)
        {
            await Clients.All.SendAsync("ReceiveImage", image);
        }
    }
}
