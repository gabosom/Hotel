using HotelEden.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelEden.App_Start
{
    public class HotelContextInitializer : DropCreateDatabaseIfModelChanges<HotelContext>
    {
        protected override void Seed(HotelContext context)    
        {        
            // Use the context to seed the db.    

            //Adding roomtypes
            List<RoomType> roomTypes = new List<RoomType>{
                                           new RoomType{
                                               Description = "Sencillo",
                                               Rate = 10,
                                               MaxGuestNo = 1
                                           },
                                           new RoomType{
                                               Description = "Doble",
                                               Rate = 20,
                                               MaxGuestNo = 2
                                           },
                                           new RoomType{
                                               Description = "Triple",
                                               Rate = 30,
                                               MaxGuestNo = 3
                                           },
                                           new RoomType{
                                               Description = "Cuadruple",
                                               Rate = 40,
                                               MaxGuestNo = 4
                                           }
                                       };

            foreach (RoomType rt in roomTypes)
            {
                context.RoomTypes.Add(rt);
                context.SaveChanges();
            }

            //adding rooms
            List<Room> rooms = new List<Room>
            {
                new Room{
                    Floor=1,
                    RoomNumber=01,
                    RoomTypeId=1
                },

                new Room{
                    Floor=1,
                    RoomNumber=02,
                    RoomTypeId=1
                },

                new Room{
                    Floor=2,
                    RoomNumber=10,
                    RoomTypeId=2
                },

                new Room{
                    Floor=2,
                    RoomNumber=11,
                    RoomTypeId=3
                },

                new Room{
                    Floor=2,
                    RoomNumber=12,
                    RoomTypeId=4
                }
            };

            foreach (Room r in rooms)
            {
                context.Rooms.Add(r);
                context.SaveChanges();
            }


        }
    }
}