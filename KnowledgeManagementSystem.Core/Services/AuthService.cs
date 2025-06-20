using AutoMapper;
using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthService(
            IUserRepository userRepository,
            IConfiguration config,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _config = config;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterUserDto registerDto)
        {
            if (await _userRepository.GetByEmailAsync(registerDto.Email) != null)
                throw new Exception("Пользователь с таким email уже существует");

            CreatePasswordHash(registerDto.Password, out byte[] hash, out byte[] salt);

            var user = _mapper.Map<UserEntity>(registerDto);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(Convert.ToDouble(_config["Jwt:RefreshTokenExpireDays"]));

            await _userRepository.AddAsync(user);

            return new AuthResponseDto
            {
                Email = user.Email,
                Role = user.Role?.Title ?? "User",
                Token = GenerateToken(user),
                RefreshToken = user.RefreshToken,
                TokenExpires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"]))
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginUserDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email)
                ?? throw new Exception("Пользователь не найден");

            if (!VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Неверный пароль");

            var token = GenerateToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(Convert.ToDouble(_config["Jwt:RefreshTokenExpireDays"]));
            await _userRepository.UpdateAsync(user);

            return new AuthResponseDto
            {
                Email = user.Email,
                Role = user.Role?.Title ?? "User",
                Token = token,
                RefreshToken = refreshToken,
                TokenExpires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"]))
            };
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                throw new SecurityTokenException("Неверный идентификатор пользователя в токене");

            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("Пользователь не найден");

            if (user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.UtcNow)
                throw new SecurityTokenException("Недействительный refresh token");

            var newJwtToken = GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(Convert.ToDouble(_config["Jwt:RefreshTokenExpireDays"]));
            await _userRepository.UpdateAsync(user);

            return new AuthResponseDto
            {
                Email = user.Email,
                Role = user.Role?.Title ?? "User",
                Token = newJwtToken,
                RefreshToken = newRefreshToken,
                TokenExpires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"]))
            };
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("Пользователь не найден");

            if (!VerifyPasswordHash(changePasswordDto.CurrentPassword, user.PasswordHash, user.PasswordSalt))
                return false;

            CreatePasswordHash(changePasswordDto.NewPassword, out byte[] hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            await _userRepository.UpdateAsync(user);
            return true;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Недействительный токен");

            return principal;
        }

        private string GenerateToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role?.Title ?? "User"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(hash);
        }
    }
}