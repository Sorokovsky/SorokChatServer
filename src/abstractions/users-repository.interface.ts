import { CreateUserDto } from "src/contracts/user/create.dto";
import { UpdateUserDto } from "src/contracts/user/update.dto";
import { User } from "src/database/entities/user.entity";

export interface UsersRepository {
    findAll(): Promise<User[]>;

    tryFindById(id: User['id']): Promise<User | null>;

    tryFindByEmail(email: string): Promise<User | null>;

    create(newUser: CreateUserDto): Promise<User>;

    update(id: User['id'], newUser: UpdateUserDto): Promise<User>;

    delete(id: User['id']): Promise<User>;
};