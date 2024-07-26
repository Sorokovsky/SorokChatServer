import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { UsersRepository } from 'src/abstractions/users-repository.abstracts';
import { CreateUserDto } from 'src/contracts/user/create.dto';
import { UpdateUserDto } from 'src/contracts/user/update.dto';
import { User } from 'src/database/entities/user.entity';
import { Repository } from 'typeorm';

@Injectable()
export class UsersRepositoryService extends UsersRepository {
    constructor(
        @InjectRepository(User) private readonly usersRepository: Repository<User>,
    ) {
        super();
    };

    async findAll(): Promise<User[]> {
        return await this.usersRepository.find();
    }

    async tryFindById(id: Pick<User, 'id'>): Promise<User> {
        throw new Error('Method not implemented.');
    }

    async tryFindByEmail(email: string): Promise<User> {
        throw new Error('Method not implemented.');
    }

    async create(newUser: CreateUserDto): Promise<User> {
        throw new Error('Method not implemented.');
    }

    async update(id: Pick<User, 'id'>, newUser: UpdateUserDto): Promise<User> {
        throw new Error('Method not implemented.');
    }

    async delete(id: Pick<User, 'id'>): Promise<User> {
        throw new Error('Method not implemented.');
    }
};
