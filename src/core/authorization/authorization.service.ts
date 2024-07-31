import { Injectable } from '@nestjs/common';
import { IAuthorizationService } from 'src/abstractions/authorization.interface';
import { LoginDto } from 'src/contracts/auth/login.dto';
import { RegistrationDto } from 'src/contracts/auth/register.dto';
import { User } from 'src/database/entities/user.entity';
import { TokensService } from 'src/tokens/tokens.service';
import { UsersService } from '../users/users.service';

@Injectable()
export class AuthorizationService implements IAuthorizationService {
    constructor(
        private readonly userService: UsersService,
        private readonly tokensService: TokensService,
    ) { };
    
    registration(registerDto: RegistrationDto): Promise<User> {
        throw new Error('Method not implemented.');
    }

    login(loginDto: LoginDto): Promise<User> {
        throw new Error('Method not implemented.');
    }

    logout(): Promise<void> {
        throw new Error('Method not implemented.');
    }

    isAuthenticated(): Promise<boolean> {
        throw new Error('Method not implemented.');
    }

    authenticate(id: User['id']): Promise<User> {
        throw new Error('Method not implemented.');
    }
;
};
