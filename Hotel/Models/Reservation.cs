﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelEden.Models
{

    #region Interfaces
    public interface IReservationRepository
    {
        Reservation Add(Reservation reservation);
        IQueryable<Reservation> GetAll();
        Reservation Get(int id);
        Reservation Edit(Reservation reservation);
        void Delete(int id);
        void Dispose();
        List<RoomType> SearchAvailabity(DateTime CheckInDate, DateTime CheckOutDate, int NumPeople);
    }

    #endregion

    public class ReservationRepository : IReservationRepository
    {
        
        private HotelContext _db { get; set; }

        public ReservationRepository()
            : this(new HotelContext())
        { }

        public ReservationRepository(HotelContext db)
        { 
            _db = db; 
        }

        public Reservation Add(Reservation reservation)
        {
            _db.Reservations.Add(reservation);
            _db.SaveChanges();
            return reservation;
        }

        public IQueryable<Reservation> GetAll()
        {
            return _db.Reservations.Include("Room");
        }

        public Reservation Get(int id)
        {
            return _db.Reservations.SingleOrDefault(r => r.Id == id);
        }

        public Reservation Edit(Reservation reservation)
        {
            _db.Entry(reservation).State = System.Data.EntityState.Modified;
            _db.SaveChanges();
            return reservation;
        }

        public void Delete(int id)
        {
            Reservation res = _db.Reservations.SingleOrDefault(r => r.Id == id);
            _db.Reservations.Remove(res);
            _db.SaveChanges();
        }

        public List<RoomType> SearchAvailabity(DateTime CheckInDate, DateTime CheckOutDate, int NumPeople)
        {
            //get rooms that are available and can fit the number of people
            List<RoomType> RoomTypesOccupied = (from reservation in _db.Reservations
                                                where 
                                                //find overlap with ending
                                                (CheckOutDate > reservation.CheckInDate && CheckOutDate <= reservation.CheckOutDate)
                                                ||
                                                //find overlap with beginning
                                                (CheckInDate < reservation.CheckOutDate && CheckInDate >= reservation.CheckInDate)
                                                //find outer overlaps overlaps, innver overlaps are take care of with both above
                                                ||
                                                (CheckInDate <= reservation.CheckInDate && CheckOutDate >= reservation.CheckOutDate)
                                           select reservation.Room.RoomType).Distinct().ToList();

            //room types that can fit that many people
            List<RoomType> RoomTypesAvailable = (from roomType in _db.RoomTypes
                                                     where roomType.MaxGuestNo >= NumPeople
                                                     select roomType).ToList();

            return RoomTypesAvailable.Except(RoomTypesOccupied).ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }

    public class Reservation
    {
        public int Id { get; set; }
        public DateTime ReservedDate { get; set; }
        public int NumNights { get; set; }
        public int NumGuests { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool CheckedIn { get; set; }
        public bool CheckedOut { get; set; }
        public bool Cancellation { get; set; }
        public double TotalPrice { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}