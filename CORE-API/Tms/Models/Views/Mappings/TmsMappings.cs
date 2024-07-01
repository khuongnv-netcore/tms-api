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
                .ForMember(dest => dest.ContainerCode, opt => opt.MapFrom(src => src.BookingContainerDetail != null ? src.BookingContainerDetail.BookingContainer.ContainerCode : ""));
            
            // CreateOrUpdateScheduleInputResource to ScheduleInputResource
            CreateMap<CreateOrUpdateScheduleInputResource, Schedule>();
            #endregion

            #region Booking
            // Resource Input to Entity
            CreateMap<BookingInputResource, Booking>();
            CreateMap<UpdateBookingInputResource, Booking>();
            //Entity Output to Resource
            CreateMap<Booking, BookingOutputResource>();
            CreateMap<BookingInputResource, UpdateBookingInputResource>();
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

            #region BookingCharge
            // Resource Input to Entity
            CreateMap<BookingChargeInputResource, BookingCharge>();

            //Entity Output to Resource
            CreateMap<BookingCharge, BookingChargeOutputResource>()
                .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.AdvanceMoneyDocument != null ? src.AdvanceMoneyDocument.DocumentName : null));
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

            #region Location
            CreateMap<LocationInputResource, Location>();
            CreateMap<Location, LocationOutputResource>();
            #endregion

            #region Employee
            CreateMap<EmployeeInputResource, Employee>();
            CreateMap<Employee, EmployeeOutputResource>();
            #endregion

            #region Container
            CreateMap<ContainerInputResource, Container>();
            CreateMap<Container, ContainerOutputResource>();
            #endregion

            #region PricingMaster
            CreateMap<PricingMasterInputResource, PricingMaster>();
            CreateMap<UpdatePricingMasterInputResource, PricingMaster>();
            CreateMap<PricingMaster , PricingMasterOutputResource>();
            CreateMap<PricingMasterInputResource, UpdatePricingMasterInputResource>();
            #endregion

            #region Pricing Master Detail
            CreateMap<PricingMasterDetailInputResource, PricingMasterDetail>();
            CreateMap<PricingMasterDetail, PricingMasterDetailOutputResource>();
            #endregion

            #region Pricing For Customer
            CreateMap<PricingForCustomerInputResource, PricingForCustomer>();
            CreateMap<UpdatePricingForCustomerInputResource, PricingForCustomer>();
            CreateMap<PricingForCustomer, PricingForCustomerOutputResource>();
            CreateMap<PricingForCustomerInputResource, UpdatePricingForCustomerInputResource>();
            #endregion

            #region Pricing For Customer Detail
            CreateMap<PricingForCustomerDetailInputResource, PricingForCustomerDetail>();
            CreateMap<PricingForCustomerDetail, PricingForCustomerDetailOutputResource>();
            #endregion

            #region Advance Money
            CreateMap<AdvanceMoneyInputResource, AdvanceMoney>();
            CreateMap<UpdateAdvanceMoneyInputResource, AdvanceMoney>();
            CreateMap<AdvanceMoney, AdvanceMoneyOutputResource>()
                .ForMember(dest => dest.BookingNo, opt => opt.MapFrom(src => src.Booking != null ? src.Booking.BookingNo : ""));
            CreateMap<AdvanceMoneyInputResource, UpdateAdvanceMoneyInputResource>();
            #endregion

            #region Advance Money Document
            CreateMap<AdvanceMoneyDocumentInputResource, AdvanceMoneyDocument>();
            CreateMap<AdvanceMoneyDocument, AdvanceMoneyDocumentOutputResource>();
            #endregion

            #region Auto Number
            CreateMap<AutoNumberInputResource, AutoNumber>();
            CreateMap<AutoNumber, AutoNumberOutputResource>();
            #endregion

            #region
            CreateMap<KindOfFeeInputResource, KindOfFee>();
            CreateMap<KindOfFee, KindOfFeeOutputResource>();
            #endregion
        }
    }
}
