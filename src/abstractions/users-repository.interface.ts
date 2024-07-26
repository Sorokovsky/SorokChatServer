import { CreateUserDto } from "src/contracts/user/create.dto";
import { User } from "src/database/entities/user.entity";

export interface IUsersRepository {
    findAll(): Promise<User[]>;

    tryFindById(id: number): Promise<User>;

    tryFindByEmail(email: string): Promise<User>;

    create(newUser: CreateUserDto): Promise<User>;

    
};