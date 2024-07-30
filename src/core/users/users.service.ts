import { Injectable } from '@nestjs/common';
import { IUsersService } from 'src/abstractions/users-service.interface';
import { CreateUserDto } from 'src/contracts/user/create.dto';
import { UpdateUserDto } from 'src/contracts/user/update.dto';
import { User } from 'src/database/entities/user.entity';
import { UsersRepositoryService } from '../../database/repositories/users-repository/users-repository.service';

@Injectable()
export class UsersService implements IUsersService {
    constructor(
        private readonly repository: UsersRepositoryService
    ) { };
    
    async tryFindById(id: User['id']): Promise<User | null> {
        return await this.repository.tryFindById(id);
    }

    async findAll(): Promise<User[]> {
        return await this.findAll();
    }

    async create(newUser: CreateUserDto): Promise<User> {
        return await this.repository.create(newUser);
    }

    async update(id: User['id'], newUser: UpdateUserDto): Promise<User> {
        return await this.repository.update(id, newUser);
    }

    async delete(id: User['id']): Promise<User> {
        return await this.repository.delete(id);
    }
};
