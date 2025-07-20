using TravelApp.BusinessLogic.Dtos.Attraction;
using TravelApp.BusinessLogic.Exceptions;
using TravelApp.BusinessLogic.Mappers;
using TravelApp.BusinessLogic.Services.Abstractions;
using TravelApp.DataAccess.Repositories.Abstractions;

namespace TravelApp.BusinessLogic.Services.Concretes;

public class AttractionService : IAttractionService
{
    private readonly IAttractionRepository _attractionRepository;

    public AttractionService(IAttractionRepository attractionRepository)
    {
        _attractionRepository = attractionRepository;
    }
    
    public async Task<IEnumerable<AttractionGetDto>> GetAllAttractionsAsync()
    {
        var attractions = await _attractionRepository.GetAllAsync();
        return AttractionMapper.MapToAttractionGetDto(attractions);
    }

    public async Task<AttractionGetDto> GetAttractionByIdAsync(int id)
    {
        var attraction = await _attractionRepository.GetByIdAsync(id);
        if (attraction == null)
        {
            throw new EntityNotFoundException("Attraction", id);
        }
        return AttractionMapper.MapToAttractionGetDto(attraction);
    }

    public async Task<AttractionGetDto> CreateAttractionAsync(AttractionCreateDto attractionCreateDto)
    {
        var attraction = AttractionMapper.MapToAttraction(attractionCreateDto);
        await _attractionRepository.CreateAsync(attraction);
        
        return AttractionMapper.MapToAttractionGetDto(attraction);
    }

    public async Task<AttractionGetDto> UpdateAttractionAsync(int id, AttractionUpdateDto attractionUpdateDto)
    {
        var attraction = await _attractionRepository.GetByIdAsync(id);
        if (attraction == null)
        {
            throw new EntityNotFoundException("Attraction", id);
        }
        attraction.MapToAttraction(attractionUpdateDto);
        await _attractionRepository.UpdateAsync(id, attraction);
        
        return AttractionMapper.MapToAttractionGetDto(attraction);
    }

    public async Task DeleteAttractionAsync(int id)
    {
        var attraction = await _attractionRepository.GetByIdAsync(id);
        if (attraction == null)
        {
            throw new EntityNotFoundException("Attraction", id);
        }
        
        await _attractionRepository.DeleteAsync(id);
    }
}