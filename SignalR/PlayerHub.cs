using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

using muagicungban.Repositories;
using muagicungban.Entities;

namespace SignalR
{
    public class Player : Hub
    {
        private ItemsRepository itemsRepository = new ItemsRepository(muagicungban.Connection.connectionString);
        private BidsRepository bidsRepository = new BidsRepository(muagicungban.Connection.connectionString);

        public void Send(string message)
        {
            // Call the addMessage method on all clients            
            Clients.All.showMessage(message);
        }

        public void placebid(string id, string price)
        {
            long itemID = long.Parse(id);
            decimal curPrice = decimal.Parse(price);

            // REQUIRE LOGIN FOR DO THIS ACTION
            if (Context.User.Identity.IsAuthenticated)
            {
                Clients.Caller.showMessage("Đang xử lý...", 30000);

                Item item = itemsRepository.Items.Single(i => i.ItemID == itemID);

                // To begin, we must check whether the remaining time of this auction are greater than 0
                double time = (item.EndDate - DateTime.Now).TotalSeconds;
                if (time <= 0)
                {
                    Clients.Caller.showMessage("Phiên đấu đã kết thúc!!!", 3000);
                    //Clients.All.updateItem(id, item.CurPrice, item.CurPrice.ToString("#,### VND"), 0, Context.User.Identity.Name,DateTime.Now.ToString());
                    return;
                }

                // Second we must prevent bid from owner
                if (item.OwnerID == Context.User.Identity.Name)
                {
                    Clients.Caller.showMessage("Chủ sở hữu không thể thực hiện chức năng này!", 3000);
                    return;
                }

                // Third, prevent bid from bidder who had the highest amount for this item
                if (bidsRepository.Bids.Any(_bid => _bid.ItemID == itemID))
                {
                    Bid lastBidder = new Bid();
                    lastBidder = bidsRepository.Bids.Where(b => b.ItemID == itemID)
                                                            .OrderByDescending(b => b.Amount)
                                                            .First();
                    // prevent the last bidder
                    if (Context.User.Identity.Name == lastBidder.BidderID.ToString())
                    {
                        Clients.Caller.showMessage("Bạn đang là người đặt giá cao nhất !!!", 3000);
                        return;
                    }

                    // prevent the bidder with the smaller than the last price
                    if (curPrice <= lastBidder.Amount)
                    {
                        Clients.Caller.showMessage("Giá đưa ra nhỏ hơn giá hiện tại!!!", 3000);
                        return;
                    }
                }

                // Finally, prevent bid greater than max price
                if (curPrice > item.MaxPrice)
                {
                    Clients.Caller.showMessage("Giá đưa ra không được lớn hơn mức giá tối đa", 3000);
                    return;
                }

                // Otherwise, place bid
                Bid bid = new Bid();
                bid.ItemID = itemID;
                bid.BidderID = Context.User.Identity.Name;
                bid.DatePlace = DateTime.Now;
                bid.Amount = curPrice;
                bidsRepository.Add(bid);

                time = (item.EndDate - DateTime.Now).TotalSeconds;

                //Clients.All.updateItem(id, price, time, Context.User.Identity.Name);
                Clients.All.updateItem(id, curPrice, curPrice.ToString("#,### VNĐ"), time, Context.User.Identity.Name, bid.DatePlace.ToString("dd/MM/yyyy hh:mm:ss"));

                Clients.Caller.showMessage("Xong", 1500);
            }
            else
            {
                Clients.Caller.showMessage("Vui lòng đăng nhập!!!", 3000);
            }
        }


    }
}