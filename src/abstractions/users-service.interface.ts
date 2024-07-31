import { CreateUserDto } from "src/contracts/user/create.dto";
import { UpdateUserDto } from "src/contracts/user/update.dto";
import { User } from "src/database/entities/user.entity";

export interface IUsersService {
    tryFindById(id: User['id']): Promise<User | null>;

    tryFindByEmail(email: User['email']): Promise<User | null>;

    findAll(): Promise<User[]>;

    create(newUser: CreateUserDto): Promise<User>;

    update(id: User['id'], newUser: UpdateUserDto): Promise<User>;

    delete(id: User['id']): Promise<User>;
};