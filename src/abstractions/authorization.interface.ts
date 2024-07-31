import { User } from "src/database/entities/user.entity";
import { RegistrationDto } from "src/contracts/auth/register.dto";
import { LoginDto } from "src/contracts/auth/login.dto";

export interface IAuthorizationService {
    registration(registerDto: RegistrationDto): Promise<User>;

    login(loginDto: LoginDto): Promise<User>;

    logout(): Promise<void>;

    isAuthenticated(): Promise<boolean>;

    authenticate(id: User['id']): Promise<void>;
};