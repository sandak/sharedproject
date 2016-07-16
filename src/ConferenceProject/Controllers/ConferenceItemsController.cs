using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using ConferenceProject.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using ConferenceProject.ViewModels.Statistics;
using System.Collections.Generic;

namespace ConferenceProject.Controllers
{
    
    public class ConferenceItemsController : Controller
    {
        static public Properties _properties;
        private ApplicationDbContext _context;

        
        public ConferenceItemsController(ApplicationDbContext context)
        {
            _context = context;
           // Properties _properties;

            _properties = _context.Properties.FirstOrDefault(a => a.ID == 1) ;
            if (_properties == null)
            {
                _properties = new Properties();
                _context.Properties.Add(_properties);
                _context.SaveChanges();
            }
        }

        // GET: ConferenceItems
        public IActionResult Index(string searchArg)
        {
            var events = from s in _context.ConferenceItemList
                         select s;
            events = _context.ConferenceItemList.Include(c => c.Lecturer);

            if (!System.String.IsNullOrEmpty(searchArg))
            {
                events = events.Where(s => s._description.Contains(searchArg)
                                            || s._title.Contains(searchArg)
                                            || s.Lecturer._lname.Contains(searchArg)
                                            || s.Lecturer._fname.Contains(searchArg));
            }
            
            // var applicationDbContext = _context.ConferenceItemList.Include(c => c.Lecturer);
            var registered = _context.UsersConferenceItemsList.Where(m => m.user.Id == User.GetUserId()).Select(m => m.item).ToList();
             ViewData["registeredList"] = registered;
            return View(events);
        }

        //GET: LecturersPage
        public IActionResult LecturersPage(string searchArg)
        {

            var lecturers = from s in _context.LecturersList
                         select s;
            if (!System.String.IsNullOrEmpty(searchArg))
            {
                lecturers = lecturers.Where(s => s._fname.Contains(searchArg)
                                            || s._lname.Contains(searchArg)
                                            || s._description.Contains(searchArg)
                                            || s._company.Contains(searchArg));
            }

            return View(lecturers.ToList());
        }

        // GET: ConferenceItems/LecturerEdit/5
        public IActionResult LecturerEdit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Lecturer lecturer = _context.LecturersList.Single(m => m.lecturerID == id);
            if (lecturer == null)
            {
                return HttpNotFound();
            }
            return View(lecturer);
        }

        // POST: ConferenceItems/LecturerEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LecturerEdit(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _context.Update(lecturer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lecturer);
        }


        // GET: ConferenceItems/LecturerDelete/5
        [ActionName("LecturerDelete")]
        public IActionResult LecturerDelete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Lecturer lecturer = _context.LecturersList.Single(m => m.lecturerID == id);
            if (lecturer == null)
            {
                return HttpNotFound();
            }

            return View(lecturer);
        }

        // POST: ConferenceItems/Delete/5
        [HttpPost, ActionName("LecturerDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult LecturerDeleteConfirmed(int id)
        {
            Lecturer lecturer = _context.LecturersList.Single(m => m.lecturerID == id);
            _context.LecturersList.Remove(lecturer);
            _context.SaveChanges();
            return RedirectToAction("LecturersPage");
        }




        // GET: ConferenceItems/Create
        public IActionResult LecturerCreate()
        {
            ViewData["lecturerID"] = new SelectList(_context.LecturersList, "lecturerID", "Lecturer");
            return View();
        }

        // POST: ConferenceItems/LecturerCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LecturerCreate(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _context.LecturersList.Add(lecturer);
                _context.SaveChanges();
                return RedirectToAction("LecturersPage");
            }
            return View(lecturer);
        }

        // GET: ConferenceItems/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ConferenceItem conferenceItem = _context.ConferenceItemList.Single(m => m.ID == id);
            if (conferenceItem == null)
            {
                return HttpNotFound();
            }
            ViewData["lecturerID"] = new SelectList(_context.LecturersList, "lecturerID", "Lecturer", conferenceItem.lecturerID);
            var tmp = _context.LecturersList.ToList();
            ViewData["lecturersList"] = tmp;
            return View(conferenceItem);
        }

        // GET: ConferenceItems/Create
        public IActionResult Create()
        {
            ViewData["lecturerID"] = new SelectList(_context.LecturersList, "lecturerID", "Lecturer");
            var tmp = _context.LecturersList.ToList();
            ViewData["lecturersList"] = tmp;
            return View();
        }

        // POST: ConferenceItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ConferenceItem item)
        {
            if (ModelState.IsValid)
            {
                _context.ConferenceItemList.Add(item);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


        // GET: ConferenceItems/Delete
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ConferenceItem item = _context.ConferenceItemList.Single(m => m.ID == id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        // POST: ConferenceItems/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            ConferenceItem item = _context.ConferenceItemList.Single(m => m.ID == id);
            _context.ConferenceItemList.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        

        // GET: ConferenceItems/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ConferenceItem conferenceItem = _context.ConferenceItemList.Single(m => m.ID == id);
            if (conferenceItem == null)
            {
                return HttpNotFound();
            }

           return View(conferenceItem);
        }

        // POST: ConferenceItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ConferenceItem conferenceItem)
        {
            if (ModelState.IsValid)
            {
                _context.Update(conferenceItem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["lecturerID"] = new SelectList(_context.LecturersList, "lecturerID", "Lecturer", conferenceItem.lecturerID);
            return View(conferenceItem);
        }


        


        // GET: Properties/Details
        public IActionResult Properties()
        {
            Properties prop = _context.Properties.Single(m => m.ID == 1);
            if (prop == null)
            {
                prop = new Properties();
                prop.ID = 1;
                _context.Properties.Add(prop);
                _context.SaveChanges();
            }
            return View(prop);
        }

        // POST: Propeties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Properties(Properties prop)
        {
            if (ModelState.IsValid)
            {
                // _context.Update(prop);
                //_context.SaveChanges();

                Properties properties = _context.Properties.Where(m => m.ID == 1).ToList()[0];
                properties._address = prop._address;
                properties._movie = prop._movie;
                properties._startTime = prop._startTime;
                properties._title = prop._title;
                properties._logo = prop._logo;

                
                _context.SaveChanges();
                _properties = prop;
                return RedirectToAction("Home");
            }
            return View(prop);
        }


        public IActionResult getCounters()
        {
            var talks = _context.ConferenceItemList.Count();
            var registered = _context.ApplicationUser.Count();
            var lecturers = _context.LecturersList.Count();

            return Json(new { talks = talks, registered = registered, lecturers = lecturers }); 

        }


        // GET: ConferenceItems/RegisterConference
        [ActionName("RegisterConference")]
    public IActionResult RegisterConference(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ConferenceItem item = _context.ConferenceItemList.Single(m => m.ID == id);
            if (item == null)
            {
                return HttpNotFound();
            }
            
            var newConnection = new UsersConferenceItems();
            newConnection.item = item;
            var user = _context.Users.First(m => m.Id == User.GetUserId());
            newConnection.user = user;
            newConnection.registerDate = System.DateTime.Now;
            _context.UsersConferenceItemsList.Add(newConnection);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: ConferenceItems/RegisterConference
        [ActionName("UnregisterConference")]
        public IActionResult UnregisterConference(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ConferenceItem item = _context.ConferenceItemList.Single(m => m.ID == id);
            if (item == null)
            {
                return HttpNotFound();
            }

            var user = _context.Users.First(m => m.Id == User.GetUserId());
            var connection = _context.UsersConferenceItemsList.Single(m => m.item.ID == item.ID && m.user.Id == user.Id);
            _context.UsersConferenceItemsList.Remove(connection);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET:  PersonalSchedule
        public IActionResult PersonalSchedule()
        {
            var registered = _context.UsersConferenceItemsList.Where(m => m.user.Id == User.GetUserId()).Select(m => m.item).ToList();
            var list = _context.ConferenceItemList.Where(m => registered.Contains(m)).ToList();
            ViewData["registeredList"] = registered;
            return View(list);
        }

        // GET:  CreateAdmin
        [HttpGet]
       // [AllowAnonymous]
        public IActionResult CreateAdmin()
        {
            // IdentityUserRole
            var role = new IdentityRole();
            //  Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            role.Name = "admin";
            role.NormalizedName = "ADMINROLE";
            _context.IdentityRoleList.Add(role);
            _context.SaveChanges();
            //  var db = Microsoft.AspNet.Identity.EntityFramework.;
            return View("AdminsMgmt");
        }

        // GET:  AddUserAdmin
        [HttpGet]
        //[AllowAnonymous]
        public IActionResult AddUserAdmin()
        {
            var userRole = new IdentityUserRole<string>();
            //  Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            userRole.RoleId = _context.IdentityRoleList.First(m => m.Name == "admin").Id;
            userRole.UserId = User.GetUserId();
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
            //  var db = Microsoft.AspNet.Identity.EntityFramework.;
            return View("AdminsMgmt");
        }

        // GET:  CreateAdmin
        [HttpGet]
        public IActionResult AdminsMgmt()
        {
            CreateAdmin();
            AddUserAdmin();
            return View("Home");
        }

        // GET:  Home
        [HttpGet]
        public IActionResult Home()
        {
            var tmp = _context.IdentityRoleList.ToList();
            int flag = 0;
            foreach (var item in tmp)
                if (item.Name == "admin")
                    flag = 1;
            if (flag == 1)
            {
                Properties prop = _context.Properties.Single(m => m.ID == 1);
                ViewData["prop"] = prop;
                return View();
            } else
            {
                return View("firstConfiguration");
            }
        }

        // GET:  firstConfiguration
        [HttpGet]
        public IActionResult firstConfiguration()
        {
            return View();
        }

        // GET:  UsersManager
        [HttpGet]
        public IActionResult UsersManager()
        {
            if(User.IsInRole("admin"))
            {
                var list = _context.Users.ToList();
                return View(list);
            }
            else
            {
                return View("unAuthurize");
            }
            
        }


        // GET:  Statisics
        [HttpGet]
        public IActionResult Statisics()
        {
            if (User.IsSignedIn())
            {
                var data = _context.UsersConferenceItemsList.GroupBy(o => new
                {
                    date = o.registerDate.Date,
                    item = o.item
                }).Select(g => new AttendGraph
                {
                    date = g.Key.date.ToString("d"),
                    item = g.Key.item,
                    count = g.Count()
                }).OrderBy(a => a.date).ToList();

                //for rational graph - stop counting today
                if (data.Last().date != System.DateTime.Now.ToString("d"))
                {
                    AttendGraph last = new AttendGraph();
                    last.date = System.DateTime.Now.ToString("d");
                    last.count = data.Last().count;
                    data.Add(last);
                }

                //for rational graph - start counting from zero
                AttendGraph first = new AttendGraph();
                first.date = System.DateTime.Parse(data[0].date).AddDays(-1.0).ToString("d");
                first.count = 0;
                data.Add(first);

           

                data = data.OrderBy(a => System.DateTime.Parse(a.date)).ToList();

                ViewData["data"] = data;
 
                var data2 = _context.ConferenceItemList.Select(o => new PopularityGraph
                {
                    title = o._title,
                    lecturer = o.lecturerID,
                    count = o.usersConferenceItems.Count()
                }).ToList();

               

                ViewData["data2"] = data2;




                return View();
            }
            else
            {
                return View("unAuthurize");
            }

        }

        // GET: ConferenceItems/EditApplicationUser/5
        public IActionResult ApplicationUserEdit(string id)
        {
            if (System.String.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            ApplicationUser user = _context.ApplicationUser.Single(m => m.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            
            return View(user);
        }

        // POST: ConferenceItems/ApplicationUserEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApplicationUserEdit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                _context.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Home");
            }
             return View(user);
        }

        // GET: ConferenceItems/ApplicationUserDelete
        [ActionName("ApplicationUserDelete")]
        public IActionResult ApplicationUserDelete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            ApplicationUser user = _context.ApplicationUser.Single(m => m.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: ConferenceItems/ApplicationUserDeleteConfirmed
        [HttpPost, ActionName("ApplicationUserDeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult ApplicationUserDeleteConfirmed(string id)
        {
            ApplicationUser user = _context.ApplicationUser.Single(m => m.Id == id);
            _context.ApplicationUser.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: ConferenceItems/ApplicationUserDetails/5
        public IActionResult ApplicationUserDetails(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ApplicationUser user = _context.ApplicationUser.Single(m => m.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

    }
}

   