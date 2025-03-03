using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private static List<Car> Cars = new List<Car>
        {
            new Car { Id = 1, Make = "Toyota", Model = "Corolla", Year = 2020, VIN = "1HGCM82633A123456", IsAvailable = true },
            new Car { Id = 2, Make = "Honda", Model = "Civic", Year = 2019, VIN = "2HGCM82633A654321", IsAvailable = false }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetCars()
        {
            return Ok(Cars);
        }

        [HttpGet("{id}")]
        public ActionResult<Car> GetCar(int id)
        {
            var car = Cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        [HttpPost]
        public ActionResult<Car> AddCar(Car car)
        {
            car.Id = Cars.Max(c => c.Id) + 1;
            Cars.Add(car);
            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCar(int id, Car updatedCar)
        {
            var car = Cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            car.Make = updatedCar.Make;
            car.Model = updatedCar.Model;
            car.Year = updatedCar.Year;
            car.VIN = updatedCar.VIN;
            car.IsAvailable = updatedCar.IsAvailable;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCar(int id)
        {
            var car = Cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            Cars.Remove(car);
            return NoContent();
        }
    }

    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; }
        public bool IsAvailable { get; set; }
    }
}