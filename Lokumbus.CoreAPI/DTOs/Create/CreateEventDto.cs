using System;
using System.Collections.Generic;
using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object for creating a new Event.
    /// </summary>
    public class CreateEventDto
    {
        /// <summary>
        /// The title of the Event.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// A brief description of the Event.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The start date and time of the Event.
        /// </summary>
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// The end date and time of the Event.
        /// </summary>
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// Indicates whether the Event lasts all day.
        /// </summary>
        public bool? IsAllDay { get; set; }

        /// <summary>
        /// The recurrence pattern of the Event.
        /// </summary>
        public string? Recurrence { get; set; }

        /// <summary>
        /// The identifier of the reminder associated with the Event.
        /// </summary>
        public string? ReminderId { get; set; }

        /// <summary>
        /// The identifier of the Location associated with the Event.
        /// </summary>
        public string? LocationId { get; set; }

        /// <summary>
        /// The identifier of the Address associated with the Event.
        /// </summary>
        public string? AddressId { get; set; }

        /// <summary>
        /// The identifier of the Organizer associated with the Event.
        /// </summary>
        public string? OrganizerId { get; set; }

        /// <summary>
        /// The collection of Calendar IDs associated with the Event.
        /// </summary>
        public ICollection<string>? CalendarIds { get; set; }

        /// <summary>
        /// The collection of Attendee IDs associated with the Event.
        /// </summary>
        public ICollection<string>? AttendeeIds { get; set; }

        /// <summary>
        /// The collection of Category IDs associated with the Event.
        /// </summary>
        public ICollection<string>? CategoryIds { get; set; }

        /// <summary>
        /// The collection of Interest IDs associated with the Event.
        /// </summary>
        public ICollection<string>? InterestIds { get; set; }

        /// <summary>
        /// Indicates whether the Event is public.
        /// </summary>
        public bool? IsPublic { get; set; }

        /// <summary>
        /// Indicates whether the Event is approved.
        /// </summary>
        public bool? IsApproved { get; set; }

        /// <summary>
        /// Indicates whether the Event is cancelled.
        /// </summary>
        public bool? IsCancelled { get; set; }

        /// <summary>
        /// The status of the Event.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// The capacity of the Event.
        /// </summary>
        public string? Capacity { get; set; }

        /// <summary>
        /// The price of the Event.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// The currency of the Event price.
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// The URL related to the Event.
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// The image URL of the Event.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// The video URL of the Event.
        /// </summary>
        public string? VideoUrl { get; set; }

        /// <summary>
        /// The slug of the Event.
        /// </summary>
        public string? Slug { get; set; }

        /// <summary>
        /// The collection of Ticket IDs associated with the Event.
        /// </summary>
        public ICollection<string>? TicketIds { get; set; }

        /// <summary>
        /// The collection of Sponsorship IDs associated with the Event.
        /// </summary>
        public ICollection<string>? SponsorshipIds { get; set; }

        /// <summary>
        /// The collection of Discount IDs associated with the Event.
        /// </summary>
        public ICollection<string>? DiscountIds { get; set; }

        /// <summary>
        /// The collection of Invite IDs associated with the Event.
        /// </summary>
        public ICollection<string>? InviteIds { get; set; }

        /// <summary>
        /// Additional metadata associated with the Event.
        /// </summary>
        public Dictionary<string, object>? Metadata { get; set; }
    }
}