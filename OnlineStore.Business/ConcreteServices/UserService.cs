using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.Dtos;
using OnlineStore.Business.Types;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Enums;
using OnlineStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.ConcreteServices
{
    public class UserService:IUserService
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtector _dataProtector;
        private readonly IMapper _mapper;
        public UserService(IRepository<UserEntity> userRepository, IDataProtectionProvider dataProtectionProvider, IMapper mapper)
        {
            _userRepository = userRepository;
            _dataProtector = dataProtectionProvider.CreateProtector("security");
            _mapper = mapper;
        }

        public async Task<ServiceMessage> AddUser(UserAddDto userAddDto)
        {
            var hasMail = _userRepository.GetAll(x => x.Email == userAddDto.Email);
            hasMail.ToList();
            // emailleri eşleşen bütün verileri çekip listeye atadım.
            // eğer eleman sayısı 0 ise bu mailden yok. Kayıt olunabilir.
            // 0'dan farklı ise geriye uyarı mesajı gidecek.

            if (hasMail.Any()) // HasMail.Count != 0 -> eski versiyonu
            {
                return new ServiceMessage()
                {
                    IsSucceed = false,
                    Message = "Bu Eposta adresli bir kullanıcı zaten mevcut."
                };
            }

            var userEntity = new UserEntity()
            {
                FirstName = userAddDto.FirstName,
                LastName = userAddDto.LastName,
                Email = userAddDto.Email,
                Password = _dataProtector.Protect(userAddDto.Password),
                UserType = UserTypeEnum.User
            };

            await _userRepository.AddAsync(userEntity);

            return  new ServiceMessage()
            {
                IsSucceed = true,
                Message = "Kayıt başarıyla tamamlandı."
            };
        }

        public async Task<List<UserListDto>> GetAllUsers()
        {
            var userEntites =  _userRepository.GetAll();
            var userDtoList = userEntites.Select(x => new UserListDto()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                CreatedDate = x.CreatedDate
            }).ToList();

            return userDtoList;
        }

        public UserDetailDto GetUserDetail(int id)
        {
            var entity = _userRepository.GetEntity(id);
          

            var DetailDto = new UserDetailDto()
            {
                Id = entity.Id,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName

            };

            return DetailDto;

        }

        public async Task<UserInfoDto> LoginUser(UserLoginDto userLoginDto)
        {
            var userEntity = await _userRepository.GetAsync(x => x.Email == userLoginDto.Email);

            if (userEntity is null)
            {
                return null;
                // Eğer form üzerinden gönderilen email ile eşleşen bir veri tabloda yoksa oturum açılamayacağı için geriye veri dönülmüyor.
            }

            var rawPassword =  _dataProtector.Unprotect(userEntity.Password); // şifreyi açtım.

            if (rawPassword == userLoginDto.Password)
            {
                return new UserInfoDto()
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    UserType = userEntity.UserType,
                };
            }
            else
            {
                return null;
            }
        }
    }
}
