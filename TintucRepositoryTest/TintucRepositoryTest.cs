using Bogus;
using BusinessLogicLayer.Tintuc;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces.Tintuc;
using Microsoft.EntityFrameworkCore;
using Moq;
namespace TintucRepositoryTest
{
    [TestClass]
    public class TintucRepositoryTest
    {

        ITintucRepo _tintucrepo;
        ITintucFilterRepo _TintucFilterRepo;

        // Set up dữ liệu
        [TestInitialize]
        public async Task Setup()
        {
            // data
            var list_generate = GenerateData();
            var data = list_generate.AsQueryable();
            var mockDbContext = new Mock<ApplicationDbContext>();
            var mockSet = new Mock<DbSet<TinTuc>>();
            mockSet.As<IQueryable<TinTuc>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TinTuc>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TinTuc>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TinTuc>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockDbContext.Setup(x => x.TinTucs).Returns(mockSet.Object);
            mockDbContext.Setup(x => x.SaveChanges()).Returns(1);
            _tintucrepo = new TintucRepo(mockDbContext.Object);
            _TintucFilterRepo = new TintucFilterRepo(mockDbContext.Object);

        }

        // Thêm tin tức
        [TestMethod]
        public void InsertNews()
        {
            // Arrange
            ITintucServices tintucServices = new TintucServices(_tintucrepo);
            var tinTuc = new TinTuc
            {
                //IdtinTuc = 11,
                TieuDe = $"Tiêu ?? tin t?c 11",
                Url = $"url-11",
                TomTat = $"Tóm t?t tin t?c 11",
                NoiDung = $"N?i dung tin t?c 11",
                HinhAnh = $"hinh-anh-11.jpg",
                NgayTao = DateTime.Now.AddDays(-11),
                NgayUpdate = DateTime.Now.AddHours(-11),
                LuotXem = 100,
                TrangThai = true, // alternating between true and false
            };
            tintucServices.Insert(tinTuc);
            tintucServices.Save();
        }

        // Cập tin tức
        [TestMethod]
        public void UpdateNews()
        {
            ITintucServices tintucServices = new TintucServices(_tintucrepo);
            var tinTucUpdate = new TinTuc
            {
                IdtinTuc = 1,
                TieuDe = $"Tiêu đề tin tức 11",
                Url = $"url-11",
                TomTat = $"Tóm tắt tin tức 11",
                NoiDung = $"Nội dung tin tức 11",
                HinhAnh = $"hinh-anh-11.jpg",
                NgayTao = DateTime.Now.AddDays(-11),
                NgayUpdate = DateTime.Now.AddHours(-11),
                LuotXem = 100,
                TrangThai = true, // alternating between true and false
            };
            tintucServices.Update(tinTucUpdate);
            tintucServices.Save();
        }
        //Xóa tin tức
        [TestMethod]
        public void DeleteNews()
        {
            ITintucServices tintucServices = new TintucServices(_tintucrepo);
            // Act
            tintucServices.Delete(12);
            tintucServices.Save();
        }

        //GET ALL tin tức
        [TestMethod]
        public async Task GetAllNews()
        {
            ITintucServices tintucServices = new TintucServices(_tintucrepo);
            var listnews = await  tintucServices.GetAll();
            var count = listnews.ToList().Count;
            Assert.AreEqual(10, count);
        }

        //GET tin tức by ID
        [TestMethod]
        public async Task GetNewsByID()
        {
            ITintucServices tintucServices = new TintucServices(_tintucrepo);
            var listnews = await tintucServices.GetById(1);
            //Kiểm tra ID == 1
            Assert.AreEqual(1, listnews.IdtinTuc);
        }
        //GET tin tức by URL
        [TestMethod]
        public async Task GetNewsByURL()
        {
            ITintucServices tintucServices = new TintucServices(_tintucrepo);
            var listnews = await tintucServices.GetByURL("url-10");
            Assert.AreEqual(10, listnews.IdtinTuc);
        }


        //Tìm kiếm
        [TestMethod]
        public void TinTucExists()
        {
            ITintucServices tintucServices = new TintucServices(_tintucrepo);
            var check = tintucServices.TinTucExists(1);
            Assert.AreEqual(true, check);
        }

        // Tạo dữ liệu danh sách tin tức
        public List<TinTuc> GenerateData()
        {
            var list = new List<TinTuc>();

            for (int i = 1; i <= 10; i++)
            {
                var tinTuc = new TinTuc
                {
                    IdtinTuc = i,
                    TieuDe = $"Tiêu đề tin tức {i}",
                    Url = $"url-{i}",
                    TomTat = $"Tóm tắt tin tức {i}",
                    NoiDung = $"Nội dung tin tức{i}",
                    HinhAnh = $"hinh-anh-{i}.jpg",
                    NgayTao = DateTime.Now.AddDays(-i),
                    NgayUpdate = DateTime.Now.AddHours(-i),
                    LuotXem = i * 100,
                    TrangThai = i % 2 == 0, // alternating between true and false
                    DanhMucTinTucs = new List<DanhMucTinTuc>
                 {
                     new DanhMucTinTuc { IdtinTuc =i, IddanhMuc=i }
                 }
                };

                list.Add(tinTuc);
            }

            return list;
        }

        [TestMethod]
        public void CheckNameNews()
        {
            ITintucServices tintucServices = new TintucServices(_tintucrepo);
            string invalidName = "11";
            //act
            bool result = tintucServices.CheckName(invalidName);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GetListByDateNews()
        {
            ITintucFilterService tintucFilterService = new TintucFilterService(_TintucFilterRepo);
            string? keywork = "Tiêu đề";
            DateTime startDay = DateTime.Now;
            DateTime endDay = DateTime.Now;
            bool? trangthai = true;

            var result = await tintucFilterService.GetListByDate(startDay, endDay, keywork, trangthai);
            Assert.IsTrue(result.Any());
        }
       
    }
}