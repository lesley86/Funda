using Application.Exceptions;
using Core.Entities;
using Core.Models;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Sql
{
	public class HouseRepository : IHouseRepository
	{
		public IEnumerable<HouseEntity> GetHouses(bool onlyWithATuin)
		{
			using (var context = new ApiContext())
			{
				var results = context.Houses.AsNoTracking();

				if(onlyWithATuin)
				{
					results = results.Where(house => house.Tuin == true);
				}

				return results.ToList();
			}
		}

		public IEnumerable<HouseEntity> AddHouses(IEnumerable<MakelaarWithObjectCount> houses)
		{
			using (var context = new ApiContext())
			{
				var IdsToAdd = houses.Select(x => x.MakelaarId).ToList();
				var existingHouses = 
					from house in context.Houses
					where IdsToAdd.Contains(house.MakelaarId.ToString())
					select house;

				if(houses.Any())
				{
					throw new DuplicateEntityAddedException();
				};

				var results = houses.Select(x => new HouseEntity(x.MakelaarId, x.MakelaarName, x.ObjectCount, x.Tuin));
				context.AddRange(results);
				context.SaveChanges();
				return results;
			}
		}

		public HouseEntity GetHouse(string Id)
		{
			using (var context = new ApiContext())
			{
				var result = context.Houses.FirstOrDefault(house => house.MakelaarId == Id);
				return result;
			}
		}

		public HouseEntity Update(string Id, int objectCount)
		{
			using (var context = new ApiContext())
			{
				var result = context.Houses.FirstOrDefault(house => house.MakelaarId == Id);
				if(result == null)
				{
					throw new EntityNotfoundException();
				}

				result.StoreUpdatedInformation(objectCount);
				context.Houses.Update(result);
				context.SaveChanges();

				return result;
			}
		}

		public IEnumerable<HouseEntity> AddOrUpdateHouse(IEnumerable<MakelaarWithObjectCount> houses)
		{
			using (var context = new ApiContext())
			{
				var IdsToAdd = houses.Select(x => x.MakelaarId).ToList();
				var newEntities = CreateNewEntities(context, IdsToAdd);
				var existingEntities = UpdateExistingHouses(houses, context, IdsToAdd);
				context.SaveChanges();
				return newEntities.Union(existingEntities);
			}
		}

		private IEnumerable<HouseEntity> UpdateExistingHouses(IEnumerable<MakelaarWithObjectCount> houses, ApiContext context, List<string> IdsToAdd)
		{
			var results =
				from house in context.Houses
				where IdsToAdd.Contains(house.MakelaarId.ToString())
				select house;

			MakelaarWithObjectCount houseToUseToUpdate;
			foreach (var existingHouse in results)
			{
				houseToUseToUpdate = houses.First(x => x.MakelaarId == existingHouse.MakelaarId);
				existingHouse.StoreUpdatedInformation(houseToUseToUpdate.ObjectCount);
				context.UpdateRange(results);
			}

			return results;
		}

		private IEnumerable<HouseEntity>  CreateNewEntities(ApiContext context, List<string> IdsToAdd)
		{
			var results = from house in context.Houses
							  where !IdsToAdd.Contains(house.MakelaarId.ToString())
							  select new HouseEntity(house.MakelaarId, house.MakelaarName, house.ObjectCount, house.Tuin);

			context.AddRange(results);
			return results;
		}
	}
}
