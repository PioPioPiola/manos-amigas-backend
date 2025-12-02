using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using ManoaAmigas.Domain.Entities;

namespace Application.Services
{
    public class ServiceService 
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUserRepository _userRepository;
        private readonly ProviderCategoryEnrollmentService _enrollmentService;

        public ServiceService(IServiceRepository serviceRepository, IUserRepository userRepository, ProviderCategoryEnrollmentService enrollmentService)
        {
            _serviceRepository = serviceRepository;
            _userRepository = userRepository;
            _enrollmentService = enrollmentService;
        }

        public async Task<ServiceDto> CreateServiceRequestAsync(ServiceDto serviceDto)
        {
            var requester = await _userRepository.GetByPersonIdAsync(serviceDto.RequesterId);
            if (requester == null)
            {
                throw new KeyNotFoundException("El solicitante no fue encontrado.");
            }

            var serviceEntity = new Service
            {
                requester_id = serviceDto.RequesterId,
                category_id = serviceDto.CategoryId,
                service_location = serviceDto.ServiceLocation,
                service_description = serviceDto.ServiceDescription,
                service_status = 'P'
            };

            await _serviceRepository.CreateAsync(serviceEntity);

            serviceDto.ServiceId = serviceEntity.service_id;

            return serviceDto;
        }

        public async Task<ServiceDto> GetServiceByIdAsync(long serviceId)
        {
            var service = await _serviceRepository.GetByIdAsync(serviceId);

            if (service == null)
            {
                throw new KeyNotFoundException($"Servicio con ID {serviceId} no encontrado.");
            }

            return new ServiceDto
            {
                RequesterId = service.requester_id,
                CategoryId = service.category_id,
                ServiceLocation = service.service_location,
                ServiceDescription = service.service_description,
            };
        }
    }
}
