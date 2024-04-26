using AutoMapper;
using CORE_API.CORE.Models.Entities;
using CORE_API.Tms.Models.Entities;
using System.Collections.Generic;
namespace CORE_API.Tms.Models.Views.Mappings
{
    public class TmsMappings : Profile
    {
        public TmsMappings()
        {
            #region Schedule
            // Resource Input to Entity
            CreateMap<ScheduleInputResource, Schedule>();

            //Entity Output to Resource
            CreateMap<Schedule, ScheduleOutputResource>();
            CreateMap<Schedule, ScheduleBookingOutputResource>()
                .ForMember(dest => dest.ContainerCode, opt => opt.MapFrom(src => src.BookingContainer != null ? src.BookingContainer.ContainerCode : ""));
            
            // CreateOrUpdateScheduleInputResource to ScheduleInputResource
            CreateMap<CreateOrUpdateScheduleInputResource, Schedule>();
            #endregion

            #region Booking
            // Resource Input to Entity
            CreateMap<BookingInputResource, Booking>();

            //Entity Output to Resource
            CreateMap<Booking, BookingOutputResource>();
            #endregion

            #region BookingEx
            //Entity Output to Resource
            CreateMap<BookingEx, BookingOutputResourceEx>();
            #endregion

            #region BookingContainer
            // Resource Input to Entity
            CreateMap<BookingContainerInputResource, BookingContainer>();

            //Entity Output to Resource
            CreateMap<BookingContainer, BookingContainerOutputResource>();
            #endregion

            #region BookingContainerDetail
            // Resource Input to Entity
            CreateMap<BookingContainerDetailInputResource, BookingContainerDetail>();

            //Entity Output to Resource
            CreateMap<BookingContainerDetail, BookingContainerDetailOutputResource>();
            #endregion

            #region Driver
            // Resource Input to Entity
            CreateMap<DriverInputResource, Driver>();

            //Entity Output to Resource
            CreateMap<Driver, DriverOutputResource>();
            #endregion

            #region Fixed Asset
            // Resource Input to Entity
            CreateMap<FixedAssetInputResource, FixedAsset>();

            //Entity Output to Resource
            CreateMap<FixedAsset, FixedAssetOutputResource>();
            #endregion

            #region Customer
            CreateMap<CustomerInputResource,Customer>();
            CreateMap<Customer, CustomerOutputResource>();
            #endregion
        }
    }
}
