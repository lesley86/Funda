using Core.Entities;
using Core.Models;

namespace Core.Repository
{
	public interface IHouseRepository
	{
		IEnumerable<HouseEntity> AddHouses(IEnumerable<MakelaarWithObjectCount> houses);
		IEnumerable<HouseEntity> AddOrUpdateHouse(IEnumerable<MakelaarWithObjectCount> houses);
		HouseEntity GetHouse(string Id);
		IEnumerable<HouseEntity> GetHouses(bool onlyWithATuin);
		HouseEntity Update(string Id, int objectCount);
	}
}
