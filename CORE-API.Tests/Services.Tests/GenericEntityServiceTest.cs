using System;
using NUnit.Framework;
using Moq;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Services;
using LinqKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using CORE_API.CORE.Repositories.Abstract;
using System.Linq;
using System.Linq.Expressions;

namespace CORE_API.Tests.Services.Test
{
    public class GenericEntityServiceTest
    {
        private Mock<IGenericEntityRepository<User>> userRepository;
        private List<User> users;
        private User addUser;
        private List<User> addUsers;
        private List<User> deleteUsers;

        [SetUp]
        public void Setup()
        {
            // Setup the mock
            userRepository = new Mock<IGenericEntityRepository<User>>();

            users = new List<User> {
                new User { Id = Guid.Parse("05cd28d8-7447-43bc-8bd4-a55df79eb80b"), FirstName = "Khuong", LastName = "Nguyen", EmailAddress = "khuong@gmail.com",
                            AuthId = "12345", Created = DateTime.UtcNow, Modified = DateTime.UtcNow},
                new User { Id = Guid.Parse("05cd28d8-7447-43bc-8bd4-a55df79eb81b"), FirstName = "Khuong", LastName = "Nguyen 1", EmailAddress = "khuong1@gmail.com",
                            AuthId = "12346", Created = DateTime.UtcNow.AddDays(1), Modified = DateTime.UtcNow.AddDays(1)},
                new User { Id = Guid.Parse("05cd28d8-7447-43bc-8bd4-a55df79eb82b"), FirstName = "Khuong", LastName = "Nguyen 2", EmailAddress = "khuong2@gmail.com",
                            AuthId = "12346", Created = DateTime.UtcNow.AddDays(2), Modified = DateTime.UtcNow.AddDays(2)}
            };

            addUser = new User
                        {
                            Id = Guid.Parse("05cd28d8-7447-43bc-8bd4-a55df79eb80d"),
                            FirstName = "Khuong",
                            LastName = "Nguyen 3",
                            EmailAddress = "khuong3@gmail.com",
                            AuthId = "12347",
                            Created = DateTime.UtcNow,
                            Modified = DateTime.UtcNow
                        };

            addUsers = new List<User> {
                new User { Id = Guid.Parse("05cd28d8-7447-43bc-8bd4-a55df79eb80b"), FirstName = "Khuong", LastName = "Nguyen 4", EmailAddress = "khuong4@gmail.com",
                            AuthId = "12345", Created = DateTime.UtcNow, Modified = DateTime.UtcNow},
                new User { Id = Guid.Parse("05cd28d8-7447-43bc-8bd4-a55df79eb81b"), FirstName = "Khuong", LastName = "Nguyen 5", EmailAddress = "khuong5@gmail.com",
                            AuthId = "12346", Created = DateTime.UtcNow, Modified = DateTime.UtcNow},
                new User { Id = Guid.Parse("05cd28d8-7447-43bc-8bd4-a55df79eb82b"), FirstName = "Khuong", LastName = "Nguyen 6", EmailAddress = "khuong6@gmail.com",
                            AuthId = "12346", Created = DateTime.UtcNow, Modified = DateTime.UtcNow}
            };

            deleteUsers = new List<User> {
                new User { Id = Guid.Parse("05cd28d8-7447-43bc-8bd4-a55df79eb80b"), FirstName = "Khuong", LastName = "Nguyen 7", EmailAddress = "khuong7@gmail.com",
                            AuthId = "12345", Created = DateTime.UtcNow, Modified = DateTime.UtcNow},
                new User { Id = Guid.Parse("05cd28d8-7447-43bc-8bd4-a55df79eb81b"), FirstName = "Khuong", LastName = "Nguyen 8", EmailAddress = "khuong8@gmail.com",
                            AuthId = "12346", Created = DateTime.UtcNow, Modified = DateTime.UtcNow},
                new User { Id = Guid.Parse("05cd28d8-7447-43bc-8bd4-a55df79eb82b"), FirstName = "Khuong", LastName = "Nguyen 9", EmailAddress = "khuong9@gmail.com",
                            AuthId = "12346", Created = DateTime.UtcNow, Modified = DateTime.UtcNow}
            };
        }

        [Test]
        public void  FindAll()
        {
            userRepository.Setup(a => a.Find(-1,-1,null, null, true)).Returns(users.AsQueryable());
            var userService = new GenericEntityService<User>(userRepository.Object);
            var userlist = userService.FindAll().ToList();
            Assert.AreEqual(3, userlist.Count);
        }


        [TestCase("05cd28d8-7447-43bc-8bd4-a55df79eb80b", "Khuong Nguyen")]
        [TestCase("05cd28d8-7447-43bc-8bd4-a55df79eb80c", "Not Found")]
        public void FindById(Guid id, string expected)
        {
            userRepository.Setup(a => a.Find(0, 1, m => m.Id.Equals(id), null, true)).Returns(users.Where(r => r.Id == id).AsQueryable());
            var userService = new GenericEntityService<User>(userRepository.Object);
            var foundUser = userService.FindById(id);

            string result = "Not Found";

            if (foundUser != null) {
                result = foundUser.GetFullName();
            }
            
            Assert.AreEqual(expected, result);
        }

        [TestCase("05cd28d8-7447-43bc-8bd4-a55df79eb80b", "Khuong Nguyen")]
        [TestCase("05cd28d8-7447-43bc-8bd4-a55df79eb80c", "Not Found")]
        public void FindOne(Guid id, string expected)
        {
            userRepository.Setup(a => a.Find(0, 1, m => m.Id.Equals(id), null, true)).Returns(users.Where(r => r.Id == id).AsQueryable());
            var userService = new GenericEntityService<User>(userRepository.Object);
            var foundUser = userService.FindOne(m => m.Id.Equals(id));

            string result = "Not Found";

            if (foundUser != null)
            {
                result = foundUser.GetFullName();
            }

            Assert.AreEqual(expected, result);
        }

        [TestCase("", 0, 3, 3)]
        [TestCase("Khuong",0,3,3)]
        [TestCase("Khuong", 1, 3, 2)]
        [TestCase("Khuong", 0, 2, 2)]
        [TestCase("Nguyen 1", 0, 3, 1)]
        [TestCase("khuong2@gmail.com", 0, 3, 1)]
        public void FindQueryableList(string SearchQuery, int skip, int count, int expected)
        {
            Expression<Func<User, object>> defaultSort = (m => m.Created);
            Expression<Func<User, bool>> defaultWhereAll = (m => true);

        var where = !string.IsNullOrEmpty(SearchQuery)
                        ?
                            PredicateBuilder.New<User>(
                            m => m.FirstName.Contains(SearchQuery) ||
                            m.LastName.Contains(SearchQuery) ||
                            m.EmailAddress.Contains(SearchQuery) ||
                            m.GetFullName().Contains(SearchQuery)
                            )
                        : null;

            if (where == null) where = defaultWhereAll;

            userRepository.Setup(a => a.Find(skip, count, where, null, true)).Returns(users.Where(where).Skip(skip).Take(count).AsQueryable());

            var userService = new GenericEntityService<User>(userRepository.Object);
            var userlist = userService.FindQueryableList(skip, count, where, null, true).ToList();

            var result = userlist.Count;
            Assert.AreEqual(expected, result);
        }

        [TestCase(true, true, "Khuong Nguyen 2")]
        [TestCase(true, false, "Khuong Nguyen")]
        [TestCase(false, true, "Khuong Nguyen 2")]
        [TestCase(false, false, "Khuong Nguyen")]
        public void FindQueryableList_SortDesc(bool isDefaultSort, bool desc, string expected)
        {
            Expression<Func<User, object>> defaultSort = (m => m.Created);
            Expression<Func<User, object>> sort = (m => m.GetFullName());

            var order = isDefaultSort ? defaultSort : sort;

            if (desc)
            {
                userRepository.Setup(a => a.Find(-1, -1, null, order, desc)).Returns(users.AsQueryable().OrderByDescending(order));
            }
            else {
                userRepository.Setup(a => a.Find(-1, -1, null, order, desc)).Returns(users.AsQueryable().OrderBy(order));
            }
            

            var userService = new GenericEntityService<User>(userRepository.Object);

            var userlist = userService.FindQueryableList(-1, -1, null, order, desc).ToList();

            var result = userlist[0].GetFullName();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task AddAsync() {
            userRepository.Setup(a => a.AddAsync(addUser)).Returns(Task.FromResult(addUser));
            var userService = new GenericEntityService<User>(userRepository.Object);
            var result = await userService.AddAsync(addUser);
            Assert.AreEqual("Khuong Nguyen 3", result.Entity.GetFullName());
        }

        [Test]
        public async Task AddManyAsync()
        {
            var userService = new GenericEntityService<User>(userRepository.Object);
            var result = await userService.AddManyAsync(addUsers);
            Assert.AreEqual("Khuong Nguyen 4", result.Entities[0].GetFullName());
            Assert.AreEqual("Khuong Nguyen 5", result.Entities[1].GetFullName());
            Assert.AreEqual("Khuong Nguyen 6", result.Entities[2].GetFullName());
        }

        [TestCase("05cd28d8-7447-43bc-8bd4-a55df79eb80b", "Khuong Nguyen")]
        public async Task DeleteById(Guid id, string expected)
        {
            userRepository.Setup(a => a.Find(0, 1, m => m.Id.Equals(id), null, true)).Returns(users.Where(r => r.Id == id).AsQueryable());

            var userService = new GenericEntityService<User>(userRepository.Object);

            var entity = userService.FindById(id);

            userRepository.Setup(a => a.Delete(entity)).Returns(entity);

            userService = new GenericEntityService<User>(userRepository.Object);

            var result = await userService.DeleteById(id);

            Assert.AreEqual(expected, result.GetFullName());
        }

        public async Task DeleteMany()
        {
            var userService = new GenericEntityService<User>(userRepository.Object);

            var result = await userService.DeleteMany(deleteUsers);

            Assert.AreEqual("Khuong Nguyen 7", result[0].GetFullName());
            Assert.AreEqual("Khuong Nguyen 8", result[1].GetFullName());
            Assert.AreEqual("Khuong Nguyen 9", result[2].GetFullName());
        }

        [Test]
        public async Task UpdateAsync()
        {
            userRepository.Setup(a => a.Update(addUser));
            var userService = new GenericEntityService<User>(userRepository.Object);
            var result = await userService.UpdateAsync(addUser);
            Assert.AreEqual("Khuong Nguyen 3", result.Entity.GetFullName());
        }

        [Test]
        public async Task UpdateManyAsync()
        {
            var userService = new GenericEntityService<User>(userRepository.Object);
            var result = await userService.UpdateManyAsync(addUsers);
            Assert.AreEqual("Khuong Nguyen 4", result.Entities[0].GetFullName());
            Assert.AreEqual("Khuong Nguyen 5", result.Entities[1].GetFullName());
            Assert.AreEqual("Khuong Nguyen 6", result.Entities[2].GetFullName());
        }

        [TestCase("", 3)]
        [TestCase("Khuong", 3)]
        [TestCase("Nguyen 1", 1)]
        public async Task Count(string SearchQuery, int expected)
        {
            var where = !string.IsNullOrEmpty(SearchQuery)
                        ?
                            PredicateBuilder.New<User>(
                            m => m.FirstName.Contains(SearchQuery) ||
                            m.LastName.Contains(SearchQuery) ||
                            m.EmailAddress.Contains(SearchQuery) ||
                            m.GetFullName().Contains(SearchQuery)
                            )
                        : null;

            if (where == null)
            {
                userRepository.Setup(a => a.CountAsync(where)).Returns(Task.FromResult(users.Count()));
            }
            else {
                userRepository.Setup(a => a.CountAsync(where)).Returns(Task.FromResult(users.Where(where).Count()));
            }
            
            var userService = new GenericEntityService<User>(userRepository.Object);
            var result = await userService.Count(where);
            Assert.AreEqual(expected, result);
            
        }

    }
}
