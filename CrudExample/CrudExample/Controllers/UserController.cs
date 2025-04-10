using CrudExample.Entities;
using CrudExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CrudExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext userContext)
        {
            _context = userContext;
        }
        [HttpGet("getallusers")]
        public IActionResult GetAllusers()
        {
            try
            {
                var userData = _context.Users.ToList();

                var result = new
                {
                    success = "200",
                    message = "Users List",
                    data = userData
                };

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    success = "500",
                    message = "Internal error : " + ex.Message,
                    data = ""
                };

                return new JsonResult(result);

            }
        }
        [HttpPost("adduser")]
        public IActionResult AddUser([FromBody] UserViewModel model)
        {
            try
            {
                // dept check ==null
                var deptdata=_context.Users.FirstOrDefault(x=>x.DeptId==Convert.ToInt32(model.dept_id));
                if (deptdata != null)
                {
                    return new JsonResult(new
                    {
                        code = "401",
                        message = "DeptId Alredy Exists",
                        data = model
                    });
                }
                var userdata = _context.Users.FirstOrDefault(x => x.Email==model.email);
                if (userdata!=null)
                {
                    return new JsonResult(new
                    {
                        code = "401",
                        message = "Email Alredy Exists",
                        data = model
                    });

                }
                User objuser = new User();
                objuser.Name=model.name;
                objuser.Email=model.email;
                objuser.Password=model.password;
                objuser.Address=model.address;
                objuser.DeptId =Convert.ToInt32( model.dept_id);
                if(model.status=="1")
                {
                    objuser.Status =true;
                }
                else
                {
                    objuser.Status =false;
                }
                _context.Users.Add(objuser);
                _context.SaveChanges();
                return new JsonResult(new
                {
                    code="200",
                    message="success",
                    data=model
                });

            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    code = "401",
                    message = "Invalid:"+ex.Message,
                    data = ""
                });
            }
          
        }

        [HttpPost("updateuser")]
        public IActionResult UpdateUser([FromBody] UserViewModel model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.name) || string.IsNullOrEmpty(model.email))
                {
                    return new JsonResult(new
                    {
                        code = "401",
                        message = "Invalid:",
                        data = model
                    });
                }
                var existingDept = _context.Users.FirstOrDefault(x => x.DeptId == Convert.ToInt32(model.dept_id));
                if(existingDept == null)
                {
                    return new JsonResult(new
                    {
                        code = "402",
                        message = "Invalid DeptId",
                        data = model
                    });
                }

                var existinguser = _context.Users.FirstOrDefault(x => x.Id == Convert.ToInt32( model.Id));
                if(existinguser == null)
                {
                    return new JsonResult(new
                    {
                        code="402",
                        message="Invalid Id",
                        data = model
                    });
                }
                var existingEmail=_context.Users.FirstOrDefault(x=>x.Email==model.email && x.Id !=existinguser.Id);
                if(existingEmail != null)
                {
                    return new JsonResult(new
                    {
                        code="401",
                        message="Email already exists",
                        data=model
                    });
                }
                existinguser.Name=model.name;
                existinguser.Email=model.email;
               // existinguser.Password=model.password;
                existinguser.Address=model.address;
                if (model.status == "1")
                {
                    existinguser.Status = true;
                }
                else
                {
                    existinguser.Status = false;
                }
                _context.Users.Update(existinguser);
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
                    message = "Invalid Data:"+ex.Message,
                    data = ""
                });
            }
        }

        [HttpPost("getUserDetails")]
        public IActionResult GetUserdetails([FromBody] UserDetailsViewModel model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.Id))
                {
                    return new JsonResult(new
                    {
                        code = "401",
                        message = "Invalid id.",
                        data = ""
                    });

                }
                try
                {
                    var userDetails = _context.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(model.Id));
                    if (userDetails == null)
                    {
                        return new JsonResult(new
                        {
                            code = "401",
                            message = "Invalid id.",
                            data = ""
                        });
                    }
                    return new JsonResult(new
                    {
                        code = "200",  
                        message = "successfull",
                        data = userDetails
                    });
                
                }
               catch
                {
                    return new JsonResult(new
                    {
                        success = "401",
                        message = "Invalid id.",
                        data = ""
                    });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = "500",
                    message = "Invalid id:"+ex.Message,
                    data = ""
                });
            }
        }
    }
}
