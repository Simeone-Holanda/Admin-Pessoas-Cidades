using AdminPersonAndCity.Models;
using AdminPersonAndCity.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminPersonAndCity.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public IActionResult Index()
        {
            List<CityModel> cities = _cityRepository.FindAll();
            return View(cities);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(int id)
        {
            CityModel? city = _cityRepository.FindById(id);
            if (city == null)
            {
                TempData["errorMessage"] = "Cidade não encontrada";
                return RedirectToAction("Index", "City");
            }
            return View(city);
        }

        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                CityModel? city = _cityRepository.FindById(id);

                if (city == null)
                {
                    TempData["ErrorMessage"] = $"Ops,Não foi possível encontrar a cidade. ";
                    return RedirectToAction("Index");
                }

                return View(city);
            } catch(Exception error)
            {
//                TempData["ErrorMessage"] = $"Ops,Não foi possível encontrar a cidade. ";
                return RedirectToAction("Index");
            }

        }
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _cityRepository.Remove(id);

                if (result)
                {
                    TempData["successMessage"] = "Cidade deletada com sucesso. ";
                }
                else
                {
                    TempData["ErrorMessage"] = "Não foi possível deletar a cidade. ";
                }
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não conseguimos apagar a cidade, tente novamente. Erro: {error.Message} ";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Create(CityModel city)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _cityRepository.Insert(city);
                    TempData["successMessage"] = "Cidade Adicionada com sucesso. ";
                    return RedirectToAction("Index", "City");
                }

                return View(city);
            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não foi possível adicionar a cidade, tente novamente. Erro: {error.Message} ";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Update(CityModel city)
        {
            try
            {
                if (ModelState.IsValid)
                { 
                    _cityRepository.Update(city);
                    TempData["successMessage"] = "Cidade Atualizada com sucesso. ";
                    return RedirectToAction("Index", "City");
                }

                return View(city);
            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não foi possível Atualizar a cidade, tente novamente. Erro: {error.Message} ";
                return RedirectToAction("Index");
            }

        }

    }
}
