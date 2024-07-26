import { CreateUserDto } from "src/contracts/user/create.dto";
import { UpdateUserDto } from "src/contracts/user/update.dto";
import { User } from "src/database/entities/user.entity";

export abstract class UsersRepository {
    abstract findAll(): Promise<User[]>;

    abstract tryFindById(id: User['id']): Promise<User | null>;

    abstract tryFindByEmail(email: string): Promise<User | null>;

    abstract create(newUser: CreateUserDto): Promise<User>;

    abstract update(id: User['id'], newUser: UpdateUserDto): Promise<User>;

    abstract delete(id: User['id']): Promise<User>;
};