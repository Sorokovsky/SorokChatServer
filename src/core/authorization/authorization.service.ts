import { Injectable, BadRequestException } from '@nestjs/common';
import { IAuthorizationService } from 'src/abstractions/authorization.interface';
import { LoginDto } from 'src/contracts/auth/login.dto';
import { RegistrationDto } from 'src/contracts/auth/register.dto';
import { User } from 'src/database/entities/user.entity';
import { TokensService } from 'src/tokens/tokens.service';
import { UsersService } from '../users/users.service';
import { BearerStorageService } from '../../bearer-storage/bearer-storage.service';
import { CookieStorageService } from 'src/cookie-storage/cookie-storage.service';
import { PasswordService } from '../password/password.service';
import { UserNotFoundException } from '../../exceptions/user/not-found.exception';

type IdPayload = {
    id: User['id'];
};

@Injectable()
export class AuthorizationService implements IAuthorizationService {
    constructor(
        private readonly userService: UsersService,
        private readonly tokensService: TokensService,
        private readonly bearerStorage: BearerStorageService,
        private readonly cookieStorage: CookieStorageService,
        private readonly passwordService: PasswordService
    ) { };
    
    async registration(registerDto: RegistrationDto): Promise<User> {
        registerDto.password = await this.passwordService.hash(registerDto.password);
        const createdUser = await this.userService.create(registerDto);
        await this.authenticate(createdUser.id);
        return createdUser;
    }

    async login(loginDto: LoginDto): Promise<User> {
        const candidate = await this.userService.tryFindByEmail(loginDto.email);
        if (candidate === null) throw new UserNotFoundException('email', loginDto.email);
        const isPasswordValid = await this.passwordService.isEqual(loginDto.password, candidate.password);
        if (isPasswordValid) {
            await this.authenticate(candidate.id);
            return candidate;
        } else {
            throw new BadRequestException("Неправильний пароль");
        }
    }

    async logout(): Promise<void> {
        await this.bearerStorage.removeToken();
        await this.cookieStorage.removeToken();
    }

    async isAuthenticated(): Promise<boolean> {
        const accessToken = await this.bearerStorage.tryGetToken();
        const refreshToken = await this.cookieStorage.tryGetToken();
        let payload: IdPayload;
        try {
            payload = await this.tokensService.extractFromToken<IdPayload>(accessToken);
            return true;
        }
        catch (error) {
            try {
                payload = await this.tokensService.extractFromToken<IdPayload>(refreshToken);
                await this.authenticate(payload.id);
                return true;
            }
            catch (error) {
                return false;
            }
        }
    }

    async authenticate(id: User['id']): Promise<void> {
        const payload: IdPayload = { id };
        const accessToken = await this.tokensService.generateAccessToken(payload);
        const refreshToken = await this.tokensService.generateRefreshToken(payload);
        await this.bearerStorage.setToken(accessToken);
        await this.cookieStorage.setToken(refreshToken);
    }
};
