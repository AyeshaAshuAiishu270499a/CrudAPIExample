using CrudExample.Entities;
using CrudExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext deptContext)
        {
            _context = deptContext;
        }
        [HttpGet("getalldepts")]
        public IActionResult GetAllusers()
        {
            try
            {
                var dept = _context.Departments.ToList();

                var result = new
                {
                    code = "200",
                    message = "Departments List",
                    data = dept
                };

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    code = "500",
                    message = "Internal error : " + ex.Message,
                    data = ""
                };

                return new JsonResult(result);
            }
        }
        [HttpPost("adddept")]
        public IActionResult AddDept([FromBody] DeptViewModel model)
        {
            try
            {
                var dept = _context.Departments.FirstOrDefault(x => x.Name == model.name);
                if (dept != null)
                {
                    return new JsonResult(new
                    {
                        code = "401",
                        message = "Alredy Exists",
                        data = model
                    });
                }
                Department objdept = new Department();
                objdept.Name = model.name;
                _context.Departments.Add(objdept);
                _context.SaveChanges();

                return new JsonResult(new
                {
                    code = "200",
                    message = "Added Successfully",
                    data = model
                });

            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    code = "500",
                    message = "Insert failed: " + ex.Message,
                    data = ""
                });
            }
        }
        [HttpPost("updatedept")]
        public IActionResult UpdateDept([FromBody] DeptViewModel model)
        {
            try
            {
                if (model == null || model.name == null)
                {
                    return new JsonResult(new
                    {
                        code = "402",
                        message = "Invalid Data",
                        data = model
                    });
                }
                var existingDept = _context.Departments.FirstOrDefault(x => x.DeptId == Convert.ToInt32(model.deptId));
                if (existingDept == null)
                {
                    return new JsonResult(new
                    {
                        code = "401",
                        message = "Invalid DeptId",
                        data = model
                    });
                }
                var existingDeptName = _context.Departments.FirstOrDefault(x => x.Name == model.name && x.DeptId != Convert.ToInt32(model.deptId));
                if (existingDeptName != null)
                {
                    return new JsonResult(new
                    {
                        code = "401",
                        message = "Invalid Name",
                        data = model
                    });
                }
                existingDept.Name = model.name;
                _context.Departments.Update(existingDept);
                _context.SaveChanges();
                return new JsonResult(new
                {
                    code = "200",
                    message = "Updated Successfully",
                    data = model
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    code = "500",
                    message = "Invalid Data:" + ex.Message,
                    data = ""
                });
            }

        }
        [HttpPost("getdeptdetails")]
        public IActionResult GetDeptDetails([FromBody] DeptViewModel model)
        {

            try
            {
                if (model == null || string.IsNullOrEmpty(model.deptId))
                {
                    return new JsonResult(new
                    {
                        code = "401",
                        message = "Invalid Deptid.",
                        data = ""
                    });

                }
                try
                {
                    var deptDetails = _context.Departments.FirstOrDefault(x => x.DeptId == Convert.ToInt32(model.deptId));
                    if (deptDetails == null)
                    {
                        return new JsonResult(new
                        {
                            code = "401",
                            message = "Invalid DeptId.",
                            data = ""
                        });
                    }
                    return new JsonResult(new
                    {
                        code = "200",
                        message = "successfull",
                        data = deptDetails
                    });

                }
                catch
                {
                    return new JsonResult(new
                    {
                        code = "401",
                        message = "Invalid DeptId.",
                        data = ""
                    });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    code = "500",
                    message = "Invalid id:" + ex.Message,
                    data = ""
                });
            }
        }
    }
}
