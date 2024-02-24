using AdminPersonAndCity.Models;
using AdminPersonAndCity.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AdminPersonAndCity.Controllers
{
    public class PersonController : Controller
    {

        private readonly IPersonRepository _personRepository;
        private readonly ICityRepository _cityRepository;

        public PersonController(IPersonRepository personRepository, ICityRepository cityRepository)
        {
            _personRepository = personRepository;
            _cityRepository = cityRepository;
        }

        public IActionResult Index()
        {
            List<PersonModel> persons = _personRepository.FindAll();
            return View(persons);
        }

        public IActionResult Create()
        {
            List<CityModel> cities = _cityRepository.FindAll();
            return View(cities);
        }

        public IActionResult Update(int id)
        {
            PersonModel? person = _personRepository.FindById(id);
            if (person == null)
            {
                TempData["errorMessage"] = "Pessoa não encontrada";
                return RedirectToAction("Index", "Person");
            }
            return View(person);
        }

        public IActionResult ConfirmDelete(int id)
        {
            PersonModel? person = _personRepository.FindById(id);

            if (person == null)
            {
                return View();
            }

            return View(person);
        }
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _personRepository.Remove(id);

                if (result)
                {
                    TempData["successMessage"] = "Pessoa deletada com sucesso. ";
                }
                else
                {
                    TempData["ErrorMessage"] = "Não foi possível deletar a pessoa. ";
                }
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não conseguimos apagar a pessoa, tente novamente. Erro: {error.Message} ";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Create(PersonModel person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _personRepository.Insert(person);
                    TempData["successMessage"] = "Pessoa Adicionado com sucesso. ";
                    return RedirectToAction("Index", "Person");
                }

                return View(person);
            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não foi possível adicionar a pessoa, tente novamente. Erro: {error.Message} ";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Update(PersonModel person)
        {
            try
            {
                if (ModelState.IsValid) {
                    _personRepository.Update(person);
                    TempData["successMessage"] = "Pessoa Atualizado com sucesso. ";
                    return RedirectToAction("Index", "Person");
                }

                return View(person);
            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não foi possível adicionar a pessoa, tente novamente. Erro: {error.Message} ";
                return RedirectToAction("Index");
            }

        }
    }
}
