using AutoMapper;
using BaMinimalTemplate.Data;
using BaMinimalTemplate.Dtos;
using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BaMinimalTemplate.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtService _jwtService;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IJwtService jwtService,
        ApplicationDbContext context,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<TokenResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
            return null;

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
            return null;

        var accessToken = await _jwtService.GenerateTokenAsync(user);
        var refreshToken = await _jwtService.GenerateRefreshTokenAsync(user);

        return new TokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddHours(24),
            TokenType = "Bearer"
        };
    }

    public async Task<TokenResponseDto?> RegisterAsync(RegisterDto registerDto)
    {
        // Check if user already exists
        var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
            return null;

        var user = new User
        {
            UserName = registerDto.Email, // Use email as username
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            UserTypeId = registerDto.UserTypeId
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
            return null;

        // Generate tokens after successful registration
        var accessToken = await _jwtService.GenerateTokenAsync(user);
        var refreshToken = await _jwtService.GenerateRefreshTokenAsync(user);

        return new TokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddHours(24),
            TokenType = "Bearer"
        };
    }

    public async Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var userId = _jwtService.GetUserIdFromToken(refreshTokenDto.AccessToken);
        if (string.IsNullOrEmpty(userId))
            return null;

        var isValid = await _jwtService.ValidateRefreshTokenAsync(userId, refreshTokenDto.RefreshToken);
        if (!isValid)
            return null;

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return null;

        var newAccessToken = await _jwtService.GenerateTokenAsync(user);
        var newRefreshToken = await _jwtService.GenerateRefreshTokenAsync(user);

        return new TokenResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddHours(24),
            TokenType = "Bearer"
        };
    }

    public async Task<bool> LogoutAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;

        user.RefreshToken = null;
        user.RefreshTokenExpires = null;

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return result.Succeeded;
    }

    public async Task<bool> ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return false; // Don't reveal if user exists or not

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        // Here you would typically send the token via email
        // For now, we'll just return true
        return true;
    }

    public async Task<bool> ResetPasswordAsync(string userId, string token, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        return result.Succeeded;
    }
}
