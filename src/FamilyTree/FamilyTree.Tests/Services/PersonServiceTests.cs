using FamilyTree.Domain.Entities;
using FamilyTree.Domain.Enums;
using FamilyTree.Persistence.Context;
using FamilyTree.Persistence.Repositories;
using FamilyTree.Service.Processors;
using FamilyTree.Service.Services;
using FamilyTree.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FamilyTree.Tests.Services
{
    [TestClass]
    public class PersonServiceTests
    {
        private Mock<TreeProcessor> _mockTreeProcessor = null;
        private Mock<FamilyTreeDatabaseContext> _mockDatabaseContext = null;
        private Mock<PersonRepository> _mockPersonRepository = null;
        private PersonService _service = null;

        [TestInitialize]
        public void Setup()
        {
            InitializeMocks();
            _service = new PersonService(_mockPersonRepository.Object, _mockTreeProcessor.Object);
        }

        #region TESTS

        #region FindById

        [TestMethod]
        [DataRow("6308ef98598504f6ae267d962", "Martin", GenderTypeEnum.MALE)]
        [DataRow("6308ef0f698504f6ae267d875", "Phoebe", GenderTypeEnum.FEMALE)]
        public async Task FindById_InformationExists(string id, string name, GenderTypeEnum genderType)
        {
            var person = ObjectBuilder.GetPerson(id, name, genderType, null, null);

            var tcsPerson = new TaskCompletionSource<Person>();
            tcsPerson.SetResult(person);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.GetByIdAsync(person.Id))
                .Returns(tcsPerson.Task);

            var persons = GetPersons();

            var tcsPersons = new TaskCompletionSource<List<Person>>();
            tcsPersons.SetResult(persons);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.GetManyBy(x => true))
                .Returns(tcsPersons.Task);

            var tcsPersonTree = new TaskCompletionSource<PersonTree>();
            tcsPersonTree.SetResult(new PersonTree());

            var findByIdResult = await _service.FindById(person.Id);

            Assert.AreEqual(person.Name, findByIdResult.Name);
        }

        [TestMethod]
        [DataRow("6308ef98598504f6ae267d962", "Martin", GenderTypeEnum.MALE)]
        [DataRow("6308ef0f698504f6ae267d875", "Phoebe", GenderTypeEnum.FEMALE)]
        public async Task FindById_InformationNotExists(string id, string name, GenderTypeEnum genderType)
        {
            string differentId = "6308ef0f695874f6ae267d875";
            var person = ObjectBuilder.GetPerson(id, name, genderType, null, null);

            var tcsPerson = new TaskCompletionSource<Person>();
            tcsPerson.SetResult(person);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.GetByIdAsync(person.Id))
                .Returns(tcsPerson.Task);

            var persons = GetPersons();

            var tcsPersons = new TaskCompletionSource<List<Person>>();
            tcsPersons.SetResult(persons);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.GetManyBy(x => true))
                .Returns(tcsPersons.Task);

            var tcsPersonTree = new TaskCompletionSource<PersonTree>();
            tcsPersonTree.SetResult(new PersonTree());

            var findByIdResult = await _service.FindById(differentId);

            Assert.IsNull(findByIdResult);
        }

        #endregion

        #region FindByName

        [TestMethod]
        [DataRow("6308eaa6685fc8d9c0fe2995", "Martin", GenderTypeEnum.MALE)]
        [DataRow("630861fa3364ed035902c124", "Phoebe", GenderTypeEnum.FEMALE)]
        public async Task FindByName_TestInformationAreExpected(string id, string name, GenderTypeEnum genderType)
        {
            var person = ObjectBuilder.GetPerson(id, name, genderType, null, null);

            var tcsPerson = new TaskCompletionSource<Person>();
            tcsPerson.SetResult(person);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.GetOneBy(x => x.Name == name))
                .Returns(tcsPerson.Task);

            var findByNameResult = await _service.FindByName(name);

            Assert.IsNotNull(findByNameResult);
            Assert.AreEqual(person.Id, findByNameResult.Id);
            Assert.AreEqual(person.Name, findByNameResult.Name);
            Assert.AreEqual(person.GenderType, findByNameResult.GenderType);
        }

        [TestMethod]
        [DataRow("6308eaa6685fc8d9c0fe2995", "Martin")]
        [DataRow("630861fa3364ed035902c124", "Phoebe")]
        public async Task FindByName_TestInformationAreNotExpected(string id, string name)
        {
            var person = ObjectBuilder.GetPerson(id, name, GenderTypeEnum.MALE, null, null);

            var tcsPerson = new TaskCompletionSource<Person>();
            tcsPerson.SetResult(person);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.GetOneBy(x => x.Name == name))
                .Returns(tcsPerson.Task);

            var findByNameResult = await _service.FindByName(name);

            Assert.IsNotNull(findByNameResult);
            Assert.AreNotEqual("630862113364ed035902c125", findByNameResult.Id);
            Assert.AreNotEqual("Dunny", findByNameResult.Name);
        }

        [TestMethod]
        [DataRow("Martin")]
        [DataRow("Phoebe")]
        public async Task FindByName_PersonNotFound(string name)
        {
            var person = ObjectBuilder.GetPerson(string.Empty, name, GenderTypeEnum.MALE, null, null);

            var tcsPerson = new TaskCompletionSource<Person>();
            tcsPerson.SetResult(person);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.GetOneBy(x => x.Name == "Dunny"))
                .Returns(tcsPerson.Task);

            var service = new PersonService(_mockPersonRepository.Object, _mockTreeProcessor.Object);

            var findByNameResult = await service.FindByName(person.Name);

            Assert.IsNull(findByNameResult);
        }

        #endregion

        #region FindAll

        [TestMethod]
        public async Task FindAll_InformationExists()
        {
            var persons = new List<Person>
            {
                ObjectBuilder.GetPerson(
                    "6308ef98598504f6ae267d962",
                    "Martin",
                    GenderTypeEnum.MALE,
                    new List<string> { "6342of0f698504f6ae267d119" },
                    new List<string> { "6308ef9859850425t119" }),

                ObjectBuilder.GetPerson(
                    "6308ef0f698504f6ae267d875",
                    "Phoebe",
                    GenderTypeEnum.FEMALE,
                    new List<string> { "6308ef0f698504f6ae267d119" },
                    new List<string> { "6308ef98598504f6ae267d119" }),

                ObjectBuilder.GetPerson(
                    "6308ef0f698504f6ae2656p19",
                    "Dunny",
                    GenderTypeEnum.MALE,
                    new List<string> { "6308ef0f698504f6ae267d119" },
                    new List<string> { "6308ef98598504f6ae267d119" }),
            };

            var tcsPersons = new TaskCompletionSource<List<Person>>();
            tcsPersons.SetResult(persons);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.GetManyBy(x => true))
                .Returns(tcsPersons.Task);

            var servico = new PersonService(_mockPersonRepository.Object, null);

            var findAllResult = await servico.FindAll();

            Assert.IsNotNull(findAllResult);
            Assert.IsInstanceOfType(findAllResult, typeof(List<Person>));
            Assert.AreEqual(3, findAllResult.Count);
        }

        [TestMethod]
        public async Task FindAll_InformationNotExists()
        {
            var persons = new List<Person>();

            var tcsPersons = new TaskCompletionSource<List<Person>>();
            tcsPersons.SetResult(persons);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.GetManyBy(x => true))
                .Returns(tcsPersons.Task);

            var service = new PersonService(_mockPersonRepository.Object, null);

            var findAllResult = await service.FindAll();

            Assert.IsNotNull(findAllResult);
            Assert.IsInstanceOfType(findAllResult, typeof(List<Person>));
            Assert.AreEqual(0, findAllResult.Count);
        }

        #endregion

        #region Create

        [TestMethod]
        [DataRow("6308eaa6685fc8d9c0fe2995", "Martin", GenderTypeEnum.MALE)]
        [DataRow("630861fa3364ed035902c124", "Phoebe", GenderTypeEnum.FEMALE)]
        public async Task Create_TestCreatedPerson(string id, string name, GenderTypeEnum genderType)
        {
            var person = ObjectBuilder.GetPerson(id, name, genderType, null, null);

            var tcsPerson = new TaskCompletionSource<Person>();
            tcsPerson.SetResult(person);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.InsertAsync(person))
                .Returns(tcsPerson.Task);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.GetByIdAsync(person.Id))
                .Returns(tcsPerson.Task);

            var service = new PersonService(_mockPersonRepository.Object, null);

            var createResult = await service.Create(person);

            Assert.IsNotNull(createResult);
            Assert.IsInstanceOfType(createResult, typeof(Person));
            Assert.IsTrue(person.Id == createResult.Id);
            Assert.IsTrue(person.Name == createResult.Name);
            Assert.IsTrue(person.GenderType == createResult.GenderType);
        }

        [TestMethod]
        [DataRow("6308eaa6685fc8d9c0fe2995", "", GenderTypeEnum.MALE)]
        [DataRow("630861fa3364ed035902c124", "", GenderTypeEnum.FEMALE)]
        public async Task Create_TestUncreatedPerson(string id, string name, GenderTypeEnum genderType)
        {
            var person = ObjectBuilder.GetPerson(id, name, genderType, null, null);

            var tcsPerson = new TaskCompletionSource<Person>();
            tcsPerson.SetResult(person);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.InsertAsync(person))
                .Returns(tcsPerson.Task);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.GetByIdAsync(person.Id))
                .Returns(tcsPerson.Task);

            var service = new PersonService(_mockPersonRepository.Object, null);

            var createResult = await service.Create(person);

            Assert.IsNull(createResult);
        }

        #endregion

        #region Update

        [TestMethod]
        [DataRow("6308eaa6685fc8d9c0fe2995", "Martin", GenderTypeEnum.MALE)]
        [DataRow("630861fa3364ed035902c124", "Phoebe", GenderTypeEnum.FEMALE)]
        public async Task Update_TestUpdatedPerson(string id, string name, GenderTypeEnum genderType)
        {
            var person = ObjectBuilder.GetPerson(id, name, genderType, null, null);

            var tcsPerson = new TaskCompletionSource<Person>();
            tcsPerson.SetResult(person);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.UpdateAsync(person))
                .Returns(tcsPerson.Task);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.GetByIdAsync(person.Id))
                .Returns(tcsPerson.Task);

            var updateResult = await _service.Update(person);

            Assert.IsNotNull(updateResult);
            Assert.IsInstanceOfType(updateResult, typeof(Person));
            Assert.IsTrue(person.Id == updateResult.Id);
            Assert.IsTrue(person.Name == updateResult.Name);
            Assert.IsTrue(person.GenderType == updateResult.GenderType);
        }

        [TestMethod]
        [DataRow("6308eaa6685fc8d9c0fe2995", "", GenderTypeEnum.MALE)]
        [DataRow("630861fa3364ed035902c124", "", GenderTypeEnum.FEMALE)]
        public async Task Update_TestNotUpdatedPerson(string id, string name, GenderTypeEnum genderType)
        {
            var person = ObjectBuilder.GetPerson(id, name, genderType, null, null);

            var tcsPerson = new TaskCompletionSource<Person>();
            tcsPerson.SetResult(person);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.UpdateAsync(person))
                .Returns(tcsPerson.Task);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.GetByIdAsync(person.Id))
                .Returns(tcsPerson.Task);

            var service = new PersonService(_mockPersonRepository.Object, null);

            var updateResult = await service.Update(person);

            Assert.IsNull(updateResult);
        }

        #endregion

        #region Delete

        [TestMethod]
        [DataRow("6308eaa6685fc8d9c0fe2995", true)]
        [DataRow("630861fa3364ed035902c124", true)]
        public async Task Delete_TestDeletedPerson(string id, bool deleted)
        {
            var tcsBool = new TaskCompletionSource<bool>();
            tcsBool.SetResult(deleted);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.DeleteAsync(id))
                .Returns(tcsBool.Task);

            var service = new PersonService(_mockPersonRepository.Object, null);

            var deleteResult = await service.Delete(id);

            Assert.AreEqual(deleted, deleteResult);
        }

        [TestMethod]
        [DataRow("6308eaa6685fc8d9c0fe2995", true)]
        [DataRow("630861fa3364ed035902c124", true)]
        public async Task Delete_TestPersonNotDeleted(string id, bool deleted)
        {
            string differentId = "63064bc32d02ba698d487598";

            var tcsBool = new TaskCompletionSource<bool>();
            tcsBool.SetResult(deleted);

            _mockPersonRepository
                .Setup(mockRepo => mockRepo.DeleteAsync(id))
                .Returns(tcsBool.Task);

            var service = new PersonService(_mockPersonRepository.Object, null);

            var deleteResult = await service.Delete(differentId);

            Assert.AreNotEqual(deleted, deleteResult);
        }

        #endregion

        #endregion

        #region PRIVATE METHODS

        private void InitializeMocks()
        {
            _mockTreeProcessor = new Mock<TreeProcessor>();
            _mockDatabaseContext = new Mock<FamilyTreeDatabaseContext>();

            _mockDatabaseContext.Object.ConnectionString = "mongodb://localhost:27017";
            _mockDatabaseContext.Object.DatabaseName = "DatabaseName";

            _mockPersonRepository = new Mock<PersonRepository>(_mockDatabaseContext.Object);
        }

        private List<Person> GetPersons()
        {
            return new List<Person>
            {
                ObjectBuilder.GetPerson(
                    "6308ef98598504f6ae267d962",
                    "Martin",
                    GenderTypeEnum.MALE,
                    new List<string> { "6342of0f698504f6ae267d119" },
                    new List<string> { "6308ef9859850425t119" }),

                ObjectBuilder.GetPerson(
                    "6308ef0f698504f6ae267d875",
                    "Phoebe",
                    GenderTypeEnum.FEMALE,
                    new List<string> { "6308ef0f698504f6ae267d119" },
                    new List<string> { "6308ef98598504f6ae267d119" }),

                ObjectBuilder.GetPerson(
                    "6308ef0f698504f6ae2656p19",
                    "Dunny",
                    GenderTypeEnum.MALE,
                    new List<string> { "6308ef0f698504f6ae267d119" },
                    new List<string> { "6308ef98598504f6ae267d119" }),
            };
        }

        #endregion
    }
}
