using CRUD_file.Data;
using CRUD_file.Models;
using CRUD_file.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;

namespace CRUD_file.Controllers
{
    [ApiController]
    [Route("SampleModel")]
    public class SampleModelController : ControllerBase
    {
        private readonly IStorageService<SampleModel> storageService;

        public SampleModelController(IStorageService<SampleModel> storageService)
        {
            this.storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }


        [HttpPost("create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(SampleModel model)
        { 
            try
            {
                storageService.AddItem(model);
                return CreatedAtAction(nameof(GetModel), new { id = model.sample_id }, model);               
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("read/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetModel(int id)
        {
            try
            {
                return Ok(storageService.GetItemWhere(x => x.sample_id == id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Edit(SampleModel model)
        {
            try
            {
                if (storageService.EditItem(model, x => x.sample_id == model.sample_id))
                {
                    return CreatedAtAction(nameof(GetModel), new { id = model.sample_id }, model);
                }
                else 
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {
            try
            {
                if (storageService.DeleteItem(x => x.sample_id == id))
                {
                    return Ok();
                } 
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}